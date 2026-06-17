using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using TicketManager.Web.Data;
using Xunit;

namespace TicketManager.Web.Tests;

public sealed class TicketManagerWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly string databaseName = $"InternalTicketManagerTests_{Guid.NewGuid():N}";

    public string ConnectionString { get; }

    public TicketManagerWebApplicationFactory()
    {
        var baseConnectionString = Environment.GetEnvironmentVariable("ConnectionStrings__TestConnection")
            ?? "Server=localhost,1433;Database=master;User Id=sa;Password=Your_password123;TrustServerCertificate=True";

        var builder = new SqlConnectionStringBuilder(baseConnectionString)
        {
            InitialCatalog = databaseName,
            Encrypt = false,
            TrustServerCertificate = true
        };

        ConnectionString = builder.ConnectionString;
    }

    public async Task InitializeAsync()
    {
        await WaitForSqlServerAsync();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(ConnectionString)
            .Options;

        await using var dbContext = new AppDbContext(options);
        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.EnsureCreatedAsync();
        SeedData.EnsureSeeded(dbContext);
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(ConnectionString)
            .Options;

        await using var dbContext = new AppDbContext(options);
        await dbContext.Database.EnsureDeletedAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");
        builder.ConfigureLogging(logging => logging.ClearProviders());

        builder.ConfigureServices(services =>
        {
            services.RemoveAll<ILoggerProvider>();
            services.RemoveAll<DbContextOptions<AppDbContext>>();
            services.AddDataProtection()
                .PersistKeysToFileSystem(Directory.CreateDirectory(
                    Path.Combine(AppContext.BaseDirectory, "DataProtectionKeys", databaseName)));
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(ConnectionString));
        });
    }

    private async Task WaitForSqlServerAsync()
    {
        var builder = new SqlConnectionStringBuilder(ConnectionString)
        {
            InitialCatalog = "master",
            Encrypt = false,
            TrustServerCertificate = true
        };

        var deadline = DateTimeOffset.UtcNow.AddSeconds(90);
        Exception? lastException = null;

        while (DateTimeOffset.UtcNow < deadline)
        {
            try
            {
                await using var connection = new SqlConnection(builder.ConnectionString);
                await connection.OpenAsync();
                return;
            }
            catch (SqlException ex)
            {
                lastException = ex;
                await Task.Delay(TimeSpan.FromSeconds(2));
            }
        }

        throw new InvalidOperationException("SQL Server was not available before the test timeout.", lastException);
    }
}

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc.Testing;
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
    private readonly string databasePath;

    public string ConnectionString { get; }

    public TicketManagerWebApplicationFactory()
    {
        databasePath = Path.Combine(Path.GetTempPath(), $"{databaseName}.db");
        ConnectionString = $"Data Source={databasePath}";
    }

    public async Task InitializeAsync()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(ConnectionString)
            .Options;

        await using var dbContext = new AppDbContext(options);
        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.MigrateAsync();
        SeedData.EnsureSeeded(dbContext);
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(ConnectionString)
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
            services.AddDbContext<AppDbContext>(options => options.UseSqlite(ConnectionString));
        });
    }
}

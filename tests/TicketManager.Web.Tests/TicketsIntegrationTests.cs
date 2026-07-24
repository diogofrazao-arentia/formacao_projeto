using System.Net;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using TicketManager.Web.Data;
using TicketManager.Web.Models;
using Xunit;

namespace TicketManager.Web.Tests;

public sealed class TicketsIntegrationTests : IClassFixture<TicketManagerWebApplicationFactory>
{
    private static readonly Regex AntiforgeryTokenRegex = new(
        "name=\"__RequestVerificationToken\" type=\"hidden\" value=\"(?<token>[^\"]+)\"",
        RegexOptions.Compiled);

    private readonly TicketManagerWebApplicationFactory factory;
    private readonly HttpClient client;

    public TicketsIntegrationTests(TicketManagerWebApplicationFactory factory)
    {
        this.factory = factory;
        client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false,
            HandleCookies = true
        });
    }

    [Fact]
    public async Task Index_ReturnsSuccessAndShowsSeedTickets()
    {
        var response = await client.GetAsync("/Tickets");

        await EnsureSuccessAsync(response);
        var body = await ReadDecodedBodyAsync(response);
        Assert.Contains("Criar ticket", body);
    }

    [Fact]
    public async Task Index_OrdersTicketsByNewestFirst()
    {
        await using var dbContext = CreateDbContext();
        var oldestTicket = await dbContext.Tickets.OrderBy(ticket => ticket.CreatedAt).FirstAsync();
        var newestTicket = await CreateTicketAsync(
            "Newest ticket should be first",
            "A recently created ticket should appear before older tickets.",
            TicketPriority.Medium,
            TicketStatus.Open,
            DateTime.UtcNow.AddMinutes(5));

        var response = await client.GetAsync("/Tickets");

        await EnsureSuccessAsync(response);
        var body = await ReadDecodedBodyAsync(response);
        Assert.True(
            body.IndexOf(newestTicket.Title, StringComparison.Ordinal) <
            body.IndexOf(oldestTicket.Title, StringComparison.Ordinal));
    }

    [Fact]
    public async Task Root_ReturnsTicketsIndex()
    {
        var response = await client.GetAsync("/");

        await EnsureSuccessAsync(response);
        var body = await ReadDecodedBodyAsync(response);
        Assert.Contains("Criar ticket", body);
    }

    [Fact]
    public async Task Create_Get_ReturnsSuccess()
    {
        var response = await client.GetAsync("/Tickets/Create");

        await EnsureSuccessAsync(response);
        var body = await ReadDecodedBodyAsync(response);
        Assert.Contains("Registar um novo pedido interno", body);
    }

    [Fact]
    public async Task Create_Post_WithValidData_CreatesTicketAndRedirectsToDetails()
    {
        var token = await GetAntiforgeryTokenAsync("/Tickets/Create");

        var response = await client.PostAsync("/Tickets/Create", FormContent(new Dictionary<string, string>
        {
            ["__RequestVerificationToken"] = token,
            ["Title"] = "Test ticket from integration test",
            ["Description"] = "This ticket validates the create flow.",
            ["Priority"] = TicketPriority.High.ToString()
        }));

        if (response.StatusCode != HttpStatusCode.Redirect)
        {
            var body = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Expected redirect, got {response.StatusCode}. Body: {body}");
        }
        Assert.StartsWith("/Tickets/Details/", response.Headers.Location?.OriginalString);

        await using var dbContext = CreateDbContext();
        var ticket = await dbContext.Tickets.SingleAsync(t => t.Title == "Test ticket from integration test");
        Assert.Equal(TicketStatus.Open, ticket.Status);
        Assert.Equal(TicketPriority.High, ticket.Priority);
    }

    [Fact]
    public async Task Create_Post_WithInvalidData_ReturnsValidationErrorsAndDoesNotCreateTicket()
    {
        var token = await GetAntiforgeryTokenAsync("/Tickets/Create");
        await using var dbContextBefore = CreateDbContext();
        var beforeCount = await dbContextBefore.Tickets.CountAsync();

        var response = await client.PostAsync("/Tickets/Create", FormContent(new Dictionary<string, string>
        {
            ["__RequestVerificationToken"] = token,
            ["Title"] = "",
            ["Description"] = "",
            ["Priority"] = TicketPriority.Medium.ToString()
        }));

        await EnsureSuccessAsync(response);

        await using var dbContextAfter = CreateDbContext();
        Assert.Equal(beforeCount, await dbContextAfter.Tickets.CountAsync());
    }

    [Fact]
    public async Task Create_Post_WithoutAntiforgeryToken_ReturnsBadRequestAndDoesNotCreateTicket()
    {
        await using var dbContextBefore = CreateDbContext();
        var beforeCount = await dbContextBefore.Tickets.CountAsync();

        var response = await client.PostAsync("/Tickets/Create", FormContent(new Dictionary<string, string>
        {
            ["Title"] = "No token ticket",
            ["Description"] = "This request should be rejected before model persistence.",
            ["Priority"] = TicketPriority.High.ToString()
        }));

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        await using var dbContextAfter = CreateDbContext();
        Assert.Equal(beforeCount, await dbContextAfter.Tickets.CountAsync());
    }

    [Fact]
    public async Task Details_WithExistingTicket_ReturnsSuccess()
    {
        var ticket = await CreateTicketAsync(
            "Ticket detail placeholder",
            "This ticket should show the base detail page.",
            TicketPriority.Medium,
            TicketStatus.Open,
            DateTime.UtcNow);

        var response = await client.GetAsync($"/Tickets/Details/{ticket.Id}");

        await EnsureSuccessAsync(response);
        var body = await ReadDecodedBodyAsync(response);
        Assert.Contains(ticket.Title, body);
    }

    [Fact]
    public async Task Details_WithMissingTicket_ReturnsNotFound()
    {
        var response = await client.GetAsync("/Tickets/Details/999999");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Details_WithoutComments_ShowsEmptyCommentsState()
    {
        var ticket = await CreateTicketAsync(
            "Ticket sem comentarios",
            "Este ticket e usado para validar o estado vazio de comentarios.",
            TicketPriority.Low,
            TicketStatus.Open,
            DateTime.UtcNow);

        var response = await client.GetAsync($"/Tickets/Details/{ticket.Id}");

        await EnsureSuccessAsync(response);
        var body = await ReadDecodedBodyAsync(response);
        Assert.Contains("Ainda nao existem comentarios.", body);
    }

    [Fact]
    public async Task Details_WithComments_DisplaysCommentsInChronologicalOrder()
    {
        var ticket = await CreateTicketAsync(
            "Ticket com comentarios",
            "Este ticket verifica a ordenacao cronologica dos comentarios.",
            TicketPriority.Medium,
            TicketStatus.InProgress,
            DateTime.UtcNow.AddHours(-1));

        var oldestComment = await CreateCommentAsync(ticket.Id, "Ana", "Comentario antigo com pelo menos dez caracteres.", DateTime.UtcNow.AddMinutes(-30));
        var newestComment = await CreateCommentAsync(ticket.Id, "Bruno", "Comentario mais recente com texto suficiente.", DateTime.UtcNow.AddMinutes(-10));

        var response = await client.GetAsync($"/Tickets/Details/{ticket.Id}");

        await EnsureSuccessAsync(response);
        var body = await ReadDecodedBodyAsync(response);
        Assert.Contains(oldestComment.AuthorName, body);
        Assert.Contains(newestComment.AuthorName, body);
        Assert.True(
            body.IndexOf(oldestComment.Content, StringComparison.Ordinal) <
            body.IndexOf(newestComment.Content, StringComparison.Ordinal));
    }

    [Fact]
    public async Task CreateComment_Post_WithValidData_CreatesCommentAndRedirectsToDetails()
    {
        var ticket = await CreateTicketAsync(
            "Ticket para criar comentario",
            "Ticket usado para testar o post de comentarios.",
            TicketPriority.High,
            TicketStatus.Open,
            DateTime.UtcNow);

        var token = await GetAntiforgeryTokenAsync($"/Tickets/Details/{ticket.Id}");

        var response = await client.PostAsync($"/Tickets/{ticket.Id}/Comments", FormContent(new Dictionary<string, string>
        {
            ["__RequestVerificationToken"] = token,
            ["NewComment.AuthorName"] = "Carla",
            ["NewComment.Content"] = "Comentario valido para validar o fluxo de criacao."
        }));

        Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        Assert.Equal($"/Tickets/Details/{ticket.Id}", response.Headers.Location?.OriginalString);

        await using var dbContext = CreateDbContext();
        var comment = await dbContext.TicketComments.SingleAsync(c => c.TicketId == ticket.Id && c.AuthorName == "Carla");
        Assert.Equal("Comentario valido para validar o fluxo de criacao.", comment.Content);
    }

    [Fact]
    public async Task CreateComment_Post_WithoutAntiforgeryToken_ReturnsBadRequestAndDoesNotCreateComment()
    {
        var ticket = await CreateTicketAsync(
            "Ticket sem token",
            "Ticket usado para validar antiforgery em comentarios.",
            TicketPriority.Medium,
            TicketStatus.Open,
            DateTime.UtcNow);

        await using var dbContextBefore = CreateDbContext();
        var beforeCount = await dbContextBefore.TicketComments.CountAsync();

        var response = await client.PostAsync($"/Tickets/{ticket.Id}/Comments", FormContent(new Dictionary<string, string>
        {
            ["NewComment.AuthorName"] = "Daniel",
            ["NewComment.Content"] = "Comentario sem token antiforgery para falhar."
        }));

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        await using var dbContextAfter = CreateDbContext();
        Assert.Equal(beforeCount, await dbContextAfter.TicketComments.CountAsync());
    }

    [Fact]
    public async Task CreateComment_Post_WithInvalidData_ReturnsValidationErrorsAndDoesNotCreateComment()
    {
        var ticket = await CreateTicketAsync(
            "Ticket com validacao",
            "Ticket para validar erros de formulario de comentarios.",
            TicketPriority.Low,
            TicketStatus.Open,
            DateTime.UtcNow);

        var token = await GetAntiforgeryTokenAsync($"/Tickets/Details/{ticket.Id}");

        await using var dbContextBefore = CreateDbContext();
        var beforeCount = await dbContextBefore.TicketComments.CountAsync();

        var response = await client.PostAsync($"/Tickets/{ticket.Id}/Comments", FormContent(new Dictionary<string, string>
        {
            ["__RequestVerificationToken"] = token,
            ["NewComment.AuthorName"] = string.Empty,
            ["NewComment.Content"] = "curto"
        }));

        await EnsureSuccessAsync(response);
        var body = await ReadDecodedBodyAsync(response);
        Assert.Contains("O nome e obrigatorio.", body);
        Assert.Contains("O comentario deve ter entre 10 e 1000 caracteres.", body);

        await using var dbContextAfter = CreateDbContext();
        Assert.Equal(beforeCount, await dbContextAfter.TicketComments.CountAsync());
    }

    [Fact]
    public async Task CreateComment_Post_WithMissingTicket_ReturnsNotFound()
    {
        var token = await GetAntiforgeryTokenAsync("/Tickets/Create");

        var response = await client.PostAsync("/Tickets/999999/Comments", FormContent(new Dictionary<string, string>
        {
            ["__RequestVerificationToken"] = token,
            ["NewComment.AuthorName"] = "Eva",
            ["NewComment.Content"] = "Comentario para um ticket inexistente."
        }));

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Edit_Get_WithExistingTicket_ReturnsSuccess()
    {
        var ticket = await GetSeedTicketAsync();

        var response = await client.GetAsync($"/Tickets/Edit/{ticket.Id}");

        await EnsureSuccessAsync(response);
        var body = await ReadDecodedBodyAsync(response);
        Assert.Contains("Editar estado", body);
        Assert.Contains(ticket.Title, body);
    }

    [Fact]
    public async Task Edit_Post_WithExistingTicket_UpdatesStatus()
    {
        var ticket = await GetSeedTicketAsync();
        var token = await GetAntiforgeryTokenAsync($"/Tickets/Edit/{ticket.Id}");

        var response = await client.PostAsync($"/Tickets/Edit/{ticket.Id}", FormContent(new Dictionary<string, string>
        {
            ["__RequestVerificationToken"] = token,
            ["status"] = TicketStatus.Closed.ToString()
        }));

        Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        Assert.Equal($"/Tickets/Details/{ticket.Id}", response.Headers.Location?.OriginalString);

        await using var dbContext = CreateDbContext();
        var updatedTicket = await dbContext.Tickets.FindAsync(ticket.Id);
        Assert.NotNull(updatedTicket);
        Assert.Equal(TicketStatus.Closed, updatedTicket.Status);
    }

    private async Task<Ticket> GetSeedTicketAsync()
    {
        await using var dbContext = CreateDbContext();
        return await dbContext.Tickets.OrderBy(t => t.Id).FirstAsync();
    }

    private async Task<Ticket> CreateTicketAsync(
        string title,
        string description,
        TicketPriority priority,
        TicketStatus status,
        DateTime createdAt)
    {
        await using var dbContext = CreateDbContext();
        var ticket = new Ticket
        {
            Title = title,
            Description = description,
            Priority = priority,
            Status = status,
            CreatedAt = createdAt
        };

        dbContext.Tickets.Add(ticket);
        await dbContext.SaveChangesAsync();
        return ticket;
    }

    private async Task<TicketComment> CreateCommentAsync(
        int ticketId,
        string authorName,
        string content,
        DateTime createdAt)
    {
        await using var dbContext = CreateDbContext();
        var comment = new TicketComment
        {
            TicketId = ticketId,
            AuthorName = authorName,
            Content = content,
            CreatedAt = createdAt
        };

        dbContext.TicketComments.Add(comment);
        await dbContext.SaveChangesAsync();
        return comment;
    }

    private AppDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(factory.ConnectionString)
            .Options;

        return new AppDbContext(options);
    }

    private async Task<string> GetAntiforgeryTokenAsync(string url)
    {
        var response = await client.GetAsync(url);
        await EnsureSuccessAsync(response);

        var body = await ReadDecodedBodyAsync(response);
        var match = AntiforgeryTokenRegex.Match(body);

        Assert.True(match.Success, "Expected an antiforgery token in the form.");
        return WebUtility.HtmlDecode(match.Groups["token"].Value);
    }

    private static FormUrlEncodedContent FormContent(Dictionary<string, string> values)
    {
        var content = new FormUrlEncodedContent(values);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
        return content;
    }

    private static async Task EnsureSuccessAsync(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            return;
        }

        var body = await response.Content.ReadAsStringAsync();
        throw new HttpRequestException(
            $"Response status code does not indicate success: {(int)response.StatusCode} ({response.StatusCode}). Body: {body}");
    }

    private static async Task<string> ReadDecodedBodyAsync(HttpResponseMessage response)
    {
        return WebUtility.HtmlDecode(await response.Content.ReadAsStringAsync());
    }
}

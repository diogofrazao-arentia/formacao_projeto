using TicketManager.Web.Models;

namespace TicketManager.Web.Data;

/// <summary>
/// Creates the initial demonstration tickets used by the training application.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Adds sample tickets when the database does not contain any tickets yet.
    /// </summary>
    /// <param name="dbContext">Database context used to inspect and seed tickets.</param>
    public static void EnsureSeeded(AppDbContext dbContext)
    {
        if (dbContext.Tickets.Any())
        {
            return;
        }

        dbContext.Tickets.AddRange(
            new Ticket
            {
                Title = "Acesso à VPN não funciona",
                Description = "A equipa financeira não consegue ligar-se à VPN interna depois da rotação de passwords.",
                Priority = TicketPriority.High,
                Status = TicketStatus.Open,
                CreatedAt = DateTime.UtcNow.AddDays(-4)
            },
            new Ticket
            {
                Title = "Pedido de novo monitor",
                Description = "Um analista de suporte precisa de um segundo monitor para o posto de onboarding.",
                Priority = TicketPriority.Low,
                Status = TicketStatus.InProgress,
                CreatedAt = DateTime.UtcNow.AddDays(-3)
            },
            new Ticket
            {
                Title = "Atraso na entrega de emails",
                Description = "Os alertas operacionais estão a chegar com cerca de 20 minutos de atraso.",
                Priority = TicketPriority.Medium,
                Status = TicketStatus.Open,
                CreatedAt = DateTime.UtcNow.AddDays(-2)
            },
            new Ticket
            {
                Title = "Impressora partilhada offline",
                Description = "A impressora do escritório de Lisboa aparece offline para todos os utilizadores.",
                Priority = TicketPriority.Medium,
                Status = TicketStatus.Closed,
                CreatedAt = DateTime.UtcNow.AddDays(-1)
            },
            new Ticket
            {
                Title = "Criar conta de onboarding",
                Description = "Preparar uma conta interna para um novo colaborador que começa na próxima segunda-feira.",
                Priority = TicketPriority.High,
                Status = TicketStatus.InProgress,
                CreatedAt = DateTime.UtcNow.AddHours(-8)
            });

        dbContext.SaveChanges();
    }
}

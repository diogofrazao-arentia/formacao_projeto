namespace TicketManager.Web.Models;

public static class TicketDisplayExtensions
{
    public static string ToDisplayName(this TicketPriority priority)
    {
        return priority switch
        {
            TicketPriority.Low => "Baixa",
            TicketPriority.Medium => "Média",
            TicketPriority.High => "Alta",
            _ => priority.ToString()
        };
    }

    public static string ToDisplayName(this TicketStatus status)
    {
        return status switch
        {
            TicketStatus.Open => "Aberto",
            TicketStatus.InProgress => "Em progresso",
            TicketStatus.Closed => "Fechado",
            _ => status.ToString()
        };
    }
}

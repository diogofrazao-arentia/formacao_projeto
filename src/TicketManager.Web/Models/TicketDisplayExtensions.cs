namespace TicketManager.Web.Models;

/// <summary>
/// Provides display labels for ticket-related enum values used in the MVC views.
/// </summary>
public static class TicketDisplayExtensions
{
    /// <summary>
    /// Converts a ticket priority into the Portuguese label shown to users.
    /// </summary>
    /// <param name="priority">The priority value to display.</param>
    /// <returns>The user-facing label for the priority.</returns>
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

    /// <summary>
    /// Converts a ticket status into the Portuguese label shown to users.
    /// </summary>
    /// <param name="status">The status value to display.</param>
    /// <returns>The user-facing label for the status.</returns>
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

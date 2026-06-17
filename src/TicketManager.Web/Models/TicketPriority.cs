namespace TicketManager.Web.Models;

/// <summary>
/// Defines the urgency level assigned to an internal ticket.
/// </summary>
public enum TicketPriority
{
    /// <summary>
    /// Low urgency request with limited operational impact.
    /// </summary>
    Low = 1,

    /// <summary>
    /// Normal urgency request that should follow the standard support flow.
    /// </summary>
    Medium = 2,

    /// <summary>
    /// High urgency request with relevant user or business impact.
    /// </summary>
    High = 3
}

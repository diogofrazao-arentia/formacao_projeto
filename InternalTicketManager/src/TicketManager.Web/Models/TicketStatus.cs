namespace TicketManager.Web.Models;

/// <summary>
/// Defines the lifecycle state of an internal ticket.
/// </summary>
public enum TicketStatus
{
    /// <summary>
    /// The ticket was created and is waiting to be handled.
    /// </summary>
    Open = 1,

    /// <summary>
    /// The ticket is currently being followed or worked on.
    /// </summary>
    InProgress = 2,

    /// <summary>
    /// The ticket was resolved or no longer requires action.
    /// </summary>
    Closed = 3
}

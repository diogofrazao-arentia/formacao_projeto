using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TicketManager.Web.Models;

/// <summary>
/// Represents all data rendered in the ticket details page.
/// </summary>
public class TicketDetailsViewModel
{
    /// <summary>
    /// Ticket being displayed.
    /// </summary>
    [ValidateNever]
    public Ticket Ticket { get; set; } = new();

    /// <summary>
    /// Existing comments ordered by creation date.
    /// </summary>
    [ValidateNever]
    public IReadOnlyList<TicketComment> Comments { get; set; } = Array.Empty<TicketComment>();

    /// <summary>
    /// New comment bound from the details page form.
    /// </summary>
    public TicketComment NewComment { get; set; } = new();
}
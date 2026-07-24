using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketManager.Web.Data;
using TicketManager.Web.Models;

namespace TicketManager.Web.Controllers;

/// <summary>
/// Handles the MVC pages used to list, create, inspect and update internal tickets.
/// </summary>
public class TicketsController : Controller
{
    private readonly AppDbContext dbContext;

    /// <summary>
    /// Creates a controller backed by the application database context.
    /// </summary>
    /// <param name="dbContext">Database context used to read and persist tickets.</param>
    public TicketsController(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    /// <summary>
    /// Displays all tickets ordered from newest to oldest.
    /// </summary>
    /// <returns>The ticket list page.</returns>
    public async Task<IActionResult> Index()
    {
        var tickets = await dbContext.Tickets
            .OrderByDescending(ticket => ticket.CreatedAt)
            .ToListAsync();

        return View(tickets);
    }

    /// <summary>
    /// Displays the form used to create a new ticket.
    /// </summary>
    /// <returns>The ticket creation page.</returns>
    public IActionResult Create()
    {
        PopulatePriorityOptions();
        return View(new Ticket());
    }

    /// <summary>
    /// Validates and persists a new ticket submitted from the creation form.
    /// </summary>
    /// <param name="ticket">Ticket data submitted by the user.</param>
    /// <returns>The validation page when invalid, or the details page for the created ticket.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Ticket ticket)
    {
        if (!ModelState.IsValid)
        {
            PopulatePriorityOptions(ticket.Priority);
            return View(ticket);
        }

        ticket.Status = TicketStatus.Open;
        ticket.CreatedAt = DateTime.UtcNow;

        dbContext.Tickets.Add(ticket);
        await dbContext.SaveChangesAsync();

        return RedirectToAction(nameof(Details), new { id = ticket.Id });
    }

    /// <summary>
    /// Displays the details page for a single ticket.
    /// </summary>
    /// <param name="id">Identifier of the ticket to display.</param>
    /// <returns>The ticket details page, or a not found response when the ticket does not exist.</returns>
    public async Task<IActionResult> Details(int id)
    {
        var model = await BuildTicketDetailsViewModelAsync(id);
        if (model is null)
        {
            return NotFound();
        }

        return View(model);
    }

    /// <summary>
    /// Validates and persists a new comment for the ticket details timeline.
    /// </summary>
    /// <param name="id">Identifier of the ticket that receives the comment.</param>
    /// <param name="model">Details page model containing the new comment fields.</param>
    /// <returns>The validation page when invalid, or the details page for the updated ticket.</returns>
    [HttpPost("Tickets/{id}/Comments")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateComment(int id, TicketDetailsViewModel model)
    {
        var ticketExists = await dbContext.Tickets.AnyAsync(ticket => ticket.Id == id);
        if (!ticketExists)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            var invalidModel = await BuildTicketDetailsViewModelAsync(id, model.NewComment);
            if (invalidModel is null)
            {
                return NotFound();
            }

            return View(nameof(Details), invalidModel);
        }

        var comment = model.NewComment;
        comment.TicketId = id;
        comment.CreatedAt = DateTime.UtcNow;

        dbContext.TicketComments.Add(comment);
        await dbContext.SaveChangesAsync();

        return RedirectToAction(nameof(Details), new { id });
    }

    /// <summary>
    /// Displays the form used to update the status of an existing ticket.
    /// </summary>
    /// <param name="id">Identifier of the ticket to edit.</param>
    /// <returns>The ticket edit page, or a not found response when the ticket does not exist.</returns>
    public async Task<IActionResult> Edit(int id)
    {
        var ticket = await dbContext.Tickets.FindAsync(id);
        if (ticket is null)
        {
            return NotFound();
        }

        PopulateStatusOptions(ticket.Status);
        return View(ticket);
    }

    /// <summary>
    /// Validates and persists a status change for an existing ticket.
    /// </summary>
    /// <param name="id">Identifier of the ticket being edited.</param>
    /// <param name="status">New workflow status selected by the user.</param>
    /// <returns>The validation page when invalid, or the details page for the updated ticket.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, TicketStatus status)
    {
        var ticket = await dbContext.Tickets.FindAsync(id);
        if (ticket is null)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            PopulateStatusOptions(ticket.Status);
            return View(ticket);
        }

        ticket.Status = status;
        await dbContext.SaveChangesAsync();

        return RedirectToAction(nameof(Details), new { id = ticket.Id });
    }

    private void PopulatePriorityOptions(TicketPriority selected = TicketPriority.Medium)
    {
        ViewBag.PriorityOptions = Enum.GetValues<TicketPriority>()
            .Select(priority => new SelectListItem(priority.ToDisplayName(), priority.ToString(), priority == selected))
            .ToList();
    }

    private void PopulateStatusOptions(TicketStatus selected)
    {
        ViewBag.StatusOptions = Enum.GetValues<TicketStatus>()
            .Select(status => new SelectListItem(status.ToDisplayName(), status.ToString(), status == selected))
            .ToList();
    }

    private async Task<TicketDetailsViewModel?> BuildTicketDetailsViewModelAsync(int id, TicketComment? newComment = null)
    {
        var ticket = await dbContext.Tickets.FindAsync(id);
        if (ticket is null)
        {
            return null;
        }

        var comments = await dbContext.TicketComments
            .Where(comment => comment.TicketId == id)
            .OrderBy(comment => comment.CreatedAt)
            .ToListAsync();

        return new TicketDetailsViewModel
        {
            Ticket = ticket,
            Comments = comments,
            NewComment = newComment ?? new TicketComment()
        };
    }
}

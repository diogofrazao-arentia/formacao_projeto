using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketManager.Web.Data;
using TicketManager.Web.Models;

namespace TicketManager.Web.Controllers;

public class TicketsController : Controller
{
    private readonly AppDbContext dbContext;

    public TicketsController(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IActionResult> Index()
    {
        var tickets = await dbContext.Tickets
            .OrderByDescending(ticket => ticket.CreatedAt)
            .ToListAsync();

        return View(tickets);
    }

    public IActionResult Create()
    {
        PopulatePriorityOptions();
        return View(new Ticket());
    }

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

    public async Task<IActionResult> Details(int id)
    {
        var ticket = await dbContext.Tickets.FindAsync(id);
        if (ticket is null)
        {
            return NotFound();
        }

        return View(ticket);
    }

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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, TicketStatus status)
    {
        var ticket = await dbContext.Tickets.FindAsync(id);
        if (ticket is null)
        {
            return NotFound();
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
}

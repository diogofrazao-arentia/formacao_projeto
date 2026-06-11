using Microsoft.EntityFrameworkCore;
using TicketManager.Web.Models;

namespace TicketManager.Web.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Ticket> Tickets => Set<Ticket>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.Property(ticket => ticket.Title).HasMaxLength(120).IsRequired();
            entity.Property(ticket => ticket.Description).HasMaxLength(2000).IsRequired();
            entity.Property(ticket => ticket.Priority).HasConversion<string>().HasMaxLength(20).IsRequired();
            entity.Property(ticket => ticket.Status).HasConversion<string>().HasMaxLength(20).IsRequired();
            entity.Property(ticket => ticket.CreatedAt).IsRequired();
        });
    }
}

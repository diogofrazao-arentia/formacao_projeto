using System.ComponentModel.DataAnnotations;

namespace TicketManager.Web.Models;

public class Ticket
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O título é obrigatório.")]
    [StringLength(120)]
    [Display(Name = "Título")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "A descrição é obrigatória.")]
    [StringLength(2000)]
    [Display(Name = "Descrição")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "A prioridade é obrigatória.")]
    [Display(Name = "Prioridade")]
    public TicketPriority Priority { get; set; } = TicketPriority.Medium;

    [Required(ErrorMessage = "O estado é obrigatório.")]
    [Display(Name = "Estado")]
    public TicketStatus Status { get; set; } = TicketStatus.Open;

    [Display(Name = "Criado em")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

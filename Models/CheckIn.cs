using System.ComponentModel.DataAnnotations;

namespace HabitAqui.Models
{
    public class CheckIn
    {
        public int Id { get; set; }
        public string? Danos { get; set; }
        [Display(Name = "Observações")]
        public string? Observacoes { get; set; }
        public DateTime? DataCheckIn { get; set; }
        public string? FuncionarioId { get; set; }
        public ApplicationUser? Funcionario { get; set; }
        public int? AluguerId { get; set; }
        public Aluguer? Aluguer { get; set; }

        [Display(Name = "Items")]
        public ICollection<CheckInItem>? CheckInItems { get; set; }
    }
}

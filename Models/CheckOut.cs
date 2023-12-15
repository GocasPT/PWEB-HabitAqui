using System.ComponentModel.DataAnnotations;

namespace HabitAqui.Models
{
    public class CheckOut
    {
        public int Id { get; set; }
        [Display(Name = "Observações")]
        public string? Observacoes { get; set; }
        public string? Danos { get; set; }
        public DateTime? DataCheckOut { get; set; }
        public string? FuncionarioId { get; set; }
        public ApplicationUser? Funcionario { get; set; }
        public int? AluguerId { get; set; }
        public Aluguer? Aluguer { get; set; }

        [Display(Name = "Items")]
        public ICollection<CheckOutItem>? CheckOutItems { get; set; }
        public ICollection<Fotografia>? Fotografias { get; set; }
    }
}

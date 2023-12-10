using System.ComponentModel.DataAnnotations;

namespace HabitAqui.Models
{
    public class TipoLocador
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O tipo de locador é obrigatório.")]
        [Display(Name = "Tipo de Locador")]
        public string Nome { get; set; }
    }
}

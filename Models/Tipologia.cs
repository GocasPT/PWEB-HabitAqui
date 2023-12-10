using System.ComponentModel.DataAnnotations;

namespace HabitAqui.Models
{
    public class Tipologia
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A tipologia é obrigatória.")]
        [Display(Name = "Tipologia")]
        public string Nome { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace HabitAqui.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "A categoria é obrigatória.")]
        [Display(Name = "Categoria")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
    }
}

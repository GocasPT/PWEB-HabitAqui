using System.ComponentModel.DataAnnotations;

namespace HabitAqui.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        [Display(Name = "Categoria")]
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }
}

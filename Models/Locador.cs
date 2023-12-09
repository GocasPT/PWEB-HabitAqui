using System.ComponentModel.DataAnnotations;

namespace HabitAqui.Models
{
    public class Locador
    {
        public int Id { get; set; }

        [Display(Name = "Locador")]
        public string Nome { get; set; }
        public string Contacto { get; set; }

        [Display(Name = "Tipo de Locador")]
        public String? Tipo { get; set; }

        [Display(Name = "Criado a")]
        public DateTime? DataCriacao { get; set; } 
        public ICollection<Habitacao>? Habitacoes { get; set; }
        public ICollection<ApplicationUser>? GestoresFuncionarios { get; set; }

    }
}

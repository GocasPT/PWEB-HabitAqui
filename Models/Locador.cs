using HabitAqui.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HabitAqui.Models
{
    public class Locador
    {
        public int Id { get; set; }

        [Display(Name = "Nome do locador")]
        [Required(ErrorMessage = "O nome do locador é obrigatório")]
        public string Nome { get; set; }
        [Display(Name = "Email do locador")]
        [Required(ErrorMessage = "O email do locador é obrigatório")]
        public string Email { get; set; }
        [Display(Name = "Telefone do locador")]
        [Phone(ErrorMessage = "O contacto do locador é obrigatório")]
        public string Contacto { get; set; }
        [Display(Name = "É uma empresa")]
        public bool IsEmpresa { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DataCriacao { get; set; }
        public ICollection<Habitacao>? Habitacoes { get; set; }
        public ICollection<ApplicationUser>? GestoresFuncionarios { get; set; }
        public bool Ativo { get; set; }
    }
}

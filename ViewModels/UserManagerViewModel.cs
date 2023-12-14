using HabitAqui.Models;
using System.ComponentModel.DataAnnotations;

namespace HabitAqui.ViewModels
{
    public class UserManagerViewModel
    {
        public string UserId { get; set; }

        [Display(Name = "Nome de utilizador")]
        public string Username { get; set; }

        public string Email { get; set; }

        public bool Ativo {  get; set; }
        [Display(Name = "Função")]

        public IEnumerable<string>? Roles { get; set; }
        public List<CheckIn>? CheckIns { get; set; }
        public List<CheckOut>? CheckOuts { get; set; }
    }
}

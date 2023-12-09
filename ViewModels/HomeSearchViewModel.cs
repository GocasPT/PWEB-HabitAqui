using HabitAqui.Models;
using System.ComponentModel.DataAnnotations;

namespace HabitAqui.ViewModels
{
    public class HomeSearchViewModel
    {
        public List<Habitacao>? Habitacoes { get; set; }
        public String? TextoAPesquisar {  get; set; }
        public int NumResultados { get; set; }
    }
}

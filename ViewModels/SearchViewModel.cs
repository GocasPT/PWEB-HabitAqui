using HabitAqui.Models;
using System.ComponentModel.DataAnnotations;

namespace HabitAqui.ViewModels
{
    public class SearchViewModel
    {
        [Display(Name = "Texto a Pesquisar")]
        public string TextoAPesquisar { get; set; }
        [Display(Name = "Tipo de categoria")]
        public int CategoriaId { get; set; }
        [Display(Name = "CheckIn")]
        public DateTime CheckIn { get; set; }
        [Display(Name = "CheckOut")]
        public DateTime CheckOut { get; set; }
        public int? NumResultados { get; set; }
        public string? OrdemPreco { get; set; }
        public string? OrdemRating { get; set; }
        public int? LocadorId { get; set; }
        public List<Habitacao> Habitacoes { get; set; }
    }
}

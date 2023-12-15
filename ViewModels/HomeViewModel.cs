using HabitAqui.Models;

namespace HabitAqui.ViewModels
{
    public class HomeViewModel : CategoriasListViewModel
    {
        public string OrdemPreco { get; set; }
        public string OrdemRating { get; set; }
        public int? CategoriaId { get; set; }
        public List<Habitacao> Habitacoes { get; set; }
    }
}

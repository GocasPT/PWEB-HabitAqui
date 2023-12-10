using HabitAqui.Models;

namespace HabitAqui.ViewModels
{
    public class HomeIndexViewModel
    {
        public string OrdemPreco { get; set; }
        public string OrdemRating { get; set; }
        public int? CategoriaId { get; set; }
        public List<Categoria> Categorias { get; set; }
        public List<Habitacao> Habitacoes { get; set; }
    }
}

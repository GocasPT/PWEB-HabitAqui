using HabitAqui.Models;

namespace HabitAqui.ViewModels
{
    public class HomeIndexViewModel
    {
        public string CategoriaFilter { get; set; }
        public List<Categoria> Categorias { get; set; }
        public List<Habitacao> Habitacoes { get; set; }
    }
}

using HabitAqui.Models;
using System.ComponentModel.DataAnnotations;

namespace HabitAqui.ViewModels
{
    public class HomeSearchViewModel
    {
        public string Rua { get; set; }
        [DataType(DataType.Date)]
        public DateTime CheckIn { get; set; }
        [DataType(DataType.Date)]
        public DateTime CheckOut { get; set; }
        public string CategoriaFilter { get; set; }
        public List<Categoria> Categorias { get; set; }
        public List<Habitacao> Habitacoes { get; set; }
    }
}

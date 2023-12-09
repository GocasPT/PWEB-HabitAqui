using HabitAqui.Models;
using System.ComponentModel.DataAnnotations;

namespace HabitAqui.ViewModels
{
    public class HomeSearchViewModel
    {
        public string TextoAPesquisar { get; set; }
        public int NumResultados { get; set; }
        public string CategoriaFilter { get; set; }
        public List<Categoria> Categorias { get; set; }
        public List<Habitacao> Habitacoes { get; set; }
    }
}

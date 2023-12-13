using HabitAqui.Models;
using System.ComponentModel.DataAnnotations;

namespace HabitAqui.ViewModels
{
    public class SearchViewModel
    {
        public string TextoAPesquisar { get; set; }
        //TODO: a adicionar checkIn e checkOut (tbm o tipo ou deixamos nos filtros?)
        public int? CategoriaId { get; set; }
        public int NumResultados { get; set; }
        public string OrdemPreco { get; set; }
        public string OrdemRating { get; set; }
        public List<Categoria> Categorias { get; set; }
        public List<Habitacao> Habitacoes { get; set; }
    }
}

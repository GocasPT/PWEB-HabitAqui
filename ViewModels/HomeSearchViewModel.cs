using HabitAqui.Models;
using System.ComponentModel.DataAnnotations;

namespace HabitAqui.ViewModels
{
    public class HomeSearchViewModel
    {
        public string TextoAPesquisar { get; set; }
        //TODO: a adicionar checkIn e checkOut (tbm o tipo ou deixamos nos filtros?)
        public int NumResultados { get; set; }
        public List<Categoria> Categorias { get; set; }
        public List<Habitacao> Habitacoes { get; set; }
    }
}

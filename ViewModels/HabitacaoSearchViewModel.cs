using HabitAqui.Models;
using System.ComponentModel.DataAnnotations;

namespace HabitAqui.ViewModels
{
    public class HabitacaoSearchViewModel
    {
        public string Rua { get; set; }
        public string Categoria { get; set; }
        [DataType(DataType.Date)]
        public DateTime CheckIn { get; set; }
        [DataType(DataType.Date)]
        public DateTime CheckOut { get; set; }
        public int CustoPorNoite { get; set; }
        public List<Habitacao> Habitacoes { get; set; }
    }
}

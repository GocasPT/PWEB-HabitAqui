using System.ComponentModel.DataAnnotations;

namespace HabitAqui.Models
{
    public class Habitacao_Itens
    {
        public int HabitacaoId { get; set; }
        public Habitacao Habitacao { get; set; }

        public int ItemId { get; set; }
        public Itens Item { get; set; }
    }
}

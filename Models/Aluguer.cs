using System.ComponentModel.DataAnnotations.Schema;

namespace HabitAqui.Models
{
    public class Aluguer
    {
        public int Id { get; set; }
        public DateTime DataDeEntrada { get; set; }
        public DateTime DataDeSaida { get; set; }
        public bool Confirmado { get; set; }
        public String? ClienteId { get; set; }
        public ApplicationUser Cliente { get; set; }
        public int? HabitacaoId { get; set; }
        public Habitacao Habitacao { get; set; }
        public int? LocadorId { get; set; }
        public Locador Locador { get; set; }
        public int? CheckInId { get; set; }
        public CheckIn CheckIn { get; set; }
        public int? CheckOutId { get; set; }
        public CheckOut CheckOut { get; set; }
    }
}

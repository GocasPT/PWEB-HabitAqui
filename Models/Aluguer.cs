using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HabitAqui.Models
{
    public class Aluguer
    {
        public int Id { get; set; }

        [Display(Name = "Entrada")]
        public DateTime DataDeEntrada { get; set; }
        [Display(Name = "Saida")]
        public DateTime DataDeSaida { get; set; }
        [Display(Name = "Aprovado")]
        public bool Confirmado { get; set; }
        public String? ClienteId { get; set; }
        public ApplicationUser? Cliente { get; set; }

        public int? HabitacaoId { get; set; }
        [Display(Name = "Nome da Habitação")]
        public Habitacao? Habitacao { get; set; }
        public int? LocadorId { get; set; }
        public Locador? Locador { get; set; }
        public int? CheckInId { get; set; }
        [Display(Name = "Entrou")]
        public CheckIn? CheckIn { get; set; }
        public int? CheckOutId { get; set; }
        [Display(Name = "Saiu")]
        public CheckOut? CheckOut { get; set; }
        public int? PontuacaoId { get; set; }
        public Pontuacao? Pontuacao { get; set; }
    }
}

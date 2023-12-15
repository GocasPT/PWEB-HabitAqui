namespace HabitAqui.Models
{
    public class Fotografia
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Extensao { get; set; }
        public int? CheckOutId { get; set; }
        public CheckOut? CheckOut { get; set; }
        public int? HabitacaoId { get; set; }
        public Habitacao? Habitacao { get; set; }
    }
}

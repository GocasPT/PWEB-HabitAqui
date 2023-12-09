namespace HabitAqui.Models
{
    public class Fotografia
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Extensao { get; set; }
        public byte[] Data { get; set; }
        public int CheckInId { get; set; }
        public CheckIn CheckIn { get; set; }
    }
}

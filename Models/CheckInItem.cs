namespace HabitAqui.Models
{
    public class CheckInItem
    {
        public int? CheckInId { get; set; }
        public CheckIn? CheckIn { get; set; }

        public int? ItemId { get; set; }
        public Itens? Items { get; set; }
    }
}

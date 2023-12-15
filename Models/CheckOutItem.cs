namespace HabitAqui.Models
{
    public class CheckOutItem
    {
        public int? CheckOutId { get; set; }
        public CheckOut? CheckOut { get; set; }

        public int? ItemId { get; set; }
        public Itens? Items { get; set; }
    }
}

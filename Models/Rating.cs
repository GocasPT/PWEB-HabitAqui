namespace HabitAqui.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public User UserId { get; set; }
        public Rental RentalId { get; set; }
        //TODO: int or double? [1-5]
        public int Value { get; set; }
        public string Comment { get; set; } = string.Empty;
    }
}

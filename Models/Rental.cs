namespace HabitAqui.Models
{
    public enum Status
    {
        Pending,
        Approved,
        Denied
    }

    public class Rental
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public User EmployeeId {  get; set; }
        public Status Status { get; set; } = Status.Pending;
    }
}

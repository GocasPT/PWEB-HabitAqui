namespace HabitAqui.Models.Data
{
    public class EmployeeData
    {
        public int Id { get; set; }
        public User UserId { get; set; }
        public LandlordData LandlordId { get; set; }
        //TODO: dados do empregado
        public int JobsCount { get; set; }
    }
}

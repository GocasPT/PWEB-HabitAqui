namespace HabitAqui.Models.Data
{
    public class LandlordData
    {
        public int Id { get; set; }
        public EmployeeData EmployeeId { get; set; }
        //TODO: dados do locador
        public string Name { get; set; }
        public int PhoneNumber { get; set; }    
        public ICollection<Properties> Properties { get; set; }
    }
}

namespace HabitAqui.Models
{
    public enum Role
    {
        Admin,
        User,
        Employee,
        Manager
    }

    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Role? Role { get; set; }
    }
}

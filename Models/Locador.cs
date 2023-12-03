namespace HabitAqui.Models
{
    public class Locador
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Contacto { get; set; }
        public ICollection<Habitacao> Habitacoes { get; set; }
        //public ICollection<ApplicationUser> GestoresFuncionarios { get; set; }
    }
}

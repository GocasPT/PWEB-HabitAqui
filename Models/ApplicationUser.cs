using Microsoft.AspNetCore.Identity;

namespace HabitAqui.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
        public DateTime DataNascimento { get; set; }
        //TODO: Informações adicionais do usuário
        public ICollection<Aluguer> Alugueres { get; set; }
        public int? LocadorId { get; set; }
        public Locador Locador { get; set; }
        public string? CargoComLocador { get; set; } //TODO: Verificar se é necessário ou se faz sentido
    }
}

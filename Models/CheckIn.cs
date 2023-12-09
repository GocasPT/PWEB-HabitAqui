namespace HabitAqui.Models
{
    public class CheckIn
    {
        public int Id { get; set; }
        //TODO: Informação a ser guardada sobre o check-in (detalhes e cenas assim)
        public string Observacoes { get; set; }
        public string FuncionarioId { get; set; }
        public ApplicationUser Funcionario { get; set; }
        public int? AluguerId { get; set; }
        public Aluguer Aluguer { get; set; }
    }
}

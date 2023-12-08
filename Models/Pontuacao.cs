namespace HabitAqui.Models
{
    public class Pontuacao
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Comentario { get; set; }
        public double PontuacaoLimpeza { get; set; }
        public double PontuacaoLocalizacao { get; set; }
        public double PontuacaoQualidadePreco { get; set; }
        public double PontuacaoEspaco { get; set; }
    }
}

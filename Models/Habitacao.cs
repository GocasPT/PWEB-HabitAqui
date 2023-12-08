namespace HabitAqui.Models
{
    public class Habitacao
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        public string? Tipologia {  get; set; }
        public string? Pais { get; set; }
        public string? Distrito { get; set; }
        public string? Concelho { get; set; }

        public string Rua { get; set; }
        //TODO: Informação sobre a localização
        public int CustoPorNoite { get; set; }
        public bool Disponivel { get; set; }
       
        public int? LocadorId { get; set; }
        public Locador Locador { get; set; }
        public ICollection<Aluguer> Alugueres { get; set; }

        public ICollection<Pontuacao> Pontuacoes { get; set; } = new List<Pontuacao>();

        // Propriedade de leitura somente para calcular a média das pontuações
        public double mediaPontuacoes
        {
            get
            {
                if (Pontuacoes != null && Pontuacoes.Any())
                {
                    // Calcula a média das quatro pontuações
                    return (Pontuacoes.Sum(r => r.PontuacaoLimpeza) +
                            Pontuacoes.Sum(r => r.PontuacaoLocalizacao) +
                            Pontuacoes.Sum(r => r.PontuacaoQualidadePreco) +
                            Pontuacoes.Sum(r => r.PontuacaoEspaco)) / (4.0 * Pontuacoes.Count);
                }
                return 0.0; // Retorna 0 se não houver avaliações
            }
        }
    }
}

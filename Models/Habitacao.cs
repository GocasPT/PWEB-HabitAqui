using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace HabitAqui.Models
{
    public class Habitacao
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [Display(Name = "Nome")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "A categoria é obrigatória.")]
        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }

        [Required(ErrorMessage = "A tipologia é obrigatória.")]
        public string? Tipologia { get; set; }
        [Required(ErrorMessage = "O pais é obrigatório.")]
        public string? Pais { get; set; }
        [Required(ErrorMessage = "O distrito é obrigatório.")]
        public string? Distrito { get; set; }
        [Required(ErrorMessage = "O concelho é obrigatório.")]
        public string? Concelho { get; set; }

        [Required(ErrorMessage = "A rua é obrigatória.")]
        public string Rua { get; set; }
        //TODO: Informação sobre a localização

        [Display(Name = "Custo por noite")]
        [Required(ErrorMessage = "O custo por noite é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O custo por noite deve ser maior que zero.")]
        public int CustoPorNoite { get; set; }

        public bool Disponivel { get; set; }

        [Display(Name = "Casas de banho")]
        [Required(ErrorMessage = "O número de casas de banho é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O número de casas de banho deve ser maior que zero.")]
        public int? NumWC {  get; set; }

        [Display(Name = "Número de Hóspedes")]
        [Required(ErrorMessage = "O número de hóspedes é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O número de hóspedes deve ser maior que zero.")]
        public int? NumPessoas { get; set; }

        [Display(Name = "Descrição")]
        public String? Descricao { get; set; }

        [Display(Name = "Locador")]
        [Required(ErrorMessage = "O locador é obrigatório.")]
        public int? LocadorId { get; set; }
        public Locador? Locador { get; set; }

        [Display(Name = "Criado a")]
        public DateTime? DataCriacao { get; set; }
        public ICollection<Aluguer>? Alugueres { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public ICollection<Pontuacao>? Pontuacoes { get; set; } = new List<Pontuacao>();

        [Display(Name = "Estrelas")]
        // Propriedade de leitura somente para calcular a média das pontuações
        public double mediaPontuacoes
        {
            get
            {
                if (Pontuacoes != null && Pontuacoes.Any())
                {
                    // Calcula a média das quatro pontuações
                    return  (Pontuacoes.Sum(r => r.PontuacaoLimpeza) +
                            Pontuacoes.Sum(r => r.PontuacaoLocalizacao) +
                            Pontuacoes.Sum(r => r.PontuacaoQualidadePreco) +
                            Pontuacoes.Sum(r => r.PontuacaoEspaco)
                            ) / (4 * Pontuacoes.Count);
                }
                return 0.0; // Retorna 0 se não houver avaliações
            }
        }

        public double mediaPontuacaoLimpeza
        {
            get
            {
                if (Pontuacoes != null && Pontuacoes.Any())
                {
                    // Calcula a média das quatro pontuações
                    return (Pontuacoes.Sum(r => r.PontuacaoLimpeza) / Pontuacoes.Count);
                }
                return 0.0; // Retorna 0 se não houver avaliações
            }
        }

        public double mediaPontuacaoLocalizacao
        {
            get
            {
                if (Pontuacoes != null && Pontuacoes.Any())
                {
                    // Calcula a média das quatro pontuações
                    return (Pontuacoes.Sum(r => r.PontuacaoLocalizacao) / Pontuacoes.Count);
                }
                return 0.0; // Retorna 0 se não houver avaliações
            }
        }

        public double mediaPontuacaoQualidadePreco
        {
            get
            {
                if (Pontuacoes != null && Pontuacoes.Any())
                {
                    // Calcula a média das quatro pontuações
                    return (Pontuacoes.Sum(r => r.PontuacaoQualidadePreco) / Pontuacoes.Count);
                }
                return 0.0; // Retorna 0 se não houver avaliações
            }
        }

        public double mediaPontuacaoEspaco
        {
            get
            {
                if (Pontuacoes != null && Pontuacoes.Any())
                {
                    // Calcula a média das quatro pontuações
                    return (Pontuacoes.Sum(r => r.PontuacaoEspaco) / Pontuacoes.Count);
                }
                return 0.0; // Retorna 0 se não houver avaliações
            }
        }


        public string getLatitude
        {
            get
            {
                if (Latitude != null)
                    return Latitude.ToString().Replace(',', '.');
                else 
                    return "0.0";
            }
        }
    }
}

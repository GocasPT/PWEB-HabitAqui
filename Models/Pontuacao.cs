using System.ComponentModel.DataAnnotations;

namespace HabitAqui.Models
{
    public class Pontuacao
    {
        public int Id { get; set; }
        
        [Display(Name = "Título")]
        public string Titulo { get; set; }

        [Display(Name = "Comentário")]
        public string Comentario { get; set; }

        [Display(Name = "Limpeza")]
        public double PontuacaoLimpeza { get; set; }
        [Display(Name = "Localização")]
        public double PontuacaoLocalizacao { get; set; }
        [Display(Name = "Qualidade/€")]
        public double PontuacaoQualidadePreco { get; set; }
        [Display(Name = "Espaço")]
        public double PontuacaoEspaco { get; set; }

        [Display(Name = "Criado a")]
        public DateTime? DataCriacao { get; set; }
        public int? HabitacaoId { get; set; }
        
        [Display(Name = "Nome da Habitação")]
        public Habitacao? Habitacao { get; set; }
        public string? ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
    }
}

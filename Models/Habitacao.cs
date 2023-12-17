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

        [Display(Name = "Tipologia")]
        public int? TipologiaId { get; set; }
        [Display(Name = "Tipologia")]
        public Tipologia? Tipologia { get; set; }
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
        public int? NumWC { get; set; }

        [Display(Name = "Número de Hóspedes")]
        [Required(ErrorMessage = "O número de hóspedes é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O número de hóspedes deve ser maior que zero.")]
        public int? NumPessoas { get; set; }

        [Display(Name = "Descrição")]
        public String? Descricao { get; set; }

        [Display(Name = "Locador")]
        public int? LocadorId { get; set; }
        public Locador? Locador { get; set; }

        [Display(Name = "Criado a")]
        public DateTime? DataCriacao { get; set; }
        public ICollection<Aluguer>? Alugueres { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public ICollection<Habitacao_Itens>? Itens { get; set; } = new List<Habitacao_Itens>();
        public ICollection<Fotografia>? Fotografias { get; set; } = new List<Fotografia>();
    }
}

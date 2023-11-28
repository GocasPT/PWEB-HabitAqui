namespace HabitAqui.Models
{
    public class Habitacao
    {
        public int Id { get; set; }
        public string Rua { get; set; }
        //TODO: Informação sobre a localização
        public int CustoPorNoite { get; set; }
        public bool Disponivel { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        //public int LocadorId { get; set; }
        //public Locador Locador { get; set; }
        //public ICollection<Aluguer> Alugueres { get; set; }
    }
}

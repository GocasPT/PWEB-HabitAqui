﻿namespace HabitAqui.Models
{
    public class Itens
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Categoria { get; set; }

        public ICollection<Habitacao_Itens>? HabitacaoItens { get; set; } = new List<Habitacao_Itens>();
        public ICollection<CheckInItem>? CheckInItems { get; set; }
        public ICollection<CheckOutItem>? CheckOutItems { get; set; }
    }
}

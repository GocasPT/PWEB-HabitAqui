using HabitAqui.Models.Data;

namespace HabitAqui.Models
{
    public struct Addres
    {

    }

    public class Properties
    {
        public int Id { get; set; }
        //LandlordData ou Landlord?
        public LandlordData Landlord { get; set; }
        // String ou estrutura 'Addres'?
        public string Location { get; set; }
        public Category Category { get; set; }
        public double Price { get; set; }
        public ICollection<Rental> Rentals { get; set; }
    }
}

using Car_Rental.Interfaces;

namespace Car_Rental.Models
{
    public class Location: ISoftDeletable
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public ICollection<Car>? cars { set; get; }
        public bool IsDeleted { get; set; } = false;

    }
}

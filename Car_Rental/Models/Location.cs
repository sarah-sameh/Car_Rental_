namespace Car_Rental.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public ICollection<Car>? cars { set; get; }
    }
}

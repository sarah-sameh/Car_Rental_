namespace Car_Rental.DTOs
{
    public class CarDTO
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Make { get; set; }
        public int Year { get; set; }
        public string FuelType { get; set; }
        public bool IsAvailable { get; set; }
        public string Image { get; set; }
        public int LocationId { get; set; }
        public string userID { get; set; }


    }
}

namespace Car_Rental.DTOs
{
    public class commentDTO
    {
        public string Text { get; set; }
        public int Rating { get; set; }
        public int CarId { get; set; }
        public string userName { get; set; }
        public string userID { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}

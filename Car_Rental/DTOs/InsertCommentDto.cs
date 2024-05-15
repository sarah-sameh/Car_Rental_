namespace Car_Rental.DTOs
{
    public class InsertCommentDto
    {
        public string Text { get; set; }
        public int Rating { get; set; }
        public int CarId { get; set; }
        public string userId { get; set; }
    }
}

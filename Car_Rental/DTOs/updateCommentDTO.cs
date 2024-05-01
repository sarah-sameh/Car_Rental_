namespace Car_Rental.DTOs
{
    public class updateCommentDTO
    {
        public string Text { get; set; }
        public int Rating { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}

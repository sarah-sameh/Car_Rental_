namespace Car_Rental.DTOs
{
    public class ResetPasswordDto
    {
        public string UserName { get; set; }
        public string oldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}

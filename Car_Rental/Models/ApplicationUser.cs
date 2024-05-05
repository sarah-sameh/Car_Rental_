using Car_Rental.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Car_Rental.Models
{
    public class ApplicationUser : IdentityUser, ISoftDeletable
    {
        public string Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public ICollection<Rental>? rentals { set; get; }
        public ICollection<Payment>? payments { get; set; }
        public List<Comments>? comments { get; set; }
        public bool IsDeleted { get; set; } = false;


    }
}

using Car_Rental.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Car_Rental.Models
{
    public class Comments: ISoftDeletable
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }

        [ForeignKey("Car")]
        public int CarId { get; set; }
        public Car Car { get; set; }


        public ApplicationUser applicationUser { get; set; }
        [ForeignKey("applicationUser")]
        public string userId { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}

using Car_Rental.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Car_Rental.Models
{
    public class Car: ISoftDeletable
    {
        public int Id { get; set; }
        public string? Model { get; set; }
        public string Make { get; set; }
        public int Year { get; set; }
        // public float DailyRate { get; set; }
        public string FuelType { get; set; }
        public bool IsAvailable { get; set; }
        public string Image { set; get; }

        [ForeignKey("location")]
        public int Location_Id { set; get; }
        public Location location { set; get; }

        public ICollection<Rental>? rentals { set; get; }
        public ICollection<Maintenance>? maintenances { set; get; }
        public List<Comments>? comments { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string userId { get; set; }


    }
}

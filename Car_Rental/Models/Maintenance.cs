using Car_Rental.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Car_Rental.Models
{
    public class Maintenance: ISoftDeletable
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public DateTime MaintenanceDate { get; set; }

        [ForeignKey("car")]
        public int Car_Id { set; get; }
        public Car car { set; get; }
        public bool IsDeleted { get; set; } = false;

    }
}

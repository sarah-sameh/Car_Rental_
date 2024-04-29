using System.ComponentModel.DataAnnotations.Schema;

namespace Car_Rental.Models
{
    public class Rental
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal RentalRate { get; set; }
        public decimal InsuranceCharge { get; set; }
        public decimal FuelCharge { get; set; }

        [ForeignKey("car")]
        public int Car_Id { set; get; }
        public Car car { set; get; }

        [ForeignKey("customer")]
        public string Customer_Id { set; get; }

        public ApplicationUser customer { set; get; }
    }
}

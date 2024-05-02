using Car_Rental.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Car_Rental.DTOs
{
    public class RentalDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal RentalRate { get; set; }
        public decimal InsuranceCharge { get; set; }
        public decimal FuelCharge { get; set; }

        public int Car_Id { set; get; }
       
        public string Customer_Id { set; get; }


    }
}

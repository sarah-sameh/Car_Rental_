using System.ComponentModel.DataAnnotations.Schema;

namespace Car_Rental.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Method { get; set; }
        public Double Amount { get; set; }

        [ForeignKey("customer")]
        public string Customer_Id { set; get; }
        public ApplicationUser customer { set; get; }
    }
}

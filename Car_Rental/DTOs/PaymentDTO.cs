namespace Car_Rental.DTOs
{
    public class PaymentDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Method { get; set; }
        public decimal Amount { get; set; }

        public int MaintenanceId { set; get; }


    }
}

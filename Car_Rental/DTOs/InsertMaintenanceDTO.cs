namespace Car_Rental.DTOs
{
    public class InsertMaintenanceDTO
    {

        public string Type { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public int Car_Id { set; get; }
    }
}

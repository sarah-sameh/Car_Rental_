using Car_Rental.Models;

namespace Car_Rental.Repository
{
    public interface IMaintenanceRepository : IRepository<Maintenance>
    {
        public decimal GetMaintenanceCost(int maintenanceId);
    }
}

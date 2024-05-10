using Car_Rental.Models;

namespace Car_Rental.Repository
{
    public class MaintenanceRepository : Repository<Maintenance>, IMaintenanceRepository
    {
        public MaintenanceRepository(Context context) : base(context)
        {

        }
        public decimal GetMaintenanceCost(int maintenanceId)
        {
            return _context.Maintenance
                           .Where(m => m.Id == maintenanceId)
                           .Select(m => m.Cost)
                           .FirstOrDefault();
        }
    }
}

using Car_Rental.Models;

namespace Car_Rental.Repository
{
    public class MaintenanceRepository : Repository<Maintenance>, IMaintenanceRepository
    {
        public MaintenanceRepository(Context context) : base(context)
        {
        }
    }
}

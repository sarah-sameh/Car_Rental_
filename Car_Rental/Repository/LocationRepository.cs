using Car_Rental.Models;

namespace Car_Rental.Repository
{
    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        public LocationRepository(Context context) : base(context)
        {
        }
    }
}

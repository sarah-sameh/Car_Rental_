using Car_Rental.Models;

namespace Car_Rental.Repository
{
    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        public LocationRepository(Context context) : base(context)
        {
           
        }

        public List<Car> getCarsByAddress(string address)
        {
            return _context.Cars
                .Where(car => car.location.Address.ToLower() == address.ToLower())
                .ToList();
        }

    }
}

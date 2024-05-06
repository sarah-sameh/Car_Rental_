using Car_Rental.Models;

namespace Car_Rental.Repository
{
    public interface ILocationRepository:IRepository<Location>
    {
        public List<Car> getCarsByAddress(string address);
    }
}

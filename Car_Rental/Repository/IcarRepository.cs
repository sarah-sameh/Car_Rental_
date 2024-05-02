using Car_Rental.Models;

namespace Car_Rental.Repository
{
    public interface IcarRepository:IRepository<Car>
    {
        List<Car> GetCarsByUserId(string userId);
    }
}

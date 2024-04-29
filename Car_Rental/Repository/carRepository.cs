using Car_Rental.Models;

namespace Car_Rental.Repository
{
    public class CarRepository:Repository<Car>,IcarRepository
    {
        public CarRepository(Context context) : base(context)
        {
        }
    }
}

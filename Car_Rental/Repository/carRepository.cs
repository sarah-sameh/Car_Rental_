using Car_Rental.Models;

namespace Car_Rental.Repository
{
    public class CarRepository:Repository<Car>,IcarRepository
    {
        public CarRepository(Context context) : base(context)
        {
        }
        public List<Car> GetCarsByUserId(string userId)
        {
            List<Car> cars = _context.Cars.Where(c => c.userId == userId).ToList();
            return cars;
        }

        public List<Car> SearchByMode(string model)
        {
            List<Car> cars = getAll().Where(car => car.Model.ToLower().Contains(model.ToLower()) && !car.IsDeleted).ToList();
            return cars;

        }
    }
}

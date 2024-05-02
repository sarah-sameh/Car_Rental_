using Car_Rental.Models;

namespace Car_Rental.Repository
{
    public interface IRentalRepository:IRepository<Rental>
    {
        public List<Rental> getByUserID(string userId);

    }
}

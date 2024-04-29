using Car_Rental.Models;

namespace Car_Rental.Repository
{
    public class RentalRepository : Repository<Rental>, IRentalRepository
    {
        public RentalRepository(Context context) : base(context)
        {
        }
    }
}

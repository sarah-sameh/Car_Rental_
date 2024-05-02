using Car_Rental.Models;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Car_Rental.Repository
{
    public class RentalRepository : Repository<Rental>, IRentalRepository
    {
        public RentalRepository(Context context) : base(context)
        {
           
        }

        public List<Rental> getByUserID(string userId)
        {
            
                List <Rental>  rentals = _context.Rentals.Where(u => u.Customer_Id == userId).ToList();
                return rentals;
            
        }
    }
}

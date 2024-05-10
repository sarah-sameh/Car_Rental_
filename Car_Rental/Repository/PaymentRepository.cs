using Car_Rental.Models;

namespace Car_Rental.Repository
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        public PaymentRepository(Context context) : base(context)
        {
        }

        public List<Payment> getByUserID(string userId)
        {
            List<Payment> payments = _context.Payments.Where(u => u.Customer_Id == userId).ToList();
            return payments;
        }


    }
}

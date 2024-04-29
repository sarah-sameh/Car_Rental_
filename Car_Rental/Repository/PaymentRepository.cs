using Car_Rental.Models;

namespace Car_Rental.Repository
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        public PaymentRepository(Context context) : base(context)
        {
        }
    }
}

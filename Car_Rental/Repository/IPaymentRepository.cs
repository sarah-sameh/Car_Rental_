using Car_Rental.Models;

namespace Car_Rental.Repository
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        public List<Payment> getByUserID(string userId);

    }
}

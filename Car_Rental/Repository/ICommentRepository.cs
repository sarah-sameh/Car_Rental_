using Car_Rental.Models;

namespace Car_Rental.Repository
{
    public interface ICommentRepository:IRepository<Comments>
    {
        public List<Comments> getByUserID(string userId);
    }

}

using Car_Rental.Models;
using Microsoft.Identity.Client;

namespace Car_Rental.Repository
{
    public class CommentRepository : Repository<Comments>, ICommentRepository
    {
        public CommentRepository(Context context) : base(context)
        {
            
        }

        public List<Comments> getByCarID(int carID)
        {
          List<Comments> comments = _context.Comments.Where(c=>c.CarId == carID).ToList();
            return comments;
        }

        public List<Comments>getByUserID(string userId)
        {
            List<Comments>comments=_context.Comments.Where(u=>u.userId==userId).ToList();
            return comments;
        }
       
    }
}

using Car_Rental.Models;

namespace Car_Rental.Repository
{
    public class CommentRepository : Repository<Comments>, ICommentRepository
    {
        public CommentRepository(Context context) : base(context)
        {
        }
    }
}

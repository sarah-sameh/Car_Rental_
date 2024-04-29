using Car_Rental.Models;

namespace Car_Rental.Repository
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        //private readonly Context context;

        //public UserRepository(Context _context)
        //{
        //    context = _context;
        //}

        //public ApplicationUser GetById(string appUserId)
        //{
        //    ApplicationUser user = context.applicationUsers.Find(appUserId);
        //    return user;
        //}

        //public ApplicationUser GetByUserName(string userName)
        //{
        //    return context.applicationUsers
        //        .FirstOrDefault(user => user.UserName == userName);
        //}

        //public void Update(ApplicationUser appUser)
        //{
        //    context.applicationUsers.Update(appUser);
        //    context.SaveChanges();
        //}
        public UserRepository(Context context) : base(context)
        {
        }
    }
}

using Car_Rental.Models;

namespace Car_Rental.Repository
{
    public interface IUserRepository:IRepository<ApplicationUser>
    {
        //public void Update(ApplicationUser appUser);
        //public ApplicationUser GetByUserName(string userName);
        public ApplicationUser GetById(string appUserId);
    }
}

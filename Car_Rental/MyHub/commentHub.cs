using Car_Rental.Models;
using Car_Rental.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace Car_Rental.MyHub
{
    public class commentHub : Hub
    {
        private readonly ICommentRepository commentRepository;
     
        private readonly UserManager<ApplicationUser> userManager;

        public commentHub(ICommentRepository commentRepository,UserManager<ApplicationUser> userManager)
        {
            this.commentRepository = commentRepository;
          
            this.userManager = userManager;
        }
        public async Task NewComment(string text, int carID, int rating)
        {
            //save db
            //var user = await userManager.GetUserAsync(Context.User);
            //string userName = user.UserName;
            //notify clients

            Comments comments = new Comments
            {
                Text = text,
                CarId = carID,
              //  applicationUser = user,
                Rating = rating
            };
            commentRepository.Insert(comments);
            commentRepository.save();

            Clients.All.SendAsync("ReciveNewComment", text, carID, rating).Wait();
        }
        //public override Task OnConnectedAsync()
        //{

        //    string user = Context.User.Identity.Name;
        //    Clients.All.SendAsync("newUser", user, Context.ConnectionId);

        //    return base.OnConnectedAsync();
        //}
    }
}

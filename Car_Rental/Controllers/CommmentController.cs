using Car_Rental.DTOs;
using Car_Rental.Models;

using Car_Rental.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Car_Rental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository commentRepository;
        private readonly IcarRepository carRepository;
        private readonly IUserRepository userRepository;
        private readonly UserManager<ApplicationUser> userManager;


        // private readonly commentHub commentHub;

        public CommentController(ICommentRepository commentRepository, IcarRepository carRepository, IUserRepository userRepository, UserManager<ApplicationUser> userManager)
        {
            this.commentRepository = commentRepository;
            this.carRepository = carRepository;
            this.userRepository = userRepository;
            this.userManager = userManager;
            //  this.commentHub = commentHub;
        }

        [HttpGet]
        public ActionResult<GeneralResponse> GetAll()
        {
            List<Comments> comments = commentRepository.getAll().Where(i => i.IsDeleted == false).ToList();



            List<commentDTO> commentDTOs = comments.Select(c =>
            {
                Car car = carRepository.get(c.CarId);
                ApplicationUser user = userRepository.GetById(c.userId);
                return new commentDTO
                {
                    Text = c.Text,
                    Rating = c.Rating,
                    CarId = c.CarId,
                    userName = user.UserName,
                    userID = c.userId,
                    IsDeleted = c.IsDeleted,

                };
            }).ToList();
            GeneralResponse response = new GeneralResponse()
            {
                IsPass = true,
                Message = commentDTOs
            };

            return response;
        }

        [HttpDelete]
        [Authorize]
        public ActionResult<GeneralResponse> Delete(int id)
        {

            Comments comments = commentRepository.get(id);
            commentRepository.delete(id);
            GeneralResponse response = new GeneralResponse()
            {
                IsPass = true,
                Message = comments
            };

            return response;


        }



        [HttpPut("{id:int}")]
        [Authorize]
        public ActionResult<GeneralResponse> Update(int id, updateCommentDTO newComment)
        {

            Comments comments = commentRepository.get(id);

            if (comments == null)
            {
                GeneralResponse generalResponse = new GeneralResponse()
                {
                    IsPass = false,
                    Message = "couldn't find this comment"
                };
                return generalResponse;
            }
            comments.Text = newComment.Text;
            comments.Rating = newComment.Rating;

            GeneralResponse response = new GeneralResponse()
            {
                IsPass = true,
                Message = newComment
            };
            return response;
        }


        [HttpPost]
        // [Authorize]
        public async Task<ActionResult<GeneralResponse>> Insert(InsertCommentDto commentDto)
        {
            //var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //ApplicationUser user = userRepository.GetById(userId);
            //var Current_User = await userManager.GetUserAsync(User);


            if (ModelState.IsValid)
            {
                Comments comments = new Comments();
                comments.Text = commentDto.Text;
                comments.Rating = commentDto.Rating;
                comments.CarId = commentDto.CarId;
                comments.userId = commentDto.userId;
                commentRepository.Insert(comments);
                commentRepository.save();


                // await commentHub.Clients.All.SendAsync("NewComment", commentDto.Text, commentDto.CarId, commentDto.Rating);
                return new GeneralResponse { IsPass = true, Message = "Comment inserted successfully" };

            }
            return BadRequest(ModelState);
        }


        [HttpGet("user/{id:guid}")]
        [Authorize]
        public ActionResult<GeneralResponse> GetByUserId(string id)
        {
            List<Comments> comments = commentRepository.getByUserID(id).Where(i => i.IsDeleted == false).ToList();

            List<commentDTO> commentDTOs = comments.Select(c => new commentDTO
            {
                Text = c.Text,
                Rating = c.Rating,
                CarId = c.CarId,
                userID = c.userId,
                IsDeleted = c.IsDeleted,
            }).ToList();

            GeneralResponse response = new GeneralResponse()
            {
                IsPass = true,
                Message = commentDTOs
            };

            return response;
        }

        [HttpGet("{id:int}")]
        public ActionResult<GeneralResponse> getByCarID(int id)
        {
            List<Comments> comments = commentRepository.getByCarID(id);
            List<commentDTO> commentDTOs = comments.Select(c =>
            {
                Car car = carRepository.get(c.CarId);
                ApplicationUser user = userRepository.GetById(c.userId);
                return new commentDTO
                {
                    Text = c.Text,
                    Rating = c.Rating,
                    CarId = c.CarId,
                    userID = c.userId,
                    userName = user != null ? user.UserName : "Unknown", // Handle null user
                    IsDeleted = c.IsDeleted,
                };
            }).ToList();

            return new GeneralResponse()
            {
                IsPass = true,
                Message = commentDTOs
            };
        }





    }
}
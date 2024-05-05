using Car_Rental.DTOs;
using Car_Rental.Models;
using Car_Rental.MyHub;
using Car_Rental.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Car_Rental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository commentRepository;
        private readonly commentHub commentHub;

        public CommentController(ICommentRepository commentRepository, commentHub commentHub)
        {
            this.commentRepository = commentRepository;
            this.commentHub = commentHub;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<GeneralResponse> GetAll()
        {
            List<Comments> comments = commentRepository.getAll().Where(i => i.IsDeleted == false).ToList();


            List<commentDTO> commentDTOs = comments.Select(c => new commentDTO
            {
                Text = c.Text,
                Rating = c.Rating,
                CarId = c.CarId,
                userId = c.userId,
                IsDeleted = c.IsDeleted,
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
        [Authorize]
        public async Task<ActionResult<GeneralResponse>> Insert(commentDTO commentDto)
        {

            if (ModelState.IsValid)
            {
                //Comments comments = new Comments();

                //comments.Text = commentDto.Text;
                //comments.Rating = commentDto.Rating;
                //comments.IsDeleted = commentDto.IsDeleted;
                //comments.userId = commentDto.userId;
                //comments.CarId = commentDto.CarId;
                //commentRepository.Insert(comments);
                //commentRepository.save();

                await commentHub.Clients.All.SendAsync("NewComment", commentDto.Text, commentDto.CarId, commentDto.Rating);
                return new GeneralResponse { IsPass = true, Message = "Comment inserted successfully" };

            }
            return BadRequest(ModelState);
        }


        [HttpGet("{id:guid}")]
        [Authorize]
        public ActionResult<GeneralResponse> GetByUserId(string id)
        {
            List<Comments> comments = commentRepository.getByUserID(id).Where(i => i.IsDeleted == false).ToList();


            List<commentDTO> commentDTOs = comments.Select(c => new commentDTO
            {
                Text = c.Text,
                Rating = c.Rating,
                CarId = c.CarId,
                userId = c.userId,
                IsDeleted = c.IsDeleted,
            }).ToList();

            GeneralResponse response = new GeneralResponse()
            {
                IsPass = true,
                Message = commentDTOs
            };

            return response;
        }
    }


}

using Car_Rental.DTOs;
using Car_Rental.Models;
using Car_Rental.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Car_Rental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository commentRepository;

        public CommentController(ICommentRepository commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        [HttpGet]
        public ActionResult<GeneralResponse> GetAll()
        {
            List<Comments> comments = commentRepository.getAll().Where(i => i.IsDeleted == false).ToList();

            GeneralResponse response = new GeneralResponse()
            {
                IsPass = true,
                Message =comments
            };

            return response;
        }

        [HttpDelete]
        public ActionResult<GeneralResponse> Delete(int id)
        {

        Comments comments=commentRepository.get(id);
            commentRepository.delete(id);
            GeneralResponse response = new GeneralResponse()
            {
                IsPass = true,
                Message = comments
            };

            return response;


        }



        [HttpPut("{id:int}")]
        public ActionResult<GeneralResponse> update(int id,Comments newComment)
        {

            Comments comments=commentRepository.get(id);
            if (comments == null)
            {
                GeneralResponse generalResponse = new GeneralResponse()
                {
                    IsPass = false,
                    Message = comments
                };
                return generalResponse;
            }
            comments.Text = newComment.Text;
            comments.Rating = newComment.Rating;

            GeneralResponse response = new GeneralResponse()
            {
                IsPass=true,
                Message=newComment
            };
            return response;    
        }


        [HttpPost]
        public ActionResult<GeneralResponse>Insert(commentDTO commentDto)
        {

            if(ModelState.IsValid)
            {
                Comments comments = new Comments();

                comments.Text = commentDto.Text;
                comments.Rating = commentDto.Rating;
               comments.IsDeleted = commentDto.IsDeleted;
                comments.userId = commentDto.userId;
                comments.CarId=commentDto.CarId;
                commentRepository.Insert(comments);
                commentRepository.save();

                return new GeneralResponse { IsPass = true, Message = "Comment inserted successfully" };

            }
            return BadRequest(ModelState);
        }
    }


}

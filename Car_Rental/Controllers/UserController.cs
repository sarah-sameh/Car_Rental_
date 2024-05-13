using Car_Rental.DTOs;
using Car_Rental.Models;
using Car_Rental.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Car_Rental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository ) {
            this.userRepository = userRepository;
        }


        [HttpGet("{id:guid}")]
        public ActionResult<GeneralResponse> getUserByID(string id)
        {
            ApplicationUser user = userRepository.GetById(id);
            if (user == null)
            {
                return new GeneralResponse{
                    
                    IsPass = false,
                    Message="not found"
                    
                };
            }

            userDTO userDTO = new userDTO();
            userDTO.id = user.Id;
            userDTO.userName = user.UserName;
            return new GeneralResponse
            {
                IsPass = true,
                Message = userDTO

            };


        }
    }
}

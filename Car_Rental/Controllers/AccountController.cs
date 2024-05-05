using Car_Rental.DTOs;
using Car_Rental.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Car_Rental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManger;
        private readonly IConfiguration configuration;

        public AccountController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            this.userManger = userManager;
            this.configuration = configuration;
        }
        [HttpPost("register")]
        // api/account/Register
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = registerUserDto.UserName,
                    Email = registerUserDto.Email,
                    Name = registerUserDto.Name, // Match Name property
                    PhoneNumber = registerUserDto.PhoneNumber, // Match PhoneNumber property
                    Address = registerUserDto.Address // Match Address property

                };
                IdentityResult Result = await userManger.CreateAsync(user, registerUserDto.Password);
                if (Result.Succeeded)
                {
                    return Ok("Account Created");
                }
                return BadRequest(Result.Errors);

            }
            return BadRequest(ModelState);
        }
        [HttpPost("login")]
        // api/account/login (UserNAme/Password)
        public async Task<IActionResult> Login(LoginDto loginUser)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser? user = await userManger.FindByNameAsync(loginUser.UserName);
                if (user != null)
                {
                    bool found = await userManger.CheckPasswordAsync(user, loginUser.Password);
                    if (found)
                    {
                        List<Claim> Myclaims = new List<Claim>();
                        Myclaims.Add(new Claim(ClaimTypes.Name, user.UserName));
                        Myclaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        Myclaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                        var roles = await userManger.GetRolesAsync(user);
                        foreach (var role in roles)
                        {
                            Myclaims.Add(new Claim(ClaimTypes.Role, role));
                        }
                        var signKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecKey"]));
                        SigningCredentials signingCredentials = new SigningCredentials(signKey, SecurityAlgorithms.HmacSha256);
                        ///create token
                        JwtSecurityToken token = new JwtSecurityToken(
                            issuer: configuration["JWT:ValidIss"],///Provider create token
                            audience: configuration["JWT:ValidAud"], // consumer url(Anguler)
                            expires: DateTime.Now.AddDays(2),
                            claims: Myclaims,
                            signingCredentials: signingCredentials
                            );
                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expired = token.ValidTo

                        });

                    }
                }

                return Unauthorized("InValid User");

            }
            return BadRequest(ModelState);

        }
    }
}

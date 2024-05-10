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
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, SignInManager<ApplicationUser> signInManager)
        {
            this.userManger = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }






        [HttpPost("register")]
        // api/account/Register
        public async Task<ActionResult<GeneralResponse>> Register(RegisterUserDto registerUserDto)
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
                    if (registerUserDto.Role.ToUpper() == "ADMIN")
                    {
                        await userManger.AddToRoleAsync(user, "ADMIN");
                    }
                    else
                    {
                        await userManger.AddToRoleAsync(user, "CUSTOMER");
                    }

                    return new GeneralResponse { IsPass = true, Message = "Account created successfully" };
                }
                else
                {
                    return new GeneralResponse { IsPass = false, Message = "Failed to create account" };
                }



                /////
                //if (!roleManager.RoleExistsAsync("Admin").Result)
                //{
                //    var AdminRole = new IdentityRole
                //    {
                //        Name = "Admin"
                //    };
                //    roleManager.CreateAsync(AdminRole).Wait();
                //}
                //IdentityResult ResultRole = new IdentityResult();
                //try
                //{
                //    ResultRole = await userManger.AddToRoleAsync(user, registerUserDto.Role);
                //}
                //catch (Exception e)
                //{
                //    //default is customer
                //    ResultRole = await userManger.AddToRoleAsync(user, "Customer");
                //}


                /////


            }
            return BadRequest(ModelState);
        }
        private async Task AssignRole(ApplicationUser user, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
            await userManger.AddToRoleAsync(user, roleName);
        }
        [HttpPost("login")]
        // api/account/login (UserNAme/Password)
        public async Task<ActionResult<GeneralResponse>> Login(LoginDto loginUser)
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
                            expired = token.ValidTo,
                            ispass = true,


                        });

                    }
                }


            }
            return new GeneralResponse
            {
                IsPass = false,
                Message = "invalid account"
            };

        }
        [HttpPost("reset-password")]
        // api/account/reset-password
        public async Task<ActionResult<GeneralResponse>> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var user = await userManger.FindByNameAsync(resetPasswordDto.UserName);
            if (user == null)
            {

                return BadRequest(new GeneralResponse { IsPass = false, Message = "User not found." });
            }

            var signInResult = await signInManager.PasswordSignInAsync(user.UserName, resetPasswordDto.oldPassword, false, lockoutOnFailure: false);
            if (signInResult.Succeeded == false)
            {

                return BadRequest(new GeneralResponse { IsPass = false, Message = "Invalid  password." });
            }
            else
            {

                var Token = await userManger.GeneratePasswordResetTokenAsync(user);

                var result = await userManger.ResetPasswordAsync(user, Token, resetPasswordDto.NewPassword);
                if (result.Succeeded)
                {
                    return Ok(new GeneralResponse { IsPass = true, Message = "Password reset successfully." });
                }
                else
                {

                    return BadRequest(new GeneralResponse { IsPass = false, Message = "Failed to reset password." });
                }
            }
        }



    }
}


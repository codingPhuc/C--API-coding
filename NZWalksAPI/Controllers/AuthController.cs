using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repository;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepositorycs tokenRepositorycs;

        public AuthController(UserManager<IdentityUser> userManager , ITokenRepositorycs tokenRepositorycs)
        {
            this.userManager = userManager;
            this.tokenRepositorycs = tokenRepositorycs;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                 UserName  = registerRequestDto.UserName, 
                 Email   = registerRequestDto.UserName
            }; 
            var identityResult = await  userManager.CreateAsync(identityUser,registerRequestDto.Password);  

            if (identityResult.Succeeded)
            { 

                if(registerRequestDto.Roles != null && registerRequestDto.Roles.Any()) {

                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles); 
                    if (identityResult.Succeeded)
                    {
                        return Ok("User was registered| Please login "); 
                    }
                }
            }
            return BadRequest("Something went wrong");
             
        }
        [HttpPost]
        [Route("Login")]
        public async  Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Username) ;
            
            if(user != null )
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password); 
                if (checkPasswordResult)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    
                    if( roles  != null)
                    {
                        var jwtToken = tokenRepositorycs.CreateJWToken(user, roles.ToList());
                        var response = new LoginReposondDto
                        {
                            jwtToken = jwtToken 
                        };
                        return Ok( response); 


                    }
                }
            }
            return BadRequest("Username or password incorrect");  

        }
    }
}

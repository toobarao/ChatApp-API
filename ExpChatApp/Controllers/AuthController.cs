using ExpChatApp.Entity;
using ExpChatApp.Models;
using ExpChatApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ExpChatApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                           .Where(x => x.Value.Errors.Count > 0)
                           .Select(x => new
                           {
                               Field = x.Key,
                               Errors = x.Value.Errors.Select(e => e.ErrorMessage)
                           });

                return BadRequest(errors);
               
            }
            try
            {
               
                    var user=await  authService.Register(registerDto);
                    if(user==null)
                    return BadRequest("user registration failed"); 
                    return Ok(user);

            }
            catch (Exception ex) {
                return StatusCode(500,ex.Message);
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        { 
            if(!ModelState.IsValid)
            {
                var errors = ModelState
                           .Where(x => x.Value.Errors.Count > 0)
                           .Select(x => new
                           {
                               Field = x.Key,
                               Errors = x.Value.Errors.Select(e => e.ErrorMessage)
                           });

                return BadRequest(errors);
            }
            try { 
                var token=await authService.Login(loginDto);
                if (string.IsNullOrEmpty(token))
                    return BadRequest("Invalid Username or Password");
                Response.Cookies.Append("authToken", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTimeOffset.UtcNow.AddDays(7)
                });
                return Ok(new { message = "Login successful" });
            }
            catch (Exception ex) {
             return StatusCode(500, ex.Message);

            }
        
        }
       
        [HttpGet("users")]
        [Authorize]
        public async Task<IActionResult> GetUser()
        {
            var user =await authService.GetUsers();
            return Ok(user);
        }

        [HttpGet("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            return Ok("you have logout successfully");
        }
        [HttpGet("GetAuthUser")]
        [Authorize]
        public async Task<IActionResult> GetAuthUser()
        {
            var user =await authService.GetAuthenticatedUser(Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            
            return Ok(user);
        }



    }
}

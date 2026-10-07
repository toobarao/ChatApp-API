using AutoMapper;
using ExpChatApp.Data;
using ExpChatApp.Entity;
using ExpChatApp.Migrations;
using ExpChatApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;

namespace ExpChatApp.Services
{
   
    public class AuthService(IMapper mapper, ChatAppDbContext dbContext,IConfiguration configuration) : IAuthService
    {
        public async Task<User> GetAuthenticatedUser(int Id)
        {
            User user = await dbContext.users.FindAsync(Id);
            return user;
        }

        public async Task<List<User>> GetUsers()
        {
            var user = await dbContext.users.ToListAsync();
            return user;
        }

        public async Task<string?> Login(LoginDto loginDto)
        {
            var user =await dbContext.users.FirstOrDefaultAsync(x => x.Name == loginDto.Name);
            if (user == null) return null;
            var verifyPassword = new PasswordHasher<User>().VerifyHashedPassword(user, user.HashPassword,loginDto.HashPassword);
            if(PasswordVerificationResult.Success==verifyPassword)
            {
                return CreateWebToken(user);
            }
            return null;
        }

        public async Task<User> Register(RegisterDto registerDto)
        {
            if (dbContext.users.Any(x=>x.Name==registerDto.Name||x.Email==registerDto.Email))
                return null;
            User user=mapper.Map<User>(registerDto);
            var hashPassword=new PasswordHasher<User>().HashPassword(user,user.HashPassword);
            user.HashPassword = hashPassword;
            await dbContext.users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return user;
            
        }
        private string CreateWebToken(User user)
        {
            //for user information
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Name),
                new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
               
            };
            //secret key of 65 characters thats gone be converted into byte array 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes((configuration.GetValue<string>("Appsettings:Token"))!));

            //siging algorithm for our secret key
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);


            //creating token 
        
            var tokenDescripter = new JwtSecurityToken(
              
               claims: claims,
               expires: DateTime.UtcNow.AddDays(3),
               signingCredentials: creds);

            //serializes the JWT object into the standard JWT compact string format
            return new JwtSecurityTokenHandler().WriteToken(tokenDescripter);

        }
    }
}

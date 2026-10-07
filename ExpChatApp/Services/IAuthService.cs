using ExpChatApp.Entity;
using ExpChatApp.Models;

namespace ExpChatApp.Services
{
    public interface IAuthService
    {
        public Task<User> Register(RegisterDto registerDto);
        public Task<string?> Login(LoginDto loginDto);

        public Task<List<User>> GetUsers();

        public Task<User> GetAuthenticatedUser(int Id);

    }
}

using System.ComponentModel.DataAnnotations;

namespace ExpChatApp.Models
{
    public class LoginDto
    {
        [StringLength(50, MinimumLength = 3)]
        public required string Name { get; set; }

        public required string HashPassword { get; set; }
    }
}

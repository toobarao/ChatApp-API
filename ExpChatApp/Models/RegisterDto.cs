using System.ComponentModel.DataAnnotations;

namespace ExpChatApp.Models
{
    public class RegisterDto
    {
        [StringLength(50, MinimumLength = 3)]
        public required string Name { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public required string Email { get; set; }
        [StringLength(100, ErrorMessage = "The password must be at least {2} characters long.", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
       ErrorMessage = "Password must meet complexity requirements (1 uppercase, 1 lowercase, 1 digit, 1 special character).")]
        public required string HashPassword { get; set; }
    }
}

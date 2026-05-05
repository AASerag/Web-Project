using System.ComponentModel.DataAnnotations;

namespace Adam_Ahmed_Web_Project.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; } = string.Empty;

        // Optional: If they don't send a role, it defaults to "User" in the controller
        public string? Role { get; set; } 
    }
}
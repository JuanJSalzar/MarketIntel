using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Account
{
    public class RegisterDto
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [MinLength(8)]
        public string? Password { get; set; }
    }
}
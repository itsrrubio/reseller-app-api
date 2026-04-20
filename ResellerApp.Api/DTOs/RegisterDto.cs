using System.ComponentModel.DataAnnotations;

namespace ResellerApp.Api.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

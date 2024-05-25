using Microsoft.Build.Framework;

namespace TechNewsUI.Models
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

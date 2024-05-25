using System.ComponentModel.DataAnnotations;

namespace TechNewsUI.Models
{
    public class ForgetPassword
    {
        [Required(ErrorMessage = "Email id is required")]
        [RegularExpression(@"^(.+)@(.+)$",
                            ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }
    }
}

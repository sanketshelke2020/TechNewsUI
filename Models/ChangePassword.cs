using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TechNewsUI.Models
{
    public class ChangePassword
    {
        public string ?Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{4,20}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit & one special symbol.")]// Password must be more than 5 characters & less than 21 characters.")]
        [StringLength(20, MinimumLength = 8)]
        [Remote("OldPasswordDoesNotExists", "Forget", HttpMethod = "POST", ErrorMessage = "Old Password Doesnot Exists")]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{4,20}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit & one special symbol.")]// Password must be more than 5 characters & less than 21 characters.")]
        [StringLength(20, MinimumLength = 8)]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string ConfirmPassword { get; set; }
    }
}

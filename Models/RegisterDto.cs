using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TechNewsUI.Models
{
    public class RegisterDto
    {
        
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "First Name")]

        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]

        public string LastName { get; set; }
        [Required]
        [Display(Name = "Company Name")]

        public string? CompanyName { get; set; }
        [Required]
        [Display(Name = "Mobile Number")]
        [RegularExpression("^([+][0-9]{1,3}[-])?[0-9]{10}$", ErrorMessage = "Format for Valid number are : 8087339090,+91-8087339090")]
        public string? PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Email Id")]

        //[EmailAddress]
        [RegularExpression("^([a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,4})*$", ErrorMessage = "Email id should be like this :example@email.com or example.exp@mail.in")]

        [Remote("EmailsDoesNotExists", "Register", HttpMethod = "GET", ErrorMessage = "User with this email id already exists.")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9]{5,11}$", ErrorMessage = "1st letter should charcter after that it can be alphanumeric and length should between 6 to 12.")]
        public string UserName { get; set; }

        [Required]

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Password must be between 8 and 16 characters and contain one uppercase letter, one lowercase letter, one digit and one special character.")]

        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Password and confirm password should be same.")]
        [Compare("Password", ErrorMessage = "Password and confirm password should be same.")]


        public string ConfirmPassword { get; set; } = null!;
        public string Nationality { get; set; }
        [DisplayName("Country ")]
        public int CountryId { get; set; }
        [DisplayName("State ")]

        public int StateId { get; set; }
        [DisplayName("City ")]

        public int CityId { get; set; }

        public int UserRoleId { get; set; } = 1;
    }
}

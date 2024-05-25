using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TechNewsUI.Models
{
    public class UpdateUser
    {
        public int UserId { get; set; }
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
        [RegularExpression(@"^(\+\d{1,3}[- ]?)?\d{10}$", ErrorMessage = "Format for Valid number are : 8087339090, +91 8087339090, +221 8087339090")]
        public string? PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Email Id")]

        //[EmailAddress]
        [RegularExpression("^([a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,4})*$", ErrorMessage = "Email id should be like this :example@email.com or example.exp@mail.in")]

        //[Remote("EmailsDoesNotExists", "Register", HttpMethod = "POST", ErrorMessage = "User with this email id already exists.")]
        public string Email { get; set; }

        [Required]

        public string Nationality { get; set; }
        [DisplayName("Country ")]
        public int CountryId { get; set; }
        [DisplayName("State ")]

        public int StateId { get; set; }
        [DisplayName("City ")]

        public int CityId { get; set; }

        public int UserRoleId { get; set; } = 1;
        public Byte[] PasswordHash { get; set; }
        public Byte[] PasswordSalt { get; set; }
    }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TechNewsUI.Models
{
    public class UserQuery
    {
        public int QuerieId { get; set; }
        public int UserId { get; set; }
        [Required(ErrorMessage = "The Full Name field is required.")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "The Job Designation field is required.")]

        public string JobDesignation { get; set; }
        [Required(ErrorMessage = "The Company Name field is required.")]

        public string CompanyName { get; set; }
        [Display(Name = "Email Id")]

        //[EmailAddress]
        [RegularExpression("^([a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,4})*$", ErrorMessage = "Email id should be like this :example@email.com or example.exp@mail.in")]
        public string EmailAddress { get; set; }
        [Display(Name = "Mobile Number")]
        [RegularExpression("^([+][0-9]{1,3}[-])?[0-9]{10}$", ErrorMessage = "Format for Valid number are : 8087339090,+91-8087339090")]
        public string MobileNumber { get; set; }
        [Display(Name = "Reason for contact")]

        public string Subject { get; set; }
        public string Description { get; set; }
        public string CountryName { get; set; }
        [DisplayName("Country ")]
        public int CountryId { get; set; }
        [Required(ErrorMessage = "The Category Type field is required.")]

        public string CategoryType { get; set; }

    }
}

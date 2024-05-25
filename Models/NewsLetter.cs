using System.Web.Mvc;

namespace TechNewsUI.Models
{
    public class NewsLetter
    {
        [Remote("EmailsDoesNotExists", "HomePage", HttpMethod = "GET", ErrorMessage = " Email id already exists.")]
        public string Email { get; set; }
    }
}

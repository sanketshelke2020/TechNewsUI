using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace TechNewsUI.Models
{
    public class UpdateSectionMasterCommandDto
    {
        public string? flexRadioDefault { get; set; }
        public int SectionMasterId { get; set; }
        [Required]
        [StringLength(150,MinimumLength = 10)]
        public string? Title { get; set; }
        public byte[]? Image { get; set; }
        [JsonIgnore]
        [Display(Name = "Image")]
        public IFormFile? FormImage { get; set; }
        public string? FileName { get; set; }
        [Required]
        [StringLength(150, MinimumLength = 50)]
        [Display(Name = "Short Description")]
        public string? ShortDescription { get; set; }
        public int? TotalViews { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Search Keywords")]
        public string? KeyWords { get; set; }
        [Required]
        [Display(Name = "Category")]

        public string? CategoryType { get; set; }
        public int? CategoryId { get; set; }
        public DateTime CreatedDate { get; set; }
        [Required]
        [MinLength(250)]
        [Display(Name = "Long Description")]
        public string? LongDescription { get; set; }
        [RequiredIfCustomeValidationUpdate("Article")]
        [Display(Name = "Article Category")]
        public int? ArticleCategoryId { get; set; }
        [RequiredIfCustomeValidationUpdate("Article")]
        [Display(Name = "Article Subcategory")]
        public int? ArticleSubCategoryId { get; set; }
        [RequiredIfCustomeValidationUpdate("Article")]
        [Display(Name = "Country")]
        public int? CountryId { get; set; }
        [RequiredIfCustomeValidationUpdate("CaseStudies")]
        [Range(1,999)]
        [Display(Name = "Number of Chapters")]
        public int? NumberOfChapter { get; set; }
        [RequiredIfCustomeValidationUpdate("Webinar", "EventSchedule")]
        [StringLength(100, MinimumLength = 3)]
        [Display(Name = "Event Topic")]

        public string? EventTopic { get; set; }
        [RequiredIfCustomeValidationUpdate("EventSchedule")]
        [Display(Name = "Event Date")]

        public DateTime? EventDate { get; set; }
        [RequiredIfCustomeValidationUpdate("EventSchedule", "Webinar", "Podcast")]
        [StringLength(100, MinimumLength = 3)]
        [Display(Name = "Speaker Name")]
        [RegularExpression("^([\\sA-Za-z]+)$",ErrorMessage = "Only characters and spaces are allowed")]
        public string? SpeakerName { get; set; }
        [RequiredIfCustomeValidationUpdate("Magazine")]
        [Display(Name = "Number of Pages")]
        [Range(1, 9999)]
        public int? NumberOfPages { get; set; }
        [RequiredIfCustomeValidationUpdate("Webinar")]
        [Display(Name = "Start Date")]

        public DateTime? StartDate { get; set; }
        [RequiredIfCustomeValidationUpdate("Webinar")]
        [Display(Name = "End Date")]

        public DateTime? EndDate { get; set; }
        [RequiredIfCustomeValidationUpdate("Webinar")]
        [Display(Name = "Total Seats")]
        [Range(1,9999)]
        public int? TotalSeat { get; set; }
        //webinar holder
        [RequiredIfCustomeValidationUpdate("Webinar")]
        [StringLength(100, MinimumLength = 3)]
        [Display(Name = "Webinar Holder Name")]
        [RegularExpression("^([\\sA-Za-z]+)$", ErrorMessage = "Only characters and spaces are allowed")]
        public string? Name { get; set; }
        [RequiredIfCustomeValidationUpdate("Webinar")]
        [StringLength(100, MinimumLength = 3)]
        [Display(Name = "Webinar Holder Designation")]
        public string? Designation { get; set; }
        [RequiredIfCustomeValidationUpdate("Webinar")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        [Display(Name = "Webinar Holder Phone Number")]
        public string? ContactNumber { get; set; }
        [RequiredIfCustomeValidationUpdate("Webinar")]
        [Display(Name = "Webinar Holder Email")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }
        public int StoredFileId { get; set; }
        public string? StoredFileName { get; set; }
        [RequiredIfCustomeValidationUpdate("fileRequired")]
        [Display(Name = "File")]
        public IFormFile? StoredFile { get; set; }
        public byte[]? File { get; set; }
        [RequiredIfCustomeValidationUpdate("Youtube", "Webinar", "LiveInterview", "EventSchedule", "youtubeRequired")]
        [RegularExpression("^(http:\\/\\/www\\.|https:\\/\\/www\\.|http:\\/\\/|https:\\/\\/)?[a-z0-9]+([\\-\\.]{1}[a-z0-9]+)*\\.[a-z]{2,5}(:[0-9]{1,5})?(\\/.*)?$", ErrorMessage = "Please Enter Valid Url")]

        [Display(Name = "Paste Youtube link here")]
        public string? YoutubeVideoLink { get; set; }
        public int WebinarHolderId { get; set; }
    }
}

using Foolproof;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TechNewsUI.Models
{
    public class CreateSectionMasterCommand
    {
        //public string? flexRadioDefault { get; set; }
        ////[Required]
        ////[StringLength(150, MinimumLength = 10)]
        //public string? Title { get; set; }
        //public byte[]? Image { get; set; }
        ////[JsonIgnore]
        ////[Display(Name = "Image")]
        //public IFormFile FormImage { get; set; }
        //public string ?FileName { get; set; }
        ////[Required]
        ////[StringLength(150,MinimumLength = 50)]
        ////[Display(Name = "Short Description")]
        //public string? ShortDescription { get; set; }
        //public int? TotalViews { get; set; }
        ////[Required]
        ////[StringLength(50, MinimumLength = 3)]
        ////[Display(Name = "Search Keywords")]
        //public string? KeyWords { get; set; }
        ////[Required]
        ////[Display(Name = "Category")]

        //public string? CategoryType { get; set; }
        //public int? CategoryId { get; set; }

        ////[Required]
        ////[MinLength(250)]
        ////[Display(Name = "Long Description")]
        //public string? LongDescription { get; set; }
        ////[RequiredIfCustomeValidation("Article")]
        ////[Display(Name = "Article Category")]
        //public int? ArticleCategoryId { get; set; }
        ////[RequiredIfCustomeValidation("Article")]
        ////[Display(Name = "Article Subcategory")]
        //public int? ArticleSubCategoryId { get; set; }
        ////[RequiredIfCustomeValidation("Article")]
        ////[Display(Name = "Country")]
        //public int? CountryId { get; set; }
        ////[RequiredIfCustomeValidation("CaseStudies")]
        ////[Display(Name = "Number of Chapters")]
        ////[Range(1, 999)] 
        //public int? NumberOfChapter { get; set; }
        ////[RequiredIfCustomeValidation("Webinar", "EventSchedule")]
        ////[StringLength(100,MinimumLength = 3)]
        ////[Display(Name = "Event Topic")]

        //public string? EventTopic { get; set; }
        ////[RequiredIfCustomeValidation("EventSchedule")]
        ////[Display(Name = "Event Date")]

        //public DateTime? EventDate { get; set; }
        ////[RequiredIfCustomeValidation("EventSchedule", "Webinar","Podcast")]
        ////[StringLength(100, MinimumLength = 3)]
        ////[Display(Name = "Speaker Name")]
        ////[RegularExpression("^([\\sA-Za-z]+)$", ErrorMessage = "Only characters and spaces are allowed")]
        //public string? SpeakerName { get; set; }
        ////[RequiredIfCustomeValidation("Magazine")]
        ////[Display(Name = "Number of Pages")]
        ////[Range(1, 9999)]
        //public int? NumberOfPages { get; set; }
        ////[RequiredIfCustomeValidation("Webinar")]
        ////[Display(Name = "Start Date")]

        //public DateTime? StartDate { get; set; }
        ////[RequiredIfCustomeValidation("Webinar")]
        ////[Display(Name = "End Date")]

        //public DateTime? EndDate { get; set; }
        ////[RequiredIfCustomeValidation("Webinar")]
        ////[Display(Name = "Total Seats")]
        ////[Range(1, 9999)]
        //public int? TotalSeat { get; set; }
        ////webinar holder
        ////[RequiredIfCustomeValidation("Webinar")]
        ////[StringLength(100, MinimumLength = 3)]
        ////[Display(Name= "Webinar Holder Name")]
        ////[RegularExpression("^([\\sA-Za-z]+)$", ErrorMessage = "Only characters and spaces are allowed")]
        //public string? Name { get; set; }
        ////[RequiredIfCustomeValidation("Webinar")]
        ////[StringLength(100, MinimumLength = 3)]
        ////[Display(Name = "Webinar Holder Designation")]
        //public string? Designation { get; set; }
        ////[RequiredIfCustomeValidation("Webinar")]
        ////[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        ////[Display(Name = "Webinar Holder Phone Number")]
        //public string? ContactNumber { get; set; }
        ////[RequiredIfCustomeValidation("Webinar")]
        ////[Display(Name = "Webinar Holder Email")]
        ////[RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Invalid Email Address")]
        //public string? Email { get; set; }
        //public string? StoredFileName { get; set; }
        ////[RequiredIfCustomeValidation("CaseStudies", "Magazine", "fileRequired")]
        ////[Display(Name = "File")]
        //public IFormFile? StoredFile { get; set; }
        //public byte[]? File { get; set; }
        ////[RequiredIfCustomeValidation("Youtube", "Webinar", "LiveInterview", "EventSchedule", "youtubeRequired")]
        ////[RegularExpression("^(http:\\/\\/www\\.|https:\\/\\/www\\.|http:\\/\\/|https:\\/\\/)?[a-z0-9]+([\\-\\.]{1}[a-z0-9]+)*\\.[a-z]{2,5}(:[0-9]{1,5})?(\\/.*)?$",ErrorMessage ="Please Enter Valid Url")]
        ////[Display(Name = "Paste Youtube link here")]
        //public string? YoutubeVideoLink { get; set; }
        //public string DynamicData { get; set; }
        //public virtual List<ChaptersDto>? ChaptersDtos { get; set; } = new List<ChaptersDto>();
        public string? Title { get; set; }
        //public IFormFile? ImageFile { get; set; }

        public string? FileName { get; set; }
        public string? ShortDescription { get; set; }
        public int? TotalViews { get; set; }
        public string? KeyWords { get; set; }
        public string? CategoryType { get; set; }
        public string? LongDescription { get; set; }
        public string DynamicData { get; set; }



    }
}

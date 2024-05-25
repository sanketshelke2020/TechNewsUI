namespace TechNewsUI.Models
{
    public class GetSectionMasterByIdDto
    {

        public int SectionMasterId { get; set; }
        public string? Title { get; set; }
        public byte[]? Image { get; set; }
        public string? FileName { get; set; }
        public string? ShortDescription { get; set; }
        public int? TotalViews { get; set; }
        public string? KeyWords { get; set; }
        public string? CategoryType { get; set; }
        public string? LongDescription { get; set; }

        public int? ArticleCategoryId { get; set; }
        public int? ArticleSubCategoryId { get; set; }
        public int? CountryId { get; set; }
        public int? NumberOfChapter { get; set; }
        public string? EventTopic { get; set; }
        public DateTime? EventDate { get; set; }
        public string? SpeakerName { get; set; }
        public int? NumberOfPages { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? TotalSeat { get; set; }
        //webinar holder
        public string? Name { get; set; }
        public string? Designation { get; set; }
        public string? ContactNumber { get; set; }
        public string? Email { get; set; }
        public string? StoredFileName { get; set; }
        public byte[]? File { get; set; }
        public string? YoutubeVideoLink { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}


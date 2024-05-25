namespace TechNewsUI.Models
{
    public class GetSectionByKeywordDto
    {
        public int SectionMasterId { get; set; }
        public string Title { get; set; }
        public Byte[] Image { get; set; }
        public string ShortDescription { get; set; }
        public int TotalViews { get; set; }
        public string KeyWords { get; set; }
        public string CategoryType { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

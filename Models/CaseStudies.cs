namespace TechNewsUI.Models
{
    public class CaseStudies
    {
        public int CaseStudiesId { get; set; }
        public string Title { get; set; }
        public Byte[] Image { get; set; }
        public int? NumberOfChapter { get; set; }
        public string ShortDescription { get; set; }

        public int SectionMasterId { get; set; }
        public int TotalViews { get; set; }

    }
}

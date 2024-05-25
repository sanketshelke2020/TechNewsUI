namespace TechNewsUI.Models
{
    public class SectionMasterView
    {
        public int SectionMasterId { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public int TotalViews { get; set; }
        public string KeyWords { get; set; }
        public string CategoryType { get; set; }
        public byte[]? ImageFile { get; set; }
    }
}

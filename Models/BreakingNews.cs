namespace TechNewsUI.Models
{
    public class BreakingNews
    {

        public int SectionMasterId { get; set; }
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public Byte[] Image { get; set; }
        
        public int TotalViews { get; set; }
        public int YouTubeId { get; set; }
        public string ShortDescription { get; set; }

    }
}

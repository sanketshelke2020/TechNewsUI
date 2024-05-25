namespace TechNewsUI.Models
{
    public class Podcast
    {
        public int SectionMasterId { get; set; }
        public string Title { get; set; }
        public byte[] Image { get; set; }
        public string ShortDescription { get; set; }
        public int TotalViews { get; set; }
        public string KeyWords { get; set; }
        public string CategoryType { get; set; }
        public int PodcastId { get; set; }
        public string SpeakerName { get; set; }
        public string LongDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<PodcastChapter> Chapters { get; set; }
        public List<Comment> comment { get; set; }
       
    }
}

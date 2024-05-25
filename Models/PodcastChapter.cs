namespace TechNewsUI.Models
{
    public class PodcastChapter
    {
        public int ChapterId { get; set; }
        public Byte[] Audio { get; set; }
        public string ChapterTitle { get; set; }
        public int ChapterNumber { get; set; }
        public string ChapterDescription { get; set; }
        public int PodcastId { get; set; }

    }
}

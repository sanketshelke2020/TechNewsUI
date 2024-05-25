namespace TechNewsUI.Models
{
    public class YoutubeDtoDetail
    {
        public int YouTubeId { get; set; }
        public string Title { get; set; }
        public Byte[] Image { get; set; }

        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }


        public DateTime Published { get; set; }
        public string? YoutubeVideoLink { get; set; }
        public int TotalViews { get; set; }
        public int SectionMasterId { get; set; }
        public List<Comment> comments { get; set; }

    }
}

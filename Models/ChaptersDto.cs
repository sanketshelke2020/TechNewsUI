using Newtonsoft.Json;

namespace TechNewsUI.Models
{
    public class ChaptersDto
    {
        public int ChapterId { get; set; }
        public Byte[]? Audio { get; set; }
        public string? ChapterTitle { get; set; }
        public int? ChapterNumber { get; set; }
        public string FileName { get; set; }
        public int PodcastId { get; set; }
        public string? ChapterDescription { get; set; }
        [JsonIgnore]
        public IFormFile? ChapterFile { get; set; }
    }
}

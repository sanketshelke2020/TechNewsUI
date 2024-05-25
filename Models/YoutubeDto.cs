using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TechNewsUI.Models
{
    public class YoutubeDto
    {
        public int YouTubeId { get; set; }
        public string Title { get; set; }
        public Byte[] Image { get; set; }
      
        public string ShortDescription { get; set; }
        

        public DateTime Published { get; set; }
        public string? YoutubeVideoLink { get; set; }
        public int TotalViews { get; set; }
        public int SectionMasterId { get; set; }
    }
}

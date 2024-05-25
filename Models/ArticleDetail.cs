using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TechNewsUI.Models
{
    public class ArticleDetail
    {
        public string Title { get; set; }
        public Byte[] Image { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
       

        public DateTime Published { get; set; }

        public string Video { get; set; }
        public int SectionMasterId { get; set; }
        public IList<StoredFiles> StoredFiles { get; set; }
        public List<Comment> comments  { get; set; }
        public string? YoutubeVideoLink { get; set; }



    }
}

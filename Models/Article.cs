using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TechNewsUI.Models
{

        public class Article
        {
            // public int ArticleId { get; set; }


            public int ArticleCategoryId { get; set; }
            public int ArticleSubCategoryId { get; set; }
            public string LongDescription { get; set; }

            public int SectionMasterId { get; set; }
            //// public virtual SectionMaster SectionMaster { get; set; }
            public int CountryId { get; set; }
            //public virtual Country Country { get; set; }
            // public virtual ArticleCategory ArticleCategory { get; set; }
            // public virtual ArticleSubCategory ArticleSubCategory { get; set; }
            public string Title { get; set; }
            public Byte[] Image { get; set; }
            public string Summary { get; set; }
            public string ShortDescription { get; set; }
            [Display(Name = "Published Date and Time")]

            public DateTime Published { get; set; }
        public string? YoutubeVideoLink { get; set; }
        public int TotalViews { get; set; }

    }
    
}

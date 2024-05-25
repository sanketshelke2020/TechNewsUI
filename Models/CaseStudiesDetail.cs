﻿namespace TechNewsUI.Models
{
    public class CaseStudiesDetail
    {
        public int CaseStudiesId { get; set; }
        public string Title { get; set; }
        public Byte[] Image { get; set; }
        public int? NumberOfChapter { get; set; }
        public string ShortDescription { get; set; }

        public string? LongDescription { get; set; }

        public int SectionMasterId { get; set; }
        public IList<StoredFiles> StoredFiles { get; set; }
        public List<Comment> comments { get; set; }
        public string? YoutubeVideoLink { get; set; }
        public int TotalViews { get; set; }


    }
}

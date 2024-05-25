﻿namespace TechNewsUI.Models
{
    public class MagazineListing
    {
        public int SectionMasterId { get; set; }
        public string Title { get; set; }
        public Byte[] Image { get; set; }
        public string ShortDescription { get; set; }
        public int TotalViews { get; set; }
        public int? NumberOfPages { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

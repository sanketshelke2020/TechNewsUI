namespace TechNewsUI.Models
{
    public class MagazineDetail
    {
        public int SectionMasterId { get; set; }
        public string Title { get; set; }
        public Byte[] Image { get; set; }
        public string ShortDescription { get; set; }
        public int TotalViews { get; set; }
        public string KeyWords { get; set; }
        public string CategoryType { get; set; }
        public int MagazineId { get; set; }
        public int? NumberOfPages { get; set; }
        public string? LongDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<StoredFiles> StoredFiles { get; set; }
        public List<Comment> comments { get; set; }
    }
}

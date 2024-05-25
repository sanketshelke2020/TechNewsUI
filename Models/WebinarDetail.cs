namespace TechNewsUI.Models
{
    public class WebinarDetail
    {
        public int WebinarId { get; set; }
        public string Topic { get; set; }
        public string SpeakerName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? TotalSeat { get; set; }
        public string LongDescription { get; set; }
        public string? YoutubeVideoLink { get; set; }
        public int SectionMasterId { get; set; }
        //public virtual SectionMaster SectionMaster { get; set; }

        //public ICollection<WebinarHolder> WebinarHolders { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }
        public string Title { get; set; }
        public Byte[] Image { get; set; }

        public int TotalViews { get; set; }

        public string ShortDescription { get; set; }
        public List<Comment> comments { get; set; }
        public List<WebinarHolder> webinarHolders { get; set; }


    }
}

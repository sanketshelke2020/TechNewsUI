namespace TechNewsUI.Models
{
    public class EventScheduleDetails
    {
        public int SectionMasterId { get; set; }
        public int EventScheduleId { get; set; }
        public byte[] Image { get; set; }
        public string EventTopic { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string SpeakerName { get; set; }
        public int TotalViews { get; set; }
        public string KeyWords { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? YoutubeVideoLink { get; set; }
        public IList<Comment>? Comments { get; set; }
        public IList<StoredFile>? StoredFiles { get; set; }
        //no. of view, comments
    }

    //public ICollection<Comment>? Comments { get; set; }
}

namespace TechNewsUI.Models
{
    public class EventSchedule
    {
        public string Title { get; set; }
        public Byte[] Image { get; set; }
        public string Summary { get; set; }
        public string Video { get; set; }
        public DateTime EventDateAndTime { get; set; }
        public string EventTopics { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string SpeakerName { get; set; }
        public int SectionMasterId { get; set; }
    }
}

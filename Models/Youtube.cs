namespace TechNewsUI.Models
{
    public class Youtube
    {
        public int YouTubeId { get; set; }
        public string LongDescription { get; set; }
        //public int SectionMasterId { get; set; }
        //public virtual SectionMaster SectionMaster { get; set; }
        public virtual CreateSectionMasterCommand SectionMaster { get; set; }

    }
}

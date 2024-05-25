namespace TechNewsUI.Models
{
    public class Comment
    {
        public int CommentId { get; set; }

        public string CommentDescription { get; set; }
        public string Email { get; set; }

        public int SectionMasterId { get; set; }
        //public virtual SectionMaster SectionMaster { get; set; }
    }
}

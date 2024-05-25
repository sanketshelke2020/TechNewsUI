namespace TechNewsUI.Models
{
    public class ArticleCategory
    {
        public int ArticleCategoryId { get; set; }
        public string ArticleCategoryName { get; set; }
        public virtual Article Articles { get; set; }
    }
}

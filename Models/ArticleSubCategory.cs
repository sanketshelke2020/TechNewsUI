namespace TechNewsUI.Models
{
    public class ArticleSubCategory
    {
        public int ArticleSubCategoryId { get; set; }
        public string ArticleSubCategoryName { get; set; }
        public virtual Article Articles { get; set; }

    }
}

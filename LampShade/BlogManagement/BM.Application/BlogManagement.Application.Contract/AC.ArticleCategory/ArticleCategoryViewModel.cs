namespace BlogManagement.Application.Contract.AC.ArticleCategory
{
    public class ArticleCategoryViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public int ShowOrder { get; set; }
        public string CreationDate { get; set; }
    }
}
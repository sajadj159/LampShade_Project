using _0_Framework.Domain;

namespace BlogManagement.Domain.ArticleCategoryAgg
{
    public class ArticleCategory : EntityBase
    {
        public string Name { get; private set; }
        public string PictureUrl { get; private set; }
        public string Description { get; private set; }
        public int ShowOrder { get; private set; }
        public string Slug { get; private set; }
        public string Keywords { get; private set; }
        public string MetaDescription { get; private set; }
        public string CanonicalAddress { get; private set; }

        protected ArticleCategory()
        {
        }

        public ArticleCategory(string name, string pictureUrl, string description, int showOrder, string slug, string keywords, string metaDescription, string canonicalAddress)
        {
            Name = name;
            PictureUrl = pictureUrl;
            Description = description;
            ShowOrder = showOrder;
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;
            CanonicalAddress = canonicalAddress;
        }

        public void Edit(string name, string pictureUrl, string description, int showOrder, string slug, string keywords, string metaDescription, string canonicalAddress)
        {
            Name = name;
            if (!string.IsNullOrWhiteSpace(pictureUrl))
            {
                PictureUrl = pictureUrl;
            }
            Description = description;
            ShowOrder = showOrder;
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;
            CanonicalAddress = canonicalAddress;
        }

    }
}
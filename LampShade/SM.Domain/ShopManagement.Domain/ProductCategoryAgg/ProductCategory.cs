using _0_Framework.Domain;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public class ProductCategory : EntityBase
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string PictureUrl { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
    }
}
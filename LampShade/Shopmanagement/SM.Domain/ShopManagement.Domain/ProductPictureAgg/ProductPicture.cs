using _0_Framework.Domain;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Domain.ProductPictureAgg
{
    public class ProductPicture: EntityBase
    {
        public string PictureUrl { get; private set; }
        public string PictureTitle { get; private set; }
        public string PictureAlt { get; private set; }
        public bool IsRemoved { get; private set; }

        public long ProductId { get; private set; }
        public Product Product { get; private set; }
        public ProductPicture()
        {
        }

        public ProductPicture(long productId, string pictureUrl, string pictureTitle, string pictureAlt)
        {
            ProductId = productId;
            PictureUrl = pictureUrl;
            PictureTitle = pictureTitle;
            PictureAlt = pictureAlt;
            IsRemoved = false;
        }
        public void Edit(long productId, string pictureUrl, string pictureTitle, string pictureAlt)
        {
            ProductId = productId;
            PictureUrl = pictureUrl;
            PictureTitle = pictureTitle;
            PictureAlt = pictureAlt;
        }

        public void Remove()
        {
            IsRemoved = true;
        }

        public void Restore()
        {
            IsRemoved = false;
        }
    }
}
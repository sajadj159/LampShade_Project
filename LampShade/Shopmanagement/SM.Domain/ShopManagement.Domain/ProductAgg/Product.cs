using System.Collections.Generic;
using _0_Framework.Domain;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Domain.ProductAgg
{
    public class Product : EntityBase
    {
        public string Name { get; private set; }
        public string Code { get; private set; }
        public string ShortDescription { get; private set; }
        public string Description { get; private set; }
        public string PictureUrl { get; private set; }
        public string PictureTitle { get; private set; }
        public string PictureAlt { get; private set; }
        public string Slug { get; private set; }
        public string Keywords { get; private set; }
        public string MetaDescription { get; private set; }


        public long CategoryId { get; private set; }
        public ProductCategory Category { get; private set; }
        public List<ProductPicture> ProductPictures { get; private set; }

        public Product()
        {
            ProductPictures = new List<ProductPicture>();
        }
        public Product(string name, string code, string shortDescription,
            string description, string pictureUrl, string pictureTitle, string pictureAlt,
            string slug, string keywords, string metaDescription, long categoryId)
        {
            Name = name;
            Code = code;
            ShortDescription = shortDescription;
            Description = description;
            PictureUrl = pictureUrl;
            PictureTitle = pictureTitle;
            PictureAlt = pictureAlt;
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;
            CategoryId = categoryId;
            ProductPictures = new List<ProductPicture>();
        }
        public void AddPicture(string pictureUrl, string pictureTitle, string pictureAlt)
        {
            ProductPictures ??= new List<ProductPicture>();
            ProductPictures.Add(new ProductPicture(0, pictureUrl, pictureTitle, pictureAlt));
        }
        public void ClearPicture()
        {
            PictureUrl = string.Empty;
        }
        public void Edit(string name, string code, string shortDescription,
            string description, string pictureUrl, string pictureTitle, string pictureAlt,
            string slug, string keywords, string metaDescription, long categoryId)
        {
            Name = name;
            Code = code;
            ShortDescription = shortDescription;
            Description = description;
            if (!string.IsNullOrWhiteSpace(pictureUrl))
            {
                PictureUrl = pictureUrl;
            }
            PictureTitle = pictureTitle;
            PictureAlt = pictureAlt;
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;
            CategoryId = categoryId;
        }
    }
}
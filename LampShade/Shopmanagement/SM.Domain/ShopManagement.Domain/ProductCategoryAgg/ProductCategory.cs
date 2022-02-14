using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using _0_Framework.Domain;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public class ProductCategory : EntityBase
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string PictureUrl { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Keywords { get; private set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string MetaDescription { get; private set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Slug { get; private set; }

        public ProductCategory(string name, string description, string pictureUrl, string pictureAlt, string pictureTitle, string keywords, string metaDescription, string slug)
        {
            Name = name;
            Description = description;
            PictureUrl = pictureUrl;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Keywords = keywords;
            MetaDescription = metaDescription;
            Slug = slug;
        }

        public void Edit(string name, string description, string pictureUrl, string pictureAlt, string pictureTitle,
            string keywords, string metaDescription, string slug)
        {
            Name = name;
            Description = description;
            PictureUrl = pictureUrl;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Keywords = keywords;
            MetaDescription = metaDescription;
            Slug = slug;
        }
    }
}
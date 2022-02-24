using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using ShopManagement.Application.Contract.A.Product;

namespace ShopManagement.Application.Contract.A.ProductPicture
{
    public class CreateProductPicture
    {
        [Range(1,10000,ErrorMessage = ValidationMessages.IsRequired)]
        public long ProductId { get; set; }

        [MaxFileSIze(1*1024*1024,ErrorMessage = ValidationMessages.MaxFileSize)]
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public IFormFile PictureUrl { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PictureAlt { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PictureTitle { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
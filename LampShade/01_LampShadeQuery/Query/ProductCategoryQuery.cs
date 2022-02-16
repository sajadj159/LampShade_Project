using System.Collections.Generic;
using System.Linq;
using _01_LampShadeQuery.Contract.ProductCategory;
using ShopManagement.Infrastructure.EFCore;

namespace _01_LampShadeQuery.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly ShopContext _context;

        public ProductCategoryQuery(ShopContext context)
        {
            _context = context;
        }

        public List<ProductCategoryQueryModel> GetProductCategoryQueries()
        {
            return _context.ProductCategories.Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    PictureTitle = x.PictureTitle,
                    PictureAlt = x.PictureAlt,
                    PictureUrl = x.PictureUrl,
                    Slug = x.Slug,

                }).ToList();
        }
    }
}
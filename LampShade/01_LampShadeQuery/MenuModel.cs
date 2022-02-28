using System.Collections.Generic;
using _01_LampShadeQuery.Contract.ArticleCategory;
using _01_LampShadeQuery.Contract.ProductCategory;

namespace _01_LampShadeQuery
{
    public class MenuModel
    {
        public List<ArticleCategoryQueryModel> ArticleCategories { get; set; }
        public List<ProductCategoryQueryModel> ProductCategories { get; set; }
    }
}
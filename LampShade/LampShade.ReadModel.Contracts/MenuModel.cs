using System.Collections.Generic;
using LampShade.ReadModel.Contracts.ArticleCategory;
using LampShade.ReadModel.Contracts.ProductCategory;

namespace LampShade.ReadModel.Contracts
{
    public class MenuModel
    {
        public List<ArticleCategoryQueryModel> ArticleCategories { get; set; }
        public List<ProductCategoryQueryModel> ProductCategories { get; set; }
    }
}
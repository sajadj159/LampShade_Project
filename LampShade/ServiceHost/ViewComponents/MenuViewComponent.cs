using _01_LampShadeQuery;
using _01_LampShadeQuery.Contract.ArticleCategory;
using _01_LampShadeQuery.Contract.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IProductCategoryQuery _productCategory;
        private readonly IArticleCategoryQuery _articleCategory;
        public MenuViewComponent(IProductCategoryQuery productCategory, IArticleCategoryQuery articleCategory)
        {
            _productCategory = productCategory;
            _articleCategory = articleCategory;
        }

        public IViewComponentResult Invoke()
        {
            var result = new MenuModel
            {
                ArticleCategories = _articleCategory.GetArticleCategories(),
                ProductCategories = _productCategory.GetProductCategoryQueries()
            };
            return View(result);
        }
    }
}
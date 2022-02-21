using _01_LampShadeQuery.Contract.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IProductCategoryQuery _productCategory;

        public MenuViewComponent(IProductCategoryQuery productCategory)
        {
            _productCategory = productCategory;
        }

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
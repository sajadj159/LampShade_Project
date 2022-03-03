using _0_Framework.Repository;
using _01_LampShadeQuery.Contract.Product;
using _01_LampShadeQuery.Contract.ProductCategory;
using _01_LampShadeQuery.Contract.Slide;
using _01_LampShadeQuery.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Application.Contract.A.Product;
using ShopManagement.Application.Contract.A.ProductPicture;
using ShopManagement.Application.Contract.A.Slide;
using ShopManagement.Application.Contract.ProductCategory;
using ShopManagement.Application.Product;
using ShopManagement.Application.ProductCategory;
using ShopManagement.Application.ProductPicture;
using ShopManagement.Application.Slide;
using ShopManagement.Configuration.Permissions;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Domain.SlideAgg;
using ShopManagement.Infrastructure.EFCore;
using ShopManagement.Infrastructure.EFCore.Repository;

namespace ShopManagement.Configuration
{
    public class ShopManagementBootstrapper
    {
        public static void Configure(IServiceCollection service, string connectionString)
        {
            service.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();
            service.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();

            service.AddTransient<IProductApplication, ProductApplication>();
            service.AddTransient<IProductRepository, ProductRepository>();

            service.AddTransient<IProductPictureApplication, ProductPictureApplication>();
            service.AddTransient<IProductPictureRepository, ProductPictureRepository>();

            service.AddTransient<ISlideApplication, SlideApplication>();
            service.AddTransient<ISlideRepository, SlideRepository>();

            service.AddTransient<ISlideQuery, SlideQuery>();
            service.AddTransient<IProductCategoryQuery, ProductCategoryQuery>();
            service.AddTransient<IProductQuery, ProductQuery>();

            service.AddTransient<IPermissionExposer, ShopPermissionsExposer>();

            service.AddDbContext<ShopContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
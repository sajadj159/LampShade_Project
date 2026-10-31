using System.Reflection;
using LampShade.BuildingBlocks.Application;
using LampShade.BuildingBlocks.Infrastructure.Modules;
using LampShade.BuildingBlocks.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Application.Features.Products.Commands.CreateProduct;
using ShopManagement.Application.Orders;
using ShopManagement.Application.ReadModel.Admin;
using ShopManagement.Application.ReadModel.Order;
using ShopManagement.Application.ReadModel.Product;
using ShopManagement.Application.ReadModel.ProductCategory;
using ShopManagement.Application.ReadModel.Slide;
using ShopManagement.Configuration.Permissions;
using ShopManagement.Contracts;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Domain.SlideAgg;
using ShopManagement.Infrastructure.EFCore;
using ShopManagement.Infrastructure.EFCore.Repository;
using ShopManagement.Infrastructure.ReadModel;

namespace ShopManagement.Infrastructure;

public sealed class ShopModule : IModule
{
    public string Name => "Shop";

    public IEnumerable<Assembly> HandlerAssemblies => [typeof(CreateProductCommandHandler).Assembly, typeof(ShopModule).Assembly];

    public void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddModuleDbContext<ShopContext>(configuration, ShopContext.Schema);
        services.AddScoped<IShopOutbox, ShopOutbox>();

        services.AddTransient<ProductCategoryRepository>();
        services.AddTransient<IProductCategoryRepository>(sp => sp.GetRequiredService<ProductCategoryRepository>());
        services.AddTransient<IProductCategoryReadRepository>(sp => sp.GetRequiredService<ProductCategoryRepository>());

        services.AddTransient<ProductRepository>();
        services.AddTransient<IProductRepository>(sp => sp.GetRequiredService<ProductRepository>());
        services.AddTransient<IProductReadRepository>(sp => sp.GetRequiredService<ProductRepository>());

        services.AddTransient<ProductPictureRepository>();
        services.AddTransient<IProductPictureRepository>(sp => sp.GetRequiredService<ProductPictureRepository>());
        services.AddTransient<IProductPictureReadRepository>(sp => sp.GetRequiredService<ProductPictureRepository>());

        services.AddTransient<SlideRepository>();
        services.AddTransient<ISlideRepository>(sp => sp.GetRequiredService<SlideRepository>());
        services.AddTransient<ISlideReadRepository>(sp => sp.GetRequiredService<SlideRepository>());

        services.AddTransient<OrderRepository>();
        services.AddTransient<IOrderRepository>(sp => sp.GetRequiredService<OrderRepository>());
        services.AddTransient<IOrderReadRepository>(sp => sp.GetRequiredService<OrderRepository>());

        services.AddTransient<ISlideQuery, SlideQuery>();
        services.AddTransient<IProductCategoryQuery, ProductCategoryQuery>();
        services.AddTransient<IProductQuery, ProductQuery>();
        services.AddTransient<IOrderQuery, OrderQuery>();

        services.AddTransient<CartCalculator>();
        services.AddTransient<OrderConfirmation>();
        services.AddTransient<OrderAccessPolicy>();

        services.AddTransient<IShopModuleApi, ShopModuleApi>();
        services.AddTransient<IPermissionExposer, ShopPermissionsExposer>();
    }

    public Task InitializeAsync(IServiceProvider services, CancellationToken cancellationToken) =>
        services.GetRequiredService<ShopContext>().Database.MigrateAsync(cancellationToken);
}

internal sealed class ShopOutbox(ShopContext context) : Outbox<ShopContext>(context), IShopOutbox;

using System.Reflection;
using DiscountManagement.Application.Features.CustomerDiscounts.Commands.DefineCustomerDiscount;
using DiscountManagement.Application.ReadModel.Discounts;
using DiscountManagement.Contracts;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using DiscountManagement.Domain.CustomerDiscountAgg;
using DiscountManagement.Infrastructure.EFCore;
using DiscountManagement.Infrastructure.EFCore.Repository;
using DiscountManagement.Infrastructure.ReadModel;
using LampShade.BuildingBlocks.Infrastructure.Modules;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DiscountManagement.Infrastructure;

public sealed class DiscountModule : IModule
{
    public string Name => "Discount";

    public IEnumerable<Assembly> HandlerAssemblies => [typeof(DefineCustomerDiscountCommandHandler).Assembly, typeof(DiscountModule).Assembly];

    public void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddModuleDbContext<DiscountContext>(configuration, DiscountContext.Schema);
        services.AddTransient<ICustomerDiscountRepository, CustomerDiscountRepository>();
        services.AddTransient<IColleagueDiscountRepository, ColleagueDiscountRepository>();
        services.AddTransient<IDiscountQuery, DiscountQuery>();
        services.AddTransient<IDiscountModuleApi, DiscountModuleApi>();
    }

    public Task InitializeAsync(IServiceProvider services, CancellationToken cancellationToken) =>
        services.GetRequiredService<DiscountContext>().Database.MigrateAsync(cancellationToken);
}

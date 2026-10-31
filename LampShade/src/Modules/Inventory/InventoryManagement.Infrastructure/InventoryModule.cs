using System.Reflection;
using InventoryManagement.Application.Features.Inventories.Commands.CreateInventory;
using InventoryManagement.Application.ReadModel.Inventory;
using InventoryManagement.Configuration.Permissions;
using InventoryManagement.Contracts;
using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore.Repository;
using InventoryManagement.Infrastructure.ReadModel;
using LampShade.BuildingBlocks.Application;
using LampShade.BuildingBlocks.Infrastructure.Modules;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagement.Infrastructure;

public sealed class InventoryModule : IModule
{
    public string Name => "Inventory";

    public IEnumerable<Assembly> HandlerAssemblies => [typeof(CreateInventoryCommandHandler).Assembly, typeof(InventoryModule).Assembly];

    public void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddModuleDbContext<InventoryContext>(configuration, InventoryContext.Schema);
        services.AddTransient<IInventoryRepository, InventoryRepository>();
        services.AddTransient<IInventoryQuery, InventoryQuery>();
        services.AddTransient<IInventoryModuleApi, InventoryModuleApi>();
        services.AddTransient<IPermissionExposer, InventoryPermissionExposer>();
    }

    public Task InitializeAsync(IServiceProvider services, CancellationToken cancellationToken) =>
        services.GetRequiredService<InventoryContext>().Database.MigrateAsync(cancellationToken);
}

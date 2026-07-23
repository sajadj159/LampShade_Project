using _0_Framework.Repository;
using LampShade.ReadModel.Contracts.Inventory;
using LampShade.ReadModel.Application.Query;
using InventoryManagement.Configuration.Permissions;
using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagement.Configuration
{
    public class InventoryManagementBootstrapper
    {
        public static void Configure(IServiceCollection service, string connectionString)
        {
            service.AddTransient<IInventoryRepository,InventoryRepository>();
            service.AddTransient<IPermissionExposer, InventoryPermissionExposer>();
            service.AddTransient<IInventoryQuery, InventoryQuery>();

            service.AddDbContext<InventoryContext>(x => x.UseNpgsql(connectionString));
        }
    }
}



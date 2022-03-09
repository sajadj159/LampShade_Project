using _0_Framework.Repository;
using _01_LampShadeQuery.Contract.Inventory;
using _01_LampShadeQuery.Query;
using InventoryManagement.Application;
using InventoryManagement.Application.Contract.AC.Inventory;
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
            service.AddTransient<IInventoryApplication, InventoryApplication>();

            service.AddTransient<IPermissionExposer, InventoryPermissionExposer>();
            service.AddTransient<IInventoryQuery, InventoryQuery>();

            service.AddDbContext<InventoryContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
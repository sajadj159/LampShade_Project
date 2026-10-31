using _0_Framework.Repository;
using LampShade.ReadModel.Contracts.Inventory;
using LampShade.ReadModel.Application.Query;
using InventoryManagement.Configuration.Permissions;
using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace InventoryManagement.Configuration
{
    public class InventoryManagementBootstrapper
    {
        public static void Configure(IServiceCollection service, string connectionString)
        {
            service.AddTransient<IInventoryRepository,InventoryRepository>();
            service.AddTransient<IPermissionExposer, InventoryPermissionExposer>();
            service.AddTransient<IInventoryQuery, InventoryQuery>();

            service.AddDbContext<InventoryContext>((sp, options) => options.UseNpgsql(sp.GetRequiredService<NpgsqlConnection>()));
            service.AddScoped<_0_Framework.Domain.IDbContext>(sp => sp.GetRequiredService<InventoryContext>());
        }
    }
}



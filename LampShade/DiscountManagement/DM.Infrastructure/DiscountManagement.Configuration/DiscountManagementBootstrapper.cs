using DiscountManagement.Application.A.CustomerDiscount;
using DiscountManagement.Application.Contract.AC.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;
using DiscountManagement.Infrastructure.EFCore;
using DiscountManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DiscountManagement.Configuration
{
    public class DiscountManagementBootstrapper
    {
        public static void Configure(IServiceCollection service, string connectionString)
        {
            service.AddTransient<ICustomerDiscountApplication, CustomerDiscountApplication>();
            service.AddTransient<ICustomerDiscountRepository, CustomerDiscountRepository>();

            service.AddDbContext<DiscountContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
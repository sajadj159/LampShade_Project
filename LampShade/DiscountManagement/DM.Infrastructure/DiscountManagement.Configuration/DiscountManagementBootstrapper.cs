using DiscountManagement.Domain.ColleagueDiscountAgg;
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
        {            service.AddTransient<ICustomerDiscountRepository, CustomerDiscountRepository>();            service.AddTransient<IColleagueDiscountRepository, ColleagueDiscountRepository>();

            service.AddDbContext<DiscountContext>(x => x.UseNpgsql(connectionString));
        }
    }
}



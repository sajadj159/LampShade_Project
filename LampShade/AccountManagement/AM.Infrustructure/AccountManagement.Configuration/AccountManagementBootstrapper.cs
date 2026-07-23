using LampShade.ReadModel.Contracts.Account;
using LampShade.ReadModel.Application.Query;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;
using AccountManagement.Infrastructure.EFCore;
using AccountManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AccountManagement.Configuration
{
    public class AccountManagementBootstrapper
    {
        public static void Configure(IServiceCollection service, string connectionString)
        {
            service.AddTransient<IAccountRepository, AccountRepository>();
            service.AddTransient<IRoleRepository, RoleRepository>();
            service.AddTransient<IAccountQuery, AccountQuery>();
            service.AddDbContext<AccountContext>(x => x.UseNpgsql(connectionString));
        }
    }
}




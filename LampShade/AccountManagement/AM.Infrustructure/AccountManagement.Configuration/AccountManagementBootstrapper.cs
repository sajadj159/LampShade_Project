using _01_LampShadeQuery.Contract.Account;
using _01_LampShadeQuery.Query;
using AccountManagement.Application.A.Account;
using AccountManagement.Application.A.Role;
using AccountManagement.Application.Contracts.AC.Account;
using AccountManagement.Application.Contracts.AC.Role;
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
            service.AddTransient<IAccountApplication, AccountApplication>();

            service.AddTransient<IRoleRepository, RoleRepository>();
            service.AddTransient<IRoleApplication, RoleApplication>();

            service.AddTransient<IAccountQuery, AccountQuery>();
            service.AddDbContext<AccountContext>(x => x.UseSqlServer(connectionString));
        }
    }
}

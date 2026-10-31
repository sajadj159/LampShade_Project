using System.Reflection;
using AccountManagement.Application.Features.Accounts.Commands.Register;
using AccountManagement.Application.ReadModel.Account;
using AccountManagement.Contracts;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;
using AccountManagement.Infrastructure.EFCore;
using AccountManagement.Infrastructure.EFCore.Repository;
using AccountManagement.Infrastructure.ReadModel;
using AccountManagement.Infrastructure.Seed;
using LampShade.BuildingBlocks.Infrastructure.Modules;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AccountManagement.Infrastructure;

public sealed class AccountModule : IModule
{
    public string Name => "Account";

    public IEnumerable<Assembly> HandlerAssemblies => [typeof(RegisterCommandHandler).Assembly, typeof(AccountModule).Assembly];

    public void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddModuleDbContext<AccountContext>(configuration, AccountContext.Schema);
        services.AddTransient<IAccountRepository, AccountRepository>();
        services.AddTransient<IRoleRepository, RoleRepository>();
        services.AddTransient<IAccountQuery, AccountQuery>();
        services.AddTransient<IAccountModuleApi, AccountModuleApi>();
    }

    public async Task InitializeAsync(IServiceProvider services, CancellationToken cancellationToken)
    {
        var context = services.GetRequiredService<AccountContext>();
        await context.Database.MigrateAsync(cancellationToken);
        await context.SeedDefaultRolesAsync(cancellationToken);
        await context.SeedBootstrapAdminAsync(services.GetRequiredService<IConfiguration>(),
            services.GetRequiredService<LampShade.BuildingBlocks.Application.IPasswordHasher>(), cancellationToken);
    }
}

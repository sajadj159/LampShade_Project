using AccountManagement.Application.Contracts.Commands.Accounts.Register;
using AccountManagement.Infrastructure.EFCore;
using AwesomeAssertions;
using LampShade.BuildingBlocks.Application;
using LampShade.BuildingBlocks.Infrastructure;
using LampShade.IntegrationTests.Fixtures;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace LampShade.IntegrationTests.Account;

[Collection(nameof(ModularMonolithCollection))]
public class AccountSecurityTests(ModularMonolithFixture app)
{
    [Fact]
    public async Task Register_ByAnonymousVisitor_IgnoresRequestedRole()
    {
        app.Auth.SignOutUser();
        var userName = ModularMonolithFixture.Unique("visitor");

        var result = await app.SendAsync(new RegisterCommand { UserName = userName, FullName = "Visitor", Password = "secret", Mobile = "0912" + Random.Shared.Next(1000000, 9999999), RoleId = long.Parse(Roles.Administrator) });

        result.IsSucceeded.Should().BeTrue();
        (await RoleOfAsync(userName)).Should().Be(long.Parse(Roles.SystemUser));
    }

    [Fact]
    public async Task Register_ByAdministrator_KeepsChosenRole()
    {
        app.Auth.SignInAs(1, Roles.Administrator);
        var userName = ModularMonolithFixture.Unique("staff");

        var result = await app.SendAsync(new RegisterCommand { UserName = userName, FullName = "Staff", Password = "secret", Mobile = "0913" + Random.Shared.Next(1000000, 9999999), RoleId = long.Parse(Roles.Administrator) });

        result.IsSucceeded.Should().BeTrue();
        (await RoleOfAsync(userName)).Should().Be(long.Parse(Roles.Administrator));
    }

    [Fact]
    public async Task Modules_EachOwnTheirSchema_WithTheirOwnMigrationHistory()
    {
        var schemas = await app.WithAsync<AccountContext, List<string>>(c => c.Database
            .SqlQueryRaw<string>("""SELECT table_schema AS "Value" FROM information_schema.tables WHERE table_name = '__EFMigrationsHistory'""")
            .ToListAsync(TestContext.Current.CancellationToken));

        schemas.Should().BeEquivalentTo(["account", "blog", "comment", "discount", "inventory", "shop"]);
    }

    private Task<long> RoleOfAsync(string userName) =>
        app.WithAsync<AccountContext, long>(c => c.Accounts.Where(x => x.UserName == userName).Select(x => x.RoleId).SingleAsync(TestContext.Current.CancellationToken));
}

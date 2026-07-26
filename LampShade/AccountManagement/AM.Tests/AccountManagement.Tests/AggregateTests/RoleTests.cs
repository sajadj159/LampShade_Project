using AwesomeAssertions;
using _0_Framework.Domain;
using AccountManagement.Domain.RoleAgg;
using AccountManagement.Domain.RoleAgg.Contracts;
using Xunit;

namespace AccountManagement.Tests.AggregateTests;

public class RoleTests
{
    [Fact] public void Constructor_InheritsFromEntityBase() => CreateRole().Should().BeAssignableTo<EntityBase>();
    [Fact] public void Constructor_SetsCreationDate() { var beforeCreation = DateTime.UtcNow; var role = CreateRole(); role.CreationDate.Should().BeOnOrAfter(beforeCreation); role.CreationDate.Should().BeOnOrBefore(DateTime.UtcNow); }
    [Fact] public void Constructor_SetsDefaultId() => CreateRole().Id.Should().Be(0);
    [Fact] public void Constructor_SetsName() => CreateRole().Name.Should().Be("Administrator");
    [Fact] public void Constructor_InitializesPermissionsFromEmptyInput() => CreateRole().Permissions.Should().BeEmpty();
    [Fact] public void Edit_UpdatesName() { var role = CreateRole(); role.Edit("Editor", []); role.Name.Should().Be("Editor"); }
    [Fact] public void Edit_ReplacesPermissionsFromEmptyInput() { var role = CreateRole(); role.Edit("Editor", []); role.Permissions.Should().BeEmpty(); }
    private static Role CreateRole() => new("Administrator", new List<CreatePermissionDto>());
}
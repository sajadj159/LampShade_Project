using AwesomeAssertions;
using AccountManagement.Domain.RoleAgg;
using Xunit;

namespace AccountManagement.Tests.AggregateTests;

public class PermissionTests
{
    [Fact] public void Constructor_SetsDefaultId() => CreatePermission().Id.Should().Be(0);
    [Fact] public void Constructor_SetsName() => CreatePermission().Name.Should().Be("Manage products");
    [Fact] public void Constructor_SetsCode() => CreatePermission().Code.Should().Be(100);
    [Fact] public void Constructor_SetsRoleId() => CreatePermission().RoleId.Should().Be(3);
    [Fact] public void Constructor_LeavesRoleUninitialized() => CreatePermission().Role.Should().BeNull();
    private static Permission CreatePermission() => new("Manage products", 100, 3);
}
using AwesomeAssertions;
using _0_Framework.Domain;
using AccountManagement.Domain.AccountAgg;
using Xunit;

namespace AccountManagement.Tests.AggregateTests;

public class AccountTests
{
    [Fact] public void Constructor_InheritsFromEntityBase() => CreateAccount().Should().BeAssignableTo<EntityBase>();
    [Fact] public void Constructor_SetsCreationDate() { var beforeCreation = DateTime.UtcNow; var account = CreateAccount(); account.CreationDate.Should().BeOnOrAfter(beforeCreation); account.CreationDate.Should().BeOnOrBefore(DateTime.UtcNow); }
    [Fact] public void Constructor_SetsDefaultId() => CreateAccount().Id.Should().Be(0);
    [Fact] public void Constructor_SetsUserName() => CreateAccount().UserName.Should().Be("jane");
    [Fact] public void Constructor_SetsFullName() => CreateAccount().FullName.Should().Be("Jane Doe");
    [Fact] public void Constructor_SetsPassword() => CreateAccount().Password.Should().Be("hashed-password");
    [Fact] public void Constructor_SetsMobile() => CreateAccount().Mobile.Should().Be("09120000000");
    [Fact] public void Constructor_SetsSpecifiedRoleId() => CreateAccount().RoleId.Should().Be(3);
    [Fact] public void Constructor_UsesCustomerRoleWhenRoleIdIsZero() => CreateAccount(roleId: 0).RoleId.Should().Be(2);
    [Fact] public void Constructor_SetsProfilePhoto() => CreateAccount().ProfilePhoto.Should().Be("profile.jpg");
    [Fact] public void Constructor_SetsAddress() => CreateAccount().Address.Should().Be("Tehran");
    [Fact] public void Constructor_SetsPostalCode() => CreateAccount().PostalCode.Should().Be("1234567890");
    [Fact] public void Constructor_LeavesRoleUninitialized() => CreateAccount().Role.Should().BeNull();
    [Fact] public void Edit_UpdatesUserName() { var account = CreateAccount(); account.Edit("updated-jane", "Updated Jane", "09121111111", 4, "updated.jpg", "Updated Tehran", "0987654321"); account.UserName.Should().Be("updated-jane"); }
    [Fact] public void Edit_UpdatesFullName() { var account = CreateAccount(); account.Edit("updated-jane", "Updated Jane", "09121111111", 4, "updated.jpg", "Updated Tehran", "0987654321"); account.FullName.Should().Be("Updated Jane"); }
    [Fact] public void Edit_UpdatesMobile() { var account = CreateAccount(); account.Edit("updated-jane", "Updated Jane", "09121111111", 4, "updated.jpg", "Updated Tehran", "0987654321"); account.Mobile.Should().Be("09121111111"); }
    [Fact] public void Edit_UpdatesRoleId() { var account = CreateAccount(); account.Edit("updated-jane", "Updated Jane", "09121111111", 4, "updated.jpg", "Updated Tehran", "0987654321"); account.RoleId.Should().Be(4); }
    [Fact] public void Edit_WithProfilePhoto_UpdatesProfilePhoto() { var account = CreateAccount(); account.Edit("updated-jane", "Updated Jane", "09121111111", 4, "updated.jpg", "Updated Tehran", "0987654321"); account.ProfilePhoto.Should().Be("updated.jpg"); }
    [Fact] public void Edit_WithWhitespaceProfilePhoto_PreservesCurrentProfilePhoto() { var account = CreateAccount(); account.Edit("updated-jane", "Updated Jane", "09121111111", 4, " ", "Updated Tehran", "0987654321"); account.ProfilePhoto.Should().Be("profile.jpg"); }
    [Fact] public void Edit_UpdatesAddress() { var account = CreateAccount(); account.Edit("updated-jane", "Updated Jane", "09121111111", 4, "updated.jpg", "Updated Tehran", "0987654321"); account.Address.Should().Be("Updated Tehran"); }
    [Fact] public void Edit_UpdatesPostalCode() { var account = CreateAccount(); account.Edit("updated-jane", "Updated Jane", "09121111111", 4, "updated.jpg", "Updated Tehran", "0987654321"); account.PostalCode.Should().Be("0987654321"); }
    [Fact] public void ChangePassword_UpdatesPassword() { var account = CreateAccount(); account.ChangePassword("new-hashed-password"); account.Password.Should().Be("new-hashed-password"); }
    [Fact] public void MakeAddress_UpdatesAddress() { var account = CreateAccount(); account.MakeAddress("New address", "1111111111"); account.Address.Should().Be("New address"); }
    [Fact] public void MakeAddress_UpdatesPostalCode() { var account = CreateAccount(); account.MakeAddress("New address", "1111111111"); account.PostalCode.Should().Be("1111111111"); }
    private static Account CreateAccount(long roleId = 3) => new("jane", "Jane Doe", "hashed-password", "09120000000", roleId, "profile.jpg", "Tehran", "1234567890");
}
using _0_Framework.Application;

namespace InventoryManagement.IntegrationTests.Fixtures;

public sealed class TestAuthHelper(long accountId = 1) : IAuthHelper
{
    public bool IsAuthenticated() => true;
    public void Signin(AuthViewModel account) { }
    public void SignOut() { }
    public string CurrentAccountRole() => "Customer";
    public AuthViewModel CurrentAccountInfo() => new();
    public List<int> GetPermissions() => [];
    public long CurrentAccountId() => accountId;
    public string CurrentAccountMobile() => "09120000000";
}
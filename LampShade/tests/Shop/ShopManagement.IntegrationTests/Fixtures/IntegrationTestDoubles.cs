using _0_Framework.Application;
using _0_Framework.Application.SMS;
using Microsoft.AspNetCore.Http;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.Services;

namespace ShopManagement.IntegrationTests.Fixtures;

public sealed class TestFileUploader : IFIleUploader
{
    public string Upload(IFormFile file, string path) => $"{path}/uploaded-{file.FileName}";
}

public sealed class TestAuthHelper(long accountId = 1, string role = "Customer") : IAuthHelper
{
    public bool IsAuthenticated() => true;
    public void Signin(AuthViewModel account) { }
    public void SignOut() { }
    public string CurrentAccountRole() => role;
    public AuthViewModel CurrentAccountInfo() => new();
    public List<int> GetPermissions() => [];
    public long CurrentAccountId() => accountId;
    public string CurrentAccountMobile() => "09120000000";
}

public sealed class TestSmsService : ISmsService
{
    public List<(string Number, string Message)> Messages { get; } = [];
    public void Send(string number, string message) => Messages.Add((number, message));
}

public sealed class SuccessfulInventoryAcl : IShopInventoryAcl
{
    public Task<bool> ReduceFromInventoryAsync(List<OrderItem> items, CancellationToken cancellationToken = default) => Task.FromResult(true);
}

public sealed class TestAccountAcl : IShopAccountAcl
{
    public Task<(string name, string mobile)> GetAccountByAsync(long id, CancellationToken cancellationToken = default) => Task.FromResult(("Test customer", "09120000000"));
}
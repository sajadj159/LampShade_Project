using LampShade.BuildingBlocks.Application;
using Microsoft.AspNetCore.Http;

namespace LampShade.IntegrationTests.Fixtures;

public sealed class TestFileUploader : IFileUploader
{
    public string Upload(IFormFile file, string path) => file is null || file.Length == 0 ? string.Empty : $"{path}/uploaded-{file.FileName}";
}

/// <summary>Stands in for the cookie user. Tests switch identity with <see cref="SignInAs"/>.</summary>
public sealed class TestAuthHelper : IAuthHelper
{
    private readonly AsyncLocal<(long AccountId, string Role)?> _current = new();

    public void SignInAs(long accountId, string role) => _current.Value = (accountId, role);
    public void SignOutUser() => _current.Value = null;

    public bool IsAuthenticated() => _current.Value is not null;
    public void Signin(AuthViewModel account) => SignInAs(account.Id, account.RoleId.ToString());
    public void SignOut() => SignOutUser();
    public string CurrentAccountRole() => _current.Value?.Role;
    public AuthViewModel CurrentAccountInfo() => new();
    public List<int> GetPermissions() => [];
    public long CurrentAccountId() => _current.Value?.AccountId ?? 0;
    public string CurrentAccountMobile() => string.Empty;
}

public sealed class TestSmsService : ISmsService
{
    private readonly List<(string Number, string Message)> _messages = [];

    public IReadOnlyList<(string Number, string Message)> Messages
    {
        get { lock (_messages) return _messages.ToList(); }
    }

    public void Send(string number, string message)
    {
        lock (_messages) _messages.Add((number, message));
    }
}

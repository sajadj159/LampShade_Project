using _0_Framework.Application;
using AccountManagement.Application.Contracts.Commands.Accounts.Login;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;
using MediatR;

namespace AccountManagement.Application.Features.Accounts.Commands.Login;

public class LoginCommandHandler(IAccountRepository accounts, IRoleRepository roles, IPasswordHasher passwordHasher, IAuthHelper authHelper) : IRequestHandler<LoginCommand, OperationResult>
{
    public Task<OperationResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult();
        var account = accounts.GetBy(request.UserName);
        if (account is null || !passwordHasher.Check(account.Password, request.Password).Verified) return Task.FromResult(operation.Failed(ApplicationMessages.WrongUserPass));
        var role = roles.Get(account.RoleId);
        authHelper.Signin(new AuthViewModel(account.Id, account.UserName, account.FullName, account.Mobile, account.RoleId, role.Permissions.Select(x => x.Code).ToList(), account.ProfilePhoto) { Role = role.Name });
        return Task.FromResult(operation.Succeeded());
    }
}

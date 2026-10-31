using System.Threading;
using System.Threading.Tasks;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.Commands.Accounts.Login;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;
using MediatR;

namespace AccountManagement.Application.Features.Accounts.Commands.Login;

public class LoginCommandHandler(IAccountRepository accounts, IRoleRepository roles, IPasswordHasher passwordHasher, IAuthHelper authHelper) : IRequestHandler<LoginCommand, OperationResult>
{
    public async Task<OperationResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult(); var account = await accounts.GetByAsync(request.UserName, cancellationToken);
        if (account is null || !passwordHasher.Check(account.Password, request.Password).Verified) return operation.Failed(ApplicationMessages.WrongUserPass);
        var role = await roles.GetAsync(account.RoleId, cancellationToken);
        if (role is null) return operation.Failed(ApplicationMessages.WrongUserPass);
        authHelper.Signin(new AuthViewModel(account.Id, account.UserName, account.FullName, account.Mobile, account.RoleId, role.Permissions.Select(x => x.Code).ToList(), account.ProfilePhoto) { Role = role.Name });
        return operation.Succeeded();
    }
}

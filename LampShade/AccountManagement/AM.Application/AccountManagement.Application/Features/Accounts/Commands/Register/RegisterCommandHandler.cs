#nullable enable

using AccountManagement.Application.Contracts.AC.Account;
using MediatR;
using Microsoft.AspNetCore.Http;
using _0_Framework.Application;

using AccountManagement.Application.Contracts.Commands.Accounts.Register;

namespace AccountManagement.Application.Features.Accounts.Commands.Register;



public class RegisterCommandHandler : IRequestHandler<RegisterCommand, OperationResult>
{
    private readonly IAccountApplication _accountApplication;

    public RegisterCommandHandler(IAccountApplication accountApplication)
    {
        _accountApplication = accountApplication;
    }

    public Task<OperationResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var command = new AccountManagement.Application.Contracts.AC.Account.RegisterAccount
        {
            UserName = request.UserName,
            FullName = request.FullName,
            Password = request.Password,
            Mobile = request.Mobile,
            Address = request.Address,
            PostalCode = request.PostalCode,
            RoleId = request.RoleId,
            ProfilePhoto = request.ProfilePhoto
        };
        return Task.FromResult(_accountApplication.Register(command));
    }
}

#nullable enable

using AccountManagement.Application.Contracts.AC.Account;
using MediatR;
using Microsoft.AspNetCore.Http;
using _0_Framework.Application;

using AccountManagement.Application.Contracts.Commands.Accounts.Edit;

namespace AccountManagement.Application.Features.Accounts.Commands.Edit;



public class EditAccountCommandHandler : IRequestHandler<EditAccountCommand, OperationResult>
{
    private readonly IAccountApplication _accountApplication;

    public EditAccountCommandHandler(IAccountApplication accountApplication)
    {
        _accountApplication = accountApplication;
    }

    public Task<OperationResult> Handle(EditAccountCommand request, CancellationToken cancellationToken)
    {
        var command = new AccountManagement.Application.Contracts.AC.Account.EditAccount
        {
            Id = request.Id,
            UserName = request.UserName,
            FullName = request.FullName,
            Password = request.Password,
            Mobile = request.Mobile,
            Address = request.Address,
            PostalCode = request.PostalCode,
            RoleId = request.RoleId,
            ProfilePhoto = request.ProfilePhoto
        };
        return Task.FromResult(_accountApplication.Edit(command));
    }
}

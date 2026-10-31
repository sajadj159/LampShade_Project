using AccountManagement.Application.Contracts.Commands.Accounts.Register;
using AccountManagement.Domain.AccountAgg;
using LampShade.BuildingBlocks.Application;
using MediatR;

namespace AccountManagement.Application.Features.Accounts.Commands.Register;

public class RegisterCommandHandler(IAccountRepository accounts, IFileUploader uploader, IPasswordHasher passwordHasher, IAuthHelper authHelper)
    : IRequestHandler<RegisterCommand, OperationResult>
{
    public async Task<OperationResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult();
        if (await accounts.ExistAsync(x => x.UserName == request.UserName || x.Mobile == request.Mobile, cancellationToken))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);

        // Only an administrator creating a user from the admin panel may choose the role;
        // self-registration always produces a regular system user.
        var roleId = authHelper.CurrentAccountRole() == LampShade.BuildingBlocks.Application.Roles.Administrator && request.RoleId > 0
            ? request.RoleId
            : long.Parse(LampShade.BuildingBlocks.Application.Roles.SystemUser);

        accounts.Add(new Account(request.UserName, request.FullName, passwordHasher.Hash(request.Password), request.Mobile, roleId,
            uploader.Upload(request.ProfilePhoto, "profilePhotos"), request.Address, request.PostalCode));
        return operation.Succeeded();
    }
}

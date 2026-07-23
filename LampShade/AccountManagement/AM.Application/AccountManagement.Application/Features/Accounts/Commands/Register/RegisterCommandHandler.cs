using _0_Framework.Application;
using AccountManagement.Application.Contracts.Commands.Accounts.Register;
using AccountManagement.Domain.AccountAgg;
using MediatR;

namespace AccountManagement.Application.Features.Accounts.Commands.Register;

public class RegisterCommandHandler(IAccountRepository accounts, IFIleUploader uploader, IPasswordHasher passwordHasher) : IRequestHandler<RegisterCommand, OperationResult>
{
    public Task<OperationResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult();
        if (accounts.Exist(x => x.UserName == request.UserName || x.Mobile == request.Mobile)) return Task.FromResult(operation.Failed(ApplicationMessages.DuplicatedRecord));
        var profilePath = uploader.Upload(request.ProfilePhoto, "profilePhotos");
        accounts.Create(new Account(request.UserName, request.FullName, passwordHasher.Hash(request.Password), request.Mobile, request.RoleId, profilePath, request.Address, request.PostalCode));
        accounts.Save();
        return Task.FromResult(operation.Succeeded());
    }
}

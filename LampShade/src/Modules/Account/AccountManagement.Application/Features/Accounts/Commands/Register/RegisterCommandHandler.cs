using System.Threading;
using System.Threading.Tasks;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.Commands.Accounts.Register;
using AccountManagement.Domain.AccountAgg;
using MediatR;

namespace AccountManagement.Application.Features.Accounts.Commands.Register;

public class RegisterCommandHandler(IAccountRepository accounts, IFIleUploader uploader, IPasswordHasher passwordHasher) : IRequestHandler<RegisterCommand, OperationResult>
{
    public async Task<OperationResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult();
        if (await accounts.ExistAsync(x => x.UserName == request.UserName || x.Mobile == request.Mobile, cancellationToken)) return operation.Failed(ApplicationMessages.DuplicatedRecord);
        accounts.Add(new Account(request.UserName, request.FullName, passwordHasher.Hash(request.Password), request.Mobile, request.RoleId, uploader.Upload(request.ProfilePhoto, "profilePhotos"), request.Address, request.PostalCode)); return operation.Succeeded();
    }
}

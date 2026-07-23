using System.Threading;
using System.Threading.Tasks;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.Commands.Accounts.Edit;
using AccountManagement.Domain.AccountAgg;
using MediatR;

namespace AccountManagement.Application.Features.Accounts.Commands.Edit;

public class EditAccountCommandHandler(IAccountRepository accounts, IFIleUploader uploader) : IRequestHandler<EditAccountCommand, OperationResult>
{
    public async Task<OperationResult> Handle(EditAccountCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult(); var account = await accounts.GetAsync(request.Id, cancellationToken);
        if (account is null) return operation.Failed(ApplicationMessages.RecordNotFound);
        if (await accounts.ExistAsync(x => (x.UserName == request.UserName || x.Mobile == request.Mobile) && x.Id != request.Id, cancellationToken)) return operation.Failed(ApplicationMessages.DuplicatedRecord);
        account.Edit(request.UserName, request.FullName, request.Mobile, request.RoleId, uploader.Upload(request.ProfilePhoto, "profilePhotos"), request.Address, request.PostalCode); return operation.Succeeded();
    }
}

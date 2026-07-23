using _0_Framework.Application;
using AccountManagement.Application.Contracts.Commands.Accounts.Edit;
using AccountManagement.Domain.AccountAgg;
using MediatR;

namespace AccountManagement.Application.Features.Accounts.Commands.Edit;

public class EditAccountCommandHandler(IAccountRepository accounts, IFIleUploader uploader) : IRequestHandler<EditAccountCommand, OperationResult>
{
    public Task<OperationResult> Handle(EditAccountCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult();
        var account = accounts.Get(request.Id);
        if (account is null) return Task.FromResult(operation.Failed(ApplicationMessages.RecordNotFound));
        if (accounts.Exist(x => (x.UserName == request.UserName || x.Mobile == request.Mobile) && x.Id != request.Id)) return Task.FromResult(operation.Failed(ApplicationMessages.DuplicatedRecord));
        account.Edit(request.UserName, request.FullName, request.Mobile, request.RoleId, uploader.Upload(request.ProfilePhoto, "profilePhotos"), request.Address, request.PostalCode);
        accounts.Save();
        return Task.FromResult(operation.Succeeded());
    }
}

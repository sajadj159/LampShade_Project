using System.Threading;
using System.Threading.Tasks;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.Commands.Roles.EditRole;
using AccountManagement.Domain.RoleAgg;
using MediatR;

namespace AccountManagement.Application.Features.Roles.Commands.EditRole;

public class EditRoleCommandHandler(IRoleRepository roles) : IRequestHandler<EditRoleCommand, OperationResult>
{
    public async Task<OperationResult> Handle(EditRoleCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult(); var role = await roles.GetAsync(request.Id, cancellationToken);
        if (role is null) return operation.Failed(ApplicationMessages.RecordNotFound);
        if (await roles.ExistAsync(x => x.Name == request.Name && x.Id != request.Id, cancellationToken)) return operation.Failed(ApplicationMessages.DuplicatedRecord);
        role.Edit(request.Name, request.Permissions); return operation.Succeeded();
    }
}

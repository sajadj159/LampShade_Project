using _0_Framework.Application;
using AccountManagement.Application.Contracts.Commands.Roles.EditRole;
using AccountManagement.Domain.RoleAgg;
using MediatR;

namespace AccountManagement.Application.Features.Roles.Commands.EditRole;

public class EditRoleCommandHandler(IRoleRepository roles) : IRequestHandler<EditRoleCommand, OperationResult>
{
    public Task<OperationResult> Handle(EditRoleCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult();
        var role = roles.Get(request.Id);
        if (role is null) return Task.FromResult(operation.Failed(ApplicationMessages.RecordNotFound));
        if (roles.Exist(x => x.Name == request.Name && x.Id != request.Id)) return Task.FromResult(operation.Failed(ApplicationMessages.DuplicatedRecord));
        role.Edit(request.Name, request.Permissions);
        roles.Save();
        return Task.FromResult(operation.Succeeded());
    }
}

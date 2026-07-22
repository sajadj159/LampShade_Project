using _0_Framework.Application;
using AccountManagement.Application.Contracts.AC.Role;
using AccountManagement.Domain.RoleAgg.Contracts;
using MediatR;

using AccountManagement.Application.Contracts.Commands.Roles.EditRole;

namespace AccountManagement.Application.Features.Roles.Commands.EditRole;



public class EditRoleCommandHandler : IRequestHandler<EditRoleCommand, OperationResult>
{
    private readonly IRoleApplication _roleApplication;

    public EditRoleCommandHandler(IRoleApplication roleApplication)
    {
        _roleApplication = roleApplication;
    }

    public Task<OperationResult> Handle(EditRoleCommand request, CancellationToken cancellationToken)
    {
        var command = new AccountManagement.Application.Contracts.AC.Role.EditRole
        {
            Id = request.Id,
            Name = request.Name,
            Permissions = request.Permissions
        };
        return Task.FromResult(_roleApplication.Edit(command));
    }
}

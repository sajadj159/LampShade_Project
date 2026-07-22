using _0_Framework.Application;
using AccountManagement.Application.Contracts.AC.Role;
using AccountManagement.Domain.RoleAgg.Contracts;
using MediatR;

namespace AccountManagement.Application.Features.Roles.Commands.EditRole;

public class EditRoleCommand : IRequest<OperationResult>
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<CreatePermissionDto> Permissions { get; set; } = new();
}

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

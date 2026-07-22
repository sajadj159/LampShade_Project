using _0_Framework.Application;
using AccountManagement.Application.Contracts.AC.Role;
using AccountManagement.Domain.RoleAgg.Contracts;
using MediatR;

namespace AccountManagement.Application.Features.Roles.Commands.CreateRole;

public class CreateRoleCommand : IRequest<OperationResult>
{
    public string Name { get; set; } = string.Empty;
    public List<CreatePermissionDto> Permissions { get; set; } = new();
}

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, OperationResult>
{
    private readonly IRoleApplication _roleApplication;

    public CreateRoleCommandHandler(IRoleApplication roleApplication)
    {
        _roleApplication = roleApplication;
    }

    public Task<OperationResult> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var command = new AccountManagement.Application.Contracts.AC.Role.CreateRole
        {
            Name = request.Name,
            Permissions = request.Permissions
        };
        return Task.FromResult(_roleApplication.Create(command));
    }
}

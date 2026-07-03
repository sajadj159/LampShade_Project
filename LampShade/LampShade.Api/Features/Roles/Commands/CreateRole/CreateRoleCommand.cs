using AccountManagement.Application.Contracts.AC.Role;
using MediatR;
using _0_Framework.Application;

namespace LampShade.Api.Features.Roles.Commands.CreateRole;

public class CreateRoleCommand : IRequest<OperationResult>
{
    public string Name { get; set; } = string.Empty;
    public List<int> Permissions { get; set; } = new();
}

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, OperationResult>
{
    private readonly IRoleApplication _roleApplication;

    public CreateRoleCommandHandler(IRoleApplication roleApplication)
    {
        _roleApplication = roleApplication;
    }

    public async Task<OperationResult> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var command = new AccountManagement.Application.Contracts.AC.Role.CreateRole
        {
            Name = request.Name,
            Permissions = request.Permissions
        };
        return await Task.FromResult(_roleApplication.Create(command));
    }
}

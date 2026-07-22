using _0_Framework.Application;
using AccountManagement.Application.Contracts.AC.Role;
using AccountManagement.Domain.RoleAgg.Contracts;
using MediatR;

namespace AccountManagement.Application.Contracts.Commands.Roles.CreateRole;

public class CreateRoleCommand : IRequest<OperationResult>
{
    public string Name { get; set; } = string.Empty;
    public List<CreatePermissionDto> Permissions { get; set; } = new();
}

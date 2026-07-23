using _0_Framework.Application;
using AccountManagement.Application.Contracts.AC.Role;
using AccountManagement.Domain.RoleAgg.Contracts;
using MediatR;

namespace AccountManagement.Application.Contracts.Commands.Roles.EditRole;

public class EditRoleCommand : ICommand<OperationResult>
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<CreatePermissionDto> Permissions { get; set; } = new();
}

using LampShade.BuildingBlocks.Application;
using AccountManagement.Domain.RoleAgg.Contracts;
using MediatR;

namespace AccountManagement.Application.Contracts.Commands.Roles.CreateRole;

public class CreateRoleCommand : ICommand<OperationResult>
{
    public string Name { get; set; } = string.Empty;
    public List<CreatePermissionDto> Permissions { get; set; } = new();
}

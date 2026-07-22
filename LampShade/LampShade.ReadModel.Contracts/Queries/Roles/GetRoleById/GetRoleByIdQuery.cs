using AccountManagement.Application.Contracts.AC.Role;
using AccountManagement.Domain.RoleAgg.Contracts;
using MediatR;

namespace LampShade.ReadModel.Contracts.Queries.Roles.GetRoleById;

public class GetRoleByIdQuery : IRequest<RoleDto>
{
    public long Id { get; set; }
}

using LampShade.ReadModel.Contracts.Roles.Dto;
using MediatR;

namespace LampShade.ReadModel.Contracts.Queries.Roles.GetRoleById;

public class GetRoleByIdQuery : IRequest<RoleDto>
{
    public long Id { get; set; }
}


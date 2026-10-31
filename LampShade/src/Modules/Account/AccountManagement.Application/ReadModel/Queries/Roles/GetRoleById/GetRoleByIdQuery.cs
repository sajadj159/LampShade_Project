using AccountManagement.Application.ReadModel.Roles.Dto;
using MediatR;

namespace AccountManagement.Application.ReadModel.Queries.Roles.GetRoleById;

public class GetRoleByIdQuery : IRequest<RoleDto>
{
    public long Id { get; set; }
}


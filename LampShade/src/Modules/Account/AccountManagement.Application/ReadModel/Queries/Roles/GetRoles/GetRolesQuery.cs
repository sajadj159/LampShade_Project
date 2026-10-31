using LampShade.ReadModel.Contracts.Roles.Dto;
using MediatR;

namespace LampShade.ReadModel.Contracts.Queries.Roles.GetRoles;

public class GetRolesQuery : IRequest<List<RoleDto>> { }



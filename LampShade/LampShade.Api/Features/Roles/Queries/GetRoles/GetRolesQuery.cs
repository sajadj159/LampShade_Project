using AccountManagement.Domain.RoleAgg.Contracts;
using MediatR;

namespace LampShade.Api.Features.Roles.Queries.GetRoles;

public class GetRolesQuery : IRequest<List<RoleDto>> { }

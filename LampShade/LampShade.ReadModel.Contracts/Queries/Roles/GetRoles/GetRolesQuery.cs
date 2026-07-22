using AccountManagement.Domain.RoleAgg.Contracts;
using MediatR;

namespace LampShade.ReadModel.Contracts.Queries.Roles.GetRoles;

public class GetRolesQuery : IRequest<List<RoleDto>> { }

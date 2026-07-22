using AccountManagement.Domain.RoleAgg.Contracts;
using MediatR;

namespace AccountManagement.Application.Features.Roles.Queries.GetRoles;

public class GetRolesQuery : IRequest<List<RoleDto>> { }

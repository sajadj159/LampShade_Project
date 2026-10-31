using AccountManagement.Application.ReadModel.Roles.Dto;
using MediatR;

namespace AccountManagement.Application.ReadModel.Queries.Roles.GetRoles;

public class GetRolesQuery : IRequest<List<RoleDto>> { }



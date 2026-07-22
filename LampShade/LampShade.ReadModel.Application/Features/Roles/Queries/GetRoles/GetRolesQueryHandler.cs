using AccountManagement.Application.Contracts.AC.Role;
using AccountManagement.Domain.RoleAgg.Contracts;
using MediatR;

using LampShade.ReadModel.Contracts.Queries.Roles.GetRoles;

namespace LampShade.ReadModel.Application.Features.Roles.Queries.GetRoles;

public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, List<RoleDto>>
{
    private readonly IRoleApplication _roleApplication;

    public GetRolesQueryHandler(IRoleApplication roleApplication)
    {
        _roleApplication = roleApplication;
    }

    public Task<List<RoleDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_roleApplication.GetRolls());
    }
}

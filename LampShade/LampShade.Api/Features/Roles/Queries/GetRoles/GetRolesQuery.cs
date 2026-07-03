using AccountManagement.Application.Contracts.AC.Role;
using MediatR;

namespace LampShade.Api.Features.Roles.Queries.GetRoles;

public class GetRolesQuery : IRequest<List<RoleViewModel>> { }

public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, List<RoleViewModel>>
{
    private readonly IRoleApplication _roleApplication;

    public GetRolesQueryHandler(IRoleApplication roleApplication)
    {
        _roleApplication = roleApplication;
    }

    public async Task<List<RoleViewModel>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_roleApplication.GetRolls());
    }
}

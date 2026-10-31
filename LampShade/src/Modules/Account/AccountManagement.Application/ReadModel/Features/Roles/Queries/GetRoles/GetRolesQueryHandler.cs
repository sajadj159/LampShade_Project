using System.Threading;
using System.Threading.Tasks;
using AccountManagement.Application.ReadModel.Account;
using AccountManagement.Application.ReadModel.Roles.Dto;
using MediatR;

using AccountManagement.Application.ReadModel.Queries.Roles.GetRoles;

namespace AccountManagement.Application.ReadModel.Features.Roles.Queries.GetRoles;

public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, List<RoleDto>>
{
    private readonly IAccountQuery _query;

    public GetRolesQueryHandler(IAccountQuery query)
    {
        _query = query;
    }

    public Task<List<RoleDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        return _query.GetRolesAsync(cancellationToken);
    }
}

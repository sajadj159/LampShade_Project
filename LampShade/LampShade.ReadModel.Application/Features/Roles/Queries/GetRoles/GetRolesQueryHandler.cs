using System.Threading;
using System.Threading.Tasks;
using LampShade.ReadModel.Contracts.Account;
using LampShade.ReadModel.Contracts.Roles.Dto;
using MediatR;

using LampShade.ReadModel.Contracts.Queries.Roles.GetRoles;

namespace LampShade.ReadModel.Application.Features.Roles.Queries.GetRoles;

public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, List<RoleDto>>
{
    private readonly IAccountQuery _query;

    public GetRolesQueryHandler(IAccountQuery query)
    {
        _query = query;
    }

    public Task<List<RoleDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_query.GetRoles());
    }
}

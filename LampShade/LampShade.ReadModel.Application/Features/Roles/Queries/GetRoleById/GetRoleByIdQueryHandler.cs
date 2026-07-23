using System.Threading;
using System.Threading.Tasks;
using LampShade.ReadModel.Contracts.Account;
using LampShade.ReadModel.Contracts.Roles.Dto;
using MediatR;

using LampShade.ReadModel.Contracts.Queries.Roles.GetRoleById;

namespace LampShade.ReadModel.Application.Features.Roles.Queries.GetRoleById;



public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, RoleDto>
{
    private readonly IAccountQuery _query;

    public GetRoleByIdQueryHandler(IAccountQuery query)
    {
        _query = query;
    }

    public Task<RoleDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_query.GetRole(request.Id));
    }
}

using System.Threading;
using System.Threading.Tasks;
using AccountManagement.Application.ReadModel.Account;
using AccountManagement.Application.ReadModel.Roles.Dto;
using MediatR;

using AccountManagement.Application.ReadModel.Queries.Roles.GetRoleById;

namespace AccountManagement.Application.ReadModel.Features.Roles.Queries.GetRoleById;



public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, RoleDto>
{
    private readonly IAccountQuery _query;

    public GetRoleByIdQueryHandler(IAccountQuery query)
    {
        _query = query;
    }

    public Task<RoleDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        return _query.GetRoleAsync(request.Id, cancellationToken);
    }
}

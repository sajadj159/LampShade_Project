using AccountManagement.Application.Contracts.AC.Role;
using AccountManagement.Domain.RoleAgg.Contracts;
using MediatR;

namespace LampShade.Api.Features.Roles.Queries.GetRoleById;

public class GetRoleByIdQuery : IRequest<RoleDto>
{
    public long Id { get; set; }
}

public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, RoleDto>
{
    private readonly IRoleApplication _roleApplication;

    public GetRoleByIdQueryHandler(IRoleApplication roleApplication)
    {
        _roleApplication = roleApplication;
    }

    public async Task<RoleDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_roleApplication.GetDetails(request.Id));
    }
}

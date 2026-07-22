using AccountManagement.Application.Contracts.AC.Role;
using AccountManagement.Domain.RoleAgg.Contracts;
using MediatR;

namespace AccountManagement.Application.Features.Roles.Queries.GetRoleById;

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

    public Task<RoleDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_roleApplication.GetDetails(request.Id));
    }
}

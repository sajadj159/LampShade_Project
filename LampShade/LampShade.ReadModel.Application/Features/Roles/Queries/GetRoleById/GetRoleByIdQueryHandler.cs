using AccountManagement.Application.Contracts.AC.Role;
using AccountManagement.Domain.RoleAgg.Contracts;
using MediatR;

using LampShade.ReadModel.Contracts.Queries.Roles.GetRoleById;

namespace LampShade.ReadModel.Application.Features.Roles.Queries.GetRoleById;



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

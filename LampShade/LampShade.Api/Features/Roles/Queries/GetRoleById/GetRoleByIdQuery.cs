using AccountManagement.Application.Contracts.AC.Role;
using MediatR;

namespace LampShade.Api.Features.Roles.Queries.GetRoleById;

public class GetRoleByIdQuery : IRequest<EditRole>
{
    public long Id { get; set; }
}

public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, EditRole>
{
    private readonly IRoleApplication _roleApplication;

    public GetRoleByIdQueryHandler(IRoleApplication roleApplication)
    {
        _roleApplication = roleApplication;
    }

    public async Task<EditRole> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_roleApplication.GetDetails(request.Id));
    }
}

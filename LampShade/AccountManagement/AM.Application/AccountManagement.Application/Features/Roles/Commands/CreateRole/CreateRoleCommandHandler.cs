using _0_Framework.Application;
using AccountManagement.Application.Contracts.Commands.Roles.CreateRole;
using AccountManagement.Domain.RoleAgg;
using MediatR;

namespace AccountManagement.Application.Features.Roles.Commands.CreateRole;

public class CreateRoleCommandHandler(IRoleRepository roles) : IRequestHandler<CreateRoleCommand, OperationResult>
{
    public Task<OperationResult> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult();
        if (roles.Exist(x => x.Name == request.Name)) return Task.FromResult(operation.Failed(ApplicationMessages.DuplicatedRecord));
        roles.Create(new Role(request.Name, request.Permissions));
        roles.Save();
        return Task.FromResult(operation.Succeeded());
    }
}

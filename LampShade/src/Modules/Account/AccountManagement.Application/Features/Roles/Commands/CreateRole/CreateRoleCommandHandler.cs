using System.Threading;
using System.Threading.Tasks;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.Commands.Roles.CreateRole;
using AccountManagement.Domain.RoleAgg;
using MediatR;

namespace AccountManagement.Application.Features.Roles.Commands.CreateRole;

public class CreateRoleCommandHandler(IRoleRepository roles) : IRequestHandler<CreateRoleCommand, OperationResult>
{
    public async Task<OperationResult> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult();
        if (await roles.ExistAsync(x => x.Name == request.Name, cancellationToken)) return operation.Failed(ApplicationMessages.DuplicatedRecord);
        roles.Add(new Role(request.Name, request.Permissions)); return operation.Succeeded();
    }
}

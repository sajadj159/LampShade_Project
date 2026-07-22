using InventoryManagement.Application.Contract.AC.Inventory;
using MediatR;

using LampShade.ReadModel.Contracts.Queries.Inventories.GetInventoryOperations;

namespace LampShade.ReadModel.Application.Features.Inventories.Queries.GetInventoryOperations;



public class GetInventoryOperationsQueryHandler : IRequestHandler<GetInventoryOperationsQuery, List<InventoryOperationViewModel>>
{
    private readonly IInventoryApplication _application;

    public GetInventoryOperationsQueryHandler(IInventoryApplication application)
    {
        _application = application;
    }

    public Task<List<InventoryOperationViewModel>> Handle(GetInventoryOperationsQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_application.GetOperationLog(request.InventoryId));
    }
}

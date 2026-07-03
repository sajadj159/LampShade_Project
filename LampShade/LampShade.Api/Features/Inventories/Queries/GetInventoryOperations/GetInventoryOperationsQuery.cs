using InventoryManagement.Application.Contract.AC.Inventory;
using MediatR;

namespace LampShade.Api.Features.Inventories.Queries.GetInventoryOperations;

public class GetInventoryOperationsQuery : IRequest<List<InventoryOperationViewModel>>
{
    public long InventoryId { get; set; }
}

public class GetInventoryOperationsQueryHandler : IRequestHandler<GetInventoryOperationsQuery, List<InventoryOperationViewModel>>
{
    private readonly IInventoryApplication _application;

    public GetInventoryOperationsQueryHandler(IInventoryApplication application)
    {
        _application = application;
    }

    public async Task<List<InventoryOperationViewModel>> Handle(GetInventoryOperationsQuery request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_application.GetOperationLog(request.InventoryId));
    }
}

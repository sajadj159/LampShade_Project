using System.Threading;
using System.Threading.Tasks;
using InventoryManagement.Application.Contract.AC.Inventory;
using MediatR;
using InventoryManagement.Application.ReadModel.Inventory;

using InventoryManagement.Application.ReadModel.Queries.Inventories.GetInventoryOperations;

namespace InventoryManagement.Application.ReadModel.Features.Inventories.Queries.GetInventoryOperations;



public class GetInventoryOperationsQueryHandler : IRequestHandler<GetInventoryOperationsQuery, List<InventoryOperationViewModel>>
{
    private readonly IInventoryQuery _repository;

    public GetInventoryOperationsQueryHandler(IInventoryQuery repository)
    {
        _repository = repository;
    }

    public Task<List<InventoryOperationViewModel>> Handle(GetInventoryOperationsQuery request, CancellationToken cancellationToken)
    {
        return _repository.GetOperationLogAsync(request.InventoryId, cancellationToken);
    }
}

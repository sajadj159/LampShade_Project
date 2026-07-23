using InventoryManagement.Application.Contract.AC.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using MediatR;

using LampShade.ReadModel.Contracts.Queries.Inventories.GetInventoryOperations;

namespace LampShade.ReadModel.Application.Features.Inventories.Queries.GetInventoryOperations;



public class GetInventoryOperationsQueryHandler : IRequestHandler<GetInventoryOperationsQuery, List<InventoryOperationViewModel>>
{
    private readonly IInventoryRepository _repository;

    public GetInventoryOperationsQueryHandler(IInventoryRepository repository)
    {
        _repository = repository;
    }

    public Task<List<InventoryOperationViewModel>> Handle(GetInventoryOperationsQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_repository.GetOperationLog(request.InventoryId));
    }
}


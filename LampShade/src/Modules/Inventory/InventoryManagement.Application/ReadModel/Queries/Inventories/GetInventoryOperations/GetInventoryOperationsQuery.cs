using InventoryManagement.Application.Contract.AC.Inventory;
using MediatR;

namespace InventoryManagement.Application.ReadModel.Queries.Inventories.GetInventoryOperations;

public class GetInventoryOperationsQuery : IRequest<List<InventoryOperationViewModel>>
{
    public long InventoryId { get; set; }
}

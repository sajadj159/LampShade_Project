using InventoryManagement.Application.Contract.AC.Inventory;
using MediatR;

namespace LampShade.ReadModel.Contracts.Queries.Inventories.GetInventoryOperations;

public class GetInventoryOperationsQuery : IRequest<List<InventoryOperationViewModel>>
{
    public long InventoryId { get; set; }
}

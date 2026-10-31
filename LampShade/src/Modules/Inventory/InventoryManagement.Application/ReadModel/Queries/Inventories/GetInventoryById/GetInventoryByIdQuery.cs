using InventoryManagement.Application.Contract.AC.Inventory;
using MediatR;

namespace LampShade.ReadModel.Contracts.Queries.Inventories.GetInventoryById;

public class GetInventoryByIdQuery : IRequest<EditInventory>
{
    public long Id { get; set; }
}

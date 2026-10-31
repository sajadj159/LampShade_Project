#nullable enable

using InventoryManagement.Application.Contract.AC.Inventory;
using MediatR;

namespace InventoryManagement.Application.ReadModel.Queries.Inventories.SearchInventories;

public class SearchInventoriesQuery : IRequest<List<InventoryViewModel>>
{
    public long? ProductId { get; set; }
    public bool? InStock { get; set; }
}

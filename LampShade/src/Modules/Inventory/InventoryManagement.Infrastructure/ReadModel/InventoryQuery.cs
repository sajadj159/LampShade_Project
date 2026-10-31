using InventoryManagement.Infrastructure.EFCore;
using LampShade.ReadModel.Contracts.Acl;
using LampShade.ReadModel.Contracts.Inventory;
using Microsoft.EntityFrameworkCore;

namespace LampShade.ReadModel.Application.Query;

public class InventoryQuery(InventoryContext context, IInventoryProductReadAcl productAcl) : IInventoryQuery
{
    public async Task<StockStatus> CheckStockAsync(IsInStock command, CancellationToken cancellationToken = default)
    {
        var inventory = await context.Inventory.AsNoTracking().FirstOrDefaultAsync(item => item.ProductId == command.ProductId, cancellationToken);
        if (inventory is not null && inventory.CalculateCurrentCount() >= command.Count) return new StockStatus { IsStock = true };
        return new StockStatus { IsStock = false, ProductName = await productAcl.GetProductNameAsync(command.ProductId, cancellationToken) };
    }
}
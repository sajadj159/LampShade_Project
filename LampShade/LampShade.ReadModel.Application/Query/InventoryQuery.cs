using InventoryManagement.Infrastructure.EFCore;
using LampShade.ReadModel.Contracts.Inventory;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EFCore;

namespace LampShade.ReadModel.Application.Query;

public class InventoryQuery(InventoryContext context, ShopContext shopContext) : IInventoryQuery
{
    public async Task<StockStatus> CheckStockAsync(IsInStock command, CancellationToken cancellationToken = default)
    {
        var inventory = await context.Inventory.AsNoTracking().FirstOrDefaultAsync(x => x.ProductId == command.ProductId, cancellationToken);
        if (inventory is not null && inventory.CalculateCurrentCount() >= command.Count)
            return new StockStatus { IsStock = true };

        var productName = await shopContext.Products.AsNoTracking().Where(x => x.Id == command.ProductId).Select(x => x.Name).FirstOrDefaultAsync(cancellationToken);
        return new StockStatus { IsStock = false, ProductName = productName };
    }
}
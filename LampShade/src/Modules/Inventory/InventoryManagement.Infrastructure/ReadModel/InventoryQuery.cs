using AccountManagement.Contracts;
using InventoryManagement.Application.Contract.AC.Inventory;
using InventoryManagement.Application.ReadModel.Inventory;
using InventoryManagement.Infrastructure.EFCore;
using LampShade.BuildingBlocks.Application;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Contracts;

namespace InventoryManagement.Infrastructure.ReadModel;

public class InventoryQuery(InventoryContext context, IShopModuleApi shop, IAccountModuleApi accounts) : IInventoryQuery
{
    public async Task<StockStatus> CheckStockAsync(IsInStock command, CancellationToken cancellationToken = default)
    {
        var inventory = await context.Inventory.AsNoTracking().FirstOrDefaultAsync(item => item.ProductId == command.ProductId, cancellationToken);
        if (inventory is not null && inventory.CalculateCurrentCount() >= command.Count) return new StockStatus { IsStock = true };
        var names = await shop.GetProductNamesAsync([command.ProductId], cancellationToken);
        return new StockStatus { IsStock = false, ProductName = names.GetValueOrDefault(command.ProductId) };
    }

    public async Task<List<InventoryViewModel>> SearchAsync(InventorySearchModel searchModel, CancellationToken cancellationToken = default)
    {
        var query = context.Inventory.AsNoTracking();
        if (searchModel.InStock)
            query = query.Where(x => !x.InStock);
        if (searchModel.ProductId > 0)
            query = query.Where(x => x.ProductId == searchModel.ProductId);

        var inventory = (await query.OrderByDescending(x => x.Id).ToListAsync(cancellationToken))
            .Select(x => new InventoryViewModel
            {
                Id = x.Id,
                ProductId = x.ProductId,
                InStock = x.InStock,
                UnitPrice = x.UnitPrice,
                CurrentCount = x.CalculateCurrentCount(),
                CreationDate = x.CreationDate.ToFarsi()
            }).ToList();

        var productNames = await shop.GetProductNamesAsync(inventory.Select(x => x.ProductId), cancellationToken);
        inventory.ForEach(item => item.Product = productNames.GetValueOrDefault(item.ProductId));
        return inventory;
    }

    public Task<EditInventory> GetDetailsAsync(long id, CancellationToken cancellationToken = default) =>
        context.Inventory.AsNoTracking().Where(x => x.Id == id)
            .Select(x => new EditInventory { Id = x.Id, ProductId = x.ProductId, UnitPrice = x.UnitPrice })
            .FirstOrDefaultAsync(cancellationToken);

    public async Task<List<InventoryOperationViewModel>> GetOperationLogAsync(long inventoryId, CancellationToken cancellationToken = default)
    {
        var inventory = await context.Inventory.AsNoTracking().FirstOrDefaultAsync(x => x.Id == inventoryId, cancellationToken);
        if (inventory is null) return [];

        var operations = inventory.InventoryOperations.Select(x => new InventoryOperationViewModel
        {
            Id = x.Id,
            Count = x.Count,
            CurrentCount = x.CurrentCount,
            Description = x.Description,
            Operation = x.Operation,
            OperationDate = x.OperationDate.ToFarsi(),
            OperatorId = x.OperatorId,
            OrderId = x.OrderId
        }).OrderByDescending(x => x.Id).ToList();

        var operators = await accounts.GetAccountsAsync(operations.Select(x => x.OperatorId), cancellationToken);
        foreach (var operation in operations)
            operation.Operator = operators.GetValueOrDefault(operation.OperatorId)?.FullName;
        return operations;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LampShade.ReadModel.Contracts.Acl;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.EFCore.ReadAcl;

public sealed class ShopInventoryReadAcl(InventoryContext context) : IShopInventoryReadAcl
{
    public Task<List<InventoryReadModel>> GetInventoriesAsync(CancellationToken cancellationToken = default) =>
        context.Inventory.AsNoTracking().Select(item => new InventoryReadModel(item.ProductId, item.InStock, item.UnitPrice, item.InventoryOperations.Where(operation => operation.Operation).Sum(operation => operation.Count) - item.InventoryOperations.Where(operation => !operation.Operation).Sum(operation => operation.Count))).ToListAsync(cancellationToken);
}
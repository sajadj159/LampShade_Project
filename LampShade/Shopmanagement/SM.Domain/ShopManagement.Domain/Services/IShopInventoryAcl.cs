using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Domain.Services;

public interface IShopInventoryAcl
{
    Task<bool> ReduceFromInventoryAsync(List<OrderItem> items, CancellationToken cancellationToken = default);
}

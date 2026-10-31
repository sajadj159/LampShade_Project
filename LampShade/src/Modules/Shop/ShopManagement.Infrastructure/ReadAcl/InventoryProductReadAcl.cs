using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LampShade.ReadModel.Contracts.Acl;
using Microsoft.EntityFrameworkCore;

namespace ShopManagement.Infrastructure.EFCore.ReadAcl;

public sealed class InventoryProductReadAcl(ShopContext context) : IInventoryProductReadAcl
{
    public Task<string> GetProductNameAsync(long productId, CancellationToken cancellationToken = default) =>
        context.Products.AsNoTracking().Where(product => product.Id == productId).Select(product => product.Name).FirstOrDefaultAsync(cancellationToken);
}
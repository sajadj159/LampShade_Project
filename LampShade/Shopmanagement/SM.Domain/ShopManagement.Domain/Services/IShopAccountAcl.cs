using System.Threading;
using System.Threading.Tasks;
namespace ShopManagement.Domain.Services
{
    public interface IShopAccountAcl
    {
        Task<(string name, string mobile)> GetAccountByAsync(long id, CancellationToken cancellationToken = default);
    }
}
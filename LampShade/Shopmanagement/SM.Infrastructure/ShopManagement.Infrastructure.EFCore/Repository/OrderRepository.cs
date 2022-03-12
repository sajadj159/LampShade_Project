using System.Linq;
using _0_Framework.Repository;
using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class OrderRepository : RepositoryBase<long, Order>, IOrderRepository
    {
        private readonly ShopContext _context;

        public OrderRepository(ShopContext context):base(context)
        {
            _context = context;
        }

        public double GetAmountBy(long id)
        {
            var orderPayAmount = _context.Orders.Select(x => new {x.PayAmount, x.Id}).FirstOrDefault(x => x.Id == id);
            if (orderPayAmount != null)
                return orderPayAmount.PayAmount;
            return 0;
        }
    }
}
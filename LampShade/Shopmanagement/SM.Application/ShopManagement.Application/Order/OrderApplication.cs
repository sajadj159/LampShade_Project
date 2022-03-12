using _0_Framework.Application;
using Microsoft.Extensions.Configuration;
using ShopManagement.Application.Contract.Order;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.Services;

namespace ShopManagement.Application.Order
{
    public class OrderApplication : IOrderApplication
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IAuthHelper _authHelper;
        private readonly IShopInventoryAcl _inventoryAcl;

        public OrderApplication(IOrderRepository orderRepository, IAuthHelper authHelper, IShopInventoryAcl inventoryAcl)
        {
            _orderRepository = orderRepository;
            _authHelper = authHelper;
            _inventoryAcl = inventoryAcl;
        }

        public long PlaceOrder(Contract.Order.Cart cart)
        {
            var accountId = _authHelper.CurrentAccountId();
            var order = new Domain.OrderAgg.Order(accountId, cart.TotalAmount, cart.PaymentMethod, cart.DiscountAmount, cart.PayAmount);
            foreach (var cartItem in cart.Items)
            {
                var orderItem = new OrderItem(cartItem.Id, cartItem.Count, cartItem.UnitPrice, cartItem.DiscountRate);
                order.AddItem(orderItem);
            }
            _orderRepository.Create(order);
            _orderRepository.Save();
            return order.Id;
        }

        public double GetAmountBy(long id)
        {
            return _orderRepository.GetAmountBy(id);
        }

        public string PaymentSucceeded(long orderId, long refId)
        {
            var order = _orderRepository.Get(orderId);
            order.PaymentSucceeded(refId);
            var issueCodeTracking = CodeGenerator.Generate("S");
            order.SetIssueTrackingNumber(issueCodeTracking);
            if (!_inventoryAcl.ReduceFromInventory(order.Items)) return "";

            _orderRepository.Save();
            return issueCodeTracking;
        }
    }
}
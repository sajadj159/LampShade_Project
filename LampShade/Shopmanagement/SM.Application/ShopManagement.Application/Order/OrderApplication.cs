using System.Collections.Generic;
using _0_Framework.Application;
using _0_Framework.Application.SMS;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Asn1.Cms;
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
        private readonly ISmsService _smsService;
        private readonly IShopAccountAcl _accountAcl;
        public OrderApplication(IOrderRepository orderRepository, IAuthHelper authHelper, IShopInventoryAcl inventoryAcl, ISmsService smsService, IShopAccountAcl accountAcl)
        {
            _orderRepository = orderRepository;
            _authHelper = authHelper;
            _inventoryAcl = inventoryAcl;
            _smsService = smsService;
            _accountAcl = accountAcl;
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
            var (name, mobile) = _accountAcl.GetAccountBy(order.AccountId);
            _smsService.Send(mobile,$"{name} گرامی سفارش شما با شماره پیگیری {issueCodeTracking} موفقیت پرداخت شد و ارسال خواهد شد.");
            return issueCodeTracking;
        }

        public void Cancel(long id)
        {
            var order = _orderRepository.Get(id);
            order?.Cancel();
            _orderRepository.Save();
        }

        public List<OrderViewModel> Search(OrderSearchModel searchModel)
        {
            return _orderRepository.Search(searchModel);
        }

        public List<OrderItemViewModel> GetItemsBy(long orderId)
        {
            return _orderRepository.GetItemsBy(orderId);
        }
    }
}
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
            return ConfirmOrder(orderId, refId, false);
        }

        public string ApproveCashOnDelivery(long orderId)
        {
            var order = _orderRepository.Get(orderId);
            if (order != null && order.PaymentMethod == 2 && order.IsPaid && !order.IsCanceled)
                return order.IssueTrackingNumber;

            return ConfirmOrder(orderId, 0, true);
        }

        public string ApprovePaymentProof(long orderId)
        {
            var order = _orderRepository.Get(orderId);
            if (order == null || order.PaymentMethod != 1 || string.IsNullOrWhiteSpace(order.PaymentProofUrl) || order.IsCanceled)
                return string.Empty;
            if (order.IsPaid)
                return order.IssueTrackingNumber;

            return ConfirmOrder(orderId, 0, false);
        }
        public OperationResult UploadPaymentProof(long orderId, string paymentProofUrl)
        {
            var result = new OperationResult();
            var order = _orderRepository.Get(orderId);
            if (order == null || order.AccountId != _authHelper.CurrentAccountId() || order.PaymentMethod != 1 || order.IsCanceled || order.IsPaid)
                return result.Failed("Payment proof cannot be uploaded for this order.");

            order.SetPaymentProof(paymentProofUrl);
            _orderRepository.Save();
            return result.Succeeded();
        }
        private string ConfirmOrder(long orderId, long refId, bool cashOnDeliveryOnly)
        {
            var order = _orderRepository.Get(orderId);
            if (order == null || order.IsCanceled || order.IsPaid || (cashOnDeliveryOnly && order.PaymentMethod != 2))
                return string.Empty;

            order.PaymentSucceeded(refId);
            var issueCodeTracking = CodeGenerator.Generate("S");
            order.SetIssueTrackingNumber(issueCodeTracking);
            if (!_inventoryAcl.ReduceFromInventory(order.Items)) return string.Empty;

            _orderRepository.Save();
            var (name, mobile) = _accountAcl.GetAccountBy(order.AccountId);
            _smsService.Send(mobile,$"{name} گرامی سفارش شما با شماره پیگیری {issueCodeTracking} تایید شد و ارسال خواهد شد.");
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





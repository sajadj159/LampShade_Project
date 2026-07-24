using System.Threading;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Application.SMS;
using ShopManagement.Application.Contract.Order;
using ShopManagement.Application.Contracts.Commands.Orders.PlaceOrder;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.Services;

namespace ShopManagement.Application.Order;

public class OrderApplication(IOrderRepository orderRepository, IAuthHelper authHelper, IShopInventoryAcl inventoryAcl, ISmsService smsService, IShopAccountAcl accountAcl) : IOrderApplication
{
    public async Task<PlaceOrderResult> PlaceOrderAsync(ShopManagement.Application.Contract.Order.Cart cart, CancellationToken cancellationToken = default)
    {
        var order = new Domain.OrderAgg.Order(authHelper.CurrentAccountId(), cart.TotalAmount, cart.PaymentMethod, cart.DiscountAmount, cart.PayAmount);
        foreach (var item in cart.Items) order.AddItem(new OrderItem(item.Id, item.Count, item.UnitPrice, item.DiscountRate));
        orderRepository.Add(order); return new PlaceOrderResult(() => order.Id);
    }

    public double GetAmountBy(long id) => orderRepository.GetAmountBy(id);
    public Task<string> PaymentSucceededAsync(long orderId, long refId, CancellationToken cancellationToken = default) => ConfirmOrderAsync(orderId, refId, false, cancellationToken);

    public async Task<string> ApproveCashOnDeliveryAsync(long orderId, CancellationToken cancellationToken = default)
    {
        var order = await orderRepository.GetAsync(orderId, cancellationToken);
        if (order is not null && order.PaymentMethod == 2 && order.IsPaid && !order.IsCanceled) return order.IssueTrackingNumber;
        return await ConfirmOrderAsync(orderId, 0, true, cancellationToken);
    }

    public async Task<string> ApprovePaymentProofAsync(long orderId, CancellationToken cancellationToken = default)
    {
        var order = await orderRepository.GetAsync(orderId, cancellationToken);
        if (order is null || order.PaymentMethod != 1 || string.IsNullOrWhiteSpace(order.PaymentProofUrl) || order.IsCanceled) return string.Empty;
        return order.IsPaid ? order.IssueTrackingNumber : await ConfirmOrderAsync(orderId, 0, false, cancellationToken);
    }

    public async Task<OperationResult> UploadPaymentProofAsync(long orderId, string paymentProofUrl, CancellationToken cancellationToken = default)
    {
        var result = new OperationResult(); var order = await orderRepository.GetAsync(orderId, cancellationToken);
        if (order is null || order.AccountId != authHelper.CurrentAccountId() || order.PaymentMethod != 1 || order.IsCanceled || order.IsPaid) return result.Failed("Payment proof cannot be uploaded for this order.");
        order.SetPaymentProof(paymentProofUrl); return result.Succeeded();
    }

    public async Task CancelAsync(long id, CancellationToken cancellationToken = default)
    {
        var order = await orderRepository.GetAsync(id, cancellationToken); order?.Cancel();
    }

    private async Task<string> ConfirmOrderAsync(long orderId, long refId, bool cashOnDeliveryOnly, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetAsync(orderId, cancellationToken);
        if (order is null || order.IsCanceled || order.IsPaid || (cashOnDeliveryOnly && order.PaymentMethod != 2)) return string.Empty;
        order.PaymentSucceeded(refId); var issue = CodeGenerator.Generate("S"); order.SetIssueTrackingNumber(issue);
        if (!await inventoryAcl.ReduceFromInventoryAsync(order.Items, cancellationToken)) return string.Empty;
        var (name, mobile) = await accountAcl.GetAccountByAsync(order.AccountId, cancellationToken); smsService.Send(mobile, $"{name} گرامی سفارش شما با شماره پیگیری {issue} تایید شد و ارسال خواهد شد.");
        return issue;
    }

    public List<OrderViewModel> Search(OrderSearchModel searchModel) => orderRepository.Search(searchModel);
    public List<OrderItemViewModel> GetItemsBy(long orderId) => orderRepository.GetItemsBy(orderId);
}

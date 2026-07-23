using ShopManagement.Application.Contracts.Commands.Orders.PlaceOrder;
using System.Threading;
using System.Threading.Tasks;
using _0_Framework.Application;

namespace ShopManagement.Application.Contract.Order;

public interface IOrderApplication
{
    Task<PlaceOrderResult> PlaceOrderAsync(Cart cart, CancellationToken cancellationToken = default);
    Task<string> PaymentSucceededAsync(long orderId, long refId, CancellationToken cancellationToken = default);
    Task<string> ApproveCashOnDeliveryAsync(long orderId, CancellationToken cancellationToken = default);
    Task<OperationResult> UploadPaymentProofAsync(long orderId, string paymentProofUrl, CancellationToken cancellationToken = default);
    Task<string> ApprovePaymentProofAsync(long orderId, CancellationToken cancellationToken = default);
    Task CancelAsync(long id, CancellationToken cancellationToken = default);
    double GetAmountBy(long id);
    List<OrderViewModel> Search(OrderSearchModel searchModel);
    List<OrderItemViewModel> GetItemsBy(long orderId);
}

using System.Collections.Generic;
using _0_Framework.Application;

namespace ShopManagement.Application.Contract.Order
{
    public interface IOrderApplication
    {
        long PlaceOrder(Cart cart);
        double GetAmountBy(long id);
        string PaymentSucceeded(long orderId, long refId);
        string ApproveCashOnDelivery(long orderId);
        OperationResult UploadPaymentProof(long orderId, string paymentProofUrl);
        string ApprovePaymentProof(long orderId);
        void Cancel(long id);
        List<OrderViewModel> Search(OrderSearchModel searchModel);
        List<OrderItemViewModel> GetItemsBy(long orderId);
    }
}




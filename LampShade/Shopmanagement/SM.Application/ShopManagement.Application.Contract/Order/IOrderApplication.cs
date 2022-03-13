﻿using System.Collections.Generic;

namespace ShopManagement.Application.Contract.Order
{
    public interface IOrderApplication
    {
       long PlaceOrder(Cart cart);
       double GetAmountBy(long id);
       string PaymentSucceeded(long orderId,long refId);
       List<OrderViewModel> Search(OrderSearchModel searchModel);
    }
}
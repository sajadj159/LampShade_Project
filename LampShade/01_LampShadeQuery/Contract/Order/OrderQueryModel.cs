using System.Collections.Generic;

namespace _01_LampShadeQuery.Contract.Order
{
    public class OrderQueryModel
    {
        public long Id { get; set; }
        public int PaymentMethodId { get; set; }
        public double PayAmount { get; set; }
        public bool IsPaid { get; set; }

    }
}
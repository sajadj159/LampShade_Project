namespace _01_LampShadeQuery.Contract.Order
{
    public class OrderQueryModel
    {
        public long Id { get; set; }
        public int PaymentMethodId { get; set; }
        public double PayAmount { get; set; }
        public bool IsPaid { get; set; }
        public long AccountId { get; set; }
        public double TotalAmount { get; set; }
        public double DiscountAmount { get; set; }
        public string IssueTrackingNo { get; set; }
        public string PayDate { get; set; }

    }


}
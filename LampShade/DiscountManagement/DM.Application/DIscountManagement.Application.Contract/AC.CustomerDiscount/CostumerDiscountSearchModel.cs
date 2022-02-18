namespace DiscountManagement.Application.Contract.AC.CustomerDiscount
{
    public class CostumerDiscountSearchModel
    {
        public long ProductId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
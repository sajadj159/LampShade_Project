using System.Collections.Generic;
using ShopManagement.Application.Contract.A.Product;

namespace DiscountManagement.Application.Contract.AC.ColleagueDiscount
{
    public class DefineColleagueDiscount
    {
        public long ProductId { get; set; }
        public int DiscountRate { get; set; }
        public List<ProductViewModel> Products { get; set; }

    }
}
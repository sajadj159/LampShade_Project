using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using ShopManagement.Application.Contract.A.Product;

namespace DiscountManagement.Application.Contract.AC.ColleagueDiscount
{
    public class DefineColleagueDiscount
    {
        [Range(1,100000,ErrorMessage = ValidationMessages.IsRequired)]
        public long ProductId { get; set; }

        [Range(1,99,ErrorMessage = ValidationMessages.IsRequired)]
        public int DiscountRate { get; set; }
        public List<ProductViewModel> Products { get; set; }

    }
}
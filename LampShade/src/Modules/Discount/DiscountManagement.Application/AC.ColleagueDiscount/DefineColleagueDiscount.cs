using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LampShade.BuildingBlocks.Application;

namespace DiscountManagement.Application.Contract.AC.ColleagueDiscount
{
    public class DefineColleagueDiscount
    {
        [Range(1,100000,ErrorMessage = ValidationMessages.IsRequired)]
        public long ProductId { get; set; }

        [Range(1,99,ErrorMessage = ValidationMessages.IsRequired)]
        public int DiscountRate { get; set; }

    }
}
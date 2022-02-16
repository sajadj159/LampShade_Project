using System.Collections.Generic;
using _0_Framework.Domain;
using ShopManagement.Application.Contract.A.Slide;

namespace ShopManagement.Domain.SlideAgg
{
    public interface ISlideRepository : IRepository<long, Slide>
    {
        EditSlide GetDetails(long id);
        List<SlideViewModel> GetList();
    }
}
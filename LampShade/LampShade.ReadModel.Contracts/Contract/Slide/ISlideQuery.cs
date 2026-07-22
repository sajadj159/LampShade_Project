using System.Collections.Generic;

namespace LampShade.ReadModel.Contracts.Slide
{
    public interface ISlideQuery
    {
        List<SlideQueryModel> GetSlides();
    }
}
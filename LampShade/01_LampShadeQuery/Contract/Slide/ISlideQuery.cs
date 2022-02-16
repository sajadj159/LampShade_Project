using System.Collections.Generic;

namespace _01_LampShadeQuery.Contract.Slide
{
    public interface ISlideQuery
    {
        List<SlideQueryModel> GetSlides();
    }
}
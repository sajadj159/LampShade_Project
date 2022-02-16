using System.Collections.Generic;
using System.Linq;
using _01_LampShadeQuery.Contract.Slide;
using ShopManagement.Infrastructure.EFCore;

namespace _01_LampShadeQuery.Query
{
    public class SlideQuery:ISlideQuery
    {
        private readonly ShopContext _context;

        public SlideQuery(ShopContext context)
        {
            _context = context;
        }

        public List<SlideQueryModel> GetSlides()
        {
            return _context.Slides
                .Where(x => x.IsRemoved == false)
                .Select(x => new SlideQueryModel
                {
                    Title = x.Title,
                    Heading = x.Heading,
                    Link = x.Link,
                    BtnText = x.BtnText,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    PictureUrl = x.PictureUrl,
                    Text = x.Text
                }).ToList();
        }    
    }
}
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Repository;
using ShopManagement.Application.Contract.A.Slide;
using ShopManagement.Domain.SlideAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class SlideRepository : RepositoryBase<long, Slide>, ISlideRepository
    {
        private readonly ShopContext _context;
        public SlideRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public EditSlide GetDetails(long id)
        {
            return _context.Slides.Select(x => new EditSlide
            {
                Id = x.Id,
                Title = x.Title,
                Heading = x.Heading,
                PictureTitle = x.PictureTitle,
                PictureAlt = x.PictureAlt,
                PictureUrl = x.PictureUrl,
                Text = x.Text,
                BtnText = x.BtnText,
                Link = x.Link
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<SlideViewModel> GetList()
        {
            return _context.Slides.Select(x => new SlideViewModel
            {
                Id = x.Id,
                Heading = x.Heading,
                PictureUrl = x.PictureUrl,
                CreationDate = x.CreationDate.ToFarsi(),
                Title = x.Title,
                IsRemoved = x.IsRemoved
            }).OrderByDescending(x => x.Id).ToList();
        }
    }
}
using LampShade.ReadModel.Contracts.Slide;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EFCore;

namespace LampShade.ReadModel.Application.Query;

public class SlideQuery(ShopContext context) : ISlideQuery
{
    public Task<List<SlideQueryModel>> GetSlidesAsync(CancellationToken cancellationToken = default) =>
        context.Slides.AsNoTracking().Where(x => !x.IsRemoved).Select(x => new SlideQueryModel { Id = x.Id, Title = x.Title, Heading = x.Heading, Link = x.Link, BtnText = x.BtnText, PictureAlt = x.PictureAlt, PictureTitle = x.PictureTitle, PictureUrl = x.PictureUrl, Text = x.Text }).ToListAsync(cancellationToken);
}
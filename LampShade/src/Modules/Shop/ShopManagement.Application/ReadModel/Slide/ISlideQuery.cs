namespace ShopManagement.Application.ReadModel.Slide;

public interface ISlideQuery
{
    Task<List<SlideQueryModel>> GetSlidesAsync(CancellationToken cancellationToken = default);
}
namespace LampShade.ReadModel.Contracts.Slide;

public interface ISlideQuery
{
    Task<List<SlideQueryModel>> GetSlidesAsync(CancellationToken cancellationToken = default);
}
using LampShade.BuildingBlocks.Domain;

namespace BlogManagement.Domain.ArticleCategoryAgg
{
    public interface IArticleCategoryRepository : IRepository<long, ArticleCategory>
    {
        Task<string> GetSlugByAsync(long id, CancellationToken cancellationToken = default);
    }
}

using LampShade.BuildingBlocks.Domain;

namespace BlogManagement.Domain.ArticleAgg
{
    public interface IArticleRepository : IRepository<long , Article>
    {
        Task<Article> GetWithCategoryAsync(long id, CancellationToken cancellationToken = default);
    }
}

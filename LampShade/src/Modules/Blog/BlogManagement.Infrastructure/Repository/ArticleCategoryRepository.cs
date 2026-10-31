using BlogManagement.Domain.ArticleCategoryAgg;
using Microsoft.EntityFrameworkCore;

namespace BlogManagement.Infrastructure.EFCore.Repository
{
    public class ArticleCategoryRepository(BlogContext context) : RepositoryBase<long, ArticleCategory>(context), IArticleCategoryRepository
    {
        public Task<string> GetSlugByAsync(long id, CancellationToken cancellationToken = default) =>
            context.ArticleCategories.AsNoTracking().Where(x => x.Id == id).Select(x => x.Slug).FirstOrDefaultAsync(cancellationToken);
    }
}

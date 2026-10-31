using BlogManagement.Domain.ArticleAgg;
using Microsoft.EntityFrameworkCore;

namespace BlogManagement.Infrastructure.EFCore.Repository
{
    public class ArticleRepository(BlogContext context) : RepositoryBase<long, Article>(context), IArticleRepository
    {
        public Task<Article> GetWithCategoryAsync(long id, CancellationToken cancellationToken = default) =>
            context.Articles.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}

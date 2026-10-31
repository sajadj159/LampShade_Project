using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using _0_Framework.Domain;
using BlogManagement.Application.Contract.AC.Article;

namespace BlogManagement.Domain.ArticleAgg
{
    public interface IArticleRepository : IRepository<long , Article>
    {
        Article GetWithCategory(long id);
        Task<Article> GetWithCategoryAsync(long id, CancellationToken cancellationToken = default);
        List<ArticleViewModel> Search(ArticleSearchModel searchModel);
        EditArticle GetDetails(long id);
    }
}

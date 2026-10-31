using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using _0_Framework.Domain;
using BlogManagement.Application.Contract.AC.ArticleCategory;

namespace BlogManagement.Domain.ArticleCategoryAgg
{
    public interface IArticleCategoryRepository : IRepository<long, ArticleCategory>
    {
        string GetSlugBy(long id);
        Task<string> GetSlugByAsync(long id, CancellationToken cancellationToken = default);
        List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel);
        List<ArticleCategoryViewModel> GetArticleCategories();
        EditArticleCategory GetDetails(long id);
    }
}

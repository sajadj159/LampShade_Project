using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using _0_Framework.Application;

namespace BlogManagement.Application.Contract.AC.Article
{
    public interface IArticleApplication
    {
        Task<OperationResult> CreateAsync(CreateArticle command, CancellationToken cancellationToken = default);
        Task<OperationResult> EditAsync(EditArticle command, CancellationToken cancellationToken = default);
        List<ArticleViewModel> Search(ArticleSearchModel searchModel);
        EditArticle GetDetails(long id);
    }
}

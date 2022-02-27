using System.Collections.Generic;
using _0_Framework.Domain;
using BlogManagement.Application.Contract.AC.Article;

namespace BlogManagement.Domain.ArticleAgg
{
    public interface IArticleRepository : IRepository<long , Article>
    {
        Article GetWithCategory(long id);
        List<ArticleViewModel> Search(ArticleSearchModel searchModel);
        EditArticle GetDetails(long id);
    }
}
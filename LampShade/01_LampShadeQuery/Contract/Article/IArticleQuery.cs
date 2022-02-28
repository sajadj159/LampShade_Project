using System.Collections.Generic;

namespace _01_LampShadeQuery.Contract.Article
{
    public interface IArticleQuery
    {
        List<ArticleQueryModel> LatestArticles();
    }
}
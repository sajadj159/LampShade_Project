using System.Collections.Generic;

namespace _01_LampShadeQuery.Contract.Article
{
    public interface IArticleQuery
    {
        ArticleQueryModel GetArticleDetails(string value);
        List<ArticleQueryModel> LatestArticles();
    }
}
using System.Collections.Generic;

namespace LampShade.ReadModel.Contracts.Article
{
    public interface IArticleQuery
    {
        ArticleQueryModel GetArticleDetails(string value);
        List<ArticleQueryModel> LatestArticles();
    }
}
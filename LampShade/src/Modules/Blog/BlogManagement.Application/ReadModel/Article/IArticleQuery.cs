using BlogManagement.Application.Contract.AC.Article;

namespace BlogManagement.Application.ReadModel.Article;

public interface IArticleQuery
{
    Task<ArticleQueryModel> GetArticleDetailsAsync(string value, CancellationToken cancellationToken = default);
    Task<List<ArticleQueryModel>> LatestArticlesAsync(CancellationToken cancellationToken = default);
    Task<List<ArticleViewModel>> SearchAsync(ArticleSearchModel searchModel, CancellationToken cancellationToken = default);
    Task<EditArticle> GetDetailsAsync(long id, CancellationToken cancellationToken = default);
}

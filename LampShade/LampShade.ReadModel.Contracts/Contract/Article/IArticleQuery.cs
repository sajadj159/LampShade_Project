namespace LampShade.ReadModel.Contracts.Article;

public interface IArticleQuery
{
    Task<ArticleQueryModel> GetArticleDetailsAsync(string value, CancellationToken cancellationToken = default);
    Task<List<ArticleQueryModel>> LatestArticlesAsync(CancellationToken cancellationToken = default);
}
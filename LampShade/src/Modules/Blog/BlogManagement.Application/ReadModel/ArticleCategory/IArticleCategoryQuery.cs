using BlogManagement.Application.ReadModel.ArticleCategories.Dto;

namespace BlogManagement.Application.ReadModel.ArticleCategory;

public interface IArticleCategoryQuery
{
    Task<ArticleCategoryQueryModel> GetArticleCategoryAsync(string slug, CancellationToken cancellationToken = default);
    Task<List<ArticleCategoryQueryModel>> GetArticleCategoriesAsync(CancellationToken cancellationToken = default);
    Task<ArticleCategoryDetailsDto> GetArticleCategoryForManagementAsync(long id, CancellationToken cancellationToken = default);
    Task<List<ArticleCategoryViewModel>> GetArticleCategoriesForManagementAsync(CancellationToken cancellationToken = default);
    Task<List<ArticleCategoryViewModel>> SearchArticleCategoriesAsync(string name, CancellationToken cancellationToken = default);
}
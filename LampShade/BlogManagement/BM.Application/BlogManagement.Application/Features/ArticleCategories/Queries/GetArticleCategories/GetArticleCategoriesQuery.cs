using BlogManagement.Application.Contract.AC.ArticleCategory;
using MediatR;

namespace BlogManagement.Application.Features.ArticleCategories.Queries.GetArticleCategories;

public class GetArticleCategoriesQuery : IRequest<List<ArticleCategoryViewModel>> { }

public class GetArticleCategoriesQueryHandler : IRequestHandler<GetArticleCategoriesQuery, List<ArticleCategoryViewModel>>
{
    private readonly IArticleCategoryApplication _application;
    public GetArticleCategoriesQueryHandler(IArticleCategoryApplication application) => _application = application;
    public Task<List<ArticleCategoryViewModel>> Handle(GetArticleCategoriesQuery r, CancellationToken c) => Task.FromResult(_application.GetArticleCategories());
}

using BlogManagement.Application.Contract.AC.ArticleCategory;
using MediatR;

namespace LampShade.Api.Features.ArticleCategories.Queries.GetArticleCategoryById;

public class GetArticleCategoryByIdQuery : IRequest<EditArticleCategory>
{
    public long Id { get; set; }
}

public class GetArticleCategoryByIdQueryHandler : IRequestHandler<GetArticleCategoryByIdQuery, EditArticleCategory>
{
    private readonly IArticleCategoryApplication _application;
    public GetArticleCategoryByIdQueryHandler(IArticleCategoryApplication application) => _application = application;
    public async Task<EditArticleCategory> Handle(GetArticleCategoryByIdQuery r, CancellationToken c) => await Task.FromResult(_application.GetDetails(r.Id));
}

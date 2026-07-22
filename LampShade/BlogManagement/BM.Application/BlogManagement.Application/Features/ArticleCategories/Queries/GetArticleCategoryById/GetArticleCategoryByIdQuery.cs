using BlogManagement.Application.Contract.AC.ArticleCategory;
using MediatR;

namespace BlogManagement.Application.Features.ArticleCategories.Queries.GetArticleCategoryById;

public class GetArticleCategoryByIdQuery : IRequest<EditArticleCategory>
{
    public long Id { get; set; }
}

public class GetArticleCategoryByIdQueryHandler : IRequestHandler<GetArticleCategoryByIdQuery, EditArticleCategory>
{
    private readonly IArticleCategoryApplication _application;
    public GetArticleCategoryByIdQueryHandler(IArticleCategoryApplication application) => _application = application;
    public Task<EditArticleCategory> Handle(GetArticleCategoryByIdQuery r, CancellationToken c) => Task.FromResult(_application.GetDetails(r.Id));
}

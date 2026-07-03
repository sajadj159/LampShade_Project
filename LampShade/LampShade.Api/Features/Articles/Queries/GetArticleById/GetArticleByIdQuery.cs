using BlogManagement.Application.Contract.AC.Article;
using MediatR;

namespace LampShade.Api.Features.Articles.Queries.GetArticleById;

public class GetArticleByIdQuery : IRequest<EditArticle>
{
    public long Id { get; set; }
}

public class GetArticleByIdQueryHandler : IRequestHandler<GetArticleByIdQuery, EditArticle>
{
    private readonly IArticleApplication _articleApplication;

    public GetArticleByIdQueryHandler(IArticleApplication articleApplication)
    {
        _articleApplication = articleApplication;
    }

    public async Task<EditArticle> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_articleApplication.GetDetails(request.Id));
    }
}

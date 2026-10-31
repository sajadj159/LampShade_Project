using System.Threading;
using System.Threading.Tasks;
using BlogManagement.Application.Contract.AC.Article;
using MediatR;

using LampShade.ReadModel.Contracts.Queries.Articles.GetArticleById;

namespace LampShade.ReadModel.Application.Features.Articles.Queries.GetArticleById;



public class GetArticleByIdQueryHandler : IRequestHandler<GetArticleByIdQuery, EditArticle>
{
    private readonly IArticleApplication _articleApplication;

    public GetArticleByIdQueryHandler(IArticleApplication articleApplication)
    {
        _articleApplication = articleApplication;
    }

    public Task<EditArticle> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_articleApplication.GetDetails(request.Id));
    }
}

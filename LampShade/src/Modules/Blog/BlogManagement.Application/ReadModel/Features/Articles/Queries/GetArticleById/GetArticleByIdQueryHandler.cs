using BlogManagement.Application.Contract.AC.Article;
using BlogManagement.Application.ReadModel.Article;
using BlogManagement.Application.ReadModel.Queries.Articles.GetArticleById;
using MediatR;

namespace BlogManagement.Application.ReadModel.Features.Articles.Queries.GetArticleById;

public class GetArticleByIdQueryHandler(IArticleQuery query) : IRequestHandler<GetArticleByIdQuery, EditArticle>
{
    public Task<EditArticle> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken) => query.GetDetailsAsync(request.Id, cancellationToken);
}

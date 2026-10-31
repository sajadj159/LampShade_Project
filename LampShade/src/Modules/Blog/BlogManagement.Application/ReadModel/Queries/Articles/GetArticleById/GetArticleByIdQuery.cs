using BlogManagement.Application.Contract.AC.Article;
using MediatR;

namespace LampShade.ReadModel.Contracts.Queries.Articles.GetArticleById;

public class GetArticleByIdQuery : IRequest<EditArticle>
{
    public long Id { get; set; }
}

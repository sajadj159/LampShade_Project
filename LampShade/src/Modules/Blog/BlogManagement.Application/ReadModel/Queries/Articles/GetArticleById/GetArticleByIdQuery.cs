using BlogManagement.Application.Contract.AC.Article;
using MediatR;

namespace BlogManagement.Application.ReadModel.Queries.Articles.GetArticleById;

public class GetArticleByIdQuery : IRequest<EditArticle>
{
    public long Id { get; set; }
}

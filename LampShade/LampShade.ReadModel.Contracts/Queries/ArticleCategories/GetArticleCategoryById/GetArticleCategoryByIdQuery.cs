using BlogManagement.Application.Contract.AC.ArticleCategory;
using MediatR;

namespace LampShade.ReadModel.Contracts.Queries.ArticleCategories.GetArticleCategoryById;

public class GetArticleCategoryByIdQuery : IRequest<EditArticleCategory>
{
    public long Id { get; set; }
}

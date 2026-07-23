using LampShade.ReadModel.Contracts.ArticleCategories.Dto;
using MediatR;

namespace LampShade.ReadModel.Contracts.Queries.ArticleCategories.GetArticleCategoryById;

public class GetArticleCategoryByIdQuery : IRequest<ArticleCategoryDetailsDto>
{
    public long Id { get; set; }
}

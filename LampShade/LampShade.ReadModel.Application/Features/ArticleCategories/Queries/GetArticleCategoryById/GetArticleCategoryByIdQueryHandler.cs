using LampShade.ReadModel.Contracts.ArticleCategory;
using LampShade.ReadModel.Contracts.ArticleCategories.Dto;
using LampShade.ReadModel.Contracts.Queries.ArticleCategories.GetArticleCategoryById;
using MediatR;

namespace LampShade.ReadModel.Application.Features.ArticleCategories.Queries.GetArticleCategoryById;

public class GetArticleCategoryByIdQueryHandler(IArticleCategoryQuery query) : IRequestHandler<GetArticleCategoryByIdQuery, ArticleCategoryDetailsDto>
{
    public Task<ArticleCategoryDetailsDto> Handle(GetArticleCategoryByIdQuery request, CancellationToken cancellationToken) =>
        Task.FromResult(query.GetArticleCategoryForManagement(request.Id));
}

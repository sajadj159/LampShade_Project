using System.Threading;
using System.Threading.Tasks;
using BlogManagement.Application.ReadModel.ArticleCategory;
using BlogManagement.Application.ReadModel.ArticleCategories.Dto;
using BlogManagement.Application.ReadModel.Queries.ArticleCategories.GetArticleCategoryById;
using MediatR;

namespace BlogManagement.Application.ReadModel.Features.ArticleCategories.Queries.GetArticleCategoryById;

public class GetArticleCategoryByIdQueryHandler(IArticleCategoryQuery query) : IRequestHandler<GetArticleCategoryByIdQuery, ArticleCategoryDetailsDto>
{
    public Task<ArticleCategoryDetailsDto> Handle(GetArticleCategoryByIdQuery request, CancellationToken cancellationToken) =>
        query.GetArticleCategoryForManagementAsync(request.Id, cancellationToken);
}

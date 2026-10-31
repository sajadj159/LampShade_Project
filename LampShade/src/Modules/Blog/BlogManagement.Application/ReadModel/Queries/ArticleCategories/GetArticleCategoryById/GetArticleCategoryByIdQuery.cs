using BlogManagement.Application.ReadModel.ArticleCategories.Dto;
using MediatR;

namespace BlogManagement.Application.ReadModel.Queries.ArticleCategories.GetArticleCategoryById;

public class GetArticleCategoryByIdQuery : IRequest<ArticleCategoryDetailsDto>
{
    public long Id { get; set; }
}

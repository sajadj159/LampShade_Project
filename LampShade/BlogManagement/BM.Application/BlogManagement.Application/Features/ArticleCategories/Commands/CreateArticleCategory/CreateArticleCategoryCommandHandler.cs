#nullable enable

using BlogManagement.Application.Contract.AC.ArticleCategory;
using MediatR;
using Microsoft.AspNetCore.Http;
using _0_Framework.Application;

using BlogManagement.Application.Contracts.Commands.ArticleCategories.CreateArticleCategory;

namespace BlogManagement.Application.Features.ArticleCategories.Commands.CreateArticleCategory;



public class CreateArticleCategoryCommandHandler : IRequestHandler<CreateArticleCategoryCommand, OperationResult>
{
    private readonly IArticleCategoryApplication _application;
    public CreateArticleCategoryCommandHandler(IArticleCategoryApplication application) => _application = application;

    public Task<OperationResult> Handle(CreateArticleCategoryCommand request, CancellationToken cancellationToken)
    {
        var command = new BlogManagement.Application.Contract.AC.ArticleCategory.CreateArticleCategory
        {
            Name = request.Name, PictureUrl = request.PictureUrl, PictureAlt = request.PictureAlt,
            PictureTitle = request.PictureTitle, Description = request.Description,
            ShowOrder = request.ShowOrder, Slug = request.Slug, Keywords = request.Keywords,
            MetaDescription = request.MetaDescription, CanonicalAddress = request.CanonicalAddress
        };
        return Task.FromResult(_application.Create(command));
    }
}

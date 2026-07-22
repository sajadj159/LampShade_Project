#nullable enable

using BlogManagement.Application.Contract.AC.ArticleCategory;
using MediatR;
using Microsoft.AspNetCore.Http;
using _0_Framework.Application;

using BlogManagement.Application.Contracts.Commands.ArticleCategories.EditArticleCategory;

namespace BlogManagement.Application.Features.ArticleCategories.Commands.EditArticleCategory;



public class EditArticleCategoryCommandHandler : IRequestHandler<EditArticleCategoryCommand, OperationResult>
{
    private readonly IArticleCategoryApplication _application;
    public EditArticleCategoryCommandHandler(IArticleCategoryApplication application) => _application = application;

    public Task<OperationResult> Handle(EditArticleCategoryCommand request, CancellationToken cancellationToken)
    {
        var command = new BlogManagement.Application.Contract.AC.ArticleCategory.EditArticleCategory
        {
            Id = request.Id, Name = request.Name, PictureUrl = request.PictureUrl, PictureAlt = request.PictureAlt,
            PictureTitle = request.PictureTitle, Description = request.Description,
            ShowOrder = request.ShowOrder, Slug = request.Slug, Keywords = request.Keywords,
            MetaDescription = request.MetaDescription, CanonicalAddress = request.CanonicalAddress
        };
        return Task.FromResult(_application.Edit(command));
    }
}

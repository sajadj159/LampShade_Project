using System.Threading;
using System.Threading.Tasks;
#nullable enable

using BlogManagement.Application.Contract.AC.Article;
using MediatR;
using Microsoft.AspNetCore.Http;
using _0_Framework.Application;

using BlogManagement.Application.Contracts.Commands.Articles.EditArticle;

namespace BlogManagement.Application.Features.Articles.Commands.EditArticle;



public class EditArticleCommandHandler : IRequestHandler<EditArticleCommand, OperationResult>
{
    private readonly IArticleApplication _articleApplication;

    public EditArticleCommandHandler(IArticleApplication articleApplication)
    {
        _articleApplication = articleApplication;
    }

    public async Task<OperationResult> Handle(EditArticleCommand request, CancellationToken cancellationToken)
    {
        var command = new BlogManagement.Application.Contract.AC.Article.EditArticle
        {
            Id = request.Id,
            Title = request.Title,
            ShortDescription = request.ShortDescription,
            Description = request.Description,
            PictureUrl = request.PictureUrl,
            PictureTitle = request.PictureTitle,
            PictureAlt = request.PictureAlt,
            PublishDate = request.PublishDate,
            Slug = request.Slug,
            Keywords = request.Keywords,
            MetaDescription = request.MetaDescription,
            CanonicalAddress = request.CanonicalAddress,
            CategoryId = request.CategoryId
        };
        return await _articleApplication.EditAsync(command, cancellationToken);
    }
}

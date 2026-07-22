#nullable enable

using BlogManagement.Application.Contract.AC.Article;
using MediatR;
using Microsoft.AspNetCore.Http;
using _0_Framework.Application;

using BlogManagement.Application.Contracts.Commands.Articles.CreateArticle;

namespace BlogManagement.Application.Features.Articles.Commands.CreateArticle;



public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, OperationResult>
{
    private readonly IArticleApplication _articleApplication;

    public CreateArticleCommandHandler(IArticleApplication articleApplication)
    {
        _articleApplication = articleApplication;
    }

    public Task<OperationResult> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        var command = new BlogManagement.Application.Contract.AC.Article.CreateArticle
        {
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
        return Task.FromResult(_articleApplication.Create(command));
    }
}

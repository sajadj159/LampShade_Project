using BlogManagement.Application.Contracts.Commands.Articles.CreateArticle;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Domain.ArticleCategoryAgg;
using LampShade.BuildingBlocks.Application;
using MediatR;

namespace BlogManagement.Application.Features.Articles.Commands.CreateArticle;

public class CreateArticleCommandHandler(IArticleRepository articles, IArticleCategoryRepository categories, IFileUploader uploader, IHtmlSanitizer sanitizer)
    : IRequestHandler<CreateArticleCommand, OperationResult>
{
    public async Task<OperationResult> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult();
        if (await articles.ExistAsync(x => x.Title == request.Title, cancellationToken))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);

        var slug = request.Slug.Slugify();
        var categorySlug = await categories.GetSlugByAsync(request.CategoryId, cancellationToken);
        var picturePath = uploader.Upload(request.PictureUrl, $"{categorySlug}/{slug}");

        articles.Add(new Article(request.Title, request.ShortDescription, sanitizer.Sanitize(request.Description), picturePath,
            request.PictureTitle, request.PictureAlt, request.PublishDate.ToGeorgianDateTime(), slug, request.Keywords,
            request.MetaDescription, request.CanonicalAddress, request.CategoryId));
        return operation.Succeeded();
    }
}

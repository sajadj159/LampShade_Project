using System.Threading;
using System.Threading.Tasks;
using _0_Framework.Application;
using BlogManagement.Application.Contracts.Commands.ArticleCategories.CreateArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;
using MediatR;

namespace BlogManagement.Application.Features.ArticleCategories.Commands.CreateArticleCategory;

public class CreateArticleCategoryCommandHandler(IArticleCategoryRepository articleCategoryRepository, IFIleUploader uploader) : IRequestHandler<CreateArticleCategoryCommand, OperationResult>
{
    public async Task<OperationResult> Handle(CreateArticleCategoryCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult();
        if (await articleCategoryRepository.ExistAsync(x => x.Name == request.Name, cancellationToken)) return operation.Failed(ApplicationMessages.DuplicatedRecord);
        var slug = request.Slug.Slugify();
        articleCategoryRepository.Add(new ArticleCategory(request.Name, uploader.Upload(request.PictureUrl, slug), request.PictureAlt, request.PictureTitle, request.Description, request.ShowOrder, slug, request.Keywords, request.MetaDescription, request.CanonicalAddress)); return operation.Succeeded();
    }
}

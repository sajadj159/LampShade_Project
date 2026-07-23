using System.Threading;
using System.Threading.Tasks;
using _0_Framework.Application;
using BlogManagement.Application.Contracts.Commands.ArticleCategories.EditArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;
using MediatR;

namespace BlogManagement.Application.Features.ArticleCategories.Commands.EditArticleCategory;

public class EditArticleCategoryCommandHandler(IArticleCategoryRepository articleCategoryRepository, IFIleUploader uploader) : IRequestHandler<EditArticleCategoryCommand, OperationResult>
{
    public async Task<OperationResult> Handle(EditArticleCategoryCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult(); var category = await articleCategoryRepository.GetAsync(request.Id, cancellationToken);
        if (category is null) return operation.Failed(ApplicationMessages.RecordNotFound);
        if (await articleCategoryRepository.ExistAsync(x => x.Name == request.Name && x.Id != request.Id, cancellationToken)) return operation.Failed(ApplicationMessages.DuplicatedRecord);
        var slug = request.Slug.Slugify(); category.Edit(request.Name, uploader.Upload(request.PictureUrl, slug), request.PictureAlt, request.PictureTitle, request.Description, request.ShowOrder, slug, request.Keywords, request.MetaDescription, request.CanonicalAddress); return operation.Succeeded();
    }
}

using System.Collections.Generic;
using _0_Framework.Application;
using BlogManagement.Application.Contract.AC.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Application.A.ArticleCategory
{
    public class ArticleCategoryApplication : IArticleCategoryApplication
    {
        private readonly IArticleCategoryRepository _articleCategoryRepository;
        private readonly IFIleUploader _uploader;

        public ArticleCategoryApplication(IArticleCategoryRepository articleCategoryRepository, IFIleUploader uploader)
        {
            _articleCategoryRepository = articleCategoryRepository;
            _uploader = uploader;
        }

        public OperationResult Create(CreateArticleCategory command)
        {
            var operationResult = new OperationResult();
            if (_articleCategoryRepository.Exist(x => x.Name == command.Name))
            {
               return  operationResult.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var slugify = command.Slug.Slugify();
            var picturePath = _uploader.Upload(command.PictureUrl, slugify);

            var articleCategory = new Domain.ArticleCategoryAgg.ArticleCategory(command.Name, picturePath,command.PictureAlt,command.PictureTitle,
                command.Description, command.ShowOrder, slugify, command.Keywords, command.MetaDescription,
                command.CanonicalAddress);

            _articleCategoryRepository.Create(articleCategory);
            _articleCategoryRepository.Save();
            return operationResult.Succeeded();
        }

        public OperationResult Edit(EditArticleCategory command)
        {
            var operationResult = new OperationResult();
            var articleCategory = _articleCategoryRepository.Get(command.Id);
            if (articleCategory == null)
            {
               return operationResult.Failed(ApplicationMessages.RecordNotFound);
            }

            if (_articleCategoryRepository.Exist(x => x.Name == command.Name && x.Id != command.Id))
            {
              return  operationResult.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var slugify = command.Slug.Slugify();
            var picturePath = _uploader.Upload(command.PictureUrl,slugify);

            articleCategory.Edit(command.Name,picturePath,command.PictureAlt,command.PictureTitle,
                command.Description,command.ShowOrder,slugify,command.Keywords,command.MetaDescription,
                command.CanonicalAddress);

            _articleCategoryRepository.Save();
           return operationResult.Succeeded();
        }

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            return _articleCategoryRepository.Search(searchModel);
        }

        public List<ArticleCategoryViewModel> GetArticleCategories()
        {
            return _articleCategoryRepository.GetArticleCategories();
        }

        public EditArticleCategory GetDetails(long id)
        {
            return _articleCategoryRepository.GetDetails(id);
        }
    }
}
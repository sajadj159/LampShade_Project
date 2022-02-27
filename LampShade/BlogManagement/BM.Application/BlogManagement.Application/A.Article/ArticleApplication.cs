using System.Collections.Generic;
using _0_Framework.Application;
using BlogManagement.Application.Contract.AC.Article;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Application.A.Article
{
    public class ArticleApplication : IArticleApplication
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IFIleUploader _uploader;
        private readonly IArticleCategoryRepository _articleCategoryRepository;

        public ArticleApplication(IArticleRepository articleRepository, IFIleUploader uploader, IArticleCategoryRepository articleCategoryRepository)
        {
            _articleRepository = articleRepository;
            _uploader = uploader;
            _articleCategoryRepository = articleCategoryRepository;
        }

        public OperationResult Create(CreateArticle command)
        {
            var operationResult = new OperationResult();
            if (_articleRepository.Exist(x => x.Title == command.Title))
            {
                operationResult.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var slugify = command.Slug.Slugify();
            var slugBy = _articleCategoryRepository.GetSlugBy(command.CategoryId);
            var path = $"{slugBy}/{slugify}";
            var picturePath = _uploader.Upload(command.PictureUrl, path);
            var publishDate = command.PublishDate.ToGeorgianDateTime();

            var article = new Domain.ArticleAgg.Article(command.Title, command.ShortDescription, command.Description, picturePath,
                command.PictureTitle, command.PictureAlt, publishDate, slugify, command.Keywords, command.MetaDescription, command.CanonicalAddress, command.CategoryId);
            
            _articleRepository.Create(article);
            _articleRepository.Save();
            return operationResult.Succeeded();
        }

        public OperationResult Edit(EditArticle command)
        {
            var operationResult = new OperationResult();
            var article = _articleRepository.GetWithCategory(command.Id);

            if (article == null)
            {
                operationResult.Failed(ApplicationMessages.RecordNotFound);
            }

            if (_articleRepository.Exist(x => x.Title == command.Title && x.Id != command.Id))
            {
                operationResult.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var slugify = command.Slug.Slugify();
            var path = $"{article.Category.Slug}/{slugify}";
            var picturePath = _uploader.Upload(command.PictureUrl, path);
            var publishDate = command.PublishDate.ToGeorgianDateTime();

            article.Edit(command.Title, command.ShortDescription, command.Description, picturePath,
                command.PictureTitle, command.PictureAlt, publishDate, slugify, command.Keywords,
                command.MetaDescription, command.CanonicalAddress, command.CategoryId);

            _articleRepository.Save();
            return operationResult.Succeeded();
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            return _articleRepository.Search(searchModel);
        }

        public EditArticle GetDetails(long id)
        {
            return _articleRepository.GetDetails(id);
        }
    }
}
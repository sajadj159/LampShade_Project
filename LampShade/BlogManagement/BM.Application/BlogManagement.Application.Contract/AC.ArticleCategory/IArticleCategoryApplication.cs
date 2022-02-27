using System.Collections.Generic;
using _0_Framework.Application;

namespace BlogManagement.Application.Contract.AC.ArticleCategory
{
    public interface IArticleCategoryApplication
    {
        OperationResult Create(CreateArticleCategory command);
        OperationResult Edit(EditArticleCategory command);
        List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel);
        List<ArticleCategoryViewModel> GetArticleCategories();
        EditArticleCategory GetDetails(long id);
    }
}
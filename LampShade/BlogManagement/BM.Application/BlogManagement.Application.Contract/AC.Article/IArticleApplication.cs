using System.Collections.Generic;
using _0_Framework.Application;

namespace BlogManagement.Application.Contract.AC.Article
{
    public interface IArticleApplication
    {
        OperationResult Create(CreateArticle command);
        OperationResult Edit(EditArticle command);
        List<ArticleViewModel> Search(ArticleSearchModel searchModel);
        EditArticle GetDetails(long id);
    }
}
using System.Collections.Generic;
using BlogManagement.Application.Contract.AC.ArticleCategory;

namespace BlogManagement.Application.Contract.AC.Article
{
    public class ArticleSearchModel
    {
        public string Title { get; set; }
        public long CategoryId { get; set; }
    }
}
using System.Collections.Generic;

namespace BlogManagement.Application.Contract.AC.Article
{
    public class ArticleSearchModel
    {
        public string Title { get; set; }
        public long CategoryId { get; set; }
    }
}
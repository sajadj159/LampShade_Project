﻿using System.Collections.Generic;

namespace _01_LampShadeQuery.Contract.Article
{
    public class ArticleQueryModel
    {
        public long Id { get; set; }
        public string Title { get;  set; }
        public string ShortDescription { get;  set; }
        public string Description { get;  set; }
        public string PictureUrl { get;  set; }
        public string PictureTitle { get;  set; }
        public string PictureAlt { get;  set; }
        public string PublishDate { get;  set; }
        public string Slug { get;  set; }
        public string Keywords { get;  set; }
        public List<string> KeywordsList { get; set; }
        public string MetaDescription { get;  set; }
        public string CanonicalAddress { get;  set; }
        public long CategoryId { get;  set; }
        public string CategoryName { get; set; }
        public string CategorySlug { get; set; }

    }
}
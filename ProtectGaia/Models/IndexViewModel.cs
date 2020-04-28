using System;
using System.Collections.Generic;
using System.Linq;

namespace ProtectGaia.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Article> ArticleList { get; set; }

        public IndexViewModel()
        {
            ArticleList = new  List<Article>();
        }

    }
    public class Article
    {
        public string ImageUrl { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Source { get; set; }
        public string PublishedDate { get; set; }
    }
}

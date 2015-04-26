using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Core.Entities
{
    public class NewsArticle
    {
        public string ArticleId { get; set; }

        public string ContentUrl { get; set; }

        public string HeaderImage { get; set; }

        public string Source { get; set; }

        public string AuthorName { get; set; }

        public string PublishDate { get; set; }

        public DateTime Created { get; set; }

        public string GridContent { get; set; }

        public int Rating { get; set; }

        public string Category { get; set; }

        public string Content { get; set; }
    }
}

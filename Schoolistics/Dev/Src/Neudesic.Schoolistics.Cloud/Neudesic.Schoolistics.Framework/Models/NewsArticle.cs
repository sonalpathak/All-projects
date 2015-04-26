using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.Storage.Table;namespace Neudesic.Schoolistics.Framework.Models
{
    public class NewsArticle:TableEntity
    {
        public string ArticleId
        {
            get { return this.PartitionKey; }
            set { this.PartitionKey = value; }
        }

        public string Category
        {
            get { return this.RowKey; }
            set { this.RowKey = value; }
        }
        public string ContentUrl { get; set; }

        public string Content { get; set; }

        public string HeaderImage { get; set; }

        public string Source { get; set; }

        public string AuthorName { get; set; }

        public string PublishDate { get; set; }

        public DateTime Created { get; set; }

        public string GridContent { get; set; }

        public int Rating { get; set; }
     }
}

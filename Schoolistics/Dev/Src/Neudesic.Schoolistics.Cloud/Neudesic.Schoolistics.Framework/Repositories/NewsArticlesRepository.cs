using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Neudesic.Schoolistics.Framework.Models;
using Neudesic.Schoolistics.Framework.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Framework.Repositories
{
    public class NewsArticlesRepository : INewsArticlesRepository
    {
        CloudStorageAccount storageAccount;
        CloudTableClient tableClient;
        CloudTable table;

        public NewsArticlesRepository()
        {

            if (RoleEnvironment.IsAvailable)
                storageAccount = CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("StorageConnectionString"));
            else
                storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);

            tableClient = storageAccount.CreateCloudTableClient();

            // Create the table if it doesn't exist.
            table = tableClient.GetTableReference(TableNames.NewsArticle);
            table.CreateIfNotExists();
        }
        public List<NewsArticle> AllNewsArticles()
        {
            List<NewsArticle> allNewsArticles = table.CreateQuery<NewsArticle>().ToList();

            foreach (var newsartcile in allNewsArticles)
            {
                newsartcile.Content = ReadTextFromFile("http://schoolisticstest1.blob.core.windows.net/text/" + newsartcile.ContentUrl);
            }

            return allNewsArticles;

        }

        public List<NewsArticle> LatestNewsArticles()
        {
            List<NewsArticle> popularNewsArtilce = table.CreateQuery<NewsArticle>().ToList().OrderByDescending(p => p.Created).Take(3).ToList();

            foreach (var newsartcile in popularNewsArtilce)
            {
                newsartcile.Content = ReadTextFromFile("http://schoolisticstest1.blob.core.windows.net/text/" + newsartcile.ContentUrl);
            }

            return popularNewsArtilce;
        }

        public string ReadTextFromFile(string uri)
        {
            var webClient = new WebClient();
            string readHtml = webClient.DownloadString(uri);

            return readHtml;
        }
    }
}

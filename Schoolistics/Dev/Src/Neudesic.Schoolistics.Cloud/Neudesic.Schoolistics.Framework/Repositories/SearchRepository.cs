using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Neudesic.Schoolistics.Framework.Models;
using Neudesic.Schoolistics.Framework.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Framework.Repositories
{
    public class SearchRepository :ISearchRepository
    {
        CloudStorageAccount storageAccount;
        CloudTableClient tableClient;
        CloudTable table;

        public SearchRepository()
        {
            if (RoleEnvironment.IsAvailable)
                storageAccount = CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("StorageConnectionString"));
            else
                storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);

            tableClient = storageAccount.CreateCloudTableClient();

            // Create the table if it doesn't exist.
            table = tableClient.GetTableReference(TableNames.Search);
            table.CreateIfNotExists();        
        }

        public bool Create(SearchDetails search)
        {
            search.SearchId = Guid.NewGuid().ToString();

            TableOperation insertOperation = TableOperation.Insert(search);

            table.Execute(insertOperation);

            return true;           
        }

        public List<SearchDetails> GetUserSearches(string username)
        {
            return table.CreateQuery<SearchDetails>().Where(p => p.PartitionKey == username).ToList();
        }
    }
}

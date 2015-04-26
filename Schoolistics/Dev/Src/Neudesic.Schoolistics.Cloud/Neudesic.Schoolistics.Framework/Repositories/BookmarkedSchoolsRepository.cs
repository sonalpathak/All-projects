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
    public class BookmarkedSchoolsRepository : IBookmarkedSchoolsRepository
    {
        CloudStorageAccount storageAccount;
        CloudTableClient tableClient;
        CloudTable table;

        public BookmarkedSchoolsRepository()
        {
            if (RoleEnvironment.IsAvailable)
                storageAccount = CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("StorageConnectionString"));
            else
                storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);

            tableClient = storageAccount.CreateCloudTableClient();

            // Create the table if it doesn't exist.
            table = tableClient.GetTableReference(TableNames.BookmarkedSchools);
            table.CreateIfNotExists();                
        }


        public bool Create(BookmarkedSchools school)
        {
            school.Created = DateTime.UtcNow;

            TableOperation insertOperation = TableOperation.Insert(school);

            table.Execute(insertOperation);

            return true;
        }

        public List<BookmarkedSchools> GetUserBookmarkedSchools(string username)
        {
            return table.CreateQuery<BookmarkedSchools>().Where(p => p.PartitionKey == username).ToList();
        }
    }
}

using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Neudesic.Schoolistics.Framework.API_Models;
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
    public class UserComparisonsRepository : IUserComparisonsRepository
    {
        CloudStorageAccount storageAccount;
        CloudTableClient tableClient;
        CloudTable table;

        public UserComparisonsRepository()
        {
            if (RoleEnvironment.IsAvailable)
                storageAccount = CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("StorageConnectionString"));
            else
                storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);

            tableClient = storageAccount.CreateCloudTableClient();

            // Create the table if it doesn't exist.
            table = tableClient.GetTableReference(TableNames.UserSchoolsComparisons);
            table.CreateIfNotExists();
        }

        public bool Create(UserSchoolsComparisons comparison)
        {
            TableOperation insertOperation = TableOperation.Insert(comparison);

            table.Execute(insertOperation);

            return true;  
        }

        public List<UserSchoolsComparisons> GetUserComparisons(string username)
        {
            return table.CreateQuery<UserSchoolsComparisons>().Where(p => p.PartitionKey == username).ToList();
        }

        public List<ComparisonCount> GetPopularComparisons()
        {
            return table.CreateQuery<UserSchoolsComparisons>().ToList().GroupBy(p => p.RowKey).Select(p => new ComparisonCount { ComparisonId = p.Key, Count = p.Distinct().Count() }).OrderByDescending(p=>p.Count).ToList();

        }
    }
}

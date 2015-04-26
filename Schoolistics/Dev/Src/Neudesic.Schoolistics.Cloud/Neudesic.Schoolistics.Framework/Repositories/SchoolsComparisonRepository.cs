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
    public class SchoolsComparisonRepository :ISchoolsComparisonRepository
    {
        CloudStorageAccount storageAccount;
        CloudTableClient tableClient;
        CloudTable table;

        public SchoolsComparisonRepository()
        {
            if (RoleEnvironment.IsAvailable)
                storageAccount = CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("StorageConnectionString"));
            else
                storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);

            tableClient = storageAccount.CreateCloudTableClient();

            // Create the table if it doesn't exist.
            table = tableClient.GetTableReference(TableNames.SchoolsComparison);
            table.CreateIfNotExists();
        }

        public string Create(List<SchoolsComparison> comparisons)
        {
            var comparisonsSameCount = this.GetComparisonsBySameCount(comparisons.Count());

            string comparisonIDFound = null;

            foreach (var comparison in comparisonsSameCount)
            {
                var schools = this.GetSchoolsByComparisonID(comparison.ComparisonId);
                bool comparisonExists = false;

                foreach (var school in comparisons)
                {
                    if (schools.Exists(p => p.SchoolId == school.SchoolId))
                        comparisonExists = true;
                    else
                    {
                        comparisonExists = false;
                        break;
                    }
                }

                if (comparisonExists)
                {
                    comparisonIDFound = comparison.ComparisonId;
                    break;
                }
            }

            if (comparisonIDFound == null)
            {
                comparisonIDFound = Guid.NewGuid().ToString();
            }

            foreach (var comparison in comparisons)
            {
                comparison.ComparisonId = comparisonIDFound;

                TableOperation insertOperation = TableOperation.Insert(comparison);

                table.Execute(insertOperation);
            }

            return comparisonIDFound;
        }

        public List<ComparisonCount> GetComparisonsBySameCount(int count)
        {
            return table.CreateQuery<SchoolsComparison>().ToList().GroupBy(p => p.PartitionKey).Select(r => new ComparisonCount { ComparisonId = r.Key, Count = r.Distinct().Count() }).Where(k => k.Count == count).ToList();
        }

        public List<SchoolsComparison> GetSchoolsByComparisonID(string comparisonID)
        {
            return table.CreateQuery<SchoolsComparison>().Where(p => p.PartitionKey == comparisonID).ToList();
        }
        //public List<SchoolsComparison> GetAllComparedSchools()
        //{
        //    //return table.CreateQuery<SchoolsComparison>().
        //}
    }
}

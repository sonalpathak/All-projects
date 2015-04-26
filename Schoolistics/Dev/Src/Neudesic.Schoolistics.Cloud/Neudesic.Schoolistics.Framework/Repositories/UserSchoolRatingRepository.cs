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
    public class UserSchoolRatingRepository :IUserSchoolRatingRepository
    {
        CloudStorageAccount storageAccount;
        CloudTableClient tableClient;
        CloudTable table;

        public UserSchoolRatingRepository()
        {
            if (RoleEnvironment.IsAvailable)
                storageAccount = CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("StorageConnectionString"));
            else
                storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);

            tableClient = storageAccount.CreateCloudTableClient();

            // Create the table if it doesn't exist.
            table = tableClient.GetTableReference(TableNames.UserSchoolRating);
            table.CreateIfNotExists();
        }

        public bool CreateUserSchoolRating(UserSchoolRating schoolRating)
        {
            try
            {
                schoolRating.Username = schoolRating.Username.ToLower();
                schoolRating.SchoolId = schoolRating.SchoolId.ToLower();
                TableOperation insertOperation = TableOperation.Insert(schoolRating);

                table.Execute(insertOperation);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public double? GetUserRating(string username, string schoolId)
        {
            var userRating = table.CreateQuery<UserSchoolRating>().Where(p => p.PartitionKey == username.ToLower() && p.RowKey == schoolId.ToLower()).FirstOrDefault();

            if (userRating != null)
            {
                return userRating.Rating;
            }
            else
                return null;
        }

    }
}

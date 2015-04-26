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
    public class UserSchoolCommentRepository :IUserSchoolCommentRepository
    {
        CloudStorageAccount storageAccount;
        CloudTableClient tableClient;
        CloudTable table;

        public UserSchoolCommentRepository()
        {
            if (RoleEnvironment.IsAvailable)
                storageAccount = CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("StorageConnectionString"));
            else
                storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);

            tableClient = storageAccount.CreateCloudTableClient();

            // Create the table if it doesn't exist.
            table = tableClient.GetTableReference(TableNames.UserSchoolComment);
            table.CreateIfNotExists();
        }

        public bool CreateUserSchoolComment(UserSchoolComment schoolComment)
        {
            schoolComment.CommentId = Guid.NewGuid().ToString();
            schoolComment.Created = DateTime.UtcNow;

            TableOperation insertOperation = TableOperation.Insert(schoolComment);

            table.Execute(insertOperation);

            return true;
        }

        public List<UserSchoolComment> GetSchoolComments(string schoolId)
        {
            return table.CreateQuery<UserSchoolComment>().Where(p => p.PartitionKey == schoolId).ToList();           
        }
    }
}

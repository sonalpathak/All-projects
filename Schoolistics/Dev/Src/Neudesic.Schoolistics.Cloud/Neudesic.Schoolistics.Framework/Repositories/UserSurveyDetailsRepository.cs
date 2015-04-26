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
    public class UserSurveyDetailsRepository:IUserSurveyDetailsRepository
    {
        CloudStorageAccount storageAccount;
        CloudTableClient tableClient;
        CloudTable table;

        public UserSurveyDetailsRepository()
        {

            if (RoleEnvironment.IsAvailable)
                storageAccount = CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("StorageConnectionString"));
            else
                storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);

            tableClient = storageAccount.CreateCloudTableClient();

            // Create the table if it doesn't exist.
            table = tableClient.GetTableReference(TableNames.UserSurveyDetails);
            table.CreateIfNotExists();
        }

        public List<UserSurveyDetails> userVotingDetails(string Username)
        {
            List<UserSurveyDetails> _userSurveyDetails = table.CreateQuery<UserSurveyDetails>().Where(x => x.UserName == Username).ToList();
            return _userSurveyDetails;
        }

        public bool UserVotedSurveyDetails(UserSurveyDetails _userVotedDetails)
        {
            if (table.CreateQuery<UserSurveyDetails>().Where(x => x.SurveyId == _userVotedDetails.SurveyId && x.UserName == _userVotedDetails.UserName).FirstOrDefault()!=null)
                return false;
            else
            {

                TableOperation insertOperation = TableOperation.Insert(_userVotedDetails);

                table.Execute(insertOperation);

                return true;
            }
        }
    }
}

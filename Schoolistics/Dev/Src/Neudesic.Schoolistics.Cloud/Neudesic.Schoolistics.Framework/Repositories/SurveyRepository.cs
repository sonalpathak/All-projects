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
    public class SurveyRepository : ISurveyRepository
    {
        CloudStorageAccount storageAccount;
        CloudTableClient tableClient;
        CloudTable table;

        public SurveyRepository()
        {

            if (RoleEnvironment.IsAvailable)
                storageAccount = CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("StorageConnectionString"));
            else
                storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);

            tableClient = storageAccount.CreateCloudTableClient();

            // Create the table if it doesn't exist.
            table = tableClient.GetTableReference(TableNames.Survey);
            table.CreateIfNotExists();
        }

        public List<SurveyInfo> GetAllOpenSurveys()
        {
            var openSurveys = table.CreateQuery<Survey>().ToList();

            return this.ConvertSurveyToSurveyInfo(openSurveys);
        }

        public List<SurveyInfo> GetAllClosedSurveys()
        {
            var closedSurveys = table.CreateQuery<Survey>().Where(p => p.IsClosed == true).ToList();

            return this.ConvertSurveyToSurveyInfo(closedSurveys);
        }

        public List<SurveyInfo> GetPopularSurveys()
        {
            var popularSurveys = table.CreateQuery<Survey>().ToList().GroupBy(p => p.PartitionKey).Select(p => new Survey { SurveyId = p.Key, Votes = p.Distinct().Sum(q => q.Votes) }).OrderByDescending(p => p.Votes).Take(2).ToList();

            List<Survey> surveys = new List<Survey>();

            foreach (var survey in popularSurveys)
            {
                surveys.AddRange(table.CreateQuery<Survey>().Where(p => p.SurveyId == survey.SurveyId).ToList());
            }

            var convertedSurveys = this.ConvertSurveyToSurveyInfo(surveys);

            return convertedSurveys;
        }

        public SurveyInfo GetSurveyByID(string id)
        {
            var survey = table.CreateQuery<Survey>().Where(p => p.SurveyId == id).ToList();

            var convertedSurvey = this.ConvertSurveyToSurveyInfo(survey).FirstOrDefault();

            return convertedSurvey;
        }

        public SurveyInfo VoteOnSurvey(string surveyId, string optionID)
        {
            //if(table.CreateQuery<UserSurveyDetails>().Where(x => x.SurveyId == _userVotedDetails.SurveyId && x.UserName == _userVotedDetails.UserName).FirstOrDefault()!=null)
            Survey survey = table.CreateQuery<Survey>().Where(p => p.SurveyId == surveyId && p.OptionId == optionID).FirstOrDefault();

            survey.Votes = survey.Votes + 1;

            TableOperation replaceOperation = TableOperation.Replace(survey);

            table.Execute(replaceOperation);

            List<Survey> tempSurvey = new List<Survey>();
            tempSurvey.Add(survey);

            var convertedSurvey = this.ConvertSurveyToSurveyInfo(tempSurvey).FirstOrDefault();

            return convertedSurvey;
        }

        public List<SurveyInfo> ConvertSurveyToSurveyInfo(List<Survey> surveys)
        {
            List<SurveyInfo> convertedSurveys = new List<SurveyInfo>();
            var selectedSurveys = surveys.GroupBy(item => item.PartitionKey)
     .Select(group => new { SurveyId = group.Key, surveys = group.ToList() })
     .ToList();
            var uniqueSurveys = surveys.GroupBy(p => p.PartitionKey).Select(q => new Survey { SurveyId = q.Key, Question=q.FirstOrDefault().Question,Created=q.FirstOrDefault().Created,IsClosed=q.FirstOrDefault().IsClosed }).ToList();
            foreach (var survey in uniqueSurveys)
            {
                SurveyInfo surveyInfo = new SurveyInfo();
                surveyInfo.SurveyId = survey.SurveyId;
                surveyInfo.Question = survey.Question;
                surveyInfo.IsClosed = survey.IsClosed;
                surveyInfo.Created = survey.Created;

                List<SurveyOption> options = new List<SurveyOption>();

                var relatedSurveyOptions = surveys.Where(p => p.SurveyId == surveyInfo.SurveyId);

                foreach (var relatedSurvey in relatedSurveyOptions)
                {
                    SurveyOption option = new SurveyOption();

                    option.Option = relatedSurvey.Option;
                    option.OptionId = relatedSurvey.OptionId;
                    option.Votes = relatedSurvey.Votes;

                    options.Add(option);
                }

                surveyInfo.Options = options;

                convertedSurveys.Add(surveyInfo);
            }

            return convertedSurveys;
        }

    }
}

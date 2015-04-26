using Neudesic.Schoolistics.Framework.API_Models;
using Neudesic.Schoolistics.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Framework.Repositories
{
    public interface ISurveyRepository
    {
        List<SurveyInfo> GetAllOpenSurveys();

        List<SurveyInfo> GetAllClosedSurveys();

        List<SurveyInfo> GetPopularSurveys();

        SurveyInfo GetSurveyByID(string id);

        SurveyInfo VoteOnSurvey(string surveyId, string optionID);
    }
}

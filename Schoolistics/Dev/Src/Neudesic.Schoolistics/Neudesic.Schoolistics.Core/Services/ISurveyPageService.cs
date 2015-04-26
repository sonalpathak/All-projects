using Neudesic.Schoolistics.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Core.Services
{
    public interface ISurveyPageService
    {
         //List<Survey> DisplaySurveyQuestions();
         void GetSurveyPageDetails(List<Survey> surveyDetails,Action<ResponseMessage<Object>> success, Action<Exception> exception);
         void GetPopularSurveyDetails(List<Survey> surveyDetails, Action<ResponseMessage<Object>> success, Action<Exception> exception);
         void VoteOnSurveyoption(string SurveyId,string OptionId, Action<ResponseMessage<Object>> success, Action<Exception> exception);
         void CheckUserVoteDetails(string UserName, Action<ResponseMessage<Object>> success, Action<Exception> exception);
         void SaveUserVotedDetails(UserSurveyDetails _userVotedDetails, Action<ResponseMessage<Object>> success, Action<Exception> exception);
    }
}

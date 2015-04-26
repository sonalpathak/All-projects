using Cirrious.CrossCore;
using Neudesic.Schoolistics.Core.Entities;
using Neudesic.Schoolistics.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Core.Services
{
    public class SurveyPageService : ISurveyPageService
    {
        IRestService restService;
        public SurveyPageService(IRestService _restService)
        {
            try
            {
                restService = _restService;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SurveyPageService.cs : Constructor: " + ex.ToString());
            }
        }

        public void GetSurveyPageDetails(List<Survey> surveyDetails,Action<ResponseMessage<Object>> success, Action<Exception> exception)
        {
            try
            {
                restService.MakeRequest<List<Survey>>(Constants.Survey, "GET", null, success, exception);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SurveyPageService.cs : GetSurveyPageDetails: " + ex.ToString());
            }
        }

        public void GetPopularSurveyDetails(List<Survey> surveyDetails, Action<ResponseMessage<Object>> success, Action<Exception> exception)
        {
            try
            {
                restService.MakeRequest<List<Survey>>(Constants.PopularSurveys, "GET", null, success, exception);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SurveyPageService.cs : GetPopularSurveyDetails: " + ex.ToString());
            }
        }
        public void VoteOnSurveyoption(string SurveyId,string OptionId, Action<ResponseMessage<Object>> success, Action<Exception> exception)
        {
            try
            {
                restService.MakeRequest<Object>(Utils.URLModifier.VoteOnSurvey(SurveyId, OptionId), "GET", null, success, exception);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SurveyPageService.cs : VoteOnSurveyoption: " + ex.ToString());
            }
        }
        public void CheckUserVoteDetails(string UserName,Action<ResponseMessage<Object>> success,Action<Exception> exception)
        {
            try
            {
                restService.MakeRequest<Object>(Utils.URLModifier.CheckUserVotes(UserName), "GET", null, success, exception);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SurveyPageService.cs : CheckUserVoteDetails: " + ex.ToString());
            }
        }
        public void SaveUserVotedDetails(UserSurveyDetails _userVotedDetails,Action<ResponseMessage<Object>> success,Action<Exception> exception)
        {
            try
            {
                restService.MakeRequest<Object>(Constants.UserVotedDetails, "POST", _userVotedDetails, success, exception);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SurveyPageService.cs : SaveUserVotedDetails: " + ex.ToString());
            }
        }
    }
}













//public List<Survey> DisplaySurveyQuestions()
//{
//    List<Survey> SurveyQuestions = new List<Survey>();
//    List<SurveyOption> optionforSId1 = new List<SurveyOption>();
//    optionforSId1.Add(new SurveyOption { Answer = "Expensive", Count = (10 * 100 / 40), });
//    optionforSId1.Add(new SurveyOption { Answer = "Reasonable", Count = (5 * 100 / 40) });
//    optionforSId1.Add(new SurveyOption { Answer = "Don't even want to know", Count = (25 * 100 / 40) });

//    List<SurveyOption> optionforSId2 = new List<SurveyOption>();
//    optionforSId2.Add(new SurveyOption { Answer = "Alumni", Count = (35 * 100 / 123) });
//    optionforSId2.Add(new SurveyOption { Answer = "Faculty", Count = (23 * 100 / 123) });
//    optionforSId2.Add(new SurveyOption { Answer = "Distance to school", Count = (50 * 100 / 123) });
//    optionforSId2.Add(new SurveyOption { Answer = "Friends' recommendation", Count = (15 * 100 / 123) });

//    List<SurveyOption> optionforSId3 = new List<SurveyOption>();
//    optionforSId3.Add(new SurveyOption { Answer = "Poor", Count = (40 * 100 / 125) });
//    optionforSId3.Add(new SurveyOption { Answer = "Tastey but not hygenic", Count = (30 * 100 / 125) });
//    optionforSId3.Add(new SurveyOption { Answer = "Tastey and hygenic", Count = (18 * 100 / 125) });
//    optionforSId3.Add(new SurveyOption { Answer = "Not tastey but Hygenic", Count = (37 * 100 / 125) });

//    SurveyQuestions.Add(new Survey
//    {
//        SurveyId = 1,
//        Question = "Parents' view on International schools fee",
//        Options = optionforSId1,
//        ChartVisibility = false,
//        ListviewVisibility = true,
//        Created = System.DateTime.Now
//    });

//    SurveyQuestions.Add(new Survey
//    {
//        SurveyId = 2,
//        Question = "On what basis you select school for your child ",
//        Options = optionforSId2,
//        ChartVisibility = false,
//        ListviewVisibility = true,
//        Created = System.DateTime.Now
//    });

//    SurveyQuestions.Add(new Survey
//    {
//        SurveyId = 3,
//        Question = "What do you think about the food available in the school",
//        Options = optionforSId3,
//        ChartVisibility = false,
//        ListviewVisibility = true,
//        Created = System.DateTime.Now
//    });
//    return SurveyQuestions;
//}





 //public List<Survey> DisplaySurveyQuestions()
 //       {
 //           List<Survey> SurveyQuestions = new List<Survey>();
 //           List<Option> optionforSId1 = new List<Option>();
 //           optionforSId1.Add(new Option { Answer = "Expensive", Count = (10*100/40), });
 //           optionforSId1.Add(new Option { Answer = "Reasonable", Count = (5*100/40) });
 //           optionforSId1.Add(new Option { Answer = "Don't even want to know", Count = (25*100/40) });

 //           List<Option> optionforSId2 = new List<Option>();
 //           optionforSId2.Add(new Option { Answer = "Alumni", Count = (35*100/123) });
 //           optionforSId2.Add(new Option { Answer = "Faculty", Count = (23*100/123) });
 //           optionforSId2.Add(new Option { Answer = "Distance to school", Count = (50*100/123) });
 //           optionforSId2.Add(new Option { Answer = "Friends' recommendation", Count = (15*100/123) });

 //           List<Option> optionforSId3 = new List<Option>();
 //           optionforSId3.Add(new Option { Answer = "Poor", Count = (40*100/125) });
 //           optionforSId3.Add(new Option { Answer = "Tastey but not hygenic", Count = (30*100/125) });
 //           optionforSId3.Add(new Option { Answer = "Tastey and hygenic", Count = (18 * 100 / 125) });
 //           optionforSId3.Add(new Option { Answer = "Not tastey but Hygenic", Count = (37 * 100 / 125) });

 //           SurveyQuestions.Add(new Survey
 //           {
 //               SurveyId = 1,
 //               Question = "Parents' view on International schools fee",
 //               Options = optionforSId1,
 //               ChartVisibility=false,
 //               ListviewVisibility=true,
 //               Created = System.DateTime.Now
 //           });

 //           SurveyQuestions.Add(new Survey
 //           {
 //               SurveyId = 2,
 //               Question = "On what basis you select school for your child ",
 //               Options = optionforSId2,
 //               ChartVisibility = false,
 //               ListviewVisibility = true,
 //               Created = System.DateTime.Now
 //           });

 //           SurveyQuestions.Add(new Survey
 //           {
 //               SurveyId = 3,
 //               Question = "What do you think about the food available in the school",
 //               Options = optionforSId3,
 //               ChartVisibility = false,
 //               ListviewVisibility = true,
 //               Created = System.DateTime.Now
 //           });
 // return SurveyQuestions;
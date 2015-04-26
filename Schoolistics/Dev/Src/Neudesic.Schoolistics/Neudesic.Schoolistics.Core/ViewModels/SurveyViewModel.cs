// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the SurveyViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
using Neudesic.Schoolistics.Core.Entities;
using Neudesic.Schoolistics.Core.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using Cirrious.CrossCore;
namespace Neudesic.Schoolistics.Core.ViewModels
{
    /// <summary>
    /// Define the SurveyViewModel type.
    /// </summary>
    public class SurveyViewModel : BaseViewModel
    {
        private ISurveyPageService _service;
        private ObservableCollection<Survey> _surveyQuestions;
        public ObservableCollection<Survey> SurveyQuestions
        {
            get { return _surveyQuestions; }
            set { _surveyQuestions = value; RaisePropertyChanged(() => SurveyQuestions); }
        }

        public SurveyViewModel(ISurveyPageService service)
        {
            try
            {
                _service = service;
                GetSurveyDetails();
            }
            catch(Exception ex)
            {
                Mvx.Error("Error in SurveyViewModel.cs : Constructor: " + ex.ToString());
            }
        }

        public void GetSurveyDetails()
        {
            try
            {
                List<Survey> surveyDetails = new List<Survey>();
                _service.GetSurveyPageDetails(surveyDetails, SurveyDetailsgetSuccess, SurveyDetailsgetFailure);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SurveyViewModel.cs : GetSurveyDetails: " + ex.ToString());
            }
        }

        private void SurveyDetailsgetFailure(Exception e)
        {
            try
            {
                var message = e.Message;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SurveyViewModel.cs : GetSurveyDetails: " + ex.ToString());
            }
        }

        private void SurveyDetailsgetSuccess(ResponseMessage<object> response)
        {
            try
            {
                if (response.SuccessCode == 1)
                {
                    Survey survey = new Survey();
                    var surveyDetails = JsonConvert.DeserializeObject<List<Survey>>(response.Data.ToString());
                    int count = surveyDetails.Count;
                    for (int i = 0; i < count; i++)
                    {
                        if (surveyDetails[i].IsClosed == false)
                        {
                            surveyDetails[i].ListviewVisibility = true;
                            surveyDetails[i].ChartVisibility = false;
                        }
                        else
                        {
                            surveyDetails[i].ListviewVisibility = false;
                            surveyDetails[i].ChartVisibility = true;
                            Survey closedSurveys = surveyDetails[i];
                            surveyDetails.Remove(surveyDetails[i]);
                            surveyDetails.Add(closedSurveys);
                            count--;
                            i--;
                        }
                    }
                    SurveyQuestions = new ObservableCollection<Survey>(surveyDetails);

                    CheckUserVoteDetails(Utils.Utils.Username);

                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SurveyViewModel.cs : SurveyDetailsgetSuccess: " + ex.ToString());
            }
        }

        public void VoteOnSurveys(string SurveyId,string OptionId)
        {
            try
            {
                _service.VoteOnSurveyoption(SurveyId, OptionId, VoteOnSurveysSuccess, VoteOnSurveysFailure);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SurveyViewModel.cs : VoteOnSurveys: " + ex.ToString());
            }
        }

        private void VoteOnSurveysFailure(Exception e)
        {
            var message = e.Message;
        }

        private void VoteOnSurveysSuccess(ResponseMessage<object> response)
        {
            try
            {
                if (response.SuccessCode == 1)
                {
                    var surveyDetails = JsonConvert.DeserializeObject<Survey>(response.Data.ToString());
                    var updatedSurvey = SurveyQuestions;
                    for (int i = 0; i < updatedSurvey.Count; i++)
                        if (updatedSurvey[i].SurveyId == surveyDetails.SurveyId)
                        {
                            updatedSurvey[i].ChartVisibility = true;
                            updatedSurvey[i].ListviewVisibility = false;
                            for (int j = 0; j < SurveyQuestions[i].Options.Count; j++)
                                if (updatedSurvey[i].Options[j].OptionId == surveyDetails.Options[0].OptionId)
                                    updatedSurvey[i].Options[j].Votes = surveyDetails.Options[0].Votes;
                        }
                    SurveyQuestions = new ObservableCollection<Survey>(updatedSurvey);
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SurveyViewModel.cs : SurveyDetailsgetSuccess: " + ex.ToString());
            }
        }

        public void CheckUserVoteDetails(string UserName)
        {
            try
            {
                _service.CheckUserVoteDetails(UserName, CheckUserVoteDetailsSuccess, CheckUserVoteDetailsFailure);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SurveyViewModel.cs : CheckUserVoteDetails: " + ex.ToString());
            }
        }

        private void CheckUserVoteDetailsFailure(Exception e)
        {
            try
            {
                var message = e.Message;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SurveyViewModel.cs : CheckUserVoteDetailsFailure: " + ex.ToString());
            }
        }

        private void CheckUserVoteDetailsSuccess(ResponseMessage<object> response)
        {
            try
            {
                if (response.SuccessCode == 1)
                {
                    var _userSurveyDetails = JsonConvert.DeserializeObject<List<UserSurveyDetails>>(response.Data.ToString());
                    var _surveyQuestions = SurveyQuestions;
                    if (!(_userSurveyDetails.Count == 0))
                    {
                        IEnumerable<Survey> obsCollection = (IEnumerable<Survey>)_surveyQuestions;

                        var questionsList = new List<Survey>(obsCollection);

                        foreach (var survey in _userSurveyDetails)
                        {
                            if (questionsList.Any(x => x.SurveyId == survey.SurveyId))
                            {
                                Survey votedQuestion = questionsList.Where(x => x.SurveyId == survey.SurveyId).First();
                                var questionIndex = questionsList.IndexOf(votedQuestion);
                                questionsList.RemoveAt(questionIndex);
                                votedQuestion.ListviewVisibility = false;
                                votedQuestion.ChartVisibility = true;
                                questionsList.Insert(questionIndex, votedQuestion);
                            }
                        }
                        SurveyQuestions = new ObservableCollection<Survey>(questionsList);
                    }
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SurveyViewModel.cs : CheckUserVoteDetailsSuccess: " + ex.ToString());
            }
           
        }

        public void SaveUserVotedSurvey(string Surveyid)
        {
            try
            {
                UserSurveyDetails _userVotedDetails = new UserSurveyDetails();
                _userVotedDetails.SurveyId = Surveyid;
                _userVotedDetails.UserName = Utils.Utils.Username;
                _userVotedDetails.Created = System.DateTime.Now;
                _service.SaveUserVotedDetails(_userVotedDetails, SaveUserVotedSurveySuccess, SaveUserVotedSurveyFailure);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SurveyViewModel.cs : SaveUserVotedSurvey: " + ex.ToString());
            }
        }

        private void SaveUserVotedSurveyFailure(Exception e)
        {
            try
            {
                var message = e.Message;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SurveyViewModel.cs : SaveUserVotedSurveyFailure: " + ex.ToString());
            }
        }

        private void SaveUserVotedSurveySuccess(ResponseMessage<object> response)
        {
            try
            {
                if (response.SuccessCode == 1 && response.Message == "User already voted for this question")

                    GetSurveyDetails();
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SurveyViewModel.cs : SaveUserVotedSurveySuccess: " + ex.ToString());
            }
        }
    }
      
}


using Neudesic.Schoolistics.Framework.API_Models;
using Neudesic.Schoolistics.Framework.Models;
using Neudesic.Schoolistics.Framework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Neudesic.Schoolistics.WebRole.APIs
{
    public class SurveyController : ApiController
    {
        ISurveyRepository surveyRepository;
        IUserSurveyDetailsRepository userSurveyDetailsRepository;

        public SurveyController(ISurveyRepository _surveyRep,IUserSurveyDetailsRepository _userSurveyRep)
        {
            this.surveyRepository = _surveyRep;
            this.userSurveyDetailsRepository = _userSurveyRep;
        }

        [HttpGet]
        public HttpResponseMessage GetAllPopularSurveys()
        {
            var popularSurveys = this.surveyRepository.GetPopularSurveys();

            return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<List<SurveyInfo>> { SuccessCode = 1, Data = popularSurveys });
        }

        [HttpGet]
        public HttpResponseMessage GetAllClosedSurveys()
        {
            var closedSurveys = this.surveyRepository.GetAllClosedSurveys();

            return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<List<SurveyInfo>> { SuccessCode = 1, Data = closedSurveys });
        }

        [HttpGet]
        public HttpResponseMessage GetAllOpenSurveys()
        {
            var openSurveys = this.surveyRepository.GetAllOpenSurveys();

            return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<List<SurveyInfo>> { SuccessCode = 1, Data = openSurveys });
        }

        [HttpGet]
        public HttpResponseMessage GetSurveybyID(string id)
        {
            var survey = this.surveyRepository.GetSurveyByID(id);

            return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<SurveyInfo> { SuccessCode = 1, Data = survey });
        }

        [HttpGet]
        [Authorize]
        public HttpResponseMessage VoteOnSurvey(string surveyId, string optionId)
        {
            var survey = this.surveyRepository.VoteOnSurvey(surveyId, optionId);

            return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<SurveyInfo> { SuccessCode = 1, Data = survey });

        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage UserVotedSurvey(UserSurveyDetails _userVotedDetails)
        {
            if (this.userSurveyDetailsRepository.UserVotedSurveyDetails(_userVotedDetails))
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<List<UserSurveyDetails>> { SuccessCode = 1, Message = "UserVotedDetails added Successfully" });
            else
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<List<UserSurveyDetails>> { SuccessCode = 1, Message = "User already voted for this question" });
        }


        [HttpGet]
        [Authorize]
        public HttpResponseMessage GetUserSurveyDetails(string Username)
        {
            var userSurveyDetails = this.userSurveyDetailsRepository.userVotingDetails(Username);

            return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<List<UserSurveyDetails>> { SuccessCode = 1, Data = userSurveyDetails });
        }
        

    }
}
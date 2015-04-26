using Neudesic.Schoolistics.Framework.API_Models;
using Neudesic.Schoolistics.Framework.Models;
using Neudesic.Schoolistics.Framework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Neudesic.Schoolistics.WebRole.APIs
{
    public class ComparisonController : ApiController
    {
        IUserComparisonsRepository userComparisonRepository;
        ISchoolsComparisonRepository schoolsComparisonRepository;
        ISchoolRepository schoolRepository;

        public ComparisonController(IUserComparisonsRepository _userComparisonRep, ISchoolsComparisonRepository _schoolComparisonRep, ISchoolRepository _schoolRepository)
        {
            userComparisonRepository = _userComparisonRep;
           schoolsComparisonRepository = _schoolComparisonRep;
           schoolRepository=_schoolRepository;
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage SaveComparison(List<SchoolsComparison> schoolsComparison)
        {
            var username = HttpContext.Current.User.Identity.Name;

            var comparisonID = this.schoolsComparisonRepository.Create(schoolsComparison);

            UserSchoolsComparisons userComparison = new UserSchoolsComparisons();
            userComparison.ComparisonId = comparisonID;
            userComparison.Created = DateTime.UtcNow;
            userComparison.Username = username;

            this.userComparisonRepository.Create(userComparison);

            return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<Object> { SuccessCode = 1, Message = "Comparison saved successfully" });

        }

        [HttpGet]
        [Authorize]
        public HttpResponseMessage GetUserComparisons()
        {
            var username = HttpContext.Current.User.Identity.Name;

            var userComparisonsIDs = this.userComparisonRepository.GetUserComparisons(username);

            List<ComparisonDetail> userComparisons = new List<ComparisonDetail>();

            foreach (var comparsisonID in userComparisonsIDs)
            {
                ComparisonDetail detail = new ComparisonDetail();
                var comparisonSchools = this.schoolsComparisonRepository.GetSchoolsByComparisonID(comparsisonID.ComparisonId);
                detail.Schools = comparisonSchools;
                detail.ComparisonId = comparsisonID.ComparisonId;
                userComparisons.Add(detail);
            }


            return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<List<ComparisonDetail>> { SuccessCode = 1, Data = userComparisons });

        }

        [HttpGet]
        public HttpResponseMessage GetPopularComparisons()
        {
            var popularComparisonIDs = this.userComparisonRepository.GetPopularComparisons();

            List<ComparisonDetail> popularComparisons = new List<ComparisonDetail>();

            foreach (var comparsisonID in popularComparisonIDs)
            {
                ComparisonDetail detail = new ComparisonDetail();
                var comparisonSchools = this.schoolsComparisonRepository.GetSchoolsByComparisonID(comparsisonID.ComparisonId);
                detail.Schools = comparisonSchools;
                detail.ComparisonId = comparsisonID.ComparisonId;
                popularComparisons.Add(detail);
            }

            return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<List<ComparisonDetail>> { SuccessCode = 1, Data = popularComparisons });

        }


        [HttpPost]
        public HttpResponseMessage GetAllComparedSchools(List<SchoolsComparison> schools)
        {
            // var myschool = this.schoolRepository.GetAllComparedSchools(schools);
            List<School> AllSchool = new List<School>();
            for (int i = 0; i < schools.Count; i++)
            {
                string Id = schools[i].SchoolId;
                string City = schools[i].City;
                var schoolFound = this.schoolRepository.GetSchoolByIDAndCity(City,Id);
                AllSchool.Add(schoolFound);

            }


            if (AllSchool != null)
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<List<School>> { SuccessCode = 1, Data = AllSchool });
            else
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<List<School>> { SuccessCode = 1, Data = AllSchool });


        }
    }
}
using Neudesic.Schoolistics.Framework.API_Models;
using Neudesic.Schoolistics.Framework.Models;
using Neudesic.Schoolistics.Framework.Repositories;
using Neudesic.Schoolistics.Framework.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Neudesic.Schoolistics.WebRole.APIs
{
    public class SchoolController : ApiController
    {
        ISchoolRepository schoolRepository;

        IUserSchoolRatingRepository schoolRatingRepository;

        IUserSchoolCommentRepository schoolCommentRepository;

        public SchoolController(ISchoolRepository schoolRep, IUserSchoolRatingRepository ratingRep, IUserSchoolCommentRepository commentRep)
        {
            this.schoolRepository = schoolRep;
            this.schoolRatingRepository = ratingRep;
            this.schoolCommentRepository = commentRep;
        }

        [HttpPost]
        public HttpResponseMessage GetSchoolByIDandCity(School school)
        {
            var username = HttpContext.Current.User.Identity.Name;

            var schoolFound = this.schoolRepository.GetSchoolByIDAndCity(school.City, school.SchoolId);

            if (schoolFound != null)
            {
                var schoolDetail = ModelConverters.ConvertSchoolToSchoolDetail(schoolFound);
                var userrating = this.schoolRatingRepository.GetUserRating(username, schoolFound.SchoolId);
                if(userrating!=null)
                schoolDetail.UserRating = this.schoolRatingRepository.GetUserRating(username, schoolFound.SchoolId).Value;

                return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<SchoolDetail> { SuccessCode = 1, Data = schoolDetail });
            }
            else
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<SchoolDetail> { SuccessCode = 0, Message = "School Not Found" });
        }
        [HttpPost]
        public HttpResponseMessage GetAlumniBySchoolId(string schoolId)
        {
           // var alumniFound = this.schoolRepository.GetAlumniBySchoolId(school.SchoolId);
            List<Alumni> alumni=new List<Alumni>();
            
            return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<List<Alumni>> { SuccessCode = 1, Data = alumni });
        }
        [HttpPost]
        public HttpResponseMessage GetSchoolManagement(string schoolId)
        {
           // var schoolManagement = this.schoolRepository.GetSchoolManagement(school.SchoolId);
            List<SchoolManagement> management = new List<SchoolManagement>();
            return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<List<SchoolManagement>> { SuccessCode = 1, Data = management });
        }

        [HttpGet]
        public HttpResponseMessage GetFeaturedSchools()
        {
            var username = HttpContext.Current.User.Identity.Name;

            var schools = this.schoolRepository.GetFeaturedSchools();

            if (username != null)
            {
                List<SchoolDetail> featuredSchools = new List<SchoolDetail>();

                foreach (var school in schools)
                {
                    var schoolDetail = ModelConverters.ConvertSchoolToSchoolDetail(school);
                    var userRating = this.schoolRatingRepository.GetUserRating(username, schoolDetail.SchoolId);
                    if (userRating != null)
                        schoolDetail.UserRating = this.schoolRatingRepository.GetUserRating(username, schoolDetail.SchoolId).Value;
                    featuredSchools.Add(schoolDetail);
                }

                return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<List<SchoolDetail>> { SuccessCode = 1, Data = featuredSchools });
            }
            else
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<List<School>> { SuccessCode = 1, Data = schools });
        }

        [HttpPost]
        public async Task<HttpResponseMessage> SearchSchools(SearchDetails searchDetails)
        {
            var username = HttpContext.Current.User.Identity.Name;

            var schoolsFound = ( this.schoolRepository.SearchSchools(searchDetails));
            // return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<List<School>> { SuccessCode = 1, Data = schoolsFound });

            List<School> finalSchools = new List<School>();

            if (searchDetails.MaximumDistance != 0)
            {
                foreach (var school in schoolsFound)
                {
                    string distance = await GetDistanceFromCoordinates(searchDetails.Latitude, searchDetails.Longitude, school.Latitude, school.Longitude);
                    if (distance != string.Empty)
                    {
                        double finalDistance = Convert.ToDouble(distance);
                        if (searchDetails.MaximumDistance <= 20)
                        {
                            if (finalDistance <= searchDetails.MaximumDistance)
                                finalSchools.Add(school);
                        }
                        else
                        {
                            if (finalDistance > 20)
                                finalSchools.Add(school);
                        }
                    }
                }
            }
            else
            {
                finalSchools = schoolsFound;
            }
            if (username != null && username != string.Empty)
            {
                List<SchoolDetail> featuredSchools = new List<SchoolDetail>();
                foreach (var school in finalSchools)
                {
                    var schoolDetail = ModelConverters.ConvertSchoolToSchoolDetail(school);
                    var userRating = this.schoolRatingRepository.GetUserRating(username, schoolDetail.SchoolId);
                    if (userRating != null)
                        schoolDetail.UserRating = this.schoolRatingRepository.GetUserRating(username, schoolDetail.SchoolId).Value;
                    featuredSchools.Add(schoolDetail);
                }

                return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<List<SchoolDetail>> { SuccessCode = 1, Data = featuredSchools });
            }
            else
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<List<School>> { SuccessCode = 1, Data = finalSchools });
        }

        public async Task<string> GetDistanceFromCoordinates(double latitude,double longitude,double schoolLatitude,double schoolLongitude)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.GetAsync("http://maps.googleapis.com/maps/api/distancematrix/json?origins="+ latitude +","+ longitude+"&destinations="+ schoolLatitude + ","+ schoolLongitude +"&sensor=false");
            var jsonContent = await response.Content.ReadAsStringAsync();
            var distanceJson = JsonConvert.DeserializeObject<RootObject>(jsonContent.ToString());
            string distance = string.Empty;
            if (distanceJson.status.ToString() == "OK")
            {
                 distance = distanceJson.rows[0].elements[0].distance.text.ToString();
                 distance = distance.Substring(0,distance.IndexOf(" "));
               //  distance = distance.Substring(0, distance.IndexOf(".")) == string.Empty ? distance : distance.Substring(0, distance.IndexOf(" "));
            }
            return distance;
        }

        [HttpGet]
        public HttpResponseMessage GetAllSchools()
        {
            var schools = this.schoolRepository.GetAllSchools();

            return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<List<School>> { SuccessCode = 1, Data = schools });

        }

        [HttpGet]
        public HttpResponseMessage SearchSchoolsbyName(string keyword)
        {
            var schools = this.schoolRepository.SearchSchoolsByName(keyword);

            return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<List<School>> { SuccessCode = 1, Data = schools });
        }

        [HttpPost]
        public HttpResponseMessage RateSchool(UserSchoolRating rating)
        {
            this.schoolRatingRepository.CreateUserSchoolRating(rating);

            var school = this.schoolRepository.RateSchool(rating);

            var schoolDetail = ModelConverters.ConvertSchoolToSchoolDetail(school);
            schoolDetail.UserRating = rating.Rating;

            return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<SchoolDetail> { SuccessCode = 1, Data = schoolDetail });
        }

        [HttpPost]
        public HttpResponseMessage CommentOnSchool(UserSchoolComment comment)
        {
            this.schoolCommentRepository.CreateUserSchoolComment(comment);

            var comments = this.schoolCommentRepository.GetSchoolComments(comment.SchoolId);

            return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<List<UserSchoolComment>> { SuccessCode = 1, Data = comments });
        }

        [HttpGet]
        public HttpResponseMessage GetAllSchoolComments(string schoolId)
        {
            var comments = this.schoolCommentRepository.GetSchoolComments(schoolId);

            return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<List<UserSchoolComment>> { SuccessCode = 1, Data = comments });
        }

        [HttpGet]
        public async Task<bool> UpdateLatitudeAndLongitude()
        {
            var value = await this.schoolRepository.UpdateSchoolLatitudeAndLongitude();
            return value;
        }
    }
}
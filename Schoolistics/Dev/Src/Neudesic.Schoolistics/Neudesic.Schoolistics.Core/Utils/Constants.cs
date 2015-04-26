using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Core.Utils
{
    public class Constants
    {
#if Local 
        public static readonly string SignUp = "http://127.0.0.1:81/api/user/signup";

        public static readonly string SearchSchools = "http://127.0.0.1:81/api/school/searchschools";

        public static readonly string FeaturedSchools = "http://127.0.0.1:81/api/school/getfeaturedschools";

        public static readonly string Login = "http://127.0.0.1:81/api/user/login";

        public static readonly string SchoolDetails = " http://127.0.0.1:81/api/school/getschoolbynameid";

        public static readonly string BlobBaseUrl = "http://schoolisticstest1.blob.core.windows.net/picture/";

        public static readonly string Profile = "http://127.0.0.1:81/api/user/getprofileinfo";

        public static readonly string SaveSearch = "http://127.0.0.1:81/api/search/savesearch";

        public static readonly string UserRatingApi = "http://127.0.0.1:81/api/school/RateSchool";

        public static readonly string school = "http://127.0.0.1:81/api/school/GetSchoolByIDAndCity";

        public static readonly string Alumni = "http://127.0.0.1:81/api/school/GetAlumniBySchoolId";

        public static readonly string Management = "http://127.0.0.1:81/api/school/GetSchoolManagement";
        
        public static readonly string schoolDetailsForComparison = "http://127.0.0.1:81/api/Comparison/GetAllComparedSchools";

        public static readonly string blobimage = "http://schoolisticstest1.blob.core.windows.net/picture/";

        public static readonly string Survey = "http://127.0.0.1:81/api/Survey/GetAllOpenSurveys";

        public static readonly string PopularSurveys = "http://127.0.0.1:81/api/Survey/GetAllPopularSurveys";

        public static readonly string VoteforSurveys = "http://127.0.0.1:81/api/Survey/VoteOnSurvey?surveyId=##SURVEYID##&optionId=##OPTIONID##";

        public static readonly string BookmarkSchool = "http://127.0.0.1:81/api/bookmarkedschools/bookmarkschool";

        public static readonly string GetUserBookmarkedSchools = "http://127.0.0.1:81/api/bookmarkedschools/GetUserBookmarkedSchools";

        public static readonly string GetAllSchools = " http://127.0.0.1:81/api/school/GetAllSchools";

        public static readonly string SaveComparisons = " http://127.0.0.1:81/api/Comparison/SaveComparison";

        public static readonly string GetUserSavedComparisons = "http://127.0.0.1:81/api/Comparison/GetUserComparisons";

        public static readonly string GetPopularComparisons = " http://127.0.0.1:81/api/Comparison/GetPopularComparisons";

        public static readonly string Editprofile = "http://127.0.0.1:81/api/user/updateprofileinfo";

        public static readonly string CheckUserVotes = "http://127.0.0.1:81/api/Survey/GetUserSurveyDetails?Username=##USERNAME##";

        public static readonly string UserVotedDetails = "http://127.0.0.1:81/api/Survey/UserVotedSurvey";

        public static readonly string GetAllNewsArticles = "http://127.0.0.1:81/api/NewsArticle/GetallNewsArticles";

        public static readonly string GetLatestNewsArticles = "http://127.0.0.1:81/api/NewsArticle/GetLatestNewsArticles";

        public static readonly string PostComment = "http://127.0.0.1:81/api/School/CommentOnSchool";

        public static readonly string GetAllSchoolComments = "http://127.0.0.1:81/api/School/GetAllSchoolComments?schoolId=##SCHOOLID##";

        public static readonly string EditProfileWithoutImage = "http://127.0.0.1:81/api/user/UpdateUserInfo";


#else
        public static readonly string SignUp = "http://schoolistics.cloudapp.net/api/user/signup";

        public static readonly string SearchSchools = "http://schoolistics.cloudapp.net/api/school/searchschools";

        public static readonly string FeaturedSchools = "http://schoolistics.cloudapp.net/api/school/getfeaturedschools";

        public static readonly string Login = "http://schoolistics.cloudapp.net/api/user/login";

        public static readonly string SchoolDetails = " http://schoolistics.cloudapp.net/api/school/getschoolbynameid";

        public static readonly string Profile = "http://schoolistics.cloudapp.net/api/user/getprofileinfo";

        public static readonly string UserRatingApi = "http://schoolistics.cloudapp.net/api/school/RateSchool";

        public static readonly string Survey = "http://schoolistics.cloudapp.net/api/Survey/GetAllOpenSurveys";

        public static readonly string schoolDetailsForComparison = "http://schoolistics.cloudapp.net/api/Comparison/GetAllComparedSchools";

        public static readonly string PopularSurveys = "http://schoolistics.cloudapp.net/api/Survey/GetAllPopularSurveys";

        public static readonly string VoteforSurveys = "http://schoolistics.cloudapp.net/api/Survey/VoteOnSurvey?surveyId=##SURVEYID##&optionId=##OPTIONID##";

        public static readonly string BookmarkSchool = "http://schoolistics.cloudapp.net/api/bookmarkedschools/bookmarkschool";

        public static readonly string blobimage = "http://schoolisticstest1.blob.core.windows.net/picture/";

      //  public static readonly string GetUserBookmarkedSchools = "http://schoolistics.cloudapp.net/api/bookmarkedschools/getuserbookmarkedschools";

        public static readonly string SaveSearch = "http://schoolistics.cloudapp.net/api/search/savesearch";

        public static readonly string BlobBaseUrl = "http://schoolisticstest1.blob.core.windows.net/picture/";

        public static readonly string school = " http://schoolistics.cloudapp.net/api/School/GetSchoolByIDAndCity";

        public static readonly string GetAllSchools =" http://schoolistics.cloudapp.net/api/school/GetAllSchools";

        public static readonly string SaveComparisons =" http://schoolistics.cloudapp.net/api/Comparison/SaveComparison";

        public static readonly string GetUserSavedComparisons =" http://schoolistics.cloudapp.net/api/Comparison/GetUserComparisons";

        public static readonly string GetPopularComparisons =" http://schoolistics.cloudapp.net/api/Comparison/GetPopularComparisons";

        public static readonly string GetUserBookmarkedSchools = " http://schoolistics.cloudapp.net/api/bookmarkedschools/GetUserBookmarkedSchools";

        public static readonly string Editprofile = "http://schoolistics.cloudapp.net/api/user/updateprofileinfo";

        public static readonly string GetLatestNewsArticles = "http://schoolistics.cloudapp.net/api/NewsArticle/GetLatestNewsArticles";

        public static readonly string CheckUserVotes = "http://schoolistics.cloudapp.net/api/Survey/GetUserSurveyDetails?Username=##USERNAME##";

        public static readonly string UserVotedDetails = "http://schoolistics.cloudapp.net/api/Survey/UserVotedSurvey";

        public static readonly string GetAllNewsArticles = "http://schoolistics.cloudapp.net/api/NewsArticle/GetallNewsArticles";

        public static readonly string GetAllSchoolComments = "http://schoolistics.cloudapp.net/api/School/GetAllSchoolComments";

        public static readonly string EditProfileWithoutImage = "http://schoolistics.cloudapp.net/api/user/UpdateUserInfo";
        public static readonly string PostComment = "http://schoolistics.cloudapp.net/api/School/CommentOnSchool";

        public static readonly string Alumni = "http://127.0.0.1:81/api/school/GetAlumniBySchoolId";

        public static readonly string Management = "http://127.0.0.1:81/api/school/GetSchoolManagement";
#endif

    }
}

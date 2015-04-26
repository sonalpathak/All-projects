using Neudesic.Schoolistics.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Core.Services
{
     public interface ISchoolDetailsService
    {
        //List<School> DisplaySearchResults();
        //List<School> DisplaySchoolComparisons(List<string> names);
        //List<SchoolsComparison> DisplayPopularComparisons();
        List<School> DisplaySchoolDetails();
       List<Alumni> GetAlumniDetails();
       List<SchoolManagement> GetManagementDetails();
       void GetschoolDetails(School school, Action<ResponseMessage<Object>> success, Action<Exception> exception);
     //  void GetUserRating(UserSchoolRating rating, Action<ResponseMessage<Object>> success, Action<Exception> exception);
       void GetRatings(UserSchoolRating rating, Action<ResponseMessage<Object>> success, Action<Exception> exception);
       void GetAlumniDetails(string schoolId, Action<ResponseMessage<Object>> success, Action<Exception> exception);
       void GetSchoolManagement(string schoolId, Action<ResponseMessage<Object>> success, Action<Exception> exception);
       void AddCommentOnSchool(UserSchoolComment comment,Action<ResponseMessage<Object>> success,Action<Exception> exception);
       void AllCommentsOnSchool(string schoolId, Action<ResponseMessage<Object>> success, Action<Exception> exception);

      // void GetManagementDetails(string schoolid, Action<ResponseMessage<object>> ManagementDetailsSuccess, object ManagementDetailsFailure);

       //void GetManagementDetails(string sid, Action<ResponseMessage<object>> ManagementDetailsSuccess, object ManagementDetailsFailure);
    }
}

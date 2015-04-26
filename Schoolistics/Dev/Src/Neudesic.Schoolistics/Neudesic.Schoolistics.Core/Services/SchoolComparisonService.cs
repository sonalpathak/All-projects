using Neudesic.Schoolistics.Core.Entities;
using Neudesic.Schoolistics.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Core.Services
{
    public class SchoolComparisonService : ISchoolComparisonService
    {
         IRestService restService;

        
         public SchoolComparisonService(IRestService _restService)
        {
            restService = _restService;
        }

        public void GetSchoolDetails(List<School> schools, Action<ResponseMessage<Object>> success, Action<Exception> exception)
        {
            restService.MakeRequest<List<School>>(Constants.GetAllSchools, "GET", null, success, exception);
        }

        public void SaveSchoolComparisons(List<SchoolsComparison> comparisons, Action<ResponseMessage<object>> success, Action<Exception> exception)
        {
            restService.MakeRequest<List<SchoolsComparison>>(Constants.SaveComparisons, "POST", comparisons, success, exception);
        }

        public void GetUserSavedSchoolComparisons(List<ComparisonDetail> userSavedComparisons, Action<ResponseMessage<Object>> success, Action<Exception> exception)
        {
            restService.MakeRequest< List<ComparisonDetail>>(Constants.GetUserSavedComparisons, "GET", null, success, exception);
        }

        public void GetPopularSchoolComparisons(List<ComparisonDetail> popularComparisons, Action<ResponseMessage<Object>> success, Action<Exception> exception)
        {
            restService.MakeRequest<List<ComparisonDetail>>(Constants.GetPopularComparisons, "GET", null, success, exception);
        }

        public void GetSchoolByIDandCity(School school, Action<ResponseMessage<Object>> success, Action<Exception> exception)
        {
            restService.MakeRequest<School>(Constants.Alumni, "POST", school, success, exception);
        }
        public void GetAllComparedSchools(List<SchoolsComparison> schoolComparison, Action<ResponseMessage<Object>> success, Action<Exception> exception)
        {
          //  restService.MakeRequest<School>(Constants.schoolDetails, "POST", schools, success, exception);
            restService.MakeRequest<Object>(Constants.schoolDetailsForComparison, "POST", schoolComparison, success, exception);
        }
    }
}

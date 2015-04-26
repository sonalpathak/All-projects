using Neudesic.Schoolistics.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Core.Services
{
     public interface ISchoolComparisonService
    {
         void GetSchoolDetails(List<School> school, Action<ResponseMessage<Object>> success, Action<Exception> exception);
         void SaveSchoolComparisons(List<SchoolsComparison> comparisons, Action<ResponseMessage<object>> success, Action<Exception> exception);
         void GetUserSavedSchoolComparisons(List<ComparisonDetail> schools, Action<ResponseMessage<Object>> success, Action<Exception> exception);
         void GetPopularSchoolComparisons(List<ComparisonDetail> popularComparisons, Action<ResponseMessage<Object>> success, Action<Exception> exception);
         void GetSchoolByIDandCity(School school, Action<ResponseMessage<Object>> success, Action<Exception> exception);
         void GetAllComparedSchools(List<SchoolsComparison> schools,Action<ResponseMessage<Object>> success, Action<Exception> exception);
    }
}

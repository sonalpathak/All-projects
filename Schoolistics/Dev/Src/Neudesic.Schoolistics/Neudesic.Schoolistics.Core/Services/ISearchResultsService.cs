using Neudesic.Schoolistics.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Core.Services
{
    public interface ISearchResultsService
    {
      //  List<School> DisplaySearchResults();
        List<School> FilterSchoolsBasedOnCondition(SearchDetails searchDetails);

        List<School> FilterSchoolsBasedOnRefine(bool? midDayMeals, bool? playGround, bool? digitalClassroom, bool? dayBoarding, bool? transportationFacility);

        List<string> AssignCityData();

        List<string> AssignDistanceData();
        
        List<string> AssignAccreditionData();

        List<string> AssignClassesData();

        List<string> AssignRatingData();

        void PostSearchResults(SearchDetails searchDetails, Action<ResponseMessage<Object>> success, Action<Exception> exception);

        void SaveSearchResults(SearchDetails searchDetails, Action<ResponseMessage<Object>> success, Action<Exception> exception);
    }
}

using Neudesic.Schoolistics.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Core.Services
{
    public interface IHubPageService
    {
       // List<BookmarkedSchools> AssignSampleData();

        List<string> AssignCityData();

        List<string> AssignDistanceData();

        List<string> AssignAccreditionData();

        List<string> AssignClassesData();

        List<string> AssignRatingData();

        void GetFeaturedSchoolDetails(School featuredSchoolDetails, Action<ResponseMessage<Object>> success, Action<Exception> exception);
    }
}

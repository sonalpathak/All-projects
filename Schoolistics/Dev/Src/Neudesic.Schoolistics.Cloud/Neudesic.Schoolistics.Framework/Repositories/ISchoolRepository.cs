using Neudesic.Schoolistics.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Framework.Repositories
{
    public interface ISchoolRepository
    {
        List<School> GetSchoolsByName(string schoolName);

        School GetSchoolByIDAndCity(string city, string id);

        School GetSchoolByID(string id);
      //  List<School> GetAllComparedSchools(string sid,string city);

        List<School> GetFeaturedSchools();

        List<School> SearchSchools(SearchDetails search);

        List<School> GetAllSchools();

        List<School> SearchSchoolsByName(string keyword);

        School RateSchool(UserSchoolRating rating);

        List<Alumni> GetAlumniBySchoolId(string schoolId);

        List<SchoolManagement> GetSchoolManagement(string schoolId);

        Task<bool> UpdateSchoolLatitudeAndLongitude();
    }
}

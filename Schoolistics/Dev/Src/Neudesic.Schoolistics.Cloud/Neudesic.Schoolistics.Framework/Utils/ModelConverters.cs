using Neudesic.Schoolistics.Framework.API_Models;
using Neudesic.Schoolistics.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Framework.Utils
{
    public class ModelConverters
    {
        public static SchoolDetail ConvertSchoolToSchoolDetail(School school)
        {
            SchoolDetail detail = new SchoolDetail { Accreditation = school.Accreditation, Address = school.Address, AdmissionsInProgress = school.AdmissionsInProgress, City = school.City, DayBoarding = school.DayBoarding, Description = school.Description, DigitalClassroom = school.DigitalClassroom, IsFeatured = school.IsFeatured, LowerClassRange = school.LowerClassRange, MidDayMeals = school.MidDayMeals, Playground = school.Playground, Rating = school.Rating, RatingsCount = school.RatingsCount, SchoolBus = school.SchoolBus, SchoolId = school.SchoolId, SchoolLogo = school.SchoolLogo, SchoolName = school.SchoolName, SchoolStrength = school.SchoolStrength, Transportation = school.Transportation, UpperClassRange = school.UpperClassRange, WebsiteLink = school.WebsiteLink };

            return detail;
        }
    }
}

using Neudesic.Schoolistics.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Neudesic.Schoolistics.Core.Utils;

namespace Neudesic.Schoolistics.Core.Services
{
    public class SchoolDetailsService : ISchoolDetailsService
    {
         IRestService restService;
        List<School> schoolDetails = new List<School>();
        //ISchoolDetailsService _service;
        List<Alumni> alumni = new List<Alumni>();
        List<SchoolManagement> management = new List<SchoolManagement>();
        List<Location> loc = new List<Location>();
        School school = new School();

    

        public SchoolDetailsService(IRestService _restService)
        {

            restService = _restService;
        }

        public SchoolDetailsService()
        {
            // TODO: Complete member initialization
        }
       public void GetschoolDetails(School school, Action<ResponseMessage<Object>> success, Action<Exception> exception)
       {
           restService.MakeRequest<School>(Constants.school, "POST", school, success, exception);
       }

       public void GetSchoolManagement(string schoolId, Action<ResponseMessage<Object>> success, Action<Exception> exception)
       {
           restService.MakeRequest<string>(Constants.school, "POST", schoolId, success, exception);
       }
       //public void GetUserRating(UserSchoolRating rating, Action<ResponseMessage<Object>> success, Action<Exception> exception)
       //{
       //    restService.MakeRequest<UserSchoolRating>(Constants.UserRatingApi, "POST", rating, success, exception);
       //}
       public void GetRatings(UserSchoolRating rating, Action<ResponseMessage<Object>> success, Action<Exception> exception)
       {
           restService.MakeRequest<UserSchoolRating>(Constants.UserRatingApi,"POST",rating,success,exception);
       }
       public void GetAlumniDetails(string schoolId, Action<ResponseMessage<Object>> success, Action<Exception> exception)
       {
           restService.MakeRequest<string>(Constants.Alumni, "POST", schoolId, success, exception);
       }
       
       public void AddCommentOnSchool(UserSchoolComment comment, Action<ResponseMessage<Object>> success, Action<Exception> exception)
       {
           restService.MakeRequest<UserSchoolComment>(Constants.PostComment,"POST",comment,success,exception);
       }
       public void AllCommentsOnSchool(string schoolId, Action<ResponseMessage<Object>> success, Action<Exception> exception)
       {
           restService.MakeRequest<string>(Utils.URLModifier.GetAllSchoolComments(schoolId), "GET", null, success, exception);
       }
        //public List<School> DisplaySchoolComparisons(List<string> names)
        //{
        //    List<School> schoolDetailsToCompare = new List<School>();
        //    // AssignSampleData();

        //    for (int i = 0; i < names.Count; i++)
        //    {
        //        var schoolName = names[i];
        //        var schoolDetailsTodisplay = schoolDetails.FirstOrDefault(r => r.SchoolName == schoolName);
        //        schoolDetailsToCompare.Add(schoolDetailsTodisplay);
        //    }

        //    return schoolDetailsToCompare;
        //}

        //public List<SchoolsComparison> DisplayPopularComparisons()
        //{
        //    List<SchoolsComparison> popularSchools = new List<SchoolsComparison>();

        //    popularSchools.Add(new SchoolsComparison()
        //    {
        //        ComparisonId = "1",
        //        Schools =  schoolDetails.Take(3).ToList()
        //    });

        //    popularSchools.Add(new SchoolsComparison()
        //    {
        //        ComparisonId = "2",
        //        Schools = schoolDetails
        //    });
        //    return popularSchools;

        //}

        public List<School> DisplaySearchResults()
        {
            //AssignSampleData();
            return schoolDetails;
        }

        //private void AssignSampleData()
        //{
        //    schoolDetails.Add(new School()
        //    {
        //        SchoolId = "1",
        //        SchoolName = "Sample",
        //        SchoolLogo = "ms-appx:///Assets/school-logo-1.png",
        //        Rating = 5,
        //        Accreditation = "CBSC",
        //        AdmissionsInProgress = true,
        //        SchoolBus = true,
        //        MidDayMeals = true,
        //        Playground = false,
        //        SchoolStrength = 2000,
        //    });

        //    schoolDetails.Add(new School()
        //    {
        //        SchoolId = "2",
        //        SchoolName = "Oakridge Einstein1",
        //        SchoolLogo = "ms-appx:///Assets/school-logo-2.png",
        //        Rating = 5,
        //        Accreditation = "CBSC",
        //        AdmissionsInProgress = true,
        //        SchoolBus = true,
        //        MidDayMeals = false,
        //        Playground = true,
        //        SchoolStrength = 2000,
        //    });

        //    schoolDetails.Add(new School()
        //    {
        //        SchoolId = "3",
        //        SchoolName = "Oakridge Einstein2",
        //        SchoolLogo = "ms-appx:///Assets/school-logo-3.png",
        //        Rating = 5,
        //        Accreditation = "CBSC",
        //        AdmissionsInProgress = true,
        //        SchoolBus = true,
        //        MidDayMeals = true,
        //        Playground = true,
        //        SchoolStrength = 2000,
        //    });
        //    schoolDetails.Add(new School()
        //    {
        //        SchoolId = "4",
        //        SchoolName = "Oakridge Einstein3",
        //        SchoolLogo = "ms-appx:///Assets/school-logo-4.png",
        //        Rating = 5,
        //        Accreditation = "CBSC",
        //        AdmissionsInProgress = true,
        //        SchoolBus = true,
        //        MidDayMeals = true,
        //        Playground = true,
        //        SchoolStrength = 2000,
        //    });
        //    schoolDetails.Add(new School()
        //    {
        //        SchoolId = "5",
        //        SchoolName = "Oakridge Einstein4",
        //        SchoolLogo = "ms-appx:///Assets/school-logo-4.png",
        //        Rating = 5,
        //        Accreditation = "CBSC",
        //        AdmissionsInProgress = true,
        //        SchoolBus = true,
        //        MidDayMeals = true,
        //        Playground = true,
        //        SchoolStrength = 2000,
        //    });
        //    schoolDetails.Add(new School()
        //    {
        //        SchoolId = "6",
        //        SchoolName = "Oakridge Einstein5",
        //        SchoolLogo = "ms-appx:///Assets/school-logo-4.png",
        //        Rating = 5,
        //        Accreditation = "CBSC",
        //        AdmissionsInProgress = true,
        //        SchoolBus = true,
        //        MidDayMeals = true,
        //        Playground = true,
        //        SchoolStrength = 2000,
        //    });
        //    schoolDetails.Add(new School()
        //    {
        //        SchoolId = "7",
        //        SchoolName = "Oakridge Einstein6",
        //        SchoolLogo = "ms-appx:///Assets/school-logo-4.png",
        //        Rating = 5,
        //        Accreditation = "CBSC",
        //        AdmissionsInProgress = true,
        //        SchoolBus = true,
        //        MidDayMeals = true,
        //        Playground = true,
        //        SchoolStrength = 2000,
        //    });
        //    schoolDetails.Add(new School()
        //    {
        //        SchoolId = "8",
        //        SchoolName = "Oakridge Einstein7",
        //        SchoolLogo = "ms-appx:///Assets/school-logo-4.png",
        //        Rating = 5,
        //        Accreditation = "CBSC",
        //        AdmissionsInProgress = true,
        //        SchoolBus = true,
        //        MidDayMeals = true,
        //        Playground = true,
        //        SchoolStrength = 2000,
        //    });
        //    schoolDetails.Add(new School()
        //    {
        //        SchoolId = "9",
        //        SchoolName = "Oakridge Einstein8",
        //        SchoolLogo = "ms-appx:///Assets/school-logo-4.png",
        //        Rating = 5,
        //        Accreditation = "CBSC",
        //        AdmissionsInProgress = true,
        //        SchoolBus = true,
        //        MidDayMeals = true,
        //        Playground = true,
        //        SchoolStrength = 2000,
        //    });
        //    schoolDetails.Add(new School()
        //    {
        //        SchoolId = "10",
        //        SchoolName = "Oakridge Einstein9",
        //        SchoolLogo = "ms-appx:///Assets/school-logo-4.png",
        //        Rating = 5,
        //        Accreditation = "CBSC",
        //        AdmissionsInProgress = true,
        //        SchoolBus = true,
        //        MidDayMeals = true,
        //        Playground = true,
        //        SchoolStrength = 2000,
        //    });
        //    schoolDetails.Add(new School()
        //    {
        //        SchoolId = "11",
        //        SchoolName = "Oakridge Einstein10",
        //        SchoolLogo = "ms-appx:///Assets/school-logo-4.png",
        //        Rating = 5,
        //        Accreditation = "CBSC",
        //        AdmissionsInProgress = true,
        //        SchoolBus = true,
        //        MidDayMeals = true,
        //        Playground = true,
        //        SchoolStrength = 2000,
        //    });
        //}






       public List<School> DisplaySchoolDetails()
       {
           List<School> details = new List<School>();
           //loc.Add(new Location
           //{
           //    lat=17.1973982738,
           //    lng=78.2913782738

           //});
           //loc.Add( new Location
           //{
           //    lat = 21.1973982738,
           //    lng = 89.2913782738
           //});
           details.Add(new School()
           {
               SchoolId = "1",
               SchoolName = "Chirec",
               Accreditation = "CBSE",
               AdmissionsInProgress = true,
               SchoolStrength = 1000,
               LowerClassRange = "KG",
               UpperClassRange = "12",
               WebsiteLink = "http://www.chirec.com/index.php/about-us",
               City = "Hyderabad",
               Address = " Cyberabad, 1-55/12, Botanical Garden Rd, Sri Ramnagar, Laxmi Nagar, हैदराबाद, Andhra Pradesh 500084",
               Rating = 5,
               Distance = 14,
               //Locations=loc,
               Description = "A pioneer in any field of knowledge charters a course of change. A change in the conventional wisdom keeping in mind the larger interest of an individual for one and society at large.Oakridge International School is one such pioneer in the field of education - introducing new generation education in India.High academic standards are symbolic of its ideology and commitment to academic excellence.Within the broad based curriculum options offered, ample opportunities are provided to develop and assess the critical creative thinking skills, flexibility of approach, ability to work with and serve others, and the grit and fortitude in the face of challenges.A kaleidoscope of extra-curricular activities, support the education programme of the institution, for the ultimate Oakridge product- confident and active learners with a strong individuality."
           });
           details.Add(new School()
           {
               SchoolId = "2",
               SchoolStrength = 400,
               SchoolName = "Oakridge Einstein",
               Accreditation = "CBSE",
               AdmissionsInProgress = true,
               Rating = 4,
               LowerClassRange = "KG",
               UpperClassRange = "12",
               WebsiteLink = "http://www.oakridge.in/aboutus.html#",
              // Locations=loc,
               Description = "A pioneer in any field of knowledge charters a course of change. A change in the conventional wisdom keeping in mind the larger interest of an individual for one and society at large.Oakridge International School is one such pioneer in the field of education - introducing new generation education in India.High academic standards are symbolic of its ideology and commitment to academic excellence.Within the broad based curriculum options offered, ample opportunities are provided to develop and assess the critical creative thinking skills, flexibility of approach, ability to work with and serve others, and the grit and fortitude in the face of challenges.A kaleidoscope of extra-curricular activities, support the education programme of the institution, for the ultimate Oakridge product- confident and active learners with a strong individuality."
           });
           details.Add(new School()
           {
               SchoolId = "3",
               SchoolName = "Indus",
               Accreditation = "CBSE",
               AdmissionsInProgress = true,
               SchoolStrength = 1000,
               LowerClassRange = "KG",
               UpperClassRange = "12",
               WebsiteLink = "http://www.indusschoolhyd.com/InternationalSchool/Hyderabad/Pages/About-Us-001.aspx",
               City = "Hyderabad",
               Address = " Cyberabad, 1-55/12, Botanical Garden Rd, Sri Ramnagar, Laxmi Nagar, हैदराबाद, Andhra Pradesh 500084",
               Rating = 2,
               Distance = 14,
             //  Locations = loc,
               Description = "A pioneer in any field of knowledge charters a course of change. A change in the conventional wisdom keeping in mind the larger interest of an individual for one and society at large.Oakridge International School is one such pioneer in the field of education - introducing new generation education in India.High academic standards are symbolic of its ideology and commitment to academic excellence.Within the broad based curriculum options offered, ample opportunities are provided to develop and assess the critical creative thinking skills, flexibility of approach, ability to work with and serve others, and the grit and fortitude in the face of challenges.A kaleidoscope of extra-curricular activities, support the education programme of the institution, for the ultimate Oakridge product- confident and active learners with a strong individuality."
           });
           details.Add(new School()
           {
               SchoolId = "4",
               SchoolName = "Rockwell",
               Accreditation = "CBSE",
               AdmissionsInProgress = true,
               SchoolStrength = 1000,
               LowerClassRange = "KG",
               UpperClassRange = "12",
               WebsiteLink = "http://www.indusschoolhyd.com/InternationalSchool/Hyderabad/Pages/About-Us-001.aspx",
               City = "Hyderabad",
               Address = " Cyberabad, 1-55/12, Botanical Garden Rd, Sri Ramnagar, Laxmi Nagar, हैदराबाद, Andhra Pradesh 500084",
               Rating = 3,
               Distance = 14,
              // Locations = loc,
               Description = "A pioneer in any field of knowledge charters a course of change. A change in the conventional wisdom keeping in mind the larger interest of an individual for one and society at large.Oakridge International School is one such pioneer in the field of education - introducing new generation education in India.High academic standards are symbolic of its ideology and commitment to academic excellence.Within the broad based curriculum options offered, ample opportunities are provided to develop and assess the critical creative thinking skills, flexibility of approach, ability to work with and serve others, and the grit and fortitude in the face of challenges.A kaleidoscope of extra-curricular activities, support the education programme of the institution, for the ultimate Oakridge product- confident and active learners with a strong individuality."
           });
           details.Add(new School()
           {
               SchoolId = "5",
               SchoolName = "Meridian",
               Accreditation = "ICSE",
               AdmissionsInProgress = true,
               SchoolStrength = 1000,
               LowerClassRange = "KG",
               UpperClassRange = "12",
               WebsiteLink = "http://www.indusschoolhyd.com/InternationalSchool/Hyderabad/Pages/About-Us-001.aspx",
               City = "Hyderabad",
               Address = " Cyberabad, 1-55/12, Botanical Garden Rd, Sri Ramnagar, Laxmi Nagar, हैदराबाद, Andhra Pradesh 500084",
               Rating = 1,
               Distance = 14,
              // Locations = loc,
               Description = "A pioneer in any field of knowledge charters a course of change. A change in the conventional wisdom keeping in mind the larger interest of an individual for one and society at large.Oakridge International School is one such pioneer in the field of education - introducing new generation education in India.High academic standards are symbolic of its ideology and commitment to academic excellence.Within the broad based curriculum options offered, ample opportunities are provided to develop and assess the critical creative thinking skills, flexibility of approach, ability to work with and serve others, and the grit and fortitude in the face of challenges.A kaleidoscope of extra-curricular activities, support the education programme of the institution, for the ultimate Oakridge product- confident and active learners with a strong individuality."
           });
           details.Add(new School()
           {
               SchoolId = "6",
               SchoolName = "Glendale",
               Accreditation = "CBSE",
               AdmissionsInProgress = true,
               SchoolStrength = 1000,
               LowerClassRange = "KG",
               UpperClassRange = "12",
               WebsiteLink = "http://www.indusschoolhyd.com/InternationalSchool/Hyderabad/Pages/About-Us-001.aspx",
               City = "Hyderabad",
               Address = " Cyberabad, 1-55/12, Botanical Garden Rd, Sri Ramnagar, Laxmi Nagar, हैदराबाद, Andhra Pradesh 500084",
               Rating = 5,
               Distance = 14,
             //  Locations = loc,
               Description = "A pioneer in any field of knowledge charters a course of change. A change in the conventional wisdom keeping in mind the larger interest of an individual for one and society at large.Oakridge International School is one such pioneer in the field of education - introducing new generation education in India.High academic standards are symbolic of its ideology and commitment to academic excellence.Within the broad based curriculum options offered, ample opportunities are provided to develop and assess the critical creative thinking skills, flexibility of approach, ability to work with and serve others, and the grit and fortitude in the face of challenges.A kaleidoscope of extra-curricular activities, support the education programme of the institution, for the ultimate Oakridge product- confident and active learners with a strong individuality."
           });
           return details;
       }
       public List<SchoolManagement> GetManagementDetails()
       {
           if (management.Count > 0)
           {
               management.Clear();
           }
          management.Add(new SchoolManagement()
           {
               SchoolId="1",
               Name="Esha gupta",
               Position = "Principal",
               Image = "ms-appx:///Assets/female-thumbail.png"
           });
          management.Add(new SchoolManagement()
          {
              SchoolId = "1",
              Name = "Esha gupta",
              Position = "Vice Principal",
              Image = "ms-appx:///Assets/female-thumbail.png"
          });
          management.Add(new SchoolManagement()
          {
              SchoolId = "1",
              Name = "Esha gupta",
              Position = "Vice Principal",
              Image = "ms-appx:///Assets/female-thumbail.png"
          });
          management.Add(new SchoolManagement()
          {
              SchoolId = "1",
              Name = "Esha gupta",
              Position = "Vice Principal",
              Image = "ms-appx:///Assets/female-thumbail.png"
          });
          management.Add(new SchoolManagement()
          {
              SchoolId = "1",
              Name = "Esha gupta",
              Position = "Vice Principal",
              Image = "ms-appx:///Assets/female-thumbail.png"
          });
          return management;
       }

       public List<Alumni> GetAlumniDetails()
       {
           if (alumni.Count > 0)
               alumni.Clear();
           alumni.Add(new Alumni()
           {
               SchoolId="1",
               AlumniId="1",
               AlumniName="Sen Gupta",
               Position="Associate Consultant",
               Image = "ms-appx:///Assets/female-thumbail.png"
           });
           alumni.Add(new Alumni()
           {
               SchoolId = "1",
               AlumniId = "1",
               AlumniName = "Sen Gupta",
               Position = "Associate Consultant",
               Image = "ms-appx:///Assets/female-thumbail.png"
           });
           alumni.Add(new Alumni()
           {
               SchoolId = "1",
               AlumniId = "1",
               AlumniName = "Sen Gupta",
               Position = "Associate Consultant",
               Image = "ms-appx:///Assets/female-thumbail.png"
           });
           alumni.Add(new Alumni()
           {
               SchoolId = "1",
               AlumniId = "1",
               AlumniName = "Sen Gupta",
               Position = "Associate Consultant",
               Image = "ms-appx:///Assets/female-thumbail.png"
           });
           alumni.Add(new Alumni()
           {
               SchoolId = "1",
               AlumniId = "1",
               AlumniName = "Sen Gupta",
               Position = "Associate Consultant",
               Image = "ms-appx:///Assets/female-thumbail.png"
           });
           alumni.Add(new Alumni()
           {
               SchoolId = "1",
               AlumniId = "1",
               AlumniName = "Sen Gupta",
               Position = "Associate Consultant",
               Image = "ms-appx:///Assets/female-thumbail.png"
           });
           alumni.Add(new Alumni()
           {
               SchoolId = "1",
               AlumniId = "1",
               AlumniName = "Sen Gupta",
               Position = "Associate Consultant",
               Image = "ms-appx:///Assets/female-thumbail.png"
           });
           alumni.Add(new Alumni()
           {
               SchoolId = "1",
               AlumniId = "1",
               AlumniName = "Sen Gupta",
               Position = "Associate Consultant",
               Image = "ms-appx:///Assets/female-thumbail.png"
           });
           alumni.Add(new Alumni()
           {
               SchoolId = "1",
               AlumniId = "1",
               AlumniName = "Sen Gupta",
               Position = "Associate Consultant",
               Image = "ms-appx:///Assets/female-thumbail.png"
           });
           return alumni;
          
       }
    }
}

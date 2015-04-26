using Cirrious.CrossCore;
using Neudesic.Schoolistics.Core.Entities;
using Neudesic.Schoolistics.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Core.Services
{
    public class SearchResultsService : ISearchResultsService
    {
        List<School> schoolDetails = new List<School>();
        List<School> schools = new List<School>();
        List<School> refineSchoolDetails = new List<School>();
        IRestService restService;

        public SearchResultsService(IRestService _restService)
        {
            try
            {
                schoolDetails.Clear();
                schools.Clear();
                AssignSampleData();
                restService = _restService;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchResultsService.cs : SearchResultsService : " + ex.ToString());
            }
        }

        public void PostSearchResults(SearchDetails searchDetails, Action<ResponseMessage<Object>> success, Action<Exception> exception)
        {
            try
            {
                restService.MakeRequest<SearchDetails>(Constants.SearchSchools, "POST", searchDetails, success, exception);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchResultsService.cs : PostSearchResults : " + ex.ToString());
            }
        }

        public void SaveSearchResults(SearchDetails searchDetails, Action<ResponseMessage<Object>> success, Action<Exception> exception)
        {
            try
            {
                restService.MakeRequest<SearchDetails>(Constants.SaveSearch, "POST", searchDetails, success, exception);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchResultsService.cs : SaveSearchResults : " + ex.ToString());
            }
        }

        public List<School> FilterSchoolsBasedOnCondition(SearchDetails searchDetails)
        {
            try
            {
                schoolDetails = schools.Where(x =>
                                                    (searchDetails.Keyword == "" || x.SchoolName.ToLower() == searchDetails.Keyword.ToLower())
                                                    && (searchDetails.Accreditation == "Any Accredition" || x.Accreditation == searchDetails.Accreditation)
                                                    && (searchDetails.City == "Any City" || x.City == searchDetails.City)
                                                    && (searchDetails.Rating == 0 || x.Rating >= searchDetails.Rating)
                                                        //&& (searchDetails.MaximumDistance == 0 || x.Distance == searchDetails.MaximumDistance)
                                                    && (searchDetails.AdmissionsInProgress == false || x.AdmissionsInProgress == searchDetails.AdmissionsInProgress)).ToList();
                return schoolDetails;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchResultsService.cs : FilterSchoolsBasedOnCondition : " + ex.ToString());
                return null;
            }
        }

        public List<School> FilterSchoolsBasedOnRefine(bool? midDayMeals, bool? playGround, bool? digitalClassroom, bool? dayBoarding, bool? transportationFacility)
        {
            try
            {
                refineSchoolDetails = schoolDetails.Where(x => x.MidDayMeals == midDayMeals
                                                     && x.Playground == playGround
                                                     && x.DigitalClassroom == digitalClassroom
                                                     && x.DayBoarding == dayBoarding
                                                     && x.Transportation == transportationFacility).ToList();
                return refineSchoolDetails;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchResultsService.cs : FilterSchoolsBasedOnRefine : " + ex.ToString());
                return null;
            }
        }

        public void AssignSampleData()
        {
            try
            {
                schools.Add(new School()
                {
                    SchoolId = "1",
                    SchoolName = "Chirec",
                    SchoolLogo = "chirec.png",
                    Accreditation = "CBSE",
                    AdmissionsInProgress = true,
                    SchoolStrength = 1000,
                    LowerClassRange = "Nursery",
                    UpperClassRange = "12",
                    WebsiteLink = "",
                    Description = "",
                    City = "Hyderabad",
                    Address = " Cyberabad, 1-55/12, Botanical Garden Rd, Sri Ramnagar, Laxmi Nagar, हैदराबाद, Andhra Pradesh 500084",
                    Rating = 5,
                    MidDayMeals = true,
                    DigitalClassroom = true,
                    DayBoarding = true,
                    SchoolBus = true,
                    Playground = true,
                    Transportation = true,
                    Distance = 14
                });
                schools.Add(new School()
                {
                    SchoolId = "2",
                    SchoolName = "Oakridge Einstein",
                    SchoolLogo = "Oakridge.png",
                    Accreditation = "ICSE",
                    AdmissionsInProgress = true,
                    SchoolStrength = 1000,
                    LowerClassRange = "Nursery",
                    UpperClassRange = "12",
                    WebsiteLink = "",
                    Description = "",
                    City = "Hyderabad",
                    Address = "Oakridge International School Hyderabad Andhra Pradesh",
                    Rating = 4,
                    MidDayMeals = true,
                    DigitalClassroom = true,
                    DayBoarding = true,
                    SchoolBus = true,
                    Playground = true,
                    Transportation = true,
                    Distance = 8
                });
                schools.Add(new School()
                {
                    SchoolId = "3",
                    SchoolName = "Indus",
                    SchoolLogo = "Indus.png",
                    Accreditation = "State Board",
                    AdmissionsInProgress = true,
                    SchoolStrength = 1000,
                    LowerClassRange = "Nursery",
                    UpperClassRange = "12",
                    WebsiteLink = "",
                    Description = "",
                    City = "Hyderabad",
                    Address = "Oakridge International School Hyderabad Andhra Pradesh",
                    Rating = 2,
                    MidDayMeals = true,
                    DigitalClassroom = true,
                    DayBoarding = true,
                    SchoolBus = true,
                    Playground = true,
                    Transportation = true,
                    Distance = 3
                });
                schools.Add(new School()
                {
                    SchoolId = "4",
                    SchoolName = "Rockwell",
                    SchoolLogo = "Indus2.png",
                    Accreditation = "State Board",
                    AdmissionsInProgress = true,
                    SchoolStrength = 1000,
                    LowerClassRange = "Nursery",
                    UpperClassRange = "12",
                    WebsiteLink = "",
                    Description = "",
                    City = "Hyderabad",
                    Address = "Oakridge International School Hyderabad Andhra Pradesh",
                    Rating = 3,
                    MidDayMeals = false,
                    DigitalClassroom = false,
                    DayBoarding = false,
                    SchoolBus = false,
                    Playground = false,
                    Transportation = false,
                    Distance = 18
                });
                schools.Add(new School()
                {
                    SchoolId = "5",
                    SchoolName = "Meridian",
                    SchoolLogo = "Meridian.png",
                    Accreditation = "ICSE",
                    AdmissionsInProgress = true,
                    SchoolStrength = 1000,
                    LowerClassRange = "Nursery",
                    UpperClassRange = "12",
                    WebsiteLink = "",
                    Description = "",
                    City = "Hyderabad",
                    Address = "Oakridge International School Hyderabad Andhra Pradesh",
                    Rating = 1,
                    MidDayMeals = false,
                    DigitalClassroom = false,
                    DayBoarding = false,
                    SchoolBus = false,
                    Playground = false,
                    Transportation = false,
                    Distance = 23
                });
                schools.Add(new School()
                {
                    SchoolId = "6",
                    SchoolName = "Glendale",
                    SchoolLogo = "Glendale.png",
                    Accreditation = "CBSE",
                    AdmissionsInProgress = true,
                    SchoolStrength = 1000,
                    LowerClassRange = "Nursery",
                    UpperClassRange = "12",
                    WebsiteLink = "",
                    Description = "",
                    City = "Hyderabad",
                    Address = "Oakridge International School Hyderabad Andhra Pradesh",
                    Rating = 5,
                    MidDayMeals = false,
                    DigitalClassroom = false,
                    DayBoarding = false,
                    SchoolBus = false,
                    Playground = false,
                    Transportation = false,
                    Distance = 30
                });
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchResultsService.cs : AssignSampleData : " + ex.ToString());
            }
        }

        public List<string> AssignRatingData()
        {
            try
            {
                List<string> schoolRating = new List<string>();
                schoolRating.Add("Any Rating");
                schoolRating.Add("../Assets/1-star-rating.png");
                schoolRating.Add("../Assets/2-star-rating.png");
                schoolRating.Add("../Assets/3-star-rating.png");
                schoolRating.Add("../Assets/4-star-rating.png");
                schoolRating.Add("../Assets/5-star-rating.png");
                return schoolRating;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchResultsService.cs : AssignRatingData : " + ex.ToString());
                return null;
            }
        }

        public List<string> AssignCityData()
        {
            try
            {
                List<string> schoolCity = new List<string>();
                schoolCity.Add("Any City");
                schoolCity.Add("Hyderabad");
                schoolCity.Add("");
                schoolCity.Add("");
                return schoolCity;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchResultsService.cs : AssignCityData : " + ex.ToString());
                return null;
            }
        }

        public List<string> AssignDistanceData()
        {
            try
            {
                List<string> schoolDistance = new List<string>();
                schoolDistance.Add("Any Distance");
                schoolDistance.Add("Below 5 Kms");
                schoolDistance.Add("Below 10 Kms");
                schoolDistance.Add("Below 20 Kms");
                schoolDistance.Add("Above 20 Kms");
                return schoolDistance;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchResultsService.cs : AssignDistanceData : " + ex.ToString());
                return null;
            }
        }

        public List<string> AssignAccreditionData()
        {
            try
            {
                List<string> schoolAccredition = new List<string>();
                schoolAccredition.Add("Any Accredition");
                schoolAccredition.Add("CBSE");
                schoolAccredition.Add("ICSE");
                schoolAccredition.Add("State Board");
                return schoolAccredition;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchResultsService.cs : AssignAccreditionData : " + ex.ToString());
                return null;
            }
        }

        public List<string> AssignClassesData()
        {
            try
            {
                List<string> schoolClasses = new List<string>();
                schoolClasses.Add("Any Class");
                schoolClasses.Add("KG");
                schoolClasses.Add("1");
                schoolClasses.Add("2");
                schoolClasses.Add("3");
                schoolClasses.Add("4");
                schoolClasses.Add("5");
                schoolClasses.Add("6");
                schoolClasses.Add("7");
                schoolClasses.Add("8");
                schoolClasses.Add("9");
                schoolClasses.Add("10");
                schoolClasses.Add("11");
                schoolClasses.Add("12");
                return schoolClasses;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchResultsService.cs : AssignClassesData : " + ex.ToString());
                return null;
            }
        }
    }
}

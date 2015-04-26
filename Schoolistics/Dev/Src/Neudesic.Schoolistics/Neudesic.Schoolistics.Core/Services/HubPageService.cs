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
    public class HubPageService : IHubPageService
    {
         IRestService restService;
         public HubPageService(IRestService _restService)
        {
            try
            {
                restService = _restService;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in HubPageService.cs : Constructor: " + ex.ToString());
            }
        }

         public void GetFeaturedSchoolDetails(School featuredSchoolDetails, Action<ResponseMessage<Object>> success, Action<Exception> exception)
         {
             try
             {
                 restService.MakeRequest<School>(Constants.FeaturedSchools, "GET", featuredSchoolDetails, success, exception);
             }
             catch (Exception ex)
             {
                 Mvx.Error("Error in HubPageService.cs : GetFeaturedSchoolDetails: " + ex.ToString());
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
                Mvx.Error("Error in HubPageService.cs : AssignCityData: " + ex.ToString());
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
                Mvx.Error("Error in HubPageService.cs : AssignDistanceData: " + ex.ToString());
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
                Mvx.Error("Error in HubPageService.cs : AssignAccreditionData: " + ex.ToString());
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
                Mvx.Error("Error in HubPageService.cs : AssignClassesData: " + ex.ToString());
                return null;
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
                Mvx.Error("Error in HubPageService.cs : AssignClassesData: " + ex.ToString());
                return null;
            }
        }
    }
}

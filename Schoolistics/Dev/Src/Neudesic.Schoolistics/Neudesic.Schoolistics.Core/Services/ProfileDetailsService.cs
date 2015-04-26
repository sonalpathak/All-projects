using Neudesic.Schoolistics.Core.Entities;
using Neudesic.Schoolistics.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Core.Services
{
    public class ProfileDetailsService : IProfileDetailsService
    {
        //ProfileDetails profileDetails = new ProfileDetails();
        //public ProfileDetails DisplayProfileDetails()
        //{

        //    GetProfileDetails(Action < ResponseMessage < Object >> success, Action < Exception > exception);
        //    return profileDetails;
        //}




        //public void AssignSampleData()
        //{
        //    profileDetails.Id = "1";
        //    profileDetails.Name = "Sarasvathi Devi";
        //    profileDetails.Gender = "Female";
        //    profileDetails.MailId = "sarasvathi.devi@gmail.com";
        //    profileDetails.PhoneNo = "9999999999";
        //    profileDetails.Education = "B.Com";
        //    profileDetails.Occupation = "HouseWife";
        //    profileDetails.DOB = "11-02-1979";
        //    profileDetails.Relation = "Parent";
        //    profileDetails.Password = "Password";
        //    profileDetails.ReEnterPassword = "ReEnterPassword";
        //}



        IRestService restService;
        public ProfileDetailsService(IRestService _restService)
        {
            restService = _restService;
        }
        public void GetProfileInfo(Action<ResponseMessage<Object>> success, Action<Exception> exception)
        {
            restService.MakeRequest<ProfileDetails>(Constants.Profile, "GET",null, success, exception);
        }

        public void UpdateProfileInfo(User userDetails, Action<ResponseMessage<Object>> success, Action<Exception> exception)
        {
            restService.MakeRequest<User>(Constants.Editprofile, "POST", userDetails, success, exception);
        }


        //public void PostRegistrationDetails(ProfileDetails userDetails, Action<ResponseMessage<Object>> success, Action<Exception> exception)
        //{
        //    restService.MakeRequest<ProfileDetails>(Constants.EditProfile, "POST", userDetails, success, exception);
        //}
    }
    
}

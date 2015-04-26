using Cirrious.CrossCore;
using Neudesic.Schoolistics.Core.Entities;
using Neudesic.Schoolistics.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Core.Services
{
    public class EditProfileService : IEditProfileService
    {
        IRestService restService;
        public EditProfileService(IRestService _restService)
        {
            try
            {
                restService = _restService;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in EditProfileService.cs : EditProfileService : " + ex.ToString());
            }
        }

        public void UpdateProfile(Dictionary<string, string> userDetails, string fileName, string fileContentType, byte[] fileData, Action<ResponseMessage<Object>> success, Action<Exception> exception)
        {
            try
            {
                restService.UploadFilesToServer(Constants.Editprofile, userDetails, "picture", fileName, fileContentType, fileData, success, exception);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in EditProfileService.cs : EditProfileService : " + ex.ToString());
            }
        }

        public void UpdateProfileWithoutPicture(User userDetails, Action<ResponseMessage<Object>> success, Action<Exception> exception)
        {
            try
            {
                restService.MakeRequest<User>(Constants.EditProfileWithoutImage, "POST", userDetails, success, exception);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in EditProfileService.cs : EditProfileService : " + ex.ToString());
            }
        }
    }
}

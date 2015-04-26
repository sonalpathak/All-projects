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
    public class RegistrationService : IRegistrationService
    {
        IRestService restService;
        public RegistrationService(IRestService _restService)
        {
            restService = _restService;
        }

        public void PostRegistrationDetails(User userDetails,Action<ResponseMessage<Object>> success, Action<Exception> exception)
        {
            try
            {
                restService.MakeRequest<User>(Constants.SignUp, "POST", userDetails, success, exception);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in RegistrationService.cs : PostRegistrationDetails : " + ex.ToString());
            }
        }
        public void CheckLoginDetails(User userDetails, Action<ResponseMessage<object>> success, Action<Exception> exception)
        {
            try
            {
                restService.MakeRequest<User>(Constants.Login, "POST", userDetails, success, exception);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in RegistrationService.cs : CheckLoginDetails : " + ex.ToString());
            }
        }


    }
}

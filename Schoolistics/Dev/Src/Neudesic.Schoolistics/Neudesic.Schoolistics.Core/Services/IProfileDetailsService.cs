using Neudesic.Schoolistics.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Core.Services
{
    public interface IProfileDetailsService
    {
      //ProfileDetails  DisplayProfileDetails();
        void GetProfileInfo(Action<ResponseMessage<Object>> success, Action<Exception> exception);
      //void PostProfileDetails(ProfileDetails userDetails, Action<ResponseMessage<Object>> success, Action<Exception> exception);
        void UpdateProfileInfo(User userDetails, Action<ResponseMessage<Object>> success, Action<Exception> exception);

    }
}

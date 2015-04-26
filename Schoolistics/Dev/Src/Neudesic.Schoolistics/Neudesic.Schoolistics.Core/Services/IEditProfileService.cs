using Neudesic.Schoolistics.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Core.Services
{
    public interface IEditProfileService
    {
        void UpdateProfile(Dictionary<string, string> userDetails, string fileName, string fileContentType, byte[] fileData, Action<ResponseMessage<Object>> success, Action<Exception> exception);

        void UpdateProfileWithoutPicture(User userDetails, Action<ResponseMessage<Object>> success, Action<Exception> exception);
    }
}

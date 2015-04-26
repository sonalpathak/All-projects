using Neudesic.Schoolistics.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Core.Services
{
    public interface IRestService
    {
        void MakeRequest<T>(string requestUrl, string verb,T postObject, Action<ResponseMessage<Object>> successAction, Action<Exception> errorAction);
        void UploadFilesToServer(string uri, Dictionary<string, string> data, string paramName, string fileName, string fileContentType, byte[] fileData, Action<ResponseMessage<Object>> successAction, Action<Exception> errorAction);
    }
}

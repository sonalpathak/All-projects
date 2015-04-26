using Neudesic.Schoolistics.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Core.Services
{ 
    public interface IRegistrationService
    {
        void PostRegistrationDetails(User userDetails, Action<ResponseMessage<Object>> success, Action<Exception> exception);
        void CheckLoginDetails(User userDetails, Action<ResponseMessage<Object>> success, Action<Exception> exception);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Core.Entities
{
    public class ResponseMessage<T> where T : class
    {
        public string Message { get; set; }

        public int SuccessCode { get; set; }

        public int ErrorCode { get; set; }

        public T Data { get; set; }
    }
}

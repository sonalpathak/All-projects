using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Core.Entities
{
    public class UserSurveyDetails
    {
        public string SurveyId { get; set; }
        public string UserName{get;set;}
        public DateTime Created { get; set; }
    }
}

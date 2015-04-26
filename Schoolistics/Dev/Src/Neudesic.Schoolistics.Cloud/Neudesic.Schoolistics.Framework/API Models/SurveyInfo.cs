using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Framework.API_Models
{
    public class SurveyInfo
    {
        public string SurveyId { get; set; }

        public string Question { get; set; }

        public List<SurveyOption> Options { get; set; }

        public bool IsClosed { get; set; }

        public DateTime Created { get; set; }

    }
}

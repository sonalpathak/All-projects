using Neudesic.Schoolistics.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Core.Entities
{
    public class Survey
    {
        public string SurveyId { get; set; }

        public string Question { get; set; }

        public List<SurveyOption> Options { get; set; }
       
        public DateTime Created { get; set; }

        public bool ListviewVisibility { get; set; }

        public bool ChartVisibility { get; set; }

        public bool IsClosed { get; set; }
    }
}

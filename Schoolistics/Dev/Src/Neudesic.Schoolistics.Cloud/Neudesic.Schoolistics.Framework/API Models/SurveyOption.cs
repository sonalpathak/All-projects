using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Framework.API_Models
{
    public class SurveyOption
    {
        public string OptionId { get; set; }

        public string Option { get; set; }

        public int Votes { get; set; }
    }
}

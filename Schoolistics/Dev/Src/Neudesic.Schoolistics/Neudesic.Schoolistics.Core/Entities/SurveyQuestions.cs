using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Core.Entities
{
    public class SurveyQuestions
    {
        public string Question { get; set; }
        public List<SurveyAnswers> Answers { get; set; }
    }
}

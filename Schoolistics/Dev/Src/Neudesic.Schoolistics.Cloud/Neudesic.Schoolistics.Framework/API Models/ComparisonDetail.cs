using Neudesic.Schoolistics.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Framework.API_Models
{
    public class ComparisonDetail
    {
        public string ComparisonId { get; set; }

        public List<SchoolsComparison> Schools { get; set; }
    }
}

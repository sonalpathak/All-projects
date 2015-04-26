using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Core.Entities
{
    public class SchoolsComparison
    {
        public string ComparisonId { get; set; }

        public List<School> Schools { get; set; }
    }
}

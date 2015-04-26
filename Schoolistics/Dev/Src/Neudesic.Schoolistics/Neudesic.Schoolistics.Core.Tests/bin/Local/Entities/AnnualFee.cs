using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Core.Entities
{
    public class AnnualFee
    {
        public string SchoolId { get; set; }

        public string Class { get; set; }

        public int Fee { get; set; }
    }
}

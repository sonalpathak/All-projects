using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Core.Entities
{
    public class BookmarkedSchools
    {
        public string UserId { get; set; }

        public string SchoolId { get; set; }

        public string SchoolName { get; set; }

        public string SchoolLogo { get; set; }

        public DateTime Created { get; set; }
    }
}

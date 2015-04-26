using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Core.Entities
{
   public  class UserSchoolRating
    {
        public string Username { get;set;}

        public string SchoolId { get; set; }

        public string City { get; set; }

        public double Rating { get; set; }
    }
}

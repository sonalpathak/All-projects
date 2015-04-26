using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Core.Entities
{
   public class UserSchoolComment
    {
        public string SchoolId{ get; set;}

        public string CommentId{ get; set; }

        public string Username { get; set; }

        public string Comment { get; set; }

        public DateTime Created { get; set; }
    }
}

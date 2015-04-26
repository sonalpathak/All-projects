using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Core.Entities
{
    public class User
    {
        public string UserId { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        public string FullName { get; set; }

        public string DateOfBirth { get; set; }

        public string PhoneNumber { get; set; }

        public string Occupation { get; set; }

        public string UserType { get; set; }

        public string Gender { get; set; }

        public string Education { get; set; }

        public string Image { get; set; }

        public string Created { get; set; }

        public bool IsSocialUser { get; set; }

        public string AuthToken { get; set; }
    }
}

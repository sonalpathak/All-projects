using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Framework.API_Models
{
    public class UserProfileData
    {
        public string UserId { get; set; }

        public string Username { get; set; }
        
        public string Email { get; set; }

        public string Password { get; set; }

        public string OldPassword { get; set; }

        public string Salt { get; set; }

        public string DateOfBirth { get; set; }

        public string PhoneNumber { get; set; }

        public string Occupation { get; set; }

        public string Gender { get; set; }

        public string Image { get; set; }

        public DateTime Created { get; set; }

        public bool IsSocialUser { get; set; }
    }
}

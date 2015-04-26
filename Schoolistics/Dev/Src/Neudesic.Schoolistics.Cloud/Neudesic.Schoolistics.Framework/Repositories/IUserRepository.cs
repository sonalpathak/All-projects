using Neudesic.Schoolistics.Framework.API_Models;
using Neudesic.Schoolistics.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Framework.Repositories
{
    public interface IUserRepository
    {
        bool CreateUser(UserInfo newUser);

        UserInfo FindByUsername(string username);

        UserInfo FindByEmail(string emailId);

        bool ValidateAccount(string username, string password);

        string UpdatePassword(UserInfo user);

        bool UpdateUser(UserProfileData user);
    }
}

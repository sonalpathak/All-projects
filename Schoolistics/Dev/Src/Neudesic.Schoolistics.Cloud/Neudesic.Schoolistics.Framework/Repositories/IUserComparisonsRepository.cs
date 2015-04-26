using Neudesic.Schoolistics.Framework.API_Models;
using Neudesic.Schoolistics.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Framework.Repositories
{
    public interface IUserComparisonsRepository
    {
        bool Create(UserSchoolsComparisons comparison);

        List<UserSchoolsComparisons> GetUserComparisons(string username);

        List<ComparisonCount> GetPopularComparisons();
    }
}

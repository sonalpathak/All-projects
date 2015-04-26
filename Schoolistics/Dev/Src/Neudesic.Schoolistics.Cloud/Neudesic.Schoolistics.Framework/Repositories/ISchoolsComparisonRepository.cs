using Neudesic.Schoolistics.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Framework.Repositories
{
    public interface ISchoolsComparisonRepository
    {
        string Create(List<SchoolsComparison> comparisons);

        List<SchoolsComparison> GetSchoolsByComparisonID(string comparisonID);

       // List<SchoolsComparison> GetAllComparedSchools(List<SchoolsComparison> schools);
    }
}

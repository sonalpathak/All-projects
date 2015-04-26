using Neudesic.Schoolistics.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Core.Services
{
    public interface IBookmarkedSchoolService
    {
       void PostBookmarkedSchool(BookmarkedSchools bookmarkedschooldetails, Action<ResponseMessage<Object>> success, Action<Exception> exception);
       void GetBookmarkedSchools(Action<ResponseMessage<Object>> success, Action<Exception> exception);
       List<BookmarkedSchools> GetCachedBookmarkedSchools();
       void SetBookmarkedSchools(List<BookmarkedSchools> schools);
 
    }
}

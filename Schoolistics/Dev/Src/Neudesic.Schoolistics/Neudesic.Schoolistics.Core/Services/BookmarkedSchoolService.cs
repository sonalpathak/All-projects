using Cirrious.CrossCore;
using Neudesic.Schoolistics.Core.Entities;
using Neudesic.Schoolistics.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Core.Services
{
    public class BookmarkedSchoolService : IBookmarkedSchoolService
    {
        IRestService restService;
        public BookmarkedSchoolService(IRestService _restService)
        {
            try
            {
                restService = _restService;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in BookmarkedSchoolService.cs : BookmarkedSchoolService : " + ex.ToString());
            }
            
        }

        public static List<BookmarkedSchools> listofschools = null;

        public List<BookmarkedSchools> GetCachedBookmarkedSchools()
        {
            try
            {
                return listofschools;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in BookmarkedSchoolService.cs : GetCachedBookmarkedSchools : " + ex.ToString());
                return null;
            }
        }

        public void SetBookmarkedSchools(List<BookmarkedSchools> schools)
        {
            try
            {
                listofschools = schools;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in BookmarkedSchoolService.cs : GetCachedBookmarkedSchools : " + ex.ToString());
               
            }
            
        }

        public void PostBookmarkedSchool(BookmarkedSchools bookmarkedschooldetails, Action<ResponseMessage<Object>> success, Action<Exception> exception)
        {
            try
            {
                restService.MakeRequest<BookmarkedSchools>(Constants.BookmarkSchool, "POST", bookmarkedschooldetails, success, exception);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in BookmarkedSchoolService.cs : PostBookmarkedSchool : " + ex.ToString());

            }

        }

        public void GetBookmarkedSchools(Action<ResponseMessage<Object>> success, Action<Exception> exception)
        {
            try
            {
                restService.MakeRequest<Object>(Constants.GetUserBookmarkedSchools, "GET", null, success, exception);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in BookmarkedSchoolService.cs : GetBookmarkedSchools : " + ex.ToString());

            }
        }
    }
}

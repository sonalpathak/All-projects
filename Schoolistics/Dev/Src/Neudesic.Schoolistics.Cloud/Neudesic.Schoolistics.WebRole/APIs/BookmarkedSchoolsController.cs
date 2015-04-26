using Neudesic.Schoolistics.Framework.API_Models;
using Neudesic.Schoolistics.Framework.Models;
using Neudesic.Schoolistics.Framework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Neudesic.Schoolistics.WebRole.APIs
{
    public class BookmarkedSchoolsController : ApiController
    {
        public IBookmarkedSchoolsRepository bookmarksRepository;

        public BookmarkedSchoolsController(IBookmarkedSchoolsRepository _bookmarksRepository)
        {
            this.bookmarksRepository = _bookmarksRepository;
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage BookmarkSchool(BookmarkedSchools school)
        {
            var isSaved = this.bookmarksRepository.Create(school);

            return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<Object> { SuccessCode = 1, Message = "School bookmarked successfully" });
        }

        [HttpGet]
        [Authorize]
        public HttpResponseMessage GetUserBookmarkedSchools()
        {
            var username = HttpContext.Current.User.Identity.Name;

            var bookmarkedSchools = this.bookmarksRepository.GetUserBookmarkedSchools(username);

            return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<List<BookmarkedSchools>> { SuccessCode = 1, Data = bookmarkedSchools });
        }
    }
}
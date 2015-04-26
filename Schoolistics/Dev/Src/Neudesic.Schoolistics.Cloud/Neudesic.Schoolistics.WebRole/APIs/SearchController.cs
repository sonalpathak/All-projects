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
    public class SearchController : ApiController
    {
        ISearchRepository searchRepository;

        public SearchController(ISearchRepository _searchRepository)
        {
            this.searchRepository = _searchRepository; 
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage SaveSearch(SearchDetails search)
        {
            var isSaved = this.searchRepository.Create(search);

            return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<Object> { SuccessCode = 1, Message="Search saved successfully" });        
        }

        [HttpGet]
        [Authorize]
        public HttpResponseMessage GetUserSavedSearches()
        {
            var username = HttpContext.Current.User.Identity.Name;

            var savedSearches = this.searchRepository.GetUserSearches(username);

            return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<List<SearchDetails>> { SuccessCode = 1, Data = savedSearches });
        }
    }
}
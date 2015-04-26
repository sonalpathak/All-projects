using System;
using System.Collections.Generic;
using Neudesic.Schoolistics.Framework.Repositories;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using Neudesic.Schoolistics.Framework.API_Models;
using Neudesic.Schoolistics.Framework.Models;

namespace Neudesic.Schoolistics.WebRole.APIs
{
    public class NewsArticleController:ApiController
    {
        public INewsArticlesRepository newsArticleRepository;

        public NewsArticleController(INewsArticlesRepository _newsArticleRep)
        {
            newsArticleRepository = _newsArticleRep;
        }

        [HttpGet]
        public HttpResponseMessage GetallNewsArticles()
        {
            var newsArticles = this.newsArticleRepository.AllNewsArticles();
            return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<List<NewsArticle>> { SuccessCode = 1, Data=newsArticles});
        }


        [HttpGet]
        public HttpResponseMessage GetLatestNewsArticles()
        {
            var newsArticles = this.newsArticleRepository.LatestNewsArticles();
            return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<List<NewsArticle>> { SuccessCode = 1, Data = newsArticles });
        }
    }
}
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
    public class NewsArticlesService : INewsArticlesService
    {
         IRestService restService;
         public NewsArticlesService(IRestService _restService)
        {
            try
            {
                restService = _restService;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in NewsArticlesService.cs : NewsArticlesService : " + ex.ToString());
            }
            
        }

        public void GetNewsArticles(Action<ResponseMessage<object>> success, Action<Exception> exception)
        {
            try
            {
                restService.MakeRequest<List<NewsArticle>>(Constants.GetAllNewsArticles, "GET", null, success, exception);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in NewsArticlesService.cs : GetNewsArticles : " + ex.ToString());
            }
        }

        public void GetLatestNewsArticles(Action<ResponseMessage<object>> success, Action<Exception> exception)
        {
            try
            {
                restService.MakeRequest<List<NewsArticle>>(Constants.GetLatestNewsArticles, "GET", null, success, exception);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in NewsArticlesService.cs : GetLatestNewsArticles : " + ex.ToString());
            }
        }
    }

      
}





//
//        List<NewsArticle> newsArticle = new List<NewsArticle>();
//        public List<NewsArticle> DisplayNewsArticlesGridItems()
//        {
//            AssignNewsArticlesSampleData();
//            return newsArticle ;
//        }
//newsArticle.Add(new NewsArticle()
//             {
//                 ArticleId="1",
//                 ContentUrl = "ms-appx:///Assets/new-article-details-img.png",
//                 HeaderImage = "ms-appx:///Assets/new-article-details-img.png",
//                 Source="Education consultant",
//                 AuthorName="Ellen Church",
//                 PublishDate="February 17th,2014",
//                 GridContent = "What should I do if I'm really unhappy with my child's school?",
//                 Rating=4
                
//             });
//             newsArticle.Add(new NewsArticle()
//             {
//                 ArticleId = "2",
//                 ContentUrl = "ms-appx:///Assets/new-article-img-2.png",
//                 Source = "Education consultant",
//                 AuthorName = "Ellen Church",
//                 PublishDate = "February 17th,2014",
//                 GridContent = "Is your kid paying attention in School?",
//                 Rating = 4

//             });
//             newsArticle.Add(new NewsArticle()
//             {
//                 ArticleId = "3",
//                 ContentUrl = "ms-appx:///Assets/new-article-img-4.png",
//                 Source = "Education consultant",
//                 AuthorName = "Ellen Church",
//                 PublishDate = "February 17th,2014",
//                 GridContent = "How to spen quality time with your kids?",
//                 Rating = 4

//             });
//             newsArticle.Add(new NewsArticle()
//             {
//                 ArticleId = "4",
//                 ContentUrl = "ms-appx:///Assets/new-article-img-3.png",
//                 Source = "Education consultant",
//                 AuthorName = "Ellen Church",
//                 PublishDate = "February 17th,2014",
//                 GridContent = "Schools offering traditional dance classes",
//                 Rating = 4

//             });
//             newsArticle.Add(new NewsArticle()
//             {
//                 ArticleId = "5",
//                 ContentUrl = "ms-appx:///Assets/new-article-details-img.png",
//                 Source = "Education consultant",
//                 AuthorName = "Ellen Church",
//                 PublishDate = "February 17th,2014",
//                 GridContent = "Title of the news article goes here..",
//                 Rating = 4

//             });
//             newsArticle.Add(new NewsArticle()
//             {
//                 ArticleId = "6",
//                 ContentUrl = "ms-appx:///Assets/new-article-details-img.png",
//                 Source = "Education consultant",
//                 AuthorName = "Ellen Church",
//                 PublishDate = "February 17th,2014",
//                 GridContent = "Title of the news article goes here..",
//                 Rating = 4

//             });
//             newsArticle.Add(new NewsArticle()
//             {
//                 ArticleId = "7",
//                 ContentUrl = "ms-appx:///Assets/new-article-details-img.png",
//                 Source = "Education consultant",
//                 AuthorName = "Ellen Church",
//                 PublishDate = "February 17th,2014",
//                 GridContent = "Title of the news article goes here..",
//                 Rating = 4

//             });
//             newsArticle.Add(new NewsArticle()
//             {
//                 ArticleId = "8",
//                 ContentUrl = "ms-appx:///Assets/new-article-details-img.png",
//                 Source = "Education consultant",
//                 AuthorName = "Ellen Church",
//                 PublishDate = "February 17th,2014",
//                 GridContent = "Title of the news article goes here..",
//                 Rating = 4

//             });

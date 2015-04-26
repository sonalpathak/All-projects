using Neudesic.Schoolistics.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Core.Services
{
   public interface INewsArticlesService
    {
       //List<NewsArticle> DisplayNewsArticlesGridItems();
       void GetNewsArticles(Action<ResponseMessage<Object>> success, Action<Exception> exception);
       void GetLatestNewsArticles(Action<ResponseMessage<Object>> success, Action<Exception> exception);
      
    }
}

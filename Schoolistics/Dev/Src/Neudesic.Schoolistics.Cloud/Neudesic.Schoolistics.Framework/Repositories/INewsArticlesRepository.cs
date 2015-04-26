﻿using Neudesic.Schoolistics.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Framework.Repositories
{
    public interface INewsArticlesRepository
    {
        List<NewsArticle> AllNewsArticles();
        List<NewsArticle> LatestNewsArticles();

    }
}

using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;
// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the NewsArticlesItemsViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
using Neudesic.Schoolistics.Core.Entities;
using Neudesic.Schoolistics.Core.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
namespace Neudesic.Schoolistics.Core.ViewModels
{
    /// <summary>
    /// Define the NewsArticlesItemsViewModel type.
    /// </summary>
    public class NewsArticlesItemsViewModel : BaseViewModel
    {
         private INewsArticlesService _newsArticlesItemsService;
         private MvxCommand goBackClickCommand;

         public NewsArticlesItemsViewModel(INewsArticlesService newsArticlesItemsService)
        {
            try
            {
                _newsArticlesItemsService = newsArticlesItemsService;
                GetNewsArticles();
            }
            //var items = _newsArticlesItemsService.GetNewsArticles();
            //NewsArticleitems = new ObservableCollection<NewsArticle>(items);
            catch (Exception ex)
            {
                Mvx.Error("Error in NewsArticlesItemsViewModel.cs : NewsArticlesItemsViewModel : " + ex.ToString());
            }
            
        }

        private ObservableCollection<NewsArticle> newsArticleitems;

        public ObservableCollection<NewsArticle> NewsArticleitems
        {
            get { return newsArticleitems; }
            set
            {
                newsArticleitems = value;
                RaisePropertyChanged(() => NewsArticleitems);
            }
        }
        public ICommand GoBackClickCommand
        {
            get
            {
                goBackClickCommand = goBackClickCommand ?? new MvxCommand(GoBackClick);
                return goBackClickCommand;
            }
        }
        public void GoBackClick()
        {
           
            ShowViewModel<HubpageViewModel>();
        }


        public void GetNewsArticles()
        {
            try
            {
                _newsArticlesItemsService.GetNewsArticles(GetNewsArticlesSuccess, GetNewsArticlesFailure);
            }
            catch (Exception ex)
            {
                string a = ex.Message;
            }

        }

        private void GetNewsArticlesFailure(System.Exception e)
        {
            try
            {
                var message = e.Message;
                //    ProgressBarVisibility = false;
                //    GridViewVisibility = true;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in NewsArticlesItemsViewModel.cs : GetNewsArticlesFailure : " + ex.ToString());
            }
        }

        private void GetNewsArticlesSuccess(ResponseMessage<object> response)
        {
            try
            {
                if (response.SuccessCode == 1)
                {

                    var newsArticlesList = JsonConvert.DeserializeObject<List<NewsArticle>>(response.Data.ToString());
                    NewsArticleitems = new ObservableCollection<NewsArticle>(newsArticlesList);
                    //ProgressBarVisibility = false;
                    //GridViewVisibility = true;
                    // _Messenger.Publish(new NavigationMessage<User>(this) { Message = "FeaturedSchoolsSuccess", Data = featuredSchoolDetails });
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in NewsArticlesItemsViewModel.cs : GetNewsArticlesSuccess : " + ex.ToString());
            }
        }

        //public void GetNewsArticlesSuccess(ResponseMessage<Object> response)
        //{
        //    if (response.SuccessCode == 1)
        //    {

        //        var newsArticlesList = JsonConvert.DeserializeObject<List<NewsArticle>>(response.Data.ToString());
        //        NewsArticleitems = new ObservableCollection<NewsArticle>(newsArticlesList);
        //        ProgressBarVisibility = false;
        //        GridViewVisibility = true;
        //        // _Messenger.Publish(new NavigationMessage<User>(this) { Message = "FeaturedSchoolsSuccess", Data = featuredSchoolDetails });
        //    }
        //}

        //public void GetNewsArticlesFailure(Exception e)
        //{
        //    var message = e.Message;
        //    ProgressBarVisibility = false;
        //    GridViewVisibility = true;
        //}


        //private Cirrious.MvvmCross.ViewModels.MvxCommand _goNewsArticlesItemDetailPage;
        //public System.Windows.Input.ICommand GoNewsArticlesItemDetailPage
        //{
        //    get
        //    {
        //        _goNewsArticlesItemDetailPage = _goNewsArticlesItemDetailPage ?? new Cirrious.MvvmCross.ViewModels.MvxCommand(DoGoNewsArticlesItemDetail);
        //        return _goNewsArticlesItemDetailPage;
        //    }
        //}

        //private void DoGoNewsArticlesItemDetail()
        //{
        //    ShowViewModel<NewsArticlesItemDetailViewModel>();

        //}


        public ICommand ShowDetailCommand
        {
            get
            {
                return new MvxCommand<NewsArticle>(item => ShowViewModel<NewsArticlesItemDetailViewModel>(item));
            }
        }
    }
}

using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;
using Neudesic.Schoolistics.Core.Entities;
// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the NewsArticlesItemDetailViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
using Neudesic.Schoolistics.Core.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
namespace Neudesic.Schoolistics.Core.ViewModels
{
    /// <summary>
    /// Define the NewsArticlesItemDetailViewModel type.
    /// </summary>
    public class NewsArticlesItemDetailViewModel : BaseViewModel
    {
         private INewsArticlesService _newsArticlesItemsService;
         private MvxCommand goBackClickCommand;

         public NewsArticlesItemDetailViewModel(INewsArticlesService newsArticlesItemsService)
        {
            
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
             try
             {

                 ShowViewModel<HubpageViewModel>();
             }
             catch (Exception ex)
             {
                 Mvx.Error("Error in NewsArticlesItemDetailViewModel.xaml.cs : GoBackClick : " + ex.ToString());
             }
         }
        

        private NewsArticle newsArticleItemDetail;


        public NewsArticle NewsArticleItemDetail
        {
            get { return newsArticleItemDetail; }
            set
            {
                newsArticleItemDetail = value;
                RaisePropertyChanged(() => NewsArticleItemDetail);
            }
        }

        private string itemDetailPageTitle;


        public string ItemDetailPageTitle
        {
            get { return itemDetailPageTitle; }
            set
            {
                itemDetailPageTitle = value;
                RaisePropertyChanged(() => ItemDetailPageTitle);
            }
        }

        public void Init(NewsArticle articlelist)
        {
            try
            {
                newsArticleItemDetail = articlelist;
                itemDetailPageTitle = articlelist.GridContent.ToUpper();
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in NewsArticlesItemDetailViewModel.xaml.cs : Init : " + ex.ToString());
            }
        }
       

    }
}

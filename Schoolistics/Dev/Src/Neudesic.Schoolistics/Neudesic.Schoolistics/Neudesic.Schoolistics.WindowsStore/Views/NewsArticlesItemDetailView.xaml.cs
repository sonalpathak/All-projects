using Neudesic.Schoolistics.Core.ViewModels;
using Neudesic.Schoolistics.WindowsStore.Usercontrols;
using Windows.UI.Xaml;
// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the NewsArticlesItemDetailView.xaml type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media.Animation;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using System.Text;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Input;
using Neudesic.Schoolistics.Core.Entities;
using System;
using Cirrious.CrossCore;
namespace Neudesic.Schoolistics.WindowsStore.Views
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class NewsArticlesItemDetailView
    {
        static Popup _popup=new Popup();
        NewsArticleBottomAppBarControl na;
        rating_usercontrol uc;
        private Popup postCommentPopUp;
        private Popup commentPopUp;
        private NewsArticlePostCommentControl postComment;     


        /// <summary>
        /// Initializes a new instance of the <see cref="NewsArticlesItemDetailView"/> class.
        /// </summary>
        public NewsArticlesItemDetailView()
        {
            try
            {
                // DataTransferManager.GetForCurrentView().DataRequested -= ShareNewsHandler;
                this.InitializeComponent();
                // DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
                // DataTransferManager.GetForCurrentView().DataRequested += ShareNewsHandler;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in NewsArticlesItemDetailView.xaml.cs : NewsArticlesItemDetailView : " + ex.ToString());
            }

        }

        //private void NewsArticleBottomAppBarControl_shareNewsArticleEvent(object sender, RoutedEventArgs e)
        //{
        //    DataTransferManager.ShowShareUI();
        //}
        //private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs e)
        //{
        //    var request = e.Request;
        //    request.Data.Properties.Title = "Share Text Example";
        //    request.Data.Properties.Description = "A demonstration that shows how to share text.";
        //    request.Data.SetText("Hello World!");
        //    DataTransferManager.GetForCurrentView().DataRequested -= ShareNewsHandler;
        //}

        //private void ShareNewsHandler(DataTransferManager sender, DataRequestedEventArgs e)
        //{
        //    NewsArticle newsArticles = new NewsArticle();
        //     var request = e.Request;
        //     request.Data.Properties.Title = newsArticles.AuthorName;
        //     request.Data.Properties.Description = newsArticles.HeaderImage;
        //     DataTransferManager.GetForCurrentView().DataRequested -= ShareNewsHandler;
            
           
        //}

        private void NewsArticleBottomAppBarControl_postCommentTappedEvent(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {
                this.BottomAppBar.IsOpen = false;
                if (postCommentPopUp == null)
                {
                    postCommentPopUp = this.GetPostCommentPopUp();
                    postCommentPopUp.IsOpen = true;
                    return;
                }
                postCommentPopUp.IsOpen = !postCommentPopUp.IsOpen;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in NewsArticlesItemDetailView.xaml.cs : NewsArticleBottomAppBarControl_postCommentTappedEvent : " + ex.ToString());
            }
        }


        public Popup GetPostCommentPopUp()
        {
            try
            {

                postComment = new NewsArticlePostCommentControl();
                postComment.DataContext = (NewsArticlesItemDetailViewModel)ViewModel;

                postComment.Width = 700;
                postComment.Height = Window.Current.Bounds.Height;

                postCommentPopUp = new Popup();
                postCommentPopUp.Width = 700;
                postCommentPopUp.Height = Window.Current.Bounds.Height;
                postCommentPopUp.ChildTransitions = new TransitionCollection();
                postCommentPopUp.ChildTransitions.Add(new PaneThemeTransition());
                postCommentPopUp.IsLightDismissEnabled = true;
                postCommentPopUp.Child = postComment;
                postCommentPopUp.HorizontalOffset = Window.Current.Bounds.Width - postCommentPopUp.Width;
                postCommentPopUp.VerticalOffset = 0;
                return postCommentPopUp;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in NewsArticlesItemDetailView.xaml.cs : GetPostCommentPopUp : " + ex.ToString());
                return null;
            }
        }
        private void rp_star4_clicked_event(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {

                BottonAppBar appbar = new BottonAppBar();

                //NewsArticlePageBottomAppBar.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                //NewsArticleBottomAppBarControl na1 = new NewsArticleBottomAppBarControl();

                int i = 0;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in NewsArticlesItemDetailView.xaml.cs : rp_star4_clicked_event : " + ex.ToString());
               
            }

        
        }

        private void rp_star2_clicked_event(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }

        private void rp_star1_clicked_event(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }

        private void rp_star3_clicked_event(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }

        private void rp_star5_clicked_event(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }



        private void NewsArticleBottomAppBarControl_NewsArticleRateTappedEventHandler(object sender, TappedRoutedEventArgs e)
        {
            try
            {

                rating_usercontrol rp = new rating_usercontrol();
                _popup.Child = rp;

                _popup.HorizontalOffset = 1140;
                _popup.VerticalOffset = 650;
                _popup.IsOpen = true;
                rp.star5_clicked_event += rp_star5_clicked_event;
                rp.star3_clicked_event += rp_star3_clicked_event;
                rp.star1_clicked_event += rp_star1_clicked_event;
                rp.star2_clicked_event += rp_star2_clicked_event;
                rp.star4_clicked_event += rp_star4_clicked_event;
                _popup.Child = rp;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in NewsArticlesItemDetailView.xaml.cs : NewsArticleBottomAppBarControl_NewsArticleRateTappedEventHandler : " + ex.ToString());

            }

        

        }

        private void NewsArticlePageBottomAppBar_Closed(object sender, object e)
        {
            try
            {
                if (_popup.IsOpen)
                {
                    _popup.IsOpen = false;
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in NewsArticlesItemDetailView.xaml.cs : NewsArticlePageBottomAppBar_Closed : " + ex.ToString());

            }
            
        }

        //private void GoBack(object sender, RoutedEventArgs e)
        //{
        //    DataTransferManager.GetForCurrentView().DataRequested -= ShareNewsHandler;
        //}
       

       

    }
}
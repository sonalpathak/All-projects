using Cirrious.CrossCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Neudesic.Schoolistics.WindowsStore.Usercontrols
{
    public sealed partial class NewsArticleBottomAppBarControl : UserControl
    {
        public event TappedEventHandler NewsArticleRateTappedEventHandler;
        static Popup _popup = new Popup();
        public event RoutedEventHandler postCommentTappedEvent;
        public event RoutedEventHandler shareNewsArticles;

        public NewsArticleBottomAppBarControl()
        {
            this.InitializeComponent();
        }

        private void postCommentButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.postCommentTappedEvent(sender, e);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in NewsArticlesBottomAppBarControl.xaml.cs : postCommentButton_Click : " + ex.ToString());
            }
        }

        

        //private void shareButton_Click(object sender, RoutedEventArgs e)
        //{
            
        //    this.shareNewsArticles(sender, e);
        //    //DataTransferManager.ShowShareUI();
        //    //DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
        //    //DataTransferManager.GetForCurrentView().DataRequested += OnDataRequested;
           
        //}


        

        //private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs e)
        //{
        //    var request = e.Request;
        //    request.Data.Properties.Title = "Share Text Example";
        //    request.Data.Properties.Description = "A demonstration that shows how to share text.";
        //    request.Data.SetText("Hello World!");
        //}

        private void RateButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                this.NewsArticleRateTappedEventHandler(sender, e);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in NewsArticlesBottomAppBarControl.xaml.cs : RateButton_Tapped : " + ex.ToString());
            }


        }
       
    }
}

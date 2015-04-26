using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
using Neudesic.Schoolistics.Core.Entities;
using Neudesic.Schoolistics.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    public sealed partial class NewsArticlePostCommentControl : UserControl
    {
        public event TappedEventHandler postcomment;
        MvxSubscriptionToken token;
        public NewsArticlePostCommentControl()
        {
            try
            {
                this.InitializeComponent();
                IMvxMessenger messenger = Mvx.GetSingleton<IMvxMessenger>();
                //  token = messenger.SubscribeOnThreadPoolThread<NavigationMessage<object>>(MessageSubscribe);
                token = messenger.SubscribeOnMainThread<NavigationMessage<object>>(subscribe);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in NewsArticlePostCommentControl.xaml.cs : NewsArticlePostCommentControl : " + ex.ToString());
            }

        }

        private void subscribe(NavigationMessage<object> m)
        {
            try
            {
                if (m.Message == "PostPopup")
                {
                    CommentTextBox.Text = "";
                    Post.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in NewsArticlePostCommentControl.xaml.cs : subscribe : " + ex.ToString());
            }
        }

       

        private void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
          //  this.postcomment(sender,e);

        //    var Val= this.CommentTextBox.Text;
          
            
           // ((SchoolDetailsViewModel)(this.DataContext)).Comment.Execute(Val);
           //var Val= this.CommentTextBox.Text;
           //var data= this.DataContext as SchoolDetailsViewModel;
           //((SchoolDetailsViewModel)Viewmodel).
        }
    }
}

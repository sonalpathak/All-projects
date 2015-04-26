using Cirrious.CrossCore;
using Neudesic.Schoolistics.Core.Entities;
using Neudesic.Schoolistics.Core.Services;
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
    public sealed partial class NewsArticlesControl : UserControl
    {
        public NewsArticlesControl()
        {
            try
            {
                this.InitializeComponent();
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in NewsArticleControl.xaml.cs : NewsArticleControl : " + ex.ToString());
            }

        }

        private void GridViewItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                var item = (sender as GridViewItem).DataContext as NewsArticle;

                (this.DataContext as HubpageViewModel).NewsItemNavigate.Execute(item);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in NewsArticleControl.xaml.cs : GridViewItem_Tapped : " + ex.ToString());
            }


        }


    }
}

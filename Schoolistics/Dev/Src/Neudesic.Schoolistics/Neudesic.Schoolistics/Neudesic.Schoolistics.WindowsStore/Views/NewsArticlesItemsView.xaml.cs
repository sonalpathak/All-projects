using Cirrious.CrossCore;
// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the NewsArticlesItemsView.xaml type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
using Neudesic.Schoolistics.Core.ViewModels;
using System;
namespace Neudesic.Schoolistics.WindowsStore.Views
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class NewsArticlesItemsView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewsArticlesItemsView"/> class.
        /// </summary>
        public NewsArticlesItemsView()
        {
            try
            {
                this.InitializeComponent();
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in NewsArticlesItemsView.xaml.cs : NewsArticlesItemsView : " + ex.ToString());
            }

        }

        private void itemGridView_ItemClick(object sender, Windows.UI.Xaml.Controls.ItemClickEventArgs e)
        {
            try
            {
                ((NewsArticlesItemsViewModel)ViewModel).ShowDetailCommand.Execute(e.ClickedItem);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in NewsArticlesItemsView.xaml.cs : itemGridView_ItemClick : " + ex.ToString());
            }

        }
    }
}
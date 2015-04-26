using Cirrious.CrossCore;
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
    public sealed partial class SearchResultsGridViewControl : UserControl
    {
        //public SearchresultsViewModel _viewModel;
        public event SelectionChangedEventHandler gridSelectionChanged;
        public event ItemClickEventHandler gridItemClick;

        public SearchResultsGridViewControl()
        {
            try
            {
                this.InitializeComponent();
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchResultsGridViewControl.xaml.cs : SearchResultsGridViewControl : " + ex.ToString());
            }
        }

        private void itemGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                this.gridSelectionChanged(sender, e);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchResultsGridViewControl.xaml.cs : itemGridView_SelectionChanged : " + ex.ToString());
            }
        }

        private void itemGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                this.gridItemClick(sender, e);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchResultsGridViewControl.xaml.cs : itemGridView_ItemClick : " + ex.ToString());
            }
        }
    }
}

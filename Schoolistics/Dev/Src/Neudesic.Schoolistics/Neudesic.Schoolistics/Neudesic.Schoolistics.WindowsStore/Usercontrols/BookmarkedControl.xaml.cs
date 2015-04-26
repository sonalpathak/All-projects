using Cirrious.CrossCore;
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
    public sealed partial class BookmarkedControl : UserControl
    {

        public BookmarkedControl()
        {
            this.InitializeComponent();
        }

        private void itemGridView_ItemClick(object sender, Windows.UI.Xaml.Controls.ItemClickEventArgs e)
        {
            try
            {
                (this.DataContext as HubpageViewModel).BookmarkedToSchoolDetailsPage.Execute(e.ClickedItem);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in BookmarkedControl.xaml.cs : itemGridView_ItemClick : " + ex.ToString());
            }

        }
    }
}

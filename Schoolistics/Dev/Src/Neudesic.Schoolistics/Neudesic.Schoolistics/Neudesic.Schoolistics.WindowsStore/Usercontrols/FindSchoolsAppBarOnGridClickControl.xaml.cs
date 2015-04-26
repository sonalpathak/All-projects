using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
using Neudesic.Schoolistics.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Neudesic.Schoolistics.WindowsStore.Usercontrols
{
    public sealed partial class FindSchoolsAppBarOnGridClickControl : UserControl
    {
        public event RoutedEventHandler shareRoute;
        public event RoutedEventHandler compareRoute;

        public FindSchoolsAppBarOnGridClickControl()
        {
            try
            {
                this.InitializeComponent();
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in FindSchoolsAppBarOnGridClickControl.xaml.cs : FindSchoolsAppBarOnGridClickControl : " + ex.ToString());
            }
        }

        private void shareButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.shareRoute(sender, e);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in FindSchoolsAppBarOnGridClickControl.xaml.cs : shareButton_Click : " + ex.ToString());
            }
        }

        private void compareButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.compareRoute(sender, e);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in FindSchoolsAppBarOnGridClickControl.xaml.cs : compareButton_Click : " + ex.ToString());
            }
        }
    }
}

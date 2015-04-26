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
    public sealed partial class topAppBarControl : UserControl
    {
        public event RoutedEventHandler HomeButtonClick;
        public event RoutedEventHandler CompareButtonClick;
        public event RoutedEventHandler BookmarkbuttonClick;
        public event RoutedEventHandler SurveybuttonClick;
        public event RoutedEventHandler NewsarticlebuttonClick;
        public topAppBarControl()
        {
            this.InitializeComponent();
        }

        private void Homebutton_Click(object sender, RoutedEventArgs e)
        {
            this.HomeButtonClick(sender, e);
        }

        private void Comparebutton_Click(object sender, RoutedEventArgs e)
        {
            this.CompareButtonClick(sender, e);
        }
        private void Bookmarkbutton_Click(object sender, RoutedEventArgs e)
        {
            this.BookmarkbuttonClick(sender, e);
        }

        private void Surveybutton_Click(object sender, RoutedEventArgs e)
        {
            this.SurveybuttonClick(sender, e);
        }

        private void NewsarticleButton_Click(object sender, RoutedEventArgs e)
        {
            this.NewsarticlebuttonClick(sender, e);
        }

    }
}

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
    public sealed partial class FindSchoolsBottomAppBarControl : UserControl
    {   
        public event RoutedEventHandler refineTappedEvent;
        public event RoutedEventHandler mapTappedEvent;
        public event RoutedEventHandler gridTappedEvent;
        public event RoutedEventHandler saveTappedEvent;

        public FindSchoolsBottomAppBarControl()
        {
            try
            {
                this.InitializeComponent();
                gridButtonStackPanel.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in FindSchoolsBottomAppBarControl.xaml.cs : FindSchoolsBottomAppBarControl : " + ex.ToString());
            }
        }

        private void mapButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.mapTappedEvent(sender, e);
                mapButtonStackPanel.Visibility = Visibility.Collapsed;
                gridButtonStackPanel.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in FindSchoolsBottomAppBarControl.xaml.cs : mapButton_Click : " + ex.ToString());
            }
        }

        private void gridButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.gridTappedEvent(sender, e);
                mapButtonStackPanel.Visibility = Visibility.Visible;
                gridButtonStackPanel.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in FindSchoolsBottomAppBarControl.xaml.cs : gridButton_Click : " + ex.ToString());
            }
        }

        private void refineButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.refineTappedEvent(sender, e);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in FindSchoolsBottomAppBarControl.xaml.cs : refineButton_Click : " + ex.ToString());
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.saveTappedEvent(sender, e);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in FindSchoolsBottomAppBarControl.xaml.cs : saveButton_Click : " + ex.ToString());
            }
        }
    }
}

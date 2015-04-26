using Cirrious.CrossCore;
using Neudesic.Schoolistics.Core.Services;
using Neudesic.Schoolistics.Core.Utils;
using Neudesic.Schoolistics.Core.ViewModels;
using Neudesic.Schoolistics.WindowsStore.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Neudesic.Schoolistics.WindowsStore.Usercontrols
{
    public sealed partial class FindSchoolsSearchResultsControl : UserControl
    {
        public event RoutedEventHandler ErrorPopupVisibilityEvent;
        public event RoutedEventHandler ErrorPopupCollapsedEvent;

        public FindSchoolsSearchResultsControl()
        {
            try
            {
                this.InitializeComponent();
                if (Utils.Latitude == 0 && Utils.Longitude == 0)
                    GetLatAndLong();
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in FindSchoolsSearchResultsControl.xaml.cs : FindSchoolsSearchResultsControl : " + ex.ToString());
            }
        }

        public async void GetLatAndLong()
        {
            try
            {
                Geolocator geo = null;
                if (geo == null)
                {
                    geo = new Geolocator();
                }

                Geoposition pos = await geo.GetGeopositionAsync();

                string accuracy = "Accuracy: " + pos.Coordinate.Accuracy.ToString();
                UserPreferences.Latitude = pos.Coordinate.Latitude;
                UserPreferences.Longitude = pos.Coordinate.Longitude;

                Utils.Latitude = pos.Coordinate.Latitude;
                Utils.Longitude = pos.Coordinate.Longitude;
                distanceComboBox.IsEnabled = true;
                this.ErrorPopupCollapsedEvent(null, null);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                distanceComboBox.IsEnabled = false;
                this.ErrorPopupVisibilityEvent(null, null);
                Mvx.Error("Error in FindSchoolsSearchResultsControl.xaml.cs : GetLatAndLong : " + ex.ToString());
            }
        }
    }
}

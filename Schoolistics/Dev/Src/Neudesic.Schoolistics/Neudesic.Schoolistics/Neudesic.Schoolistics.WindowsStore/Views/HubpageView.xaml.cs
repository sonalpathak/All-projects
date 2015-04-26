// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the HubpageView.xaml type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Neudesic.Schoolistics.Core.Services;
using Neudesic.Schoolistics.Core.Utils;
using Neudesic.Schoolistics.Core.ViewModels;
using Windows.Devices.Geolocation;
using System;
using Neudesic.Schoolistics.WindowsStore.Helpers;
using Windows.UI.Xaml;
using Cirrious.CrossCore;
namespace Neudesic.Schoolistics.WindowsStore.Views
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class HubpageView
    {
        public HubpageView()
        {
            try
            {
                this.InitializeComponent();
                if (Utils.Username == null)
                    bookmarkedControl.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                else
                    bookmarkedControl.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in HubpageView.Xaml.cs : Constructor: " + ex.ToString());
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
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in HubpageView.Xaml.cs : GetlatAndLong: " + ex.ToString());
            }
        }

        private void popularComparisonsGridView_ItemClick(object sender, Windows.UI.Xaml.Controls.ItemClickEventArgs e)
        {
            try
            {
                if (e.ClickedItem != null)
                {
                    ((HubpageViewModel)ViewModel).ShowDetailComparisons.Execute(e.ClickedItem);
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in HubpageView.Xaml.cs : popularComparisonsGridView_ItemClick: " + ex.ToString());
            }
        }

        private void schoolComparisonsButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {
                ((HubpageViewModel)ViewModel).NavigateToCompareSchools.Execute(sender);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in HubpageView.Xaml.cs : schoolComparisonsButton_Click: " + ex.ToString());
            }
        }

        private void FindSchoolsControl_ErrorPopupCollapsedEvent(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {
                errorLocationPopUpUserControl.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in HubpageView.Xaml.cs : FindSchoolsControl_ErrorPopupCollapsedEvent: " + ex.ToString());
            }
        }

        private void FindSchoolsControl_ErrorPopupVisibilityEvent(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {
                errorLocationPopUpUserControl.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in HubpageView.Xaml.cs : FindSchoolsControl_ErrorPopupVisibilityEvent: " + ex.ToString());
            }
        }

        private void errorLocationPopUpUserControl_EventForClosePopUp(object sender, RoutedEventArgs e)
        {
            try
            {
                errorLocationPopUpUserControl.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in HubpageView.Xaml.cs : errorLocationPopUpUserControl_EventForClosePopUp: " + ex.ToString());
            }
        }
     
    }
}
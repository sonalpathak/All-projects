using Bing.Maps;
using Cirrious.CrossCore;
using Neudesic.Schoolistics.Core.Entities;
using Neudesic.Schoolistics.Core.Services;
using Neudesic.Schoolistics.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class SearchResultsMapViewControl : UserControl
    {
        public static School selectedSchool;
        Bing.Maps.Location location = new Bing.Maps.Location();
        public event RoutedEventHandler pushpinTappedEvent;

        public SearchResultsMapViewControl()
        {
            try
            {
                this.InitializeComponent();
                location.Latitude = 17.4577289;
                location.Longitude = 78.359116;
                myMap.SetView(location, 5);
                myMap.Children.Clear();
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchResultsMapViewControl.xaml.cs : SearchResultsMapViewControl : " + ex.ToString());
            }
        }

        private async void itemGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                var gridView = sender as GridView;
                var clickedItem = gridView.ItemContainerGenerator.ContainerFromItem(e.ClickedItem);
                Color color = new Color();
                color.A = Convert.ToByte("FF", 16);
                color.R = Convert.ToByte("05", 16);
                color.G = Convert.ToByte("6E", 16);
                color.B = Convert.ToByte("88", 16);
                Color sampleColor = Color.FromArgb(color.A, color.R, color.G, color.B);
                foreach (var item in gridView.Items)
                {
                    var gridViewItem = (FrameworkElement)gridView.ItemContainerGenerator.ContainerFromItem(item);
                    var background = FindChild<StackPanel>(gridViewItem, "SearchMapGridView");

                    background.Background = new SolidColorBrush(sampleColor);
                    var schoolNameTextBlock = FindChild<TextBlock>(background, "schoolName");
                    var cityTextBlock = FindChild<TextBlock>(background, "city");
                    schoolNameTextBlock.Foreground = new SolidColorBrush(Colors.White);
                    cityTextBlock.Foreground = new SolidColorBrush(Colors.White);
                    if (gridViewItem == clickedItem)
                    {
                        background.Background = new SolidColorBrush(Colors.White);
                        schoolNameTextBlock.Foreground = new SolidColorBrush(Colors.Black);
                        cityTextBlock.Foreground = new SolidColorBrush(Colors.Black);
                    }
                }
                myMap.Children.Clear();
                Pushpin pushPin = new Pushpin();
                string schoolId = ((School)e.ClickedItem).SchoolId;
                //  _viewModel.GetLatitudeAndLongitude(id);
                SearchresultsViewModel _viewModel = this.DataContext as SearchresultsViewModel;
                selectedSchool = (School)e.ClickedItem;
                await _viewModel.GetLatitudeAndLongitude(schoolId);
                location.Latitude = _viewModel.Latitude;
                location.Longitude = _viewModel.Longitude;
                myMap.SetView(location, 12);
                //pushPin.Name = "NewPin";
                pushPin.Style = this.Resources["PushPinStyle"] as Style;
                pushPin.Tapped += Pushpin_Tapped;
                Bing.Maps.Location locationOfPin = new Bing.Maps.Location(location.Latitude, location.Longitude);
                ToolTipService.SetToolTip(pushPin, selectedSchool.SchoolName + "\r\n" + selectedSchool.Address);
                MapLayer.SetPositionAnchor(pushPin, new Point(25 / 2, 39));
                MapLayer.SetPosition(pushPin, locationOfPin);
                myMap.Children.Add(pushPin);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchResultsMapViewControl.xaml.cs : itemGridView_ItemClick : " + ex.ToString());
            }
        }

        public static T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            try
            {
                if (parent == null) return null;
                T foundChild = null;
                int childrenCount = VisualTreeHelper.GetChildrenCount(parent);

                for (int i = 0; i < childrenCount; i++)
                {
                    var child = VisualTreeHelper.GetChild(parent, i);
                    T childType = child as T;

                    if (childType == null)
                    {
                        foundChild = FindChild<T>(child, childName);
                        if (foundChild != null) break;
                    }
                    else if (!string.IsNullOrEmpty(childName))
                    {
                        var frameworkElement = child as FrameworkElement;
                        if (frameworkElement != null && frameworkElement.Name == childName)
                        {
                            foundChild = (T)child;
                            break;
                        }

                        foundChild = FindChild<T>(child, childName);
                        if (foundChild != null) break;
                    }
                    else
                    {
                        foundChild = (T)child;
                        break;
                    }
                }

                return foundChild;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchResultsMapViewControl.xaml.cs : FindChild : " + ex.ToString());
                return null;
            }
        }

        private void Pushpin_Tapped(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                pushpinTappedEvent(sender, e);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchResultsMapViewControl.xaml.cs : Pushpin_Tapped : " + ex.ToString());
            }
        }
    }
}

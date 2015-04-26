using Bing.Maps;
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
using Bing.Maps;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.CrossCore;
using Neudesic.Schoolistics.Core.Entities;
using Neudesic.Schoolistics.Core.Services;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Neudesic.Schoolistics.WindowsStore.Usercontrols
{
    public sealed partial class LocationUserControl : UserControl
    {
        public event RoutedEventHandler loadedEvent;
        static Pushpin p = new Pushpin();
        Pushpin pushPin = new Pushpin();
        SchoolDetailsViewModel _viewModel;

        Bing.Maps.Location location = new Bing.Maps.Location();
        MvxSubscriptionToken token;

        public LocationUserControl()
        {
            try
            {
                this.InitializeComponent();
                IMvxMessenger messenger = Mvx.GetSingleton<IMvxMessenger>();
                //  token = messenger.SubscribeOnThreadPoolThread<NavigationMessage<object>>(MessageSubscribe);
                token = messenger.SubscribeOnMainThread<NavigationMessage<object>>(MessageSubscribe);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in LocationUserControl.cs :LocationUserControl: " + ex.ToString());

            }


        }
        

        public void MessageSubscribe(NavigationMessage<object> message)
        {
            try
            {
                myMap.Children.Clear();
                if (message.Message == "LatLon")
                {

                    string latitudeLongitude = message.Data.ToString();
                    char[] separetor = new char[] { '$' };
                    string[] s = latitudeLongitude.Split(separetor);
                    double latitude1 = Convert.ToDouble(s[0]);
                    double longitude1 = Convert.ToDouble(s[1]);
                    string address = s[2];
                    string logo = s[3];
                    Bing.Maps.Location location = new Bing.Maps.Location();

                    pushPin.Style = this.Resources["PushPinStyle"] as Style;
                    ToolTipService.SetToolTip(pushPin, logo + "\r\n" + address);


                    //ToolTipService.SetToolTip(p, new ToolTip()
                    //{
                    //    //DataContext=logo+"\n"+address,
                    //    Style = Application.Current.Resources["CustomInfoboxStyle"] as Style
                    //});
                    location.Latitude = latitude1;
                    location.Longitude = longitude1;
                    pushPin.Style = this.Resources["PushPinStyle"] as Style;
                    myMap.SetView(location, 12);
                    MapLayer.SetPosition(pushPin, location);
                    myMap.Children.Add(pushPin);
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in LocationUserControl.cs :MessageSubscribe: " + ex.ToString());

            }
        }
    }
}

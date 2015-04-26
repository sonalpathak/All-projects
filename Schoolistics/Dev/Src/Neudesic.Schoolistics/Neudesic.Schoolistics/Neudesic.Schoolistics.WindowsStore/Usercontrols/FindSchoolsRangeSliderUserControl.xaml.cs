using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
using Neudesic.Schoolistics.Core.Entities;
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
    public sealed partial class FindSchoolsRangeSliderUserControl : UserControl
    {
        MvxSubscriptionToken token;
        private readonly IMvxMessenger messenger;

        public FindSchoolsRangeSliderUserControl()
        {
            try
            {
                InitializeComponent();

                this.Loaded += new RoutedEventHandler(RangeSlider_Loaded);

                LayoutRoot.DataContext = this;
                UpperSlider.Value = 500000;
                messenger = Mvx.GetSingleton<IMvxMessenger>();
                messenger.Publish(new NavigationMessage<object>(this) { Message = "GetMaximumValue", Data = UpperSlider.Value });
                token = messenger.SubscribeOnMainThread<NavigationMessage<object>>(MessageSubscribe);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in FindSchoolsRangeSliderUserControl.xaml.cs : FindSchoolsRangeSliderUserControl : " + ex.ToString());
            }
        }

        public void MessageSubscribe(NavigationMessage<object> message)
        {
            try
            {
                if (message.Message == "GetMaximumAndMinimumValues")
                {
                    int index = message.Data.ToString().IndexOf(' ');
                    LowerSlider.Value = Convert.ToInt32(message.Data.ToString().Substring(0, index));
                    UpperSlider.Value = Convert.ToInt32(message.Data.ToString().Substring(index + 1));
                    UpperSlider.Value = UpperSlider.Value == 0 ? 500000 : UpperSlider.Value;
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in FindSchoolsRangeSliderUserControl.xaml.cs : FindSchoolsRangeSliderUserControl : " + ex.ToString());
            }
        }

        void RangeSlider_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LowerSlider.ValueChanged += LowerSlider_ValueChanged;
                UpperSlider.ValueChanged += UpperSlider_ValueChanged;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in FindSchoolsRangeSliderUserControl.xaml.cs : FindSchoolsRangeSliderUserControl : " + ex.ToString());
            }
        }

        void UpperSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            try
            {
                LowerSlider.Value = Math.Min(UpperSlider.Value, LowerSlider.Value);
                messenger.Publish(new NavigationMessage<object>(this) { Message = "GetMaximumValue", Data = UpperSlider.Value });
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in FindSchoolsRangeSliderUserControl.xaml.cs : FindSchoolsRangeSliderUserControl : " + ex.ToString());
            }
        }

        void LowerSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            try
            {
                UpperSlider.Value = Math.Max(UpperSlider.Value, LowerSlider.Value);
                messenger.Publish(new NavigationMessage<object>(this) { Message = "GetMinimumValue", Data = LowerSlider.Value });
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in FindSchoolsRangeSliderUserControl.xaml.cs : FindSchoolsRangeSliderUserControl : " + ex.ToString());
            }
        }
    }
}

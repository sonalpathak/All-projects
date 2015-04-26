using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
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
    public sealed partial class BottonAppBar : UserControl
    {
        //public event RoutedEventHandler Rating_Button;
        public event TappedEventHandler RateTappedEventHandler;
        public event TappedEventHandler SaveToCompare;
        public event TappedEventHandler WriteAReview;
        public event TappedEventHandler Share;
       // public 
        //Popup _popup;
        SchoolDetailsViewModel _viewModel;
        public BottonAppBar()
        {
            try
            {
                this.InitializeComponent();
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in BottonAppBar.cs :ButtomAppBar: " + ex.ToString());

            }
            //_viewModel = new SchoolDetailsViewModel(new SchoolDetailsService(), var IMvxMessenger);
            //  this.DataContext = _viewModel;
            
        }


        private void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                this.RateTappedEventHandler(sender, e);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in BottonAppBar.cs :Button_Tapped: " + ex.ToString());

            }
           // StarRatingButton.Visibility = Visibility.Collapsed;
        }

        private void Button_Tapped_1(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                this.SaveToCompare(sender, e);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in BottonAppBar.cs :Button_Tapped_1: " + ex.ToString());

            }
        }

        private void Button_Tapped_2(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                this.WriteAReview(sender, e);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in BottonAppBar.cs :Button_Tapped_2: " + ex.ToString());

            }
           
        }

        private void Button_Tapped_3(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                this.Share(sender, e);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in BottonAppBar.cs :Button_Tapped_3: " + ex.ToString());

            }
        }

    }
}

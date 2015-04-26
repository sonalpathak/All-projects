using Neudesic.Schoolistics.Core.Services;
using Neudesic.Schoolistics.Core.ViewModels;
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
//using System.Windows.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Neudesic.Schoolistics.WindowsStore.Usercontrols
{
    
    public sealed partial class rating_usercontrol : UserControl
    {
        
       // public event TappedEventHandler buttons;
        public event RoutedEventHandler star5_clicked_event;
        public event RoutedEventHandler star3_clicked_event;
        public event RoutedEventHandler star1_clicked_event;
        public event RoutedEventHandler star2_clicked_event;
        public event RoutedEventHandler star4_clicked_event;
        int count;
        public rating_usercontrol()
        {
            this.InitializeComponent();           
        }

        private Cirrious.MvvmCross.Plugins.Messenger.IMvxMessenger IMvxMMessenger()
        {
            throw new NotImplementedException();
        }
        //private void stars1_clicked(object sender, RoutedEventArgs e)
        //{
        //    BitmapImage bmp = new BitmapImage();
        //    Uri u = new Uri("ms-appx:Assets/Home.png", UriKind.RelativeOrAbsolute);

        //    bmp.UriSource = u;


        //    ImageBrush i = new ImageBrush();
        //    i.ImageSource = bmp;
        //    // i.Stretch = Stretch.None;
        //    stars1.Background = i;
        //}

        private void star1_clicked(object sender, PointerRoutedEventArgs e)
        {
            var rating = sender as string;
            rating.Substring(15, 1);
            BitmapImage bmp = new BitmapImage();
            Uri u = new Uri("ms-appx:Assets/Home.png", UriKind.RelativeOrAbsolute);

            bmp.UriSource = u;


            ImageBrush i = new ImageBrush();
            i.ImageSource = bmp;
            // i.Stretch = Stretch.None;
           // stars1.Background = i;
        }

        private void stars1_Click(object sender, RoutedEventArgs e)
        {
            count++;
            if (count % 2 == 0)
            {
                //stars1.Visibility = Visibility.Collapsed;
                //stars1.Background = white;

                BitmapImage bmp = new BitmapImage();
                Uri u = new Uri("ms-appx:Assets/rate-filled.png", UriKind.RelativeOrAbsolute);
                

                bmp.UriSource = u;


                ImageBrush i = new ImageBrush();
                i.ImageSource = bmp;
                

                
                i.Stretch = Stretch.None;

                //stars1.Background = i;
                //stars1.HorizontalAlignment = HorizontalAlignment.Center;
                //stars1.VerticalAlignment = VerticalAlignment.Center;
            }
        
        }

     

        private  void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {

            BitmapImage bmp = new BitmapImage();
            Uri u = new Uri("ms-appx:Assets/rate-filled.png", UriKind.RelativeOrAbsolute);


            bmp.UriSource = u;


            ImageBrush i = new ImageBrush();
            i.ImageSource = bmp;



            i.Stretch = Stretch.None;

        }

        private void stars5_Click(object sender, RoutedEventArgs e)
        {
            this.star5_clicked_event(sender, e);
            ((SchoolDetailsViewModel)(this.DataContext)).UserRating.Execute(5);
           // BottonAppBar b=new BottonAppBar;
            
           // b.StarRatingButton.Visibility = Visibility.Collapsed;
            
        }

        private void stars3_Click(object sender, RoutedEventArgs e)
        {
            this.star3_clicked_event(sender, e);
            ((SchoolDetailsViewModel)(this.DataContext)).UserRating.Execute(3);
        }

        private void stars1_Click_1(object sender, RoutedEventArgs e)
        {
            this.star1_clicked_event(sender, e);
            ((SchoolDetailsViewModel)(this.DataContext)).UserRating.Execute(1);

        }

        private void stars2_Click(object sender, RoutedEventArgs e)
        {
            this.star2_clicked_event(sender, e);
            ((SchoolDetailsViewModel)(this.DataContext)).UserRating.Execute(2);
        }

        private void stars4_Click(object sender, RoutedEventArgs e)
        {
           // this.star4_clicked_event(sender, e);
            ((SchoolDetailsViewModel)(this.DataContext)).UserRating.Execute(4);
            
        }

    }
}

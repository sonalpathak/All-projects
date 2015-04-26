// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MyprofileView.xaml type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using Neudesic.Schoolistics.Core.Services;
using Neudesic.Schoolistics.Core.ViewModels;
using Windows.Storage;
using Windows.Storage.Pickers;
using System.IO;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Neudesic.Schoolistics.Core.Entities;
using Cirrious.CrossCore;


namespace Neudesic.Schoolistics.WindowsStore.Views
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class MyprofileView
    {

        public MyprofileView()
        {
            try
            {
                this.InitializeComponent();
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in MyprofileView.xaml.cs : MyprofileView : " + ex.ToString());
            }
        }

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {
                var profileDetails = ((MyprofileViewModel)ViewModel).User;
                ((MyprofileViewModel)ViewModel).EditProfile.Execute(profileDetails);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in MyprofileView.xaml.cs : Button_Click : " + ex.ToString());
            }
        }
    }
}
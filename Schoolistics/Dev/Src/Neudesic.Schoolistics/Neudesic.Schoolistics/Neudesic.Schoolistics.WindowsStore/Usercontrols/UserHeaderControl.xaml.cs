using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
using Neudesic.Schoolistics.Core.Entities;
using Neudesic.Schoolistics.Core.Utils;
using Neudesic.Schoolistics.Core.ViewModels;
using Neudesic.Schoolistics.WindowsStore.Helpers;
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
    public sealed partial class UserHeaderControl : UserControl
    {
        private Popup _loginPopUp;
        private LoginControl loginControl;

        MvxSubscriptionToken token;
        UserHeaderViewModel viewModel;
        public UserHeaderControl()
        {
            
            viewModel = new UserHeaderViewModel();
            this.InitializeComponent();  
          
            this.DataContext = viewModel;

            if (Helpers.UserPreferences.AuthToken == null)
            {
                loginButton.Visibility = Windows.UI.Xaml.Visibility.Visible;
                profileButton.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            else
            {
                loginButton.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                profileButton.Visibility = Windows.UI.Xaml.Visibility.Visible;

                Utils.Username = UserPreferences.Username;
                Utils.AuthToken = UserPreferences.AuthToken;

                usernameTextBlock.Text = UserPreferences.Username;
            }

            IMvxMessenger messenger = Mvx.GetSingleton<IMvxMessenger>();
            token = messenger.SubscribeOnMainThread<NavigationMessage<User>>(MessageSubscribe);
        }

        public void MessageSubscribe(NavigationMessage<User> message)
        {
            if (message.Message == "LoginSuccess" || message.Message == "RegistrationSuccess")
            {
                if (message.Data != null)
                {
                    loginButton.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    profileButton.Visibility = Windows.UI.Xaml.Visibility.Visible;

                    UserPreferences.Username = message.Data.Username;
                    UserPreferences.AuthToken = message.Data.AuthToken;
                    Utils.Username = message.Data.Username;
                    Utils.AuthToken = message.Data.AuthToken;

                    usernameTextBlock.Text = UserPreferences.Username;
                }
            }
        }

        private void btnLogin_Tapped_1(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                if (_loginPopUp == null)
                {

                    loginControl = new LoginControl();
                    _loginPopUp = loginControl.GetLoginPopUp();
                    _loginPopUp.IsOpen = true;

                    return;
                }
                _loginPopUp.IsOpen = !_loginPopUp.IsOpen;
            }
            catch (Exception ex)
            {
            }

        }


        private void profileButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
           

            viewModel.ProfileNavigate.Execute(null);
        }

        private void logoutButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            loginButton.Visibility = Windows.UI.Xaml.Visibility.Visible;
            profileButton.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            UserPreferences.Username = null;
            UserPreferences.AuthToken = null;

            Utils.Username = UserPreferences.Username;
            Utils.AuthToken = UserPreferences.AuthToken;
        }

    }
}

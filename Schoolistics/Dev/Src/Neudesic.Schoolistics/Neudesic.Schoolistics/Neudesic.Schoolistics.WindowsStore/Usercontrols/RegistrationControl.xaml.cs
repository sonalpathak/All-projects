using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
using Neudesic.Schoolistics.Core.Entities;
using Neudesic.Schoolistics.Core.Services;
using Neudesic.Schoolistics.Core.Utils;
using Neudesic.Schoolistics.Core.ViewModels;
using Neudesic.Schoolistics.WindowsStore.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Neudesic.Schoolistics.WindowsStore.Usercontrols
{
    public sealed partial class RegistrationControl : UserControl
    {
        MvxSubscriptionToken token;
         private static Popup popUp;
         private RegistrationViewModel _viewModel; 
         public RegistrationControl()
         {
             this.InitializeComponent();

             IMvxMessenger messenger = Mvx.GetSingleton<IMvxMessenger>();
             token = messenger.SubscribeOnMainThread<NavigationMessage<User>>(MessageSubscribe);

             _viewModel = new RegistrationViewModel(new RegistrationService(new RestService()), messenger);
             this.DataContext = _viewModel;
         }

        public Popup GetRegistrationPopUp()
        {            
            this.Width = 400;
            this.Height = Window.Current.Bounds.Height;

            popUp = new Popup();
            popUp.Width = 400;
            popUp.Height = Window.Current.Bounds.Height;
            popUp.ChildTransitions = new TransitionCollection();
            popUp.ChildTransitions.Add(new PaneThemeTransition());
            popUp.IsLightDismissEnabled = true;
            popUp.Child = this;

            popUp.HorizontalOffset = Window.Current.Bounds.Width - popUp.Width;
            popUp.VerticalOffset = 0;

            return popUp;
        }
        public void MessageSubscribe(NavigationMessage<User> messageData)
        {
            if (messageData.Message == "RegistrationSuccess")
            {
                UserPreferences.Username = messageData.Data.Username;
                UserPreferences.AuthToken = messageData.Data.AuthToken;
                Utils.Username = messageData.Data.Username;
                Utils.AuthToken = messageData.Data.AuthToken;

                ShowMessage("Registration Successfull");
            }
            else if (messageData.Message == "RegistrationFailure")
            {
                ShowMessage("Failed in registration, please try after some time");
            }
        }


        Task ShowMessage(String errorMessage)
        {
            CoreDispatcher dispatcher = Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher;
            Func<object, Task<bool>> action = null;
            action = async (o) =>
            {
                try
                {
                    if (dispatcher.HasThreadAccess)
                    {
                        var asyncCommand = new MessageDialog(errorMessage).ShowAsync();
                        await Task.Delay(3000);
                        asyncCommand.Cancel();
                    }
                    else
                    {
                        await dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                           () => action(o));
                    }
                    return true;
                }
                catch (UnauthorizedAccessException)
                {
                    if (action != null)
                    {
                        Task.Delay(500).ContinueWith(async t => await action(o));
                    }
                }
                return false;

            };
            return action(null);
        }
        
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            popUp.IsOpen = false;
        }

        private void Register_Tapped(object sender, RoutedEventArgs e)
        {
            var password = passwordBox.Password;
            var reEnterPassword = ReEnterPasswordBox.Password;
            if (password == reEnterPassword)
            {
                _viewModel.RegisterCommand.Execute(password);
            }
            else
            {
                ErrorBox.Text = "Passwords do not match";
                ErrorBox.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            
        }   
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the RegistrationView.xaml type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
using Neudesic.Schoolistics.Core.Entities;
using Neudesic.Schoolistics.Core.Utils;
using Neudesic.Schoolistics.WindowsStore.Helpers;
using Neudesic.Schoolistics.WindowsStore.Usercontrols;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media.Animation;
namespace Neudesic.Schoolistics.WindowsStore.Views
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class RegistrationView
    {
        MvxSubscriptionToken token;

        public RegistrationView()
        {
            this.InitializeComponent();
             OpenRegistrationPopUp();
            IMvxMessenger messenger = Mvx.GetSingleton<IMvxMessenger>();
            token = messenger.Subscribe<NavigationMessage<User>>(MessageSubscribe);
        }
        private Popup _RegistrationPopUp;        
        private void OpenRegistrationPopUp()
        {
            if (_RegistrationPopUp == null)
            {
                _RegistrationPopUp = this.GetLoginPopUp();
                _RegistrationPopUp.IsOpen = true;

                return;
            }
            _RegistrationPopUp.IsOpen = !_RegistrationPopUp.IsOpen;

        }
        public Popup GetLoginPopUp()
        {
            var registrationControl = new RegistrationControl();
            registrationControl.Width = 380;
            registrationControl.Height = Window.Current.Bounds.Height;
            //loginControl.loginClickEvent += loginControl_loginClickEvent;

            Popup popUp = new Popup();
            popUp.Width = 380;
            popUp.Height = Window.Current.Bounds.Height;
            popUp.ChildTransitions = new TransitionCollection();
            popUp.ChildTransitions.Add(new PaneThemeTransition());
            popUp.IsLightDismissEnabled = true;
            popUp.Child =registrationControl;

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
            }
        }

        private void TextBox_TextChanged(object sender, Windows.UI.Xaml.Controls.TextChangedEventArgs e)
        {

        }
    }
}
// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the LoginView.xaml type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Cirrious.CrossCore;
using Neudesic.Schoolistics.WindowsStore.Usercontrols;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media.Animation;
namespace Neudesic.Schoolistics.WindowsStore.Views
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class LoginView
    {
        private Popup _LoginPopUp;
        public LoginView()
        {
            this.InitializeComponent();
            OpenLoginPopUp();
        }
        private void OpenLoginPopUp()
        {
            try
            {
                if (_LoginPopUp == null)
                {
                    _LoginPopUp = this.GetLoginPopUp();
                    _LoginPopUp.IsOpen = true;

                    return;
                }
                _LoginPopUp.IsOpen = !_LoginPopUp.IsOpen;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in LoginView.xaml.cs : OpenLoginPopUp : " + ex.ToString());
            }

        }
        public Popup GetLoginPopUp()
        {
            try
            {
                var loginControl = new LoginControl();
                loginControl.Width = 380;
                loginControl.Height = Window.Current.Bounds.Height;
                //loginControl.loginClickEvent += loginControl_loginClickEvent;

                Popup popUp = new Popup();
                popUp.Width = 380;
                popUp.Height = Window.Current.Bounds.Height;
                popUp.ChildTransitions = new TransitionCollection();
                popUp.ChildTransitions.Add(new PaneThemeTransition());
                popUp.IsLightDismissEnabled = true;
                popUp.Child = loginControl;

                popUp.HorizontalOffset = Window.Current.Bounds.Width - popUp.Width;
                popUp.VerticalOffset = 0;

                return popUp;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in LoginView.xaml.cs : GetLoginPopUp : " + ex.ToString());
                return null;
            }

        }

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }

        private void Image_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {

        }
    }
}
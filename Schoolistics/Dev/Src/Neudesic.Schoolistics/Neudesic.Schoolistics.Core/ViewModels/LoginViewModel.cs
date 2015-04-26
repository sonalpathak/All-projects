// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the LoginViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Neudesic.Schoolistics.Core.ViewModels
{
    using System.Windows.Input;

    using Cirrious.MvvmCross.ViewModels;
using Neudesic.Schoolistics.Core.Services;
using Cirrious.MvvmCross.Plugins.Messenger;
    using Neudesic.Schoolistics.Core.Entities;
    using System;
    using Newtonsoft.Json;
    using Cirrious.CrossCore;

    /// <summary>
    /// Define the LoginViewModel type.
    /// </summary>
    public class LoginViewModel : BaseViewModel
    {
        IRegistrationService loginService;
        IMvxMessenger messenger;

        public LoginViewModel(IRegistrationService _loginService, IMvxMessenger _messenger)
        {
            try
            {
                loginService = _loginService;
                messenger = _messenger;
                LoginButtonEnabled = true;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in LoginViewModel.cs : LoginViewModel : " + ex.ToString());
            }

        }

        private string _username;
        public string Username
        {
            get
            {
                return this._username;
            }

            set
            {
                this._username = value;
                this.RaisePropertyChanged(() => Username);
            }
        }
                
        private IMvxCommand loginCommand;
        public ICommand LoginCommand
        {
            get
            {
               loginCommand = loginCommand ?? new MvxCommand<string>(password =>LoginUser(password));
                return loginCommand;
            }
        }

        private bool _LoginButtonEnabled;
        public bool LoginButtonEnabled
        {
            get
            {
                return this._LoginButtonEnabled;
            }

            set
            {
                this._LoginButtonEnabled = value;
                this.RaisePropertyChanged(() => LoginButtonEnabled);
            }
        }

        private bool _LoginProgressRingVisibility;
        public bool LoginProgressRingVisibility
        {
            get
            {
                return this._LoginProgressRingVisibility;
            }

            set
            {
                this._LoginProgressRingVisibility = value;
                this.RaisePropertyChanged(() => LoginProgressRingVisibility);
            }
        }

        public void LoginUserFromGoogle(string userName, string emailAddress)
        {
            try
            {
                User userDetails = new User();
                userDetails.Username = userName;
                userDetails.Email = emailAddress;
                userDetails.IsSocialUser = true;

                loginService.PostRegistrationDetails(userDetails, LoginSuccessFromGoogle, LoginFailure);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in LoginViewModel.cs : LoginUserFromGoogle : " + ex.ToString());
            }

        }

        public void LoginSuccessFromGoogle(ResponseMessage<Object> response)
        {
            try
            {
                if (response.SuccessCode == 1)
                {
                    var userDetails = JsonConvert.DeserializeObject<User>(response.Data.ToString());

                    messenger.Publish(new NavigationMessage<User>(this) { Message = "RegistrationSuccess", Data = userDetails });
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in LoginViewModel.cs : LoginSuccessFromGoogle : " + ex.ToString());
            }

        }

        public void LoginUser(string password)
        {
            try
            {                
                LoginButtonEnabled = false;
                LoginProgressRingVisibility = true;
                User userDetails = new User();
                userDetails.Username = Username;
                userDetails.Password = password;

                loginService.CheckLoginDetails(userDetails, LoginSuccess, LoginFailure);
            }
            catch (Exception ex)
            {
                string a = ex.Message;
            }
        }

        public void LoginSuccess(ResponseMessage<Object> response)
        {
            try
            {
                if (response.SuccessCode == 1)
                {
                    LoginButtonEnabled = true;
                    LoginProgressRingVisibility = false;

                    var userDetails = JsonConvert.DeserializeObject<User>(response.Data.ToString());

                    messenger.Publish(new NavigationMessage<User>(this) { Message = "LoginSuccess", Data = userDetails });
                    //messenger.Publish(new NavigationMessage<User>(this) { Message = "LoginSuccessToShowMessage", Data = userDetails });
                }
                else
                {
                    LoginButtonEnabled = true;
                    LoginProgressRingVisibility = false;

                    messenger.Publish(new NavigationMessage<User>(this) { Message = "LoginSuccess", Data = null });
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in LoginViewModel.cs : LoginSuccess : " + ex.ToString());
            }
        }

        public void LoginFailure(Exception e)
        {
            try
            {
                LoginButtonEnabled = true;
                LoginProgressRingVisibility = false;

                var message = e.Message;

                messenger.Publish(new NavigationMessage<User>(this) { Message = "LoginSuccess", Data = null });
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in LoginViewModel.cs : LoginFailure : " + ex.ToString());
            }
        }
    }
}

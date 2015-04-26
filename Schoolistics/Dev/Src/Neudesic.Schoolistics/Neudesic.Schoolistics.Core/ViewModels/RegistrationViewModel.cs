// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the RegistrationViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Neudesic.Schoolistics.Core.ViewModels
{
    using System.Windows.Input;

    using Cirrious.MvvmCross.ViewModels;
    using Neudesic.Schoolistics.Core.Services;
    using Neudesic.Schoolistics.Core.Entities;
    using System;
    using Cirrious.MvvmCross.Plugins.Messenger;
    using Newtonsoft.Json;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Define the RegistrationViewModel type.
    /// </summary>
    public class RegistrationViewModel : BaseViewModel
    {
        IRegistrationService registrationService;
        private ICommand registerCommand;
        IMvxMessenger messenger;

        public RegistrationViewModel(IRegistrationService _registrationService, IMvxMessenger _messenger)
        {
            registrationService = _registrationService;
            messenger = _messenger;

            RegisterButtonEnabled = true;
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

        private string _Email;
        public string Email
        {
            get
            {
                return this._Email;
            }

            set
            {
                this._Email = value;
                this.RaisePropertyChanged(() => Email);
            }
        }

        private bool _RegisterButtonEnabled;
        public bool RegisterButtonEnabled
        {
            get
            {
                return this._RegisterButtonEnabled;
            }

            set
            {
                this._RegisterButtonEnabled = value;
                this.RaisePropertyChanged(() => RegisterButtonEnabled);
            }
        }

        private bool _RegistrationProgressRingVisibility;
        public bool RegistrationProgressRingVisibility
        {
            get
            {
                return this._RegistrationProgressRingVisibility;
            }

            set
            {
                this._RegistrationProgressRingVisibility = value;
                this.RaisePropertyChanged(() => RegistrationProgressRingVisibility);
            }
        }

        private MvxCommand<string> _Register;

        public ICommand RegisterCommand
        {
            get
            {
                registerCommand = registerCommand ?? new MvxCommand<string>(password=>RegisterUser(password));
                return registerCommand;
            }
        }

        public void RegisterUser(string password)
        {
            try
            {
                RegisterButtonEnabled = false;
                RegistrationProgressRingVisibility = true;
                User userDetails = new User();
                userDetails.Username = Username;
                userDetails.Email = Email;
                userDetails.Password = password;

                registrationService.PostRegistrationDetails(userDetails, RegistrationSuccess, RegistrationFailure);
            }
            catch (Exception ex)
            {
                string a = ex.Message;
            }
        }

        public void RegistrationSuccess(ResponseMessage<Object> response)
        {
            RegisterButtonEnabled = true;
            RegistrationProgressRingVisibility = false;

            if (response.SuccessCode == 1)
            {
                var userDetails = JsonConvert.DeserializeObject<User>(response.Data.ToString());

                messenger.Publish(new NavigationMessage<User>(this) { Message = "RegistrationSuccess", Data = userDetails });
            }
        }

        public void RegistrationFailure(Exception e)
        {
            RegisterButtonEnabled = true;
            RegistrationProgressRingVisibility = false;

            var message = e.Message;
            messenger.Publish(new NavigationMessage<User>(this) { Message = "RegistrationFailure" });
        }
        const string RegExForEmailValidation = @"^([0-9a-zA-Z]+[-._+&amp;])*[0-9a-zA-Z]+@([-0-9a-zA-Z]+[.])+[a-zA-Z]{2,6}$ <mailto:+@([-0-9a-zA-Z]+[.])+[a-zA-Z]%7b2,6%7d$> ";
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return false;

            Regex nameRegEx = new Regex(RegExForEmailValidation);
            return nameRegEx.IsMatch(email);
        }




        public void SaveDetails(System.Collections.Generic.IDictionary<string, object> result)
        {
            User userDetails = new User();

            userDetails.Gender = result["gender"].ToString();
            userDetails.IsSocialUser = true;

            userDetails.Username = result["username"].ToString();
            userDetails.Email = "sridevi@gmail.com";
            userDetails.Password = "sridevimandapaka";
            registrationService.PostRegistrationDetails(userDetails, RegistrationSuccess, RegistrationFailure);

        }


        public void SaveDetailsFromTwitter(string userName)
        {
            User userDetails = new User();
            userDetails.Gender = "Female";
            userDetails.IsSocialUser = true;
            userDetails.Username = userName;
            userDetails.Email = "sridevi@gmail.com";
            userDetails.Password = "sridevimandapaka";
            registrationService.PostRegistrationDetails(userDetails, RegistrationSuccess, RegistrationFailure);

        }
    }

}

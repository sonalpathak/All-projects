// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MyprofileViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Neudesic.Schoolistics.Core.ViewModels
{
    using System.Windows.Input;

    using Cirrious.MvvmCross.ViewModels;
    using Neudesic.Schoolistics.Core.Services;
    using Neudesic.Schoolistics.Core.Entities;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using System;
    using Cirrious.MvvmCross.Plugins.Messenger;
    using Cirrious.CrossCore;

    /// <summary>
    /// Define the MyprofileViewModel type.
    /// </summary>
    public class MyprofileViewModel : BaseViewModel
    {


        IProfileDetailsService profileDetailsService;

        IMvxMessenger messenger;

        public MyprofileViewModel(IProfileDetailsService _profileDetailsService, IMvxMessenger _messenger)
        {
            try
            {
                profileDetailsService = _profileDetailsService;
                messenger = _messenger;
                profileDetailsService.GetProfileInfo(ProfileDetailsSuccess, ProfileDetailsFailure);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in MyprofileViewModel.cs : MyprofileViewModel : " + ex.ToString());
            }
        }


        public void ProfileDetailsSuccess(ResponseMessage<Object> response)
        {
            try
            {
                if (response.SuccessCode == 1)
                {
                    var userDetails = JsonConvert.DeserializeObject<User>(response.Data.ToString());
                    User = userDetails;
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in MyprofileViewModel.cs : ProfileDetailsSuccess : " + ex.ToString());
            }
        }


        public void ProfileDetailsFailure(Exception e)
        {
            try
            {
                var message = e.Message;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in MyprofileViewModel.cs : ProfileDetailsFailure : " + ex.ToString());
            }
        }


        /// <summary>
        /// Backing field for my property.
        /// </summary>
        private string myProperty = "Mvx Ninja Coder!";

        /// <summary>
        ///  Backing field for my command.
        /// </summary>
        ///

        public ICommand EditProfileDetails
        {
            get
            {
                return new MvxCommand<User>(item => ShowViewModel<EditprofileViewModel>(item));
            }
        }

        public ICommand EditProfile
        {
            get
            {
                return new MvxCommand<User>(item => ShowViewModel<EditprofileViewModel>(item));
            }
        }

        private MvxCommand myCommand;

        /// <summary>
        /// Gets or sets my property.
        /// </summary>
        public string MyProperty
        {
            get
            {
                return this.myProperty;
            }

            set
            {
                this.myProperty = value;
                this.RaisePropertyChanged(() => this.MyProperty);
            }
        }



        private User _user;
        public User User
        {
            get { return this._user; }
            set { this._user = value; this.RaisePropertyChanged(() => User); }
        }

        /// <summary>
        /// Gets My Command.
        /// <para>
        /// An example of a command and how to navigate to another view model
        /// Note the ViewModel inside of ShowViewModel needs to change!
        /// </para>
        /// </summary>
        public ICommand MyCommand
        {
            get
            {
                return this.myCommand ?? (this.myCommand = new MvxCommand(() => this.ShowViewModel<MyprofileViewModel>()));
            }
        }
    }
}
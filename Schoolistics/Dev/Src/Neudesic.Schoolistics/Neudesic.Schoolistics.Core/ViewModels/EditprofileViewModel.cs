// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the EditprofileViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Neudesic.Schoolistics.Core.ViewModels
{
    using System.Windows.Input;

    using Cirrious.MvvmCross.ViewModels;
    using Neudesic.Schoolistics.Core.Entities;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text.RegularExpressions;
using Neudesic.Schoolistics.Core.Services;
    using Newtonsoft.Json;
    using System;
    using Cirrious.MvvmCross.Plugins.Messenger;
    using System.Net;
    using System.Net.Http;
    using System.IO;
    using Cirrious.CrossCore;

    /// <summary>
    /// Define the EditprofileViewModel type.
    /// </summary>
    public class EditprofileViewModel : MvxViewModel
    {
        IProfileDetailsService profileDetailsService;
        IEditProfileService _editProfileService;
        private readonly IMvxMessenger _messenger;

        public EditprofileViewModel(IEditProfileService editProfileService, IMvxMessenger messenger)
        {
            try
            {
                _messenger = messenger;
                _editProfileService = editProfileService;

                List<string> genderdetails = new List<string>();
                genderdetails.Add("Female");
                genderdetails.Add("Male");
                GenderDetails = new ObservableCollection<string>(genderdetails);

                //editProfileDetails.Gender = "Male";
                List<string> educationdetails = new List<string>();
                educationdetails.Add("B.Com");
                educationdetails.Add("B.Tech");
                EducationDetails = new ObservableCollection<string>(educationdetails);

                List<string> relationdetails = new List<string>();
                relationdetails.Add("Parent");
                relationdetails.Add("Guardian");
                RelationDetails = new ObservableCollection<string>(relationdetails);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in EditprofileViewModel.cs : EditprofileViewModel : " + ex.ToString());
            }
        }

        private User editProfileDetails;

        public User EditProfileDetails
        {
            get { return editProfileDetails; }
            set
            {
                editProfileDetails = value;
                RaisePropertyChanged(() => EditProfileDetails);
            }
        }

        private  ObservableCollection<string> genderDetails;

        public ObservableCollection<string> GenderDetails
        {
            get { return genderDetails; }
            set
            {
                genderDetails = value;
                RaisePropertyChanged(() => GenderDetails);
            }
        }
        private ObservableCollection<string> educationDetails;

        public ObservableCollection<string> EducationDetails
        {
            get { return educationDetails; }
            set
            {
                educationDetails = value;
                RaisePropertyChanged(() => EducationDetails);
            }
        }

        private ObservableCollection<string> relationDetails;

        public ObservableCollection<string> RelationDetails
        {
            get { return relationDetails; }
            set
            {
                relationDetails = value;
                RaisePropertyChanged(() =>RelationDetails);
            }
        }

        public void Init(User details)
        {
            EditProfileDetails = details;
            EditProfileDetails.Gender = "Male";
        }

        /// <summary>
        /// Backing field for my property.
        /// </summary>
        private string myProperty = "Mvx Ninja Coder!";

        /// <summary>
        ///  Backing field for my command.
        /// </summary>
        private MvxCommand myCommand;
        private MvxCommand<ProfileDetails> editProfile;
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
               
                return this.myCommand ?? (this.myCommand = new MvxCommand(() => this.ShowViewModel<EditprofileViewModel>()));
            }
        }

        public ICommand EditProfile
        {
            get
            {
                return new MvxCommand<User>(item => ShowViewModel<MyprofileViewModel>(item));

            }
        }

        public void UpdateProfile(Dictionary<string, string> userDetails, string fileName, string fileContentType, byte[] fileData)
        {
            try
            {
                _editProfileService.UpdateProfile(userDetails, fileName, fileContentType, fileData, UpdateProfileSuccess, UpdateProfileFailure);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in EditprofileViewModel.cs : UpdateProfile : " + ex.ToString());
            }
        }

        public void UpdateProfileSuccess(ResponseMessage<Object> response)
        {
            try
            {
                if (response.SuccessCode == 1)
                {
                    if (response.Data != null)
                    {
                        _messenger.Publish(new NavigationMessage<object>(this) { Message = "UpdateAuthTokenAfterPasswordChange", Data = response.Data });
                    }

                    ShowViewModel<MyprofileViewModel>();
                }
                if (response.SuccessCode == 0)
                {
                    _messenger.Publish(new NavigationMessage<object>(this) { Message = "OldPAsswordMismatchInUpdateProfile" });
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in EditprofileViewModel.cs : UpdateProfileSuccess : " + ex.ToString());
            }
        }

        public void UpdateProfileFailure(Exception e)
        {
            try
            {
                string a = e.Message;
                _messenger.Publish(new NavigationMessage<object>(this) { Message = "ExceptionInEditProfile" });
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in EditprofileViewModel.cs : UpdateProfileFailure : " + ex.ToString());
            }
        }

        public void UpdateProfileWithoutPicture(User userDetails)
        {
            try
            {
                _editProfileService.UpdateProfileWithoutPicture(userDetails, UpdateProfileSuccess, UpdateProfileFailure);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in EditprofileViewModel.cs : UpdateProfileWithoutPicture : " + ex.ToString());
            }
        }

        //public void EditProfileCMD(ProfileDetails item)
        //{
            
        //}
        const string RegExForEmailValidation = @"^([0-9a-zA-Z]+[-._+&amp;])*[0-9a-zA-Z]+@([-0-9a-zA-Z]+[.])+[a-zA-Z]{2,6}$";

        const string RegExForMobileValidation = @"^[0-9]{10}$";

        const string RegExForPassswordValidation=@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z \d $@$!%*#?&]{8,}$";

        
        public bool IsValidEmail(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email)) return false;
                Regex nameRegEx = new Regex(RegExForEmailValidation);
                return nameRegEx.IsMatch(email);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in EditprofileViewModel.cs : IsValidEmail : " + ex.ToString());
                return false;
            }
        }
        
        public bool IsValidPhoneNo(string phoneNo)
        {
            try
            {
                if (string.IsNullOrEmpty(phoneNo)) return false;
                Regex nameRegEx = new Regex(RegExForMobileValidation);
                return nameRegEx.IsMatch(phoneNo);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in EditprofileViewModel.cs : IsValidPhoneNo : " + ex.ToString());
                return false;
            }
        }

        public bool IsValidPassword(string password)
        {
            try
            {
                if (string.IsNullOrEmpty(password)) return false;
                Regex nameRegEx = new Regex(RegExForPassswordValidation);
                return nameRegEx.IsMatch(password);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in EditprofileViewModel.cs : IsValidPassword : " + ex.ToString());
                return false;
            }
        }
        public bool IsValidUsername(string Username)
        {
            try
            {
                if (string.IsNullOrEmpty(Username)) return false;
                return true;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in EditprofileViewModel.cs : IsValidUsername : " + ex.ToString());
                return false;
            }
        }
    }
}

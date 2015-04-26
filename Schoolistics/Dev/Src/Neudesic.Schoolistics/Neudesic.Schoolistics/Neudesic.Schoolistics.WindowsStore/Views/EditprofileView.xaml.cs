// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the EditprofileView.xaml type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Windows.Storage;
using Windows.Storage.Pickers;
using System;
using Neudesic.Schoolistics.Core.ViewModels;
using System.Windows;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Net.Http;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using System.Collections.Generic;
using Neudesic.Schoolistics.WindowsStore.Helpers;
using Windows.UI.Xaml.Media.Imaging;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.CrossCore;
using Neudesic.Schoolistics.Core.Entities;
using Windows.UI.Popups;
using Windows.UI.Core;
using Neudesic.Schoolistics.Core.Utils;

namespace Neudesic.Schoolistics.WindowsStore.Views
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class EditprofileView
    {
       // MultipartFormDataContent content = new MultipartFormDataContent();
        StorageFile file;
        MvxSubscriptionToken token;
       // Stream fileStream;

        public EditprofileView()
        {
            try
            {
                this.InitializeComponent();
                progressBar.Visibility = Visibility.Collapsed;
                progressBar.IsActive = false;
                UpdateButton.IsEnabled = true;
                IMvxMessenger messenger = Mvx.GetSingleton<IMvxMessenger>();
                token = messenger.SubscribeOnMainThread<NavigationMessage<object>>(MessageSubscribe);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in EditprofileView.xaml.cs : EditprofileView : " + ex.ToString());
            }
        }

        public async void MessageSubscribe(NavigationMessage<object> message)
        {
            try
            {
                if (message.Message == "OldPAsswordMismatchInUpdateProfile")
                {
                    await ShowMessage("Mis-match in old password, cant update profile");
                    progressBar.Visibility = Visibility.Collapsed;
                    progressBar.IsActive = false;
                    UpdateButton.IsEnabled = true;
                }
                if (message.Message == "UpdateAuthTokenAfterPasswordChange")
                {
                    UserPreferences.AuthToken = message.Data.ToString();
                    Utils.AuthToken = message.Data.ToString();
                }
                if (message.Message == "ExceptionInEditProfile")
                {
                    await ShowMessage("Sorry for inconvenience, there is a problem in service, please try after some time");
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in EditprofileView.xaml.cs : MessageSubscribe : " + ex.ToString());
            }
        }

        Task ShowMessage(String errorMessage)
        {
            try
            {
                Windows.UI.Core.CoreDispatcher dispatcher = Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher;
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
                            dispatcher.RunAsync(CoreDispatcherPriority.Normal,
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
            catch (Exception ex)
            {
                Mvx.Error("Error in EditprofileView.xaml.cs : ShowMessage : " + ex.ToString());
                return null;
            }
        }

        private async void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {
                FileOpenPicker openPicker = new FileOpenPicker();
                openPicker.ViewMode = PickerViewMode.Thumbnail;
                openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
                openPicker.FileTypeFilter.Add(".jpg");
                openPicker.FileTypeFilter.Add(".jpeg");
                openPicker.FileTypeFilter.Add(".png");
                file = await openPicker.PickSingleFileAsync();
                if (file != null)
                {
                    using (IRandomAccessStream fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read))
                    {
                        // Set the image source to the selected bitmap 
                        BitmapImage bitmapImage = new BitmapImage();
                        await bitmapImage.SetSourceAsync(fileStream);
                        profilePicture.Source = bitmapImage;
                    }
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in EditprofileView.xaml.cs : Button_Click : " + ex.ToString());
            }
        }

        private async void Button_Click1(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {
                bool success = true;
                if (EmailTextBox.Text != string.Empty && !((EditprofileViewModel)ViewModel).IsValidEmail(EmailTextBox.Text))
                {

                    Email.Text = "Invalid Email";
                    Email.Visibility = Visibility.Visible;
                    success = false;
                }
                else
                {
                    Email.Visibility = Visibility.Collapsed;
                    success = !success ? success : true;
                }
                if (PhoneNoTextBox.Text != string.Empty && !((EditprofileViewModel)ViewModel).IsValidPhoneNo(PhoneNoTextBox.Text))
                {
                    PhoneNo.Text = "Invalid PhoneNo";
                    PhoneNo.Visibility = Visibility.Visible;
                    success = false;
                }
                else
                {
                    PhoneNo.Visibility = Visibility.Collapsed;
                    success = !success ? success : true;
                }
                if (!(NewPasswordBox.Password.Equals(ConfirmPasswordBox.Password)))
                {
                    PasswordMissmatchMessage.Text = "new password and confirm password are not same";
                    PasswordMissmatchMessage.Visibility = Visibility.Visible;
                    success = false;
                }
                else
                {
                    PasswordMissmatchMessage.Visibility = Visibility.Collapsed;
                    success = !success ? success : true;
                }
                if (success)
                {
                    progressBar.Visibility = Visibility.Visible;
                    progressBar.IsActive = true;
                    UpdateButton.IsEnabled = false;
                    byte[] imageFile = new byte[64 * 1024];
                    Dictionary<string, string> userDetails = new Dictionary<string, string>();
                    string fileName = string.Empty, fileContent = string.Empty;
                    if (file != null)
                    {
                        imageFile = await GetBytesAsync(file);
                        fileName = file.DisplayName;
                        fileContent = file.ContentType;
                    }
                    if (fileName != string.Empty && fileContent != string.Empty)
                    {
                        userDetails.Add("DateOfBirth", DateOfBirthTextBox.Text.ToString());
                        userDetails.Add("Email", EmailTextBox.Text.ToString());
                        userDetails.Add("Password", NewPasswordBox.Password == string.Empty ? null : NewPasswordBox.Password);
                        userDetails.Add("Username", UserPreferences.Username);
                        userDetails.Add("Gender", comboBox3.SelectedItem.ToString());
                        userDetails.Add("Occupation", occupation.Text.ToString());
                        userDetails.Add("PhoneNumber", PhoneNoTextBox.Text.ToString());
                        userDetails.Add("OldPassword", oldPasswordBox.Password == string.Empty ? null : oldPasswordBox.Password.ToString());

                        ((EditprofileViewModel)ViewModel).UpdateProfile(userDetails, fileName, fileContent, imageFile);
                    }
                    else
                    {
                        var profileDetails = ((EditprofileViewModel)ViewModel).EditProfileDetails;
                        profileDetails.Password = NewPasswordBox.Password == string.Empty ? null : NewPasswordBox.Password;
                        profileDetails.OldPassword = oldPasswordBox.Password == string.Empty ? null : oldPasswordBox.Password;
                        ((EditprofileViewModel)ViewModel).UpdateProfileWithoutPicture(profileDetails);
                    }
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in EditprofileView.xaml.cs : Button_Click1 : " + ex.ToString());
            }
        }

        private async Task<byte[]> GetBytesAsync(IStorageFile file)
        {
            try
            {
                byte[] fileBytes = null;
                using (var stream = await file.OpenReadAsync())
                {
                    fileBytes = new byte[stream.Size];
                    using (var reader = new DataReader(stream))
                    {
                        await reader.LoadAsync((uint)stream.Size);
                        reader.ReadBytes(fileBytes);
                    }
                }

                return fileBytes;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in EditprofileView.xaml.cs : GetBytesAsync : " + ex.ToString());
                return null;
            }
        }

        private void EmailTextBox_LostFocus(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {
                var profileDetails = ((EditprofileViewModel)ViewModel).EditProfileDetails;

                if (EmailTextBox.Text != string.Empty && !((EditprofileViewModel)ViewModel).IsValidEmail(EmailTextBox.Text))
                {

                    Email.Text = "Invalid Email";
                    Email.Visibility = Visibility.Visible;

                }
                else
                {
                    Email.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in EditprofileView.xaml.cs : EmailTextBox_LostFocus : " + ex.ToString());
            }
        }
        private void PhoneNoTextBox_LostFocus(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {
                if (PhoneNoTextBox.Text != string.Empty && !((EditprofileViewModel)ViewModel).IsValidPhoneNo(PhoneNoTextBox.Text))
                {
                    PhoneNo.Text = "Invalid PhoneNo";
                    PhoneNo.Visibility = Visibility.Visible;
                }
                else
                {
                    PhoneNo.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in EditprofileView.xaml.cs : PhoneNoTextBox_LostFocus : " + ex.ToString());
            }
        }

        private void NewPasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (NewPasswordBox.Password != string.Empty && NewPasswordBox.Password.ToString().Length < 6)
                {
                    NewPassword.Text = "Invalid New Password, password should be minimum of 6 characters";
                    NewPassword.Visibility = Visibility.Visible;
                }
                else
                {
                    NewPassword.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in EditprofileView.xaml.cs : NewPasswordBox_LostFocus : " + ex.ToString());
            }
        }

        private void ConfirmPasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ConfirmPasswordBox.Password != string.Empty && ConfirmPasswordBox.Password.ToString().Length < 6)
                {
                    ConfirmPassword.Text = "Invalid Confirm Password, password should be minimum of 6 characters";
                    ConfirmPassword.Visibility = Visibility.Visible;
                }
                else
                {
                    ConfirmPassword.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in EditprofileView.xaml.cs : ConfirmPasswordBox_LostFocus : " + ex.ToString());
            }
        }

        private void UsernameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
        }
    }
}
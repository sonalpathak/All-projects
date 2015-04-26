using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
//using Facebook;
using Neudesic.Schoolistics.Core.Entities;
using Neudesic.Schoolistics.Core.Services;
using Neudesic.Schoolistics.Core.Utils;
using Neudesic.Schoolistics.Core.ViewModels;
using Neudesic.Schoolistics.WindowsStore.Helpers;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TwitterRtLibrary;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Authentication.Web;
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
using Windows.Data.Json;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;



// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Neudesic.Schoolistics.WindowsStore.Usercontrols
{
    public sealed partial class LoginControl : UserControl
    {
        CoreDispatcher coreDispatcher = Window.Current.Dispatcher;
        IMvxMessenger messenger = Mvx.GetSingleton<IMvxMessenger>();
        MvxSubscriptionToken token;
        private static Popup popUp;
        private LoginViewModel _loginViewModel;
        public LoginControl()
        {
            try
            {
                this.InitializeComponent();

                IMvxMessenger messenger = Mvx.GetSingleton<IMvxMessenger>();
                token = messenger.Subscribe<NavigationMessage<User>>(MessageSubscribe);
                _loginViewModel = new LoginViewModel(new RegistrationService(new RestService()), messenger);
                this.DataContext = _loginViewModel;

                LoadRememberedCredentials();
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in LoginControl.xaml.cs : LoginControl : " + ex.ToString());
            }
        }

        public void LoadRememberedCredentials()
        {
            try
            {
                if (UserPreferences.RememberUsername != null)
                {
                    usernametextBlock.Text = UserPreferences.RememberUsername;
                    passwordBox.Password = UserPreferences.RememberPassword;
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in LoginControl.xaml.cs : LoadRememberedCredentials : " + ex.ToString());
            }
        }

        public Popup GetLoginPopUp()
        {
            try
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
            catch (Exception ex)
            {
                Mvx.Error("Error in LoginControl.xaml.cs : GetLoginPopUp : " + ex.ToString());
                return null;
            }
        }

        private async void facebookLogin_Tapped(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                this.DataContext = new RegistrationViewModel(new RegistrationService(new RestService()), messenger);
                //Facebook Authentication Uri
                var facebookUri = "https://www.facebook.com/dialog/oauth";
                //Standard redirect uri for desktop/non-web based apps
                var redirectUri = "https://www.facebook.com/connect/login_success.html";

                //Place your app client id here
                var clientId = 661983153858271;
                //The type of token that can be requested
                var responseType = "token";
                //The Facebook access permissions.
                var scope = "manage_notifications,read_friendlists,read_stream,email";

                //Response pattern
                var pattern = string.Format("{0}#access_token={1}&expires_in={2}", redirectUri, "(?<access_token>.+)", "(?<expires_in>.+)");

                //Step 1. Create a Request and Callback Uri for the Authentication operation
                var requestUri = new Uri(string.Format("{0}?client_id={1}&redirect_uri={2}&response_type={3}&scope={4}&display=popup",
                facebookUri, clientId, redirectUri, responseType, scope), UriKind.RelativeOrAbsolute);
                var callbackUri = new Uri(redirectUri, UriKind.RelativeOrAbsolute);

                //Step 2. Initialize a Authentication operation using WebAuthenticationBroker
                var asyncOperation = await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, requestUri, callbackUri);

                switch (asyncOperation.ResponseStatus)
                {
                    case WebAuthenticationStatus.ErrorHttp:

                        //An HTTP error like a 404 was returned
                        break;
                    case WebAuthenticationStatus.Success:
                        var match = Regex.Match(asyncOperation.ResponseData, pattern);
                        var access_token = match.Groups["access_token"];
                        var expires_in = match.Groups["expires_in"];
                        //Use access_token and expires_in for further API Calls                   
                        var client = new HttpClient();
                        var AccessToken = access_token.Value;
                        // var client = new FacebookClient(AccessToken);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("OAuth", AccessToken);
                        var result = await client.GetStringAsync("https://graph.facebook.com/me?scope=email,friends");


                        var profileInformation = Windows.Data.Json.JsonObject.Parse(result);
                        var email = profileInformation["username"].GetString();


                        // dynamic me = await client.GetTaskAsync("me", new { fields = "gender,username" });                  
                        //   dynamic me = await client.GetTaskAsync("me");
                        //   var result = (IDictionary<string, object>)me;
                        // var result = (IDictionary<string, object>)me;
                        //((RegistrationViewModel)this.DataContext).SaveDetails(result);
                        //  (this.DataContext as RegistrationViewModel).SaveDetails(result);

                        var TokenExpiry = DateTime.Now.AddSeconds(double.Parse(expires_in.Value));
                        break;

                    case WebAuthenticationStatus.UserCancel:
                        AccessToken = null;
                        TokenExpiry = DateTime.MinValue;
                        //The User selected to cancel the Authentication process
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in LoginControl.xaml.cs : facebookLogin_Tapped : " + ex.ToString());
               
            }


        }
        public TwitterRt TwitterRt { get; private set; }
        private async void twitterLogin_Tapped(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                this.InitializeComponent();
                TwitterRt = new TwitterRt("QaTyK0XgxG4mZ93UhLywA", "gN1RaoVWa9A7bHf0qFPfDCTD8c9jvzk3UWk0DMzX7Q", "http://mytwitapp.com");
                await TwitterRt.GainAccessToTwitter();
                string userName = TwitterRt.ScreenName.ToString();
                (this.DataContext as RegistrationViewModel).SaveDetailsFromTwitter(userName);

                // _statusTextBlock.Text = TwitterRt.Status;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in LoginControl.xaml.cs : twitterLogin_Tapped : " + ex.ToString());

            }
        }

        public string access_token;
        public string refreshToken;
        public string fbidCurrent;
        string code;
        string clientID = "136540381977-s9dugimpf9nfuvia3p0iv8hdnft6e8jk.apps.googleusercontent.com";
        string clientSecret = "A53jFWvwjICp_cuidm4vrfY7";
        string callbackUrl = "urn:ietf:wg:oauth:2.0:oob";
        string scope = "https://www.googleapis.com/auth/userinfo.profile https://www.googleapis.com/auth/userinfo.email";
        private RegistrationControl registrationControl;
        private Popup _regsitrationPopUp;

        public async void google_Tapped(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                // string applicationUrl = Windows.Security.Authentication.Web.WebAuthenticationBroker.GetCurrentApplicationCallbackUri().AbsoluteUri;
                //  callbackUrl = applicationUrl;
                Windows.Storage.ApplicationData.Current.LocalSettings.Values["code"] = "";
                if (access_token == null)
                {
                    if (refreshToken == null && code == null)
                    {
                        try
                        {
                            String GoogleURL = "https://accounts.google.com/o/oauth2/auth?client_id=" + Uri.EscapeDataString(clientID) + "&redirect_uri=" + Uri.EscapeDataString(callbackUrl) + "&response_type=code&scope=" + Uri.EscapeDataString(scope);

                            System.Uri StartUri = new Uri(GoogleURL);
                            // When using the desktop flow, the success code is displayed in the html title of this end uri
                            System.Uri EndUri = new Uri("https://accounts.google.com/o/oauth2/approval?");

                            //  DebugPrint("Navigating to: " + GoogleURL);

                            WebAuthenticationResult WebAuthenticationResult = await WebAuthenticationBroker.AuthenticateAsync(
                                                                    WebAuthenticationOptions.UseTitle,
                                                                    StartUri,
                                                                    EndUri);
                            if (WebAuthenticationResult.ResponseStatus == WebAuthenticationStatus.Success)
                            {
                                string response = WebAuthenticationResult.ResponseData.ToString();
                                // strip to start of auth code

                                code = response.Substring(response.IndexOf("=") + 1);
                                if (!code.Equals("access_denied"))
                                {
                                    Windows.Storage.ApplicationData.Current.LocalSettings.Values["code"] = code;
                                    codeToAcccesTok();
                                }
                                else
                                {
                                    code = null;
                                }
                                // TODO: switch off button, enable writes, etc.
                            }
                            else if (WebAuthenticationResult.ResponseStatus == WebAuthenticationStatus.ErrorHttp)
                            {
                                //TODO: handle WebAuthenticationResult.ResponseErrorDetail.ToString()
                            }
                            else
                            {
                                // This could be a response status of 400 / 401
                                // Could be really useful to print debugging information such as "Your applicationID is probably wrong"
                                //TODO: handle WebAuthenticationResult.ResponseStatus.ToString()
                            }
                        }
                        catch (Exception Error)
                        {
                            string a = Error.Message;
                            //
                            // Bad Parameter, SSL/TLS Errors and Network Unavailable errors are to be handled here.
                            //
                            //  DebugPrint(Error.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in LoginControl.xaml.cs : google_Tapped : " + ex.ToString());

            }

        }
        private async void codeToAcccesTok()
        {
            try
            {
                string oauthUrl = "https://accounts.google.com/o/oauth2/token";

                HttpClient theAuthClient = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, oauthUrl);

                // default case, we have an authentication code, want a refresh/access token            
                string content = "code=" + code + "&" +
                    "client_id=" + clientID + "&" +
                    "client_secret=" + clientSecret + "&" +
                    "redirect_uri=" + callbackUrl + "&" +
                    "grant_type=authorization_code";

                if (refreshToken != null)
                {
                    content = "refresh_token=" + refreshToken + "&" +
                    "client_id=" + clientID + "&" +
                    "client_secret=" + clientSecret + "&" +
                    "grant_type=refresh_token";
                }

                request.Method = HttpMethod.Post;
                request.Content = new StreamContent(new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(content)));
                request.Content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                try
                {
                    HttpResponseMessage response = await theAuthClient.SendAsync(request);
                    parseAccessToken(response);
                }
                catch (HttpRequestException hre)
                {

                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in LoginControl.xaml.cs : google_Tapped : " + ex.ToString());

            }
        }
        public async void parseAccessToken(HttpResponseMessage response)
        {
            try
            {
                string content = await response.Content.ReadAsStringAsync();

                if (content != null)
                {
                    string[] lines = content.Replace("\"", "").Replace(" ", "").Replace(",", "").Split('\n');
                    for (int i = 0; i < lines.Length; i++)
                    {
                        string[] paramSplit = lines[i].Split(':');
                        if (paramSplit[0].Equals("access_token"))
                        {
                            access_token = paramSplit[1];
                        }
                        if (paramSplit[0].Equals("refresh_token"))
                        {
                            refreshToken = paramSplit[1];
                            Windows.Storage.ApplicationData.Current.LocalSettings.Values["refreshToken"] = refreshToken;
                        }
                    }
                    if (access_token != null)
                    {
                        getProfile();
                    }
                    else
                    {
                        // something is wrong, fix this
                    }
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in LoginControl.xaml.cs : google_Tapped : " + ex.ToString());

            }

        }
        public async void getProfile()
        {
            try
            {
                HttpClient httpClient = new HttpClient();

                var searchUrl = "https://www.googleapis.com/plus/v1/people/me/";

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + access_token);

                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(searchUrl);
                    ParseProfile(response);
                }
                catch (HttpRequestException hre)
                {
                    // DebugPrint(hre.Message);
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in LoginControl.xaml.cs : getProfile : " + ex.ToString());

            }
        }
        private async void ParseProfile(HttpResponseMessage response)
        {
            try
            {
                string content = await response.Content.ReadAsStringAsync();
                var jsonDict = (JObject)JsonConvert.DeserializeObject(content.ToString());
                string name = jsonDict["displayName"].ToString();
                string email = jsonDict["emails"][0]["value"].ToString();
                _loginViewModel.LoginUserFromGoogle(name, email);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in LoginControl.xaml.cs : ParseProfile : " + ex.ToString());

            }

        }

        private void signUp_Tapped(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                if (_regsitrationPopUp == null)
                {
                    registrationControl = new RegistrationControl();
                    // registrationControl.DataContext = this.DataContext;
                    _regsitrationPopUp = registrationControl.GetRegistrationPopUp();
                    _regsitrationPopUp.IsOpen = true;

                    return;
                }
                _regsitrationPopUp.IsOpen = !_regsitrationPopUp.IsOpen;
            }
            catch (Exception ex)
            {
                string a = ex.Message;
            }
        }

        public async void MessageSubscribe(NavigationMessage<User> messageData)
        {
            try
            {
                if (messageData.Message == "LoginSuccess")
                {
                    if (messageData.Data != null)
                    {
                        UserPreferences.Username = messageData.Data.Username;
                        UserPreferences.AuthToken = messageData.Data.AuthToken;

                        Utils.Username = messageData.Data.Username;
                        Utils.AuthToken = messageData.Data.AuthToken;

                        await coreDispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                   {
                       if (rememberMeCheckBox.IsChecked.HasValue)
                       {
                           if (rememberMeCheckBox.IsChecked.Value)
                           {
                               UserPreferences.RememberUsername = usernametextBlock.Text;
                               UserPreferences.RememberPassword = passwordBox.Password;
                           }
                       }
                   });


                        ShowMessage("Login Successfully");
                    }
                    else
                    {
                        ShowMessage("Login Failed");
                    }
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in LoginControl.xaml.cs : MessageSubscribe : " + ex.ToString());

            }

        }

        Task ShowMessage(String errorMessage)
        {
            try
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
                Mvx.Error("Error in LoginControl.xaml.cs : ShowMessage : " + ex.ToString());
                return null;

            }

        }

        private void GoBack(object sender, RoutedEventArgs e)
        {

        }

        private void Login_Tapped(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                var password = passwordBox.Password;
                _loginViewModel.LoginCommand.Execute(password);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in LoginControl.xaml.cs : Login_Tapped : " + ex.ToString());
               

            }

        }

    }
}

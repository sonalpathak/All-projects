using Microsoft.WindowsAzure.Storage.Blob;
using Neudesic.Schoolistics.Framework.API_Models;
using Neudesic.Schoolistics.Framework.Models;
using Neudesic.Schoolistics.Framework.Repositories;
using Neudesic.Schoolistics.Framework.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;

namespace Neudesic.Schoolistics.WebRole.APIs
{
    public class UserController : ApiController
    {
        IUserRepository userRepository;

        public UserController(IUserRepository _userRep)
        {
            userRepository = _userRep;
        }

        [HttpPost]
        public HttpResponseMessage SignUp(UserInfo user)
        {
            var existingEmail = this.userRepository.FindByEmail(user.Email);

            var existingUser = this.userRepository.FindByUsername(user.Username);

            if (existingEmail == null && existingUser == null)
            {
                var password = user.Password;
                user.Username = user.Username.ToLowerInvariant();
                user.Email = user.Email.ToLowerInvariant();

                this.userRepository.CreateUser(user);

                string toCrypt = user.Username + ":" + password;

                var authToken = Crypto.EncryptStringAES(toCrypt);

                return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object> { SuccessCode = 1, Message = "User Registered Successfully", Data = JObject.FromObject(new { Username = user.Username, AuthToken = authToken }) });
            }
            else if (existingUser != null)
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object> { SuccessCode = 0, Message = "Username already exists in the system, please try with different username.", ErrorCode = 1001 });
            else
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object> { SuccessCode = 0, Message = "Email id is already registered, please try with different email id.", ErrorCode = 1002 });

        }

        [HttpPost]
        public HttpResponseMessage Login(UserInfo user)
        {

            bool userExists;
            UserInfo userMatched = new UserInfo();

            if (user.Username.Contains("@"))
            {
                userMatched = this.userRepository.FindByEmail(user.Username.ToLower());
                if (userMatched != null)
                    userExists = this.userRepository.ValidateAccount(userMatched.Username.ToLower(), user.Password);
                else
                    userExists = false;
            }
            else
            {
                userExists = this.userRepository.ValidateAccount(user.Username.ToLower(), user.Password);
                userMatched = this.userRepository.FindByUsername(user.Username.ToLower());
            }

            if (userExists)
            {
                string toCrypt = userMatched.Username + ":" + user.Password;

                var authToken = Crypto.EncryptStringAES(toCrypt);

                return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object> { SuccessCode = 1, Message = "Login Successful", Data = JObject.FromObject(new { Username = userMatched.Username, AuthToken = authToken }) });
            }
            else
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object> { SuccessCode = 0, Message = "Invalid user name or password. Please try with correct username and password.", ErrorCode = 1003 });
        }

        [HttpPost]
        public HttpResponseMessage ForgotPassword(UserInfo userInfo)
        {
            var user = new UserInfo();

            if (userInfo.Username.Contains("@"))
            {
                user = this.userRepository.FindByEmail(userInfo.Username);
            }
            else
            {
                user = this.userRepository.FindByUsername(userInfo.Username);
            }

            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object> { SuccessCode = 0, Message = "Invalid Username/Email id" });
            }
            else
            {
                var newPassword = this.userRepository.UpdatePassword(user);
                MailMessage email = new MailMessage();
                email.From = new MailAddress(ConfigurationManager.AppSettings["Username"]);
                email.To.Add(new MailAddress(user.Email));
                email.Subject = "Schoolistics Password Reset";
                email.Body = EmailTemplateService.HtmlMessageBody("~/EmailTemplates/forgot-password.html", new { Username = user.Username, Password = newPassword });
                email.IsBodyHtml = true;
                var client = new SmtpClient(WebConfigurationManager.AppSettings["Host"], Convert.ToInt32(WebConfigurationManager.AppSettings["Port"]))
                {
                    Credentials = new NetworkCredential(WebConfigurationManager.AppSettings["Username"], WebConfigurationManager.AppSettings["Password"]),
                    EnableSsl = true
                };
                client.Send(email);

                return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object> { SuccessCode = 1, Message = "A mail has been sent to your registered Email id" });
            }
        }

        [HttpGet]
        [Authorize]
        public HttpResponseMessage GetProfileInfo()
        {
            String username = HttpContext.Current.User.Identity.Name;

            var existingUser = this.userRepository.FindByUsername(username);

            if (existingUser != null)
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<UserInfo> { SuccessCode = 1, Data = existingUser });
            else
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object> { SuccessCode = 0, ErrorCode = 1004 });
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage UpdateUserInfo(UserProfileData user)
        {
            String username = HttpContext.Current.User.Identity.Name;

            var success = this.userRepository.UpdateUser(user);
            
            if (!success)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object> { SuccessCode = 0, Message = "Old Password is not matching", ErrorCode = 1005 });
            }

            string authToken = null;
            if (!String.IsNullOrEmpty(user.Password))
            {
                string toCrypt = user.Username + ":" + user.Password;

                authToken = Crypto.EncryptStringAES(toCrypt);
            }

            //return ok status code as we updated the user
            return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<string> { SuccessCode = 1, Message = "User Details Updated", Data = authToken });
        }

        [HttpPost]
        [Authorize]
        public async Task<HttpResponseMessage> UpdateProfileInfo()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string path = Path.GetTempPath();

            var provider = new MultipartFormDataStreamProvider(path);

            try
            {

                // Read the form data and return an async task.
                var response = await Request.Content.ReadAsMultipartAsync(provider);

                UserProfileData user = new UserProfileData();

                // This illustrates how to get the form data.
                foreach (var key in provider.FormData.AllKeys)
                {
                    foreach (var val in provider.FormData.GetValues(key))
                    {
                        if (key == "DateOfBirth")
                            user.DateOfBirth = val;
                        if (key == "Email")
                            user.Email = val;
                        if (key == "Password")
                            user.Password = val;
                        if (key == "OldPassword")
                            user.OldPassword = val;
                        if (key == "Username")
                            user.Username = val;
                        if (key == "Gender")
                            user.Gender = val;
                        if (key == "Occupation")
                            user.Occupation = val;
                        if (key == "PhoneNumber")
                            user.PhoneNumber = val;
                    }
                }

                if (user.Username != HttpContext.Current.User.Identity.Name)
                    throw new HttpResponseException(HttpStatusCode.Unauthorized);

                string blobFilename = null;

                UserInfo oldUser = this.userRepository.FindByUsername(user.Username);

                var container = CloudStorageHelper.GetBlobContainer(ContainerNames.Picture);

                // Creating a blob and storing it in blob storage. And removing the temp local file 
                var bodyPart = provider.FileData.Where(p => p.Headers.ContentDisposition.Name.Replace("\"", string.Empty) == "picture").FirstOrDefault();
                //var bodyPart = provider.FileData.FirstOrDefault();

                if (bodyPart != null)
                {
                    string savedFile = bodyPart.LocalFileName;
                    string originalFile = bodyPart.Headers.ContentDisposition.FileName.Replace("\"", string.Empty);

                    Image originalImage = Image.FromFile(savedFile);

                    var resizedImage = MediaFuncs.ResizeImage(originalImage, 100, 100);

                    var randomFileName = Guid.NewGuid().ToString() + ".png";

                    var tempFilename = Path.Combine(path, randomFileName);

                    // Save blob contents to a file.
                    using (var fileStream = System.IO.File.OpenWrite(tempFilename))
                    {
                        resizedImage.Save(fileStream, System.Drawing.Imaging.ImageFormat.Png);
                    }

                    if (oldUser.Image == "" || oldUser.Image == null || oldUser.Image == "null" || oldUser.Image == "undefined")
                    {
                        var blob = container.GetBlockBlobReference(randomFileName);
                        blob.Properties.ContentType = Path.GetExtension(originalFile);

                        // Create or overwrite the "cloudBlob" blob with contents from a local file.
                        using (var fileStream = System.IO.File.OpenRead(tempFilename))
                        {
                            blob.UploadFromStream(fileStream);
                        }

                        user.Image = randomFileName;
                    }
                    else
                    {
                        blobFilename = oldUser.Image;
                        ICloudBlob blob = container.GetBlockBlobReference(blobFilename);

                        if (blob.Exists())
                        {
                            blob.Delete();
                        }

                        blob = container.GetBlockBlobReference(randomFileName);
                        blob.Properties.ContentType = Path.GetExtension(originalFile);

                        // Create or overwrite the "cloudBlob" blob with contents from a local file.
                        using (var fileStream = System.IO.File.OpenRead(tempFilename))
                        {
                            blob.UploadFromStream(fileStream);
                        }

                        user.Image = randomFileName;

                    }

                    // removing the temp local file
                    FileInfo file1 = new FileInfo(tempFilename);
                    file1.Delete();
                }

                var success = this.userRepository.UpdateUser(user);

                if (!success)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object> { SuccessCode = 0, Message = "Old Password is not matching", ErrorCode = 1005 });
                }

                string authToken = null;
                if (!String.IsNullOrEmpty(user.Password))
                {
                    string toCrypt = user.Username + ":" + user.Password;

                    authToken = Crypto.EncryptStringAES(toCrypt);
                }
                
                //return ok status code as we updated the user
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<string> { SuccessCode = 1, Message = "User Details Updated", Data = authToken });

            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}
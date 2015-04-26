using Cirrious.MvvmCross.Plugins.Messenger;
using Neudesic.Schoolistics.Core.Entities;
// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the SchoolDetailsViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
using Neudesic.Schoolistics.Core.Services;
using Newtonsoft.Json;
using System;
//using Neudesic.Schoolistics.Framework.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Neudesic.Schoolistics.Core.Entities;
using Cirrious.MvvmCross.ViewModels;
using System.Windows.Input;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Cirrious.CrossCore;
namespace Neudesic.Schoolistics.Core.ViewModels

{
    /// <summary>
    /// Define the SchoolDetailsViewModel type.
    /// </summary>
    public class SchoolDetailsViewModel : BaseViewModel
    {
         private ISchoolDetailsService _service;
         private double ratingNumber;
         //private int ratingNumber;
         private double _userratingview;
         private double _overallRating;
         private List<SchoolManagement> managementdetails;
         private string schoolName;
        private double latitude_schooldetails;
        private double longitude_schooldetails;
        private string schoolAddress_schooldetails;
        private string schoolName_schooldetails;
        private readonly IMvxMessenger _messenger;
        private string schoolId;
        private string schoolCity;
        private MvxCommand goBackClickCommand;
        private double myRating;
        private ICommand postcomment;
        private string _userComment;
        private string _userNameWhoCommented;
        private DateTime created;
        //public SchoolDetailsViewModel()
        //{
        //    Details = SchoolDetails.Where(x => x.SchoolId == id).First();
        //    RatingNumber = Details.Rating;
        //    SchoolName = Details.SchoolName;
        //    latitude_schooldetails = latitude;
        //    longitude_schooldetails = longitude;
        //}
        public SchoolDetailsViewModel(ISchoolDetailsService service, IMvxMessenger messenger)
        {
            try
            {
                _messenger = messenger;
                // RatingNumber = 1;
                _service = service;
                SchoolDetails = _service.DisplaySchoolDetails();
                //var newList = List<Alumni>();
                if (alumniDetails.Count > 0 && alumniDetails != null)
                {
                    alumniDetails.Clear();
                }
                AlumniDetails = _service.GetAlumniDetails();
                ManagementDetails = _service.GetManagementDetails();
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SchoolDetailsViewModel.cs :SchoolDetailsViewModel: " + ex.ToString());
            }
        }
        private string comments;
        public string Comments
        {
            get { return comments; }
            set { comments = value; this.RaisePropertyChanged(() => Comments); }
        }

               // return this._userRating ?? (this._userRating = new MvxCommand<int>(item => GetNewUserRating(item)));
       // private string 
        private MvxCommand _comment;
        public ICommand Comment
        {
            get
            {
                _comment = _comment ?? new MvxCommand(GetNewUserComment);
              
                //_messenger.Publish(new NavigationMessage<object>(this) { Message = "LatLon", Data = Latitude_schooldetails + "$" + Longitude_schooldetails + "$" + address + "$" + sname });
                
                return _comment;
                //return this._comment ?? (this._comment = new MvxCommand(item => GetNewUserComment()));
            }
        }

        private void GetNewUserComment()
        {
            string abc = Comments;

            UserSchoolComment usercomment = new UserSchoolComment();
            usercomment.Comment = Comments;
            usercomment.SchoolId = Details.SchoolId;
            usercomment.Username = Utils.Utils.Username;
            try
            {
                _service.AddCommentOnSchool(usercomment, UserCommentSuccess, UserCommentFailure);
            }
            catch (Exception ex)
            {
                string a = ex.Message;
            }
           // Comments = item;
        }

        private void UserCommentFailure(Exception exception)
        {
            var e = exception.Message;
        }
        private static ObservableCollection<UserSchoolComment> commentDetails;
        public ObservableCollection<UserSchoolComment> CommentDetails
        {
            get
            { return commentDetails; }
            set
            {
                commentDetails = value;
                RaisePropertyChanged(() => CommentDetails);
            }
        }

        private void UserCommentSuccess(Entities.ResponseMessage<Object> response)
        {
            try
            {
                if (response.SuccessCode == 1)
                {
                    List<UserSchoolComment> ReceivedComments = JsonConvert.DeserializeObject<List<UserSchoolComment>>(response.Data.ToString());
                    CommentDetails = new ObservableCollection<UserSchoolComment>(ReceivedComments);
                    _messenger.Publish(new NavigationMessage<object>(this) { Message = "PostPopup" });

                    //UserNameWhoCommented = data.Username;
                    //Created = data.Created;
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SchoolDetailsViewModel.cs :SchoolDetailsViewModel: " + ex.ToString());
            }
            //throw new NotImplementedException();
        }

        public ICommand GoBackClickCommand
        {
            get
            {
                goBackClickCommand = goBackClickCommand ?? new MvxCommand(GoBackClick);
                return goBackClickCommand;
            }
        }
        public void GoBackClick()
        {
            //detailsOfSearching = null;
            try
            {
                ShowViewModel<SearchresultsViewModel>();
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SchoolDetailsViewModel.cs :SchoolDetailsViewModel: " + ex.ToString());
            }
        }


      




        public void Init(string id, string Scity)
         {
             try
             {
                 //Details = SchoolDetails.Where(x => x.SchoolId == id).First();
                 //RatingNumber = Details.Rating;
                 //SchoolName = Details.SchoolName;
                 //SchoolId = Details.SchoolId;
                 SchoolCity = Scity;
                 SchoolId = id;
                 //Latitude_schooldetails = latitude;
                 //Longitude_schooldetails = longitude;
                 //SchoolAddress_schooldetails = schooladdress;
                 // SchoolName_schooldetails = name;


                 //  getLatitudeLongitude(schooladdress);
                 SchoolDetailsViaApi();
                 AllComments();
             }
             catch (Exception ex)
             {
                 Mvx.Error("Error in SchoolDetailsViewModel.cs :SchoolDetailsViewModel: " + ex.ToString());
             }
             //SchoolAlumniViaApi(id);
             //SchoolManagementViaApi(id);
         }

        private void AllComments()
        {
            try
            {
                School school = new School();
                string s = SchoolId;
                 _service.AllCommentsOnSchool(s, AllCommentsSuccess, AllCommentsFailure);

              
            }
            catch (Exception ex)
            {
                string a = ex.Message;
            }
        }

        private void AllCommentsFailure(Exception ex)
        {
            var e = ex.Message;
        }

        private void AllCommentsSuccess(Entities.ResponseMessage<Object> res)
        {
            try
            {
                if (res.SuccessCode == 1)
                {
                    List<UserSchoolComment> ReceivedComments = JsonConvert.DeserializeObject<List<UserSchoolComment>>(res.Data.ToString());
                    CommentDetails = new ObservableCollection<UserSchoolComment>(ReceivedComments);

                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SchoolDetailsViewModel.cs :SchoolDetailsViewModel: " + ex.ToString());
            }
        }
        private static ObservableCollection<Alumni> _alumniDetails;

        public ObservableCollection<Alumni> Alumnidetails
        {
            get
            { return _alumniDetails; }
            set
            {
                _alumniDetails = value;
                RaisePropertyChanged(() => Alumnidetails);
            }
        }
        private static ObservableCollection<SchoolManagement> _managementdetails;
        public ObservableCollection<SchoolManagement> Managementdetails
        {
            get{ return _managementdetails;}
            set{ _managementdetails=value;RaisePropertyChanged(()=>Managementdetails);}
        }



        private UserSchoolComment _selectedSchoolcomment;
        public UserSchoolComment SelectedSchoolComment
        {
            get
            {
                return _selectedSchoolcomment;
            }

            set
            {
                _selectedSchoolcomment = value; RaisePropertyChanged(() => SelectedSchoolComment);
            }





        }

        private void SchoolManagementViaApi(string schoolId)
        {
            try
            {
                _service.GetSchoolManagement(schoolId, ManagementDetailsSuccess, ManagementDetailsFailure);
            }
            catch (Exception ex)
            {
                string a = ex.Message;
            }
        }
        private void  ManagementDetailsSuccess(ResponseMessage<Object> response)
        {
             if (response.SuccessCode == 1)
            {
                List<SchoolManagement> management = JsonConvert.DeserializeObject<List<SchoolManagement>>(response.Data.ToString());
                Managementdetails = new ObservableCollection<SchoolManagement>(management);
            }
        }
        private void ManagementDetailsFailure(Exception e)
        {
            var message = e.Message;
        }
        private void SchoolAlumniViaApi(string schoolId)
        {
            try
            {
                _service.GetAlumniDetails(schoolId, AlumniDetailsSuccess, AlumniDetailsFailure);
            }
            catch (Exception ex)
            {
                string a = ex.Message;
            }
        }

        private void AlumniDetailsFailure(Exception e)
        {
            var message = e.Message;
        }

        private void AlumniDetailsSuccess(Entities.ResponseMessage<Object> response)
        {
            if (response.SuccessCode == 1)
            {
                List<Alumni> alumni = JsonConvert.DeserializeObject<List<Alumni>>(response.Data.ToString());
                Alumnidetails = new ObservableCollection<Alumni>(alumni);
            }
        }
        private MvxCommand<int> _userRating;
        public ICommand UserRating
        {
            get
            {
                //_userRating = _userRating ?? new MvxCommand<int>(item => _UserRating(item));

                return this._userRating ?? (this._userRating = new MvxCommand<int>(item => GetNewUserRating(item)));

            }
        }

        public async void  GetLatLong(School school)
        {
            try
            {
                //SchoolDetailsViaApi();
                string address = school.Address;
                string sname = school.SchoolName;
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync("http://maps.googleapis.com/maps/api/geocode/json?address=" + address + "&sensor=false");
                var jsonContent = await response.Content.ReadAsStringAsync();

                var sampleGroups1 = (JObject)JsonConvert.DeserializeObject(jsonContent.ToString());
                Type type = typeof(RootObject);
                RootObject rootObject = (RootObject)JsonConvert.DeserializeObject(sampleGroups1.ToString(), type);
                if (rootObject != null && rootObject.results.Count > 0)
                {
                    Latitude_schooldetails = rootObject.results[0].geometry.location.lat;
                    Longitude_schooldetails = rootObject.results[0].geometry.location.lng;
                }
                _messenger.Publish(new NavigationMessage<object>(this) { Message = "LatLon", Data = Latitude_schooldetails + "$" + Longitude_schooldetails + "$" + address + "$" + sname });
                //await Task.Delay(200);
                // return true;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SchoolDetailsViewModel.cs :SchoolDetailsViewModel: " + ex.ToString());
            }
        }

        public void GetNewUserRating(int item)
        {
            try
            {
                // School school = new School();
                UserSchoolRating rating = new UserSchoolRating();
                rating.City = SchoolCity;
                rating.SchoolId = SchoolId;
                rating.Rating = item;
                rating.Username = Utils.Utils.Username;

                //s.UserRating = item;
                //s.Rating = item;
                _service.GetRatings(rating, UserRatingSuccess, UserRatingFailure);
            }
            catch (Exception ex)
            {
                string a = ex.Message;
            }
        }



        private void UserRatingSuccess(ResponseMessage<Object> response)
        {
            try
            {
                if (response.SuccessCode == 1)
                {
                    School school = JsonConvert.DeserializeObject<School>(response.Data.ToString());
                    //Details.UserRating = school.UserRating;
                    Details.Rating = school.Rating;
                    MyRating = school.UserRating;
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SchoolDetailsViewModel.cs :SchoolDetailsViewModel: " + ex.ToString());
            }
        }
        private void UserRatingFailure(Exception e)
        {

            var message = e.Message;
        }
      

        //private void _UserRating(int item)
        //{
        //    School s = new School();
        //    s.UserRating = RatingNumber;
           
            
        //}

        private void SchoolDetailsViaApi()
        {
            try
            {
                School school = new School();

                school.SchoolId = SchoolId;
                // schoolDetails.SchoolName = "Chirec";
                school.City = SchoolCity;
                //school.UserRating = RatingNumber;

                _service.GetschoolDetails(school, SchoolDetailsSuccess, SchoolDetailsFailure);
                //registrationService.PostRegistrationDetails(userDetails, RegistrationSuccess, RegistrationFailure);
            }
            catch (Exception ex)
            {
                string a = ex.Message;
            }
        }

        private void SchoolDetailsFailure(Exception e)
        {
            var message = e.Message;
        }

        private  void SchoolDetailsSuccess(ResponseMessage<Object> response)
        {
            try
            {
                if (response.SuccessCode == 1)
                {
                    School school = JsonConvert.DeserializeObject<School>(response.Data.ToString());
                    //School newSchoolDetails = new School();
                    //newSchoolDetails.Accreditation = school.Accreditation;
                    //newSchoolDetails.Address = school.Address;
                    Details = school;
                    MyRating = school.UserRating;
                    //  GetNewUserRating(Convert.ToInt32(school.UserRating));
                    GetLatLong(school);

                    // _messenger.Publish(new NavigationMessage<object>(this) { Message = "LatLon" });
                    //UserratingView = school.UserRating;
                    //OverallRating = school.Rating;
                    ////Name

                    //messenger.Publish(new NavigationMessage<User>(this) { Message = "RegistrationSuccess", Data = userDetails });
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SchoolDetailsViewModel.cs :SchoolDetailsViewModel: " + ex.ToString());
            }
        }
       

     

        //public SchoolDetailsViewModel()
        //{
        //    // TODO: Complete member initialization
        //}
        public double MyRating
        {
            get { return myRating; }
            set { myRating = value; RaisePropertyChanged(()=>MyRating); }
        }
        public string SchoolCity
        {
            get { return schoolCity; }
            set { schoolCity = value; RaisePropertyChanged(()=>SchoolCity); }
        }
        public string SchoolId
        {
            get { return schoolId; }
            set { schoolId = value; RaisePropertyChanged(()=>SchoolId); }

        }
        public double UserratingView
        {
            get { return _userratingview; }
            set { _userratingview = value; RaisePropertyChanged(()=>UserratingView); }
        }
        public double OverallRating
        {
            get { return _overallRating; }
            set { _overallRating = value; RaisePropertyChanged(() => OverallRating); }
        }
       // private string _userComment;
       // private string _userNameWhoCommented;
      //  private DateTime created;

        public string Usercomments
        {
            get { return _userComment; }
            set { _userComment = value; RaisePropertyChanged(() => Usercomments); }
        }


        public string UserNameWhoCommented
        {
            get { return _userNameWhoCommented; }
            set { _userNameWhoCommented = value; RaisePropertyChanged(() => UserNameWhoCommented); }
        }

        public DateTime Created
        {
            get { return created; }
            set { created = value; RaisePropertyChanged(()=>Created); }
        }


         public string SchoolName
         {
             get { return schoolName; }
             set { schoolName = value; RaisePropertyChanged(() => SchoolName); }
         }

         public List<SchoolManagement> ManagementDetails
         {
             get { return managementdetails; }
             set { managementdetails = value;
             this.RaisePropertyChanged(()=>ManagementDetails);
             }
         }
         public string SchoolAddress_schooldetails
         {
             get { return schoolAddress_schooldetails; }
             set { schoolAddress_schooldetails = value; this.RaisePropertyChanged(() => SchoolAddress_schooldetails); }
         }

         public string SchoolName_schooldetails
         {
             get { return schoolName_schooldetails; }
             set { schoolName_schooldetails = value; this.RaisePropertyChanged(() => SchoolName_schooldetails); }
         }
         public double Latitude_schooldetails
         {
             get { return latitude_schooldetails; }
             set { latitude_schooldetails = value; RaisePropertyChanged(() => Latitude_schooldetails); }
         }
         public double Longitude_schooldetails
         {
             get { return longitude_schooldetails; }
             set { longitude_schooldetails = value; RaisePropertyChanged(() => Longitude_schooldetails); }
         }


         private List<Alumni> alumniDetails=new List<Alumni>();
         public List<Alumni> AlumniDetails
         {
             get { return alumniDetails; }
             set
             {
                 alumniDetails = value;
                 this.RaisePropertyChanged(() => AlumniDetails);
             }
         }

         public double RatingNumber
         {
             get { return ratingNumber; }
             set { ratingNumber = value; RaisePropertyChanged(() => RatingNumber); }
         }

         private List<School> schoolDetails;
         public List<School> SchoolDetails
         {
             get { return schoolDetails; }
             set { schoolDetails = value; RaisePropertyChanged(() => SchoolDetails); }
         }

        private School details;
        public School Details
        {
            get { return details; }
            set
            { 
                details = value;
            this.RaisePropertyChanged(() => Details);
            }
        }
        private ObservableCollection<Location> locations;
        public ObservableCollection<Location> Locations
        {
            get { return locations; }
            set
            {
                locations = value;
                this.RaisePropertyChanged(() => Locations);
            }
        }



        private MvxCommand _UserRatingNew;
        public ICommand UserRatingNew
        {
            get
            {
                return this._UserRatingNew ?? (this._UserRatingNew = new MvxCommand(() => UserRatingNewMethod()));

            }
        }

        private object UserRatingNewMethod()
        {
            throw new NotImplementedException();
        }

        
    }
}

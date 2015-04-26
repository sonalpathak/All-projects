using System.Collections.Generic;

// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the SearchresultsViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Neudesic.Schoolistics.Core.ViewModels
{
    using System.Windows.Input;

    using Cirrious.MvvmCross.ViewModels;
using Neudesic.Schoolistics.Core.Entities;
using Neudesic.Schoolistics.Core.Services;
    using System;
    using Newtonsoft.Json.Linq;
    using Newtonsoft.Json;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using System.Collections.ObjectModel;
using Cirrious.MvvmCross.Plugins.Messenger;
    using Cirrious.CrossCore;
    using System.Linq;

    /// <summary>
    /// Define the SearchresultsViewModel type.
    /// </summary>
    public class SearchresultsViewModel : BaseViewModel
    {
        private ISearchResultsService _searchResultsService;
        private IBookmarkedSchoolService _bookmarkedSchoolService;
        private string schoolName,schoolAddress_schooldetails, classSelectedItem, accreditionSelectedItem, distanceSelectedItem, citySelectedItem, saveSearchText,searchResultsCount, searchHeading;
        private List<string> schoolClasses = new List<string>();
        private List<string> schoolAccredition = new List<string>();
        private List<string> schoolDistance = new List<string>();
        private List<string> schoolCity = new List<string>();
        private List<string> schoolRating = new List<string>();
        private double latitude, longitude;
        private bool progressBarVisibility,stackPanelVisibility, mapVisibility, gridVisibility;
        private bool? midDayMeals = null, playGround = null, digitalClassroom = null, dayBoarding = null, transportationFacility = null;
        private bool admissionProgress;
        private ObservableCollection<School> schoolDetails;
        private MvxCommand myCommand, refineCommand, searchCommand, mapClickCommand, gridClickCommand,saveClickCommand,goBackClickCommand;
        private int rating, minimum, maximum;
        private readonly IMvxMessenger _messenger;
        private static SearchDetails detailsOfSearching;
        MvxSubscriptionToken token;

        public void Init(SearchDetails searchDetails)
        {
            try
            {
                if (detailsOfSearching == null)
                {
                    detailsOfSearching = searchDetails;
                }

                SchoolName = detailsOfSearching.Keyword;
                ClassSelectedItem = detailsOfSearching.Class == null ? "Any Class" : detailsOfSearching.Class == "0" ? "KG" : detailsOfSearching.Class;
                CitySelectedItem = detailsOfSearching.City == null ? "Any City" : detailsOfSearching.City;
                AccreditionSelectedItem = detailsOfSearching.Accreditation == null ? "Any Accredition" : detailsOfSearching.Accreditation;
                RatingSearch = Convert.ToInt32(detailsOfSearching.Rating);
                DistanceSelectedItem = detailsOfSearching.MaximumDistance == 0 ? "Any Distance" : detailsOfSearching.MaximumDistance > 20 ? "Above 20 Kms" : "Below " + detailsOfSearching.MaximumDistance + " Kms";
                detailsOfSearching.MinimumBudget = Convert.ToInt32(detailsOfSearching.MinimumBudget);
                detailsOfSearching.MaximumBudget = Convert.ToInt32(detailsOfSearching.MaximumBudget);
                Minimum = (detailsOfSearching.MinimumBudget);
                Maximum = (detailsOfSearching.MaximumBudget);
                AdmissionProgress = detailsOfSearching.AdmissionsInProgress;

                detailsOfSearching.Class = detailsOfSearching.Class == "KG" ? "0" : detailsOfSearching.Class;
                detailsOfSearching.City = citySelectedItem == "Any City" ? null : citySelectedItem;
                detailsOfSearching.Accreditation = accreditionSelectedItem == "Any Accredition" ? null : accreditionSelectedItem;
                detailsOfSearching.AdmissionsInProgress = admissionProgress;
                detailsOfSearching.Latitude = Utils.Utils.Latitude;
                detailsOfSearching.Longitude = Utils.Utils.Longitude;
                _searchResultsService.PostSearchResults(detailsOfSearching, SearchSuccessOnHubScreen, SearchFailureOnHubScreen);
                SearchHeading = detailsOfSearching.Keyword == "" || detailsOfSearching.Keyword == null ? "Search Results" : "Search Results for '" + SchoolName + "'";
                _messenger.Publish(new NavigationMessage<object>(this) { Message = "GetMaximumAndMinimumValues", Data = detailsOfSearching.MinimumBudget + " " + detailsOfSearching.MaximumBudget });
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchresultsViewModel.cs : Init : " + ex.ToString());
            }
        }


        public string SchoolAddress_schooldetails
        {
            get { return schoolAddress_schooldetails; }
            set { schoolAddress_schooldetails = value; RaisePropertyChanged(() => SchoolAddress_schooldetails); }
        }

        public SearchresultsViewModel(ISearchResultsService searchResultsService,IBookmarkedSchoolService bookmarkedSchoolService, IMvxMessenger messenger)
        {
            try
            {
                _searchResultsService = searchResultsService;
                _bookmarkedSchoolService = bookmarkedSchoolService;
                _messenger = messenger;
                StackPanelVisibility = false;
                ProgressBarVisibility = true;
                MapVisibility = false;
                GridVisibility = true;
                schoolClasses = _searchResultsService.AssignClassesData();
                schoolAccredition = _searchResultsService.AssignAccreditionData();
                schoolDistance = _searchResultsService.AssignDistanceData();
                schoolCity = _searchResultsService.AssignCityData();
                schoolRating = _searchResultsService.AssignRatingData();
                IMvxMessenger subscribMessenger = Mvx.GetSingleton<IMvxMessenger>();
                token = messenger.SubscribeOnMainThread<NavigationMessage<object>>(MessageSubscribe);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchresultsViewModel.cs : SearchresultsViewModel : " + ex.ToString());
            }
        }

        public void MessageSubscribe(NavigationMessage<object> message)
        {
            try
            {
                if (message.Message == "GetMinimumValue")
                {
                    Minimum = Convert.ToInt32(message.Data);
                }
                else if (message.Message == "GetMaximumValue")
                {
                    Maximum = Convert.ToInt32(message.Data);
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchresultsViewModel.cs : MessageSubscribe : " + ex.ToString());
            }
        }

        public int Minimum
        {
            get { return minimum; }
            set { minimum = value; RaisePropertyChanged(() => Minimum); }
        }

        public int Maximum
        {
            get { return maximum; }
            set { maximum = value; RaisePropertyChanged(() => Maximum); }
        }

        public int RatingSearch
        {
            get { return rating; }
            set { rating = value; RaisePropertyChanged(() => RatingSearch); }
        }

        public string SearchHeading
        {
            get { return searchHeading; }
            set { searchHeading = value; RaisePropertyChanged(() => SearchHeading); }
        }

        public string SaveSearchText
        {
            set { saveSearchText = value; RaisePropertyChanged(() => SaveSearchText); }
            get { return saveSearchText; }
        }

        public string SchoolName
        {
            set { schoolName = value; RaisePropertyChanged(() => SchoolName); }
            get { return schoolName; }
        }

        public string SearchResultsCount
        {
            set { searchResultsCount = value; RaisePropertyChanged(() => SearchResultsCount); }
            get { return searchResultsCount; }
        }

        public string ClassSelectedItem
        {
            set { classSelectedItem = value; RaisePropertyChanged(() => ClassSelectedItem); }
            get { return classSelectedItem; }
        }

        public string DistanceSelectedItem
        {
            set { distanceSelectedItem = value; RaisePropertyChanged(() => DistanceSelectedItem); }
            get { return distanceSelectedItem; }
        }

        public string AccreditionSelectedItem
        {
            set { accreditionSelectedItem = value; RaisePropertyChanged(() => AccreditionSelectedItem); }
            get { return accreditionSelectedItem; }
        }

        public string CitySelectedItem
        {
            set { citySelectedItem = value; RaisePropertyChanged(() => CitySelectedItem); }
            get { return citySelectedItem; }
        }

        public List<string> SchoolClasses
        {
            get{ return schoolClasses; }
            set { schoolClasses = value; RaisePropertyChanged(() => SchoolClasses); }
        }

        public List<string> SchoolAccredition
        {
            get { return schoolAccredition; }
            set { schoolAccredition = value; RaisePropertyChanged(() => SchoolAccredition); }
        }

        public List<string> SchoolDistance
        {
            get { return schoolDistance; }
            set { schoolDistance = value; RaisePropertyChanged(() => SchoolDistance); }
        }

        public List<string> SchoolCity
        {
            get { return schoolCity; }
            set { schoolCity = value; RaisePropertyChanged(() => SchoolCity); }
        }

        public List<string> SchoolRating
        {
            get { return schoolRating; }
            set { schoolRating = value; RaisePropertyChanged(() => SchoolRating); }
        }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        public double Latitude
        {
            get { return latitude; }
            set { latitude = value; RaisePropertyChanged(() => Latitude); }
        }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        public double Longitude
        {
            get { return longitude; }
            set { longitude = value; RaisePropertyChanged(() => Longitude); }
        }


        public bool? TransportationFacility
        {
            get { return transportationFacility; }
            set { transportationFacility = value; RaisePropertyChanged(() => TransportationFacility); }
        }

        public bool AdmissionProgress
        {
            get { return admissionProgress; }
            set { admissionProgress = value; RaisePropertyChanged(() => AdmissionProgress); }
        }

        public bool? DayBoarding
        {
            get { return dayBoarding; }
            set { dayBoarding = value; RaisePropertyChanged(() => DayBoarding); }
        }

        public bool? DigitalClassroom
        {
            get { return digitalClassroom; }
            set { digitalClassroom = value; RaisePropertyChanged(() => DigitalClassroom); }
        }

        public bool? MidDayMeals
        {
            get { return midDayMeals; }
            set { midDayMeals = value; RaisePropertyChanged(() => MidDayMeals); }
        }

        public bool? PlayGround
        {
            get { return playGround; }
            set { playGround = value; RaisePropertyChanged(() => PlayGround); }
        }

        public bool MapVisibility
        {
            get { return mapVisibility; }
            set { mapVisibility = value; RaisePropertyChanged(() => MapVisibility); }
        }

        public bool ProgressBarVisibility
        {
            get { return progressBarVisibility; }
            set { progressBarVisibility = value; RaisePropertyChanged(() => ProgressBarVisibility); }
        }

        public bool StackPanelVisibility
        {
            get { return stackPanelVisibility; }
            set { stackPanelVisibility = value; RaisePropertyChanged(() => StackPanelVisibility); }
        }

        public bool GridVisibility
        {
            get { return gridVisibility; }
            set { gridVisibility = value; RaisePropertyChanged(() => GridVisibility); }
        }

        /// <summary>
        /// Hides the map and show grid.
        /// </summary>
        public void HideMapAndShowGrid()
        {
            MapVisibility = false;
            GridVisibility = true;
        }

        /// <summary>
        /// Shows the map and hide grid.
        /// </summary>
        public void ShowMapAndHideGrid()
        {
            MapVisibility = true;
            GridVisibility = false;
        }

        /// <summary>
        /// Backing field for my property.
        /// </summary>
        private string myProperty = "Mvx Ninja Coder!";


        public ObservableCollection<School> SchoolDetails
        {
            get { return schoolDetails; }
            set
            {
                schoolDetails = value; 
                RaisePropertyChanged(() => SchoolDetails);
            }
        }


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

        public async Task<bool> GetLatitudeAndLongitude(string schoolId)
        {
            try
            {
                string address = string.Empty;
                foreach (var school in schoolDetails)
                {
                    if (school.SchoolId == schoolId)
                    {
                        address = school.Address;
                        break;
                    }
                }
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("http://maps.googleapis.com/maps/api/geocode/json?address=" + address + "&sensor=false");
                var jsonContent = await response.Content.ReadAsStringAsync();
                var sampleGroups1 = (JObject)JsonConvert.DeserializeObject(jsonContent.ToString());
                Type type = typeof(RootObject);
                RootObject rootObject = (RootObject)JsonConvert.DeserializeObject(sampleGroups1.ToString(), type);
                if (rootObject != null && rootObject.results.Count > 0)
                {
                    Latitude = rootObject.results[0].geometry.location.lat;
                    Longitude = rootObject.results[0].geometry.location.lng;
                }

                return true;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchresultsViewModel.cs : GetLatitudeAndLongitude : " + ex.ToString());
                return false;
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
                return this.myCommand ?? (this.myCommand = new MvxCommand(() => this.ShowViewModel<SearchresultsViewModel>()));
            }
        }

        public ICommand RefineCommand
        {
            get
            {
                refineCommand = refineCommand ?? new MvxCommand(FilterSearchResultsBasedOnRefine);
                return refineCommand;
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                return this.searchCommand ?? (this.searchCommand = new MvxCommand(() => FilterSearchResultsBasedOnSearch()));
            }
        }

        public ICommand MapClickCommand
        {
            get
            {
                mapClickCommand = mapClickCommand ?? new MvxCommand(ShowMapAndHideGrid);
                return mapClickCommand;
            }
        }

        public ICommand GridClickCommand
        {
            get
            {
                gridClickCommand = gridClickCommand ?? new MvxCommand(HideMapAndShowGrid);
                return gridClickCommand;
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                saveClickCommand = saveClickCommand ?? new MvxCommand(SaveSearchResults);
                return saveClickCommand;
            }
        }

        public ICommand GoBackClickCommand
        {
            get
            {
                goBackClickCommand = goBackClickCommand ?? new MvxCommand(GoBackClick);
                return goBackClickCommand;
            }
        }

        public void NavigateToSchoolDetails(string schoolId)
        {
            try
            {
                string city = string.Empty;
                foreach (var school in schoolDetails)
                {
                    if (school.SchoolId == schoolId)
                    {
                        city = school.City;
                        break;
                    }
                }
                ShowViewModel<SchoolDetailsViewModel>(new { id = schoolId, Scity = city });
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchresultsViewModel.cs : NavigateToSchoolDetails : " + ex.ToString());
            }
        }

        public void GoBackClick()
        {
            detailsOfSearching = null;
            ShowViewModel<HubpageViewModel>();
        }

        public void SaveSearchResults()
        {
            try
            {
                if (Utils.Utils.Username != null)
                {
                    if (saveSearchText != string.Empty)
                    {
                        var searchDetails = new SearchDetails();
                        searchDetails.Username = Utils.Utils.Username;
                        searchDetails.SearchName = saveSearchText;
                        searchDetails.Keyword = schoolName == null ? "" : schoolName;
                        searchDetails.Class = classSelectedItem;
                        searchDetails.Accreditation = accreditionSelectedItem;
                        searchDetails.Rating = rating;
                        searchDetails.MaximumDistance = 1;
                        searchDetails.MinimumBudget = minimum;
                        searchDetails.MaximumBudget = maximum;
                        searchDetails.City = citySelectedItem;
                        searchDetails.MidDayMeals = midDayMeals;
                        searchDetails.Playground = playGround;
                        searchDetails.DigitalClassroom = digitalClassroom;
                        searchDetails.DayBoarding = dayBoarding;
                        searchDetails.Transportation = transportationFacility;
                        searchDetails.AdmissionsInProgress = admissionProgress;

                        SaveSearchText = string.Empty;
                        _searchResultsService.SaveSearchResults(searchDetails, SaveSuccess, SaveFailure);
                    }
                    else
                    {
                        _messenger.Publish(new NavigationMessage<Object>(this) { Message = "CheckingSaveSearchText" });
                    }
                }
                else
                {
                    SaveSearchText = string.Empty;
                    _messenger.Publish(new NavigationMessage<Object>(this) { Message = "CheckingLoginBeforeSave" });
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchresultsViewModel.cs : SaveSearchResults : " + ex.ToString());
            }
        }

        public void FilterSearchResultsBasedOnRefine()
        {
            try
            {
                SchoolDetails = new ObservableCollection<School>(SchoolDetails.Where(x => x.MidDayMeals == midDayMeals
                                                     && x.Playground == playGround
                                                     && x.DigitalClassroom == digitalClassroom
                                                     && x.DayBoarding == dayBoarding
                                                     && x.Transportation == transportationFacility));
                SearchResultsCount = SchoolDetails.Count == 0 ? "No Results" : SchoolDetails.Count + " results";
                MapVisibility = false;
                GridVisibility = true;
                _messenger.Publish(new NavigationMessage<object>(this) { Message = "CloseRefinePopUp" });
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchresultsViewModel.cs : FilterSearchResultsBasedOnRefine : " + ex.ToString());
            }
        }

        public void FilterSearchResultsBasedOnSearch()
        {
            try
            {
                StackPanelVisibility = false;
                ProgressBarVisibility = true;
                SearchDetails searchDetails = new SearchDetails();

                searchDetails.Keyword = schoolName == "" ? null : schoolName;
                searchDetails.Class = classSelectedItem == "KG" ? "0" : classSelectedItem;
                searchDetails.City = citySelectedItem == "Any City" ? null : citySelectedItem;
                searchDetails.Accreditation = accreditionSelectedItem == "Any Accredition" ? null : accreditionSelectedItem;
                searchDetails.Rating = RatingSearch;
                searchDetails.AdmissionsInProgress = admissionProgress;
                searchDetails.MinimumBudget = Minimum;
                searchDetails.MaximumBudget = Maximum;
                if (DistanceSelectedItem == "Any Distance")
                    searchDetails.MaximumDistance = 0;
                else if (DistanceSelectedItem == "Above 20 Kms")
                    searchDetails.MaximumDistance = 30;
                else
                {
                    int index = DistanceSelectedItem.IndexOf(" ");
                    int lastIndex = DistanceSelectedItem.LastIndexOf(" ");
                    string distanceInNumber = DistanceSelectedItem.Substring(index, (lastIndex - index));
                    searchDetails.MaximumDistance = Convert.ToInt32(distanceInNumber);
                }
                searchDetails.Latitude = 17.442164;
                searchDetails.Longitude = 78.376843;
                detailsOfSearching = searchDetails;

                _searchResultsService.PostSearchResults(searchDetails, SearchSuccess, SearchFailure);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchresultsViewModel.cs : FilterSearchResultsBasedOnSearch : " + ex.ToString());
            }
        }

        public void SearchSuccess(ResponseMessage<Object> response)
        {
            try
            {
                if (response.SuccessCode == 1)
                {
                    var schools = JsonConvert.DeserializeObject<List<School>>(response.Data.ToString());

                    SchoolDetails = new ObservableCollection<School>(schools);
                    SearchResultsCount = SchoolDetails.Count == 0 ? "No Results" : SchoolDetails.Count + " results";
                    SearchHeading = detailsOfSearching.Keyword == "" || detailsOfSearching.Keyword == null ? "Search Results" : "Search Results for '" + SchoolName + "'";
                    MapVisibility = false;
                    GridVisibility = true;
                    StackPanelVisibility = true;
                    ProgressBarVisibility = false;
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchresultsViewModel.cs : SearchSuccess : " + ex.ToString());
            }
        }

        public void SaveSuccess(ResponseMessage<Object> response)
        {
            try
            {
                if (response.SuccessCode == 1)
                {
                    _messenger.Publish(new NavigationMessage<Object>(this) { Message = "SaveSuccess", Data = response.Message.ToString() });
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchresultsViewModel.cs : SaveSuccess : " + ex.ToString());
            }
        }

        public void SaveFailure(Exception e)
        {
            try
            {
                var message = e.Message;
                StackPanelVisibility = true;
                ProgressBarVisibility = false;
                _messenger.Publish(new NavigationMessage<Object>(this) { Message = "SaveSuccess", Data = "Failed in saving search" });
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchresultsViewModel.cs : SaveFailure : " + ex.ToString());
            }
        }

        public void SearchFailure(Exception e)
        {
            try
            {
                var message = e.Message;
                StackPanelVisibility = true;
                ProgressBarVisibility = false;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchresultsViewModel.cs : SearchFailure : " + ex.ToString());
            }
        }

        public void SearchSuccessOnHubScreen(ResponseMessage<Object> response)
        {
            try
            {
                if (response.SuccessCode == 1)
                {
                    var schools = JsonConvert.DeserializeObject<List<School>>(response.Data.ToString());

                    SchoolDetails = new ObservableCollection<School>(schools);
                    SearchResultsCount = SchoolDetails.Count == 0 ? "No Results" : SchoolDetails.Count + " results";
                    MapVisibility = false;
                    GridVisibility = true;
                    StackPanelVisibility = true;
                    ProgressBarVisibility = false;
                    ChangingSearchDetails();
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchresultsViewModel.cs : SearchSuccessOnHubScreen : " + ex.ToString());
            }
        }

        public void SearchFailureOnHubScreen(Exception e)
        {
            try
            {
                var message = e.Message;
                StackPanelVisibility = true;
                ProgressBarVisibility = false;
                ChangingSearchDetails();
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchresultsViewModel.cs : SearchFailureOnHubScreen : " + ex.ToString());
            }
        }
        
        private School _selectedSchooldata;
        public School SelectedSchoolData
        {
            get { return _selectedSchooldata; }

            set { _selectedSchooldata = value; RaisePropertyChanged(() => SelectedSchoolData); }
        }

        private MvxCommand _bookmarkSchool;
        public ICommand BookmarkSchool
        {
            get { return this._bookmarkSchool ?? (this._bookmarkSchool = new MvxCommand(() => BookmarkedSchool())); }
        }

        public void BookmarkedSchool()
        {
            try
            {
                if (Utils.Utils.Username != null)
                {
                    BookmarkedSchools bookmarkedschooldetails = new BookmarkedSchools();
                    bookmarkedschooldetails.Username = Utils.Utils.Username;
                    bookmarkedschooldetails.SchoolId = SelectedSchoolData.SchoolId;
                    bookmarkedschooldetails.SchoolName = SelectedSchoolData.SchoolName;
                    bookmarkedschooldetails.SchoolLogo = SelectedSchoolData.SchoolLogo;
                    bookmarkedschooldetails.City = SelectedSchoolData.City;

                    _bookmarkedSchoolService.PostBookmarkedSchool(bookmarkedschooldetails, BookmarkedSchoolSuccess, BookmarkedSchoolFailure);
                }
                else
                {
                    _messenger.Publish(new NavigationMessage<object>(this) { Message = "CheckingUserLogInBeforeBookmarking" });
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchresultsViewModel.cs : BookmarkedSchool : " + ex.ToString());
            }
        }

        public void BookmarkedSchoolSuccess(ResponseMessage<Object> response)
        {
            try
            {
                if (response.SuccessCode == 1)
                {
                    _messenger.Publish(new NavigationMessage<object>(this) { Message = "BookmarkedSchoolSuccess" });
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchresultsViewModel.cs : BookmarkedSchoolSuccess : " + ex.ToString());
            }
        }

        public void BookmarkedSchoolFailure(Exception e)
        {
            try
            {
                var message = e.Message;
                _messenger.Publish(new NavigationMessage<object>(this) { Message = "BookmarkedSchoolFailure" });
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchresultsViewModel.cs : BookmarkedSchoolFailure : " + ex.ToString());
            }
        }

        #region private methods

        private void ChangingSearchDetails()
        {
            try
            {
                if (detailsOfSearching != null)
                {
                    detailsOfSearching.Class = detailsOfSearching.Class == "0" ? "KG" : detailsOfSearching.Class;
                    detailsOfSearching.City = citySelectedItem == null ? "Any City" : citySelectedItem;
                    detailsOfSearching.Accreditation = accreditionSelectedItem == null ? "Any Accredition" : accreditionSelectedItem;
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchresultsViewModel.cs : ChangingSearchDetails : " + ex.ToString());
            }
        }
        


        #endregion

        
    }
}

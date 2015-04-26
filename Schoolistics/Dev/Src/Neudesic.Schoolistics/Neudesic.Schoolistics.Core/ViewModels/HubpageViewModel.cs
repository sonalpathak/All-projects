// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the HubpageViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Neudesic.Schoolistics.Core.ViewModels
{
    using System.Windows.Input;
    using System.Collections.Generic;
    using Cirrious.MvvmCross.ViewModels;
    using Neudesic.Schoolistics.Core.Services;
    using Neudesic.Schoolistics.Core.Entities;
    using System;
    using System.Collections.ObjectModel;
    using Cirrious.MvvmCross.Plugins.Messenger;
    using System.Linq;
using Cirrious.CrossCore;
    using Newtonsoft.Json;
    
    /// <summary>
    /// Define the HubpageViewModel type.
    /// </summary>
    public class HubpageViewModel : BaseViewModel
    {
        private IHubPageService _hubPageService;
        private IBookmarkedSchoolService _bookmarkedSchoolService;
        
       // private List<BookmarkedSchools> featuredSchools;
        private MvxCommand myCommand, searchCommand;
        private readonly IMvxMessenger _Messenger;

        private List<string> schoolClasses = new List<string>();
        private List<string> schoolAccredition = new List<string>();
        private List<string> schoolDistance = new List<string>();
        private List<string> schoolCity = new List<string>();
        private List<string> schoolRating = new List<string>();
        private int rating;
        private string schoolName, classSelectedItem, accreditionSelectedItem, distanceSelectedItem, citySelectedItem;
        private bool admissionProgress, progressbarVisibility, gridviewVisibility, progressbarVisibilityForSurvey, gridviewVisibilityForSurvey, progressbarVisibilityForBookmarked, gridviewVisibilityForBookmarked, progressbarVisibilityForComparision, gridviewVisibilityForComparision;
        private int minimum;
        private int maximum;
        private readonly IMvxMessenger _messenger;
        MvxSubscriptionToken token;

        public HubpageViewModel(IHubPageService hubPageService, IMvxMessenger messenger, IBookmarkedSchoolService bookmarkedSchoolService, ISurveyPageService surveyPageService, INewsArticlesService newsArticleService, ISchoolComparisonService schoolComparisonService)
        {
            try
            {
                _hubPageService = hubPageService;
                _surveyPageService = surveyPageService;
                _bookmarkedSchoolService = bookmarkedSchoolService;
                _newsArticleService = newsArticleService;
                _bookmarkedSchoolService.GetBookmarkedSchools(BookmarkedSchoolsSuccess, BookmarkedSchoolsFailure);
                GetPopularSurveys();
                _Messenger = messenger;
                schoolClasses = _hubPageService.AssignClassesData();
                schoolAccredition = _hubPageService.AssignAccreditionData();
                schoolDistance = _hubPageService.AssignDistanceData();
                schoolCity = _hubPageService.AssignCityData();
                schoolRating = _hubPageService.AssignRatingData();
                RatingSearch = 0;
                classSelectedItem = "Any Class";
                accreditionSelectedItem = "Any Accredition";
                distanceSelectedItem = "Any Distance";
                citySelectedItem = "Any City";
                _messenger = messenger;
                IMvxMessenger subscribMessenger = Mvx.GetSingleton<IMvxMessenger>();
                token = messenger.Subscribe<NavigationMessage<object>>(MessageSubscribe);
                FeaturedSchoolDetails();
                GetLatestNewsArticles();
                GridViewVisibility = gridviewVisibilityForSurvey = GridViewVisibilityForSurvey = GridViewVisibilityForBookmarked = GridViewVisibilityForComparision = false;
                _schoolComparisonService = schoolComparisonService;
                GetSchoolDetails();
                ProgressBarVisibility = progressbarVisibilityForSurvey = ProgressBarVisibilityForSurvey = ProgressBarVisibilityForBookmarked = ProgressBarVisibilityForComparision = true;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in HubpageViewModel.cs : Constructor: " + ex.ToString());
            }
        }

        /// <summary>
        /// Backing field for my property.
        /// </summary>
        private string myProperty = "Mvx Ninja Coder!";


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
                Mvx.Error("Error in HubpageViewModel.cs : MessageSubscribe: " + ex.ToString());
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
        public bool ProgressBarVisibility
        {
            get { return progressbarVisibility; }
            set { progressbarVisibility = value; RaisePropertyChanged(() => ProgressBarVisibility); }
        }

        public bool GridViewVisibility
        {
            get { return gridviewVisibility; }
            set { gridviewVisibility = value; RaisePropertyChanged(() => GridViewVisibility); }
        }

        public bool ProgressBarVisibilityForSurvey
        {
            get { return progressbarVisibilityForSurvey; }
            set { progressbarVisibilityForSurvey = value; RaisePropertyChanged(() => ProgressBarVisibilityForSurvey); }
        }

        public bool GridViewVisibilityForSurvey
        {
            get { return gridviewVisibilityForSurvey; }
            set { gridviewVisibilityForSurvey = value; RaisePropertyChanged(() => GridViewVisibilityForSurvey); }
        }

        public bool ProgressBarVisibilityForBookmarked
        {
            get { return progressbarVisibilityForBookmarked; }
            set { progressbarVisibilityForBookmarked = value; RaisePropertyChanged(() => ProgressBarVisibilityForBookmarked); }
        }

        public bool GridViewVisibilityForBookmarked
        {
            get { return gridviewVisibilityForBookmarked; }
            set { gridviewVisibilityForBookmarked = value; RaisePropertyChanged(() => GridViewVisibilityForBookmarked); }
        }

        public bool ProgressBarVisibilityForComparision
        {
            get { return progressbarVisibilityForComparision; }
            set { progressbarVisibilityForComparision = value; RaisePropertyChanged(() => ProgressBarVisibilityForComparision); }
        }

        public bool GridViewVisibilityForComparision
        {
            get { return gridviewVisibilityForComparision; }
            set { gridviewVisibilityForComparision = value; RaisePropertyChanged(() => GridViewVisibilityForComparision); }
        }

        public bool AdmissionProgress
        {
            get { return admissionProgress; }
            set { admissionProgress = value; RaisePropertyChanged(() => AdmissionProgress); }
        }

        public int RatingSearch
        {
            get { return rating; }
            set { rating = value; RaisePropertyChanged(() => RatingSearch); }
        }

        public string SchoolName
        {
            set { schoolName = value; RaisePropertyChanged(() => SchoolName); }
            get { return schoolName; }
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
            get { return schoolClasses; }
            set { schoolClasses = value; RaisePropertyChanged(() => SchoolClasses); }
        }

        public List<string> SchoolRating
        {
            get { return schoolRating; }
            set { schoolRating = value; RaisePropertyChanged(() => SchoolRating); }
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


        private static ObservableCollection<School> schoolDetails;

        public ObservableCollection<School> SchoolDetails
        {
            get
            { return schoolDetails; }
            set
            {
                schoolDetails = value;
                RaisePropertyChanged(() => SchoolDetails);
            }
        }


        private static ObservableCollection<ComparisonDetail> popularSchoolComparisons;

        public ObservableCollection<ComparisonDetail> PopularSchoolComparisons
        {
            get
            { return popularSchoolComparisons; }
            set
            {
                popularSchoolComparisons = value;
                RaisePropertyChanged(() => PopularSchoolComparisons);
            }
        }
        private static ObservableCollection<School> schoolComparisonsListDetails;
        public ObservableCollection<School> SchoolComparisonsListDetails
        {
            get
            { return schoolComparisonsListDetails; }
            set
            {
                schoolComparisonsListDetails = value;
                RaisePropertyChanged(() => SchoolComparisonsListDetails);
            }
        }

        private ISchoolComparisonService _schoolComparisonService;
        private void GetSchoolDetails()
        {
            try
            {
                List<School> schools = new List<School>();
                _schoolComparisonService.GetSchoolDetails(schools, AllSchoolDetailsSuccess, AllSchoolDetailsFailure);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in HubpageViewModel.cs : GetSchoolDetails: " + ex.ToString());
            }
        }
        private void AllSchoolDetailsSuccess(ResponseMessage<object> response)
        {
            try
            {
                if (response.SuccessCode == 1)
                {
                    List<School> data = JsonConvert.DeserializeObject<List<School>>(response.Data.ToString());
                    SchoolDetails = new ObservableCollection<School>(data);
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in HubpageViewModel.cs : AllSchoolDetailsSuccess: " + ex.ToString());
            }
        }

        private void AllSchoolDetailsFailure(System.Exception e)
        {
            try
            {
                var message = e.Message;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in HubpageViewModel.cs : AllSchoolDetailsFailure: " + ex.ToString());
            }
        }



        private void GetPopularSchoolComparisons()
        {
            try
            {
                List<ComparisonDetail> popularComparisons = new List<ComparisonDetail>();
                _schoolComparisonService.GetPopularSchoolComparisons(popularComparisons, PopularSchoolComparisonsSuccess, PopularSchoolComparisonsFailure);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in HubpageViewModel.cs : GetPopularSchoolComparisons: " + ex.ToString());
            }
        }

        private void PopularSchoolComparisonsSuccess(ResponseMessage<object> response)
        {
            try
            {
                if (response.SuccessCode == 1)
                {
                    List<ComparisonDetail> data = JsonConvert.DeserializeObject<List<ComparisonDetail>>(response.Data.ToString());
                    PopularSchoolComparisons = new ObservableCollection<ComparisonDetail>(data.Take(2));
                }
                ProgressBarVisibilityForComparision = false;
                GridViewVisibilityForComparision = true;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in HubpageViewModel.cs : PopularSchoolComparisonsSuccess: " + ex.ToString());
            }
        }

        private void PopularSchoolComparisonsFailure(System.Exception e)
        {
            try
            {
                var message = e.Message;
                ProgressBarVisibilityForComparision = false;
                GridViewVisibilityForComparision = true;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in HubpageViewModel.cs : PopularSchoolComparisonsFailure: " + ex.ToString());
            }
        }

        private MvxCommand<ComparisonDetail> showDetailComparisons;

        public ICommand ShowDetailComparisons
        {
            get
            {
                showDetailComparisons = showDetailComparisons ?? new MvxCommand<ComparisonDetail>(item =>GoToSchoolComparisonDetails(item.Schools));
                return showDetailComparisons;
            }
        }

        private void GoToSchoolComparisonDetails(List<SchoolsComparison> list)
        {
            try
            {
                _schoolComparisonService.GetAllComparedSchools(list, SchoolDetailsSuccess, SchoolDetailsFailure);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in HubpageViewModel.cs : PopularSchoolComparisonsFailure: " + ex.ToString());
            }
        }

        private void SchoolDetailsFailure(Exception e)
        {
            try
            {
                var message = e.Message;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in HubpageViewModel.cs : SchoolDetailsFailure: " + ex.ToString());
            }
        }

        private void SchoolDetailsSuccess(ResponseMessage<object> response)
        {
            try
            {
                if (response.SuccessCode == 1)
                {
                    List<School> school = JsonConvert.DeserializeObject<List<School>>(response.Data.ToString());
                    ShowViewModel<SchoolComparisonDetailsViewModel>(new SchoolComparisonDetailsViewModel(new SchoolComparisonService(new RestService()), null) { SchoolComparisonsDetails = new ObservableCollection<School>(school) });
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in HubpageViewModel.cs : SchoolDetailsFailure: " + ex.ToString());
            }
        }

        private MvxCommand _navigateToCompareSchools;

        public ICommand NavigateToCompareSchools
        {

            get
            {
                _navigateToCompareSchools = _navigateToCompareSchools ?? new MvxCommand(Navigate);
                return _navigateToCompareSchools;
            }
        }

        private MvxCommand<NewsArticle> _NewsArticleCommand;

        public ICommand NewsItemNavigate
        {

            get
            {
                _NewsArticleCommand = _NewsArticleCommand ?? new MvxCommand<NewsArticle>(item => ShowViewModel<NewsArticlesItemDetailViewModel>(item));
                return _NewsArticleCommand;
            }
        }
        private Cirrious.MvvmCross.ViewModels.MvxCommand _goNewsArticlesItemsPage;
        public System.Windows.Input.ICommand GoNewsArticlesItemsPage
        {
            get
            {
                _goNewsArticlesItemsPage = _goNewsArticlesItemsPage ?? new Cirrious.MvvmCross.ViewModels.MvxCommand(DoGoNewsArticlesItems);
                return _goNewsArticlesItemsPage;
            }
        }

        private void DoGoNewsArticlesItems()
        {
            try
            {
                ShowViewModel<NewsArticlesItemsViewModel>();
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in HubpageViewModel.cs : DoGoNewsArticlesItems: " + ex.ToString());
            }
        }



        public ICommand SchoolDetailsPageNavigate
        {
            get
            {

                return new MvxCommand<School>(item => ShowViewModel<SchoolDetailsViewModel>(new { id = item.SchoolId, Scity = item.City }));
            }
        }

        public ICommand BookmarkedToSchoolDetailsPage
        {
            get
            {

                return new MvxCommand<BookmarkedSchools>(item => ShowViewModel<SchoolDetailsViewModel>(new { id = item.SchoolId, Scity = item.City }));
            }
        }

        private void Navigate()
        {
            try
            {
                ShowViewModel<CompareschoolsViewModel>();
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in HubpageViewModel.cs : Navigate: " + ex.ToString());
            }
        }

        /// <summary>
        /// Gets My Command.
        /// <para>
        /// An example of a command and how to navigate to another view model
        /// Note the ViewModel inside of ShowViewModel needs to change!
        /// </para>
        /// </summary>
        /// 

        private MvxCommand _profileNavigate;
        public ICommand ProfileNavigate
        {
            get
            {
                return this._profileNavigate ?? (this._profileNavigate = new MvxCommand(() => this.ShowViewModel<MyprofileViewModel>()));
            }
        }

        private ISurveyPageService _surveyPageService;
        private INewsArticlesService _newsArticleService;
        
        private MvxCommand _surveyPage;

        public ICommand GoSurveyPage
        {
            get
            {
                return this._surveyPage ?? (this._surveyPage = new MvxCommand(() => this.ShowViewModel<SurveyViewModel>()));
            }
        }
        private ObservableCollection<Survey> _surveyQuestions;
        public ObservableCollection<Survey> SurveyQuestions
        {
            get { return _surveyQuestions; }
            set { _surveyQuestions = value; RaisePropertyChanged(() => SurveyQuestions); }
        }
        private void GetPopularSurveys()
        {
            try
            {
                List<Survey> surveyDetails = new List<Survey>();
                _surveyPageService.GetPopularSurveyDetails(surveyDetails, popularSurveyGetSuccess, popularSurveyGetFailure);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in HubpageViewModel.cs : GetPopularSurveys: " + ex.ToString());
            }
        }

        private void popularSurveyGetFailure(Exception e)
        {
            try
            {
                var message = e.Message;
                ProgressBarVisibilityForSurvey = false;
                GridViewVisibilityForSurvey = true;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in HubpageViewModel.cs : popularSurveyGetFailure: " + ex.ToString());
            }
        }

        private void popularSurveyGetSuccess(ResponseMessage<object> response)
        {
            try
            {
                if (response.SuccessCode == 1)
                {
                    Survey survey = new Survey();
                    var surveyDetails = JsonConvert.DeserializeObject<List<Survey>>(response.Data.ToString());
                    SurveyQuestions = new ObservableCollection<Survey>(surveyDetails);
                    ProgressBarVisibilityForSurvey = false;
                    GridViewVisibilityForSurvey = true;
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in HubpageViewModel.cs : popularSurveyGetSuccess: " + ex.ToString());
            }
        }


        public ICommand SearchCommand
        {
            get
            {
                searchCommand = searchCommand ?? new MvxCommand(GoToFindSchools);
                return searchCommand;
            }
        }

        private ObservableCollection<School> featuredSchools;
        public ObservableCollection<School> FeaturedSchools
        {
            get { return featuredSchools; }
            set { featuredSchools = value; RaisePropertyChanged(() => FeaturedSchools); }
        }


        public void FeaturedSchoolDetails()
        {
            try
            {
                School featuredSchoolDetails = new School();
                _hubPageService.GetFeaturedSchoolDetails(featuredSchoolDetails, FeaturedSchoolsSuccess, FeaturedSchoolsFailure);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in HubpageViewModel.cs : FeaturedSchoolDetails: " + ex.ToString());
            }

        }

        public void FeaturedSchoolsSuccess(ResponseMessage<Object> response)
        {
            try
            {
                if (response.SuccessCode == 1)
                {
                    var featuredSchoolDetails = JsonConvert.DeserializeObject<List<School>>(response.Data.ToString());
                    FeaturedSchools = new ObservableCollection<School>(featuredSchoolDetails.Take(6));
                    ProgressBarVisibility = false;
                    GridViewVisibility = true;
                    // _Messenger.Publish(new NavigationMessage<User>(this) { Message = "FeaturedSchoolsSuccess", Data = featuredSchoolDetails });
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in HubpageViewModel.cs : FeaturedSchoolsSuccess: " + ex.ToString());
            }
        }

        public void FeaturedSchoolsFailure(Exception e)
        {
            try
            {
                var message = e.Message;
                ProgressBarVisibility = false;
                GridViewVisibility = true;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in HubpageViewModel.cs : FeaturedSchoolsFailure: " + ex.ToString());
            }
        }
        private ObservableCollection<NewsArticle> popularNewsArticleitems;

        public ObservableCollection<NewsArticle> PopularNewsArticleitems
        {
            get { return popularNewsArticleitems; }
            set
            {
                popularNewsArticleitems = value;
                RaisePropertyChanged(() => PopularNewsArticleitems);
            }
        }


        public void GetLatestNewsArticles()
        {
            try
            {
                _newsArticleService.GetLatestNewsArticles(GetLatestNewsArticlesSuccess, GetLatestNewsArticlesFailure);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in HubpageViewModel.cs : GetLatestNewsArticles: " + ex.ToString());
            }
        }

        public void GetLatestNewsArticlesSuccess(ResponseMessage<Object> response)
        {
            try
            {
                if (response.SuccessCode == 1)
                {

                    var popularNewsArticlesList = JsonConvert.DeserializeObject<List<NewsArticle>>(response.Data.ToString());
                    PopularNewsArticleitems = new ObservableCollection<NewsArticle>(popularNewsArticlesList);
                    ProgressBarVisibility = false;
                    GridViewVisibility = true;
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in HubpageViewModel.cs : GetLatestNewsArticlesSuccess: " + ex.ToString());
            }
        }

        public void GetLatestNewsArticlesFailure(Exception e)
        {
            try
            {
                var message = e.Message;
                ProgressBarVisibility = false;
                GridViewVisibility = true;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in HubpageViewModel.cs : GetLatestNewsArticlesFailure: " + ex.ToString());
            }
        }
 

        private ObservableCollection<BookmarkedSchools> bookmarkedSchools;
        public ObservableCollection<BookmarkedSchools> BookmarkedSchools
        {
            get { return bookmarkedSchools; }
            set { bookmarkedSchools = value; RaisePropertyChanged(() => BookmarkedSchools); }
        }
        public void BookmarkedSchoolsSuccess(ResponseMessage<Object> response)
        {
            try
            {
                if (response.SuccessCode == 1)
                {
                    var bookmarkedschools = JsonConvert.DeserializeObject<List<BookmarkedSchools>>(response.Data.ToString());

                    _bookmarkedSchoolService.SetBookmarkedSchools(bookmarkedschools);

                    BookmarkedSchools = new ObservableCollection<BookmarkedSchools>(bookmarkedschools.Take(6));
                    ProgressBarVisibilityForBookmarked = false;
                    GridViewVisibilityForBookmarked = true;

                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in HubpageViewModel.cs : BookmarkedSchoolsSuccess: " + ex.ToString());
            }
        }

        public void BookmarkedSchoolsFailure(Exception e)
        {
            try
            {
                var message = e.Message;
                ProgressBarVisibilityForBookmarked = false;
                GridViewVisibilityForBookmarked = true;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in HubpageViewModel.cs : BookmarkedSchoolsFailure: " + ex.ToString());
            }
        }


        private Cirrious.MvvmCross.ViewModels.MvxCommand _goBookmarkedSchoolItemsPage;
        public System.Windows.Input.ICommand GoBookmarkedSchoolItemsPage
        {
            get
            {
                _goBookmarkedSchoolItemsPage = _goBookmarkedSchoolItemsPage ?? new Cirrious.MvvmCross.ViewModels.MvxCommand(DoGoBookmarkedSchoolsItems);
                return _goBookmarkedSchoolItemsPage;
            }
        }

        private void DoGoBookmarkedSchoolsItems()
        {
            try
            {
                ShowViewModel<BookmarkedSchoolItemsPageViewModel>();
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in HubpageViewModel.cs : DoGoBookmarkedSchoolsItems: " + ex.ToString());
            }

        } 

        
        #region private methods

        private void GoToFindSchools()
        {
            try
            {
                int distance = 0;
                if (DistanceSelectedItem == "Any Distance")
                    distance = 0;
                else if (DistanceSelectedItem == "Above 20 Kms")
                    distance = 30;
                else
                {
                    int index = DistanceSelectedItem.IndexOf(" ");
                    int lastIndex = DistanceSelectedItem.LastIndexOf(" ");
                    string distanceInNumber = DistanceSelectedItem.Substring(index, (lastIndex - index));
                    distance = Convert.ToInt32(distanceInNumber);
                }
                ShowViewModel<SearchresultsViewModel>(new SearchDetails()
                {
                    Keyword = SchoolName,
                    Class = ClassSelectedItem,
                    City = CitySelectedItem,
                    Accreditation = AccreditionSelectedItem,
                    Rating = RatingSearch,
                    MaximumDistance = distance,
                    AdmissionsInProgress = AdmissionProgress,
                    MinimumBudget = Minimum,
                    MaximumBudget = Maximum = Maximum == 0 ? 500000 : Maximum
                });
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in HubpageViewModel.cs : GoToFindSchools: " + ex.ToString());
            }
        }

        #endregion
    }
}

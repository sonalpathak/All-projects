// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the CompareschoolsViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Neudesic.Schoolistics.Core.ViewModels
{
    using System.Windows.Input;

    using Cirrious.MvvmCross.ViewModels;
    using Neudesic.Schoolistics.Core.Services;
    using Neudesic.Schoolistics.Core.Entities;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Newtonsoft.Json;
    using System;
    using Cirrious.MvvmCross.Plugins.Messenger;
    using Cirrious.CrossCore;

    /// <summary>
    /// Define the CompareschoolsViewModel type.
    /// </summary>
    public class CompareschoolsViewModel : BaseViewModel
    {
        
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

        private static ObservableCollection<School> schoolDetailsToCompare;

        public ObservableCollection<School> SchoolDetailsToCompare
        {
            get { return schoolDetailsToCompare; }
            set
            {
                schoolDetailsToCompare = value;
                RaisePropertyChanged(() => SchoolDetailsToCompare);
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




        private static ObservableCollection<ComparisonDetail> yourSavedSchoolComparisons;

        public ObservableCollection<ComparisonDetail> YourSavedSchoolComparisons
        {
            get
            { return yourSavedSchoolComparisons; }
            set
            {
                yourSavedSchoolComparisons = value;
                RaisePropertyChanged(() => YourSavedSchoolComparisons);
            }
        }

        private ISchoolComparisonService _schoolComparisonService;
        IMvxMessenger messenger;

        public CompareschoolsViewModel(ISchoolComparisonService schoolComparisonService, IMvxMessenger _messenger)
        {
            try
            {
            SchoolDetailsToCompare = null;
            _schoolComparisonService = schoolComparisonService;
            messenger = _messenger;
            if (Utils.AppData.SavedComparedSchools.Count > 0)
            {
                SchoolDetailsToCompare = new ObservableCollection<School>(Utils.AppData.SavedComparedSchools);
            }
            GetSchoolDetails();
            GetUserSavedComparisons();
            GetPopularComparisons();
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in CompareschoolsViewModel.cs :CompareschoolsViewModel: " + ex.ToString());
            }
        }



        #region Get School Details



        private void GetSchoolDetails()
        {
            try
            {
                List<School> schools = new List<School>();
                _schoolComparisonService.GetSchoolDetails(schools, AllSchoolDetailsSuccess, AllSchoolDetailsFailure);
            }
            catch (Exception ex)
            {
                string a = ex.Message;
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
                Mvx.Error("Error in CompareschoolsViewModel.cs :SchoolDetailsSuccess: " + ex.ToString());
            }
        }

        private void AllSchoolDetailsFailure(System.Exception e)
        {
            var message = e.Message;
        }
        #endregion

        #region School Comparisons List


        public string GetSchoolDetailsToCompare(List<string> schoolIdList)
        {
            try{
            List<School> schoolsList = new List<School>();
            //var schoolsList = SchoolDetails.Where(r => r.SchoolName == names).ToList();

            for (int i = 0; i < schoolIdList.Count; i++)
            {
                var schoolID = schoolIdList[i];

                var schoolDetailsTodisplay = SchoolDetails.FirstOrDefault(r => r.SchoolId == schoolID);
                schoolsList.Add(schoolDetailsTodisplay);
            }
            if (SchoolDetailsToCompare == null)
            {
                if (schoolsList.Count < 7)
                {
                    SchoolDetailsToCompare = new ObservableCollection<School>(schoolsList);
                    return "LessThan7";
                }
                else
                    SchoolDetailsToCompare = new ObservableCollection<School>(schoolsList.Take(7));
                return "GreaterThan7";
            }
            else
            {

                foreach (var item in schoolsList)
                {
                    if (SchoolDetailsToCompare.Count < 7)
                    {
                        if ((SchoolDetailsToCompare.ToList().Where(p => p.SchoolId == item.SchoolId).FirstOrDefault() == null))
                        {
                            SchoolDetailsToCompare.Add(item);

                        }
                        else
                            return "AlreadyExists";
                    }
                    else
                        return "GreaterThan7";
                    
                   
                }

               
            }
        }

            catch (Exception ex)
            {
                Mvx.Error("Error in CompareschoolsViewModel.cs :GetSchoolDetailsToCompare: " + ex.ToString());
            }
                return "Success";
            }

        #endregion

        #region Save Comparisons



        public void SaveComparisons(List<School> schoolsList)
        {
            try
            {
                List<SchoolsComparison> comparisons = new List<SchoolsComparison>();
                foreach (var list in schoolsList)
                {

                    comparisons.Add(new SchoolsComparison()
                        {
                            SchoolId = list.SchoolId,
                            SchoolName = list.SchoolName,
                            SchoolLogo = list.SchoolLogo,
                            City = list.City
                        });
                }

                _schoolComparisonService.SaveSchoolComparisons(comparisons, SaveComparisonsSuccess, SaveComparisonsFailure);
               
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                
            }
        }



        private void SaveComparisonsSuccess(ResponseMessage<object> response)
        {
            if (response.SuccessCode == 1)
            {
                messenger.Publish(new NavigationMessage<SchoolsComparison>(this) { Message = "Success" });
            }
        }


        private void SaveComparisonsFailure(System.Exception e)
        {
            var message = e.Message;
            messenger.Publish(new NavigationMessage<SchoolsComparison>(this) { Message = "Failure" });
        }
        #endregion

        #region Get user saved comparisons


        private void GetUserSavedComparisons()
        {
            try
            {
                List<ComparisonDetail> savedComparisons = new List<ComparisonDetail>();
                _schoolComparisonService.GetUserSavedSchoolComparisons(savedComparisons, SavedSchoolComparisonsSuccess, SavedSchoolComparisonsFailure);
            }
            catch (Exception ex)
            {
                string a = ex.Message;
            }
        }

        private void SavedSchoolComparisonsSuccess(ResponseMessage<object> response)
        {
            if (response.SuccessCode == 1)
            {
                List<ComparisonDetail> data = JsonConvert.DeserializeObject<List<ComparisonDetail>>(response.Data.ToString());
                //List<SchoolsComparison> comparisons = new List<SchoolsComparison>();
                //for (int i = 0; i < data.Count; i++)
                //{
                //    for (int j = 0; j < data[i].Schools.Count; j++)
                //    {

                //        comparisons.Add(data[i].Schools[j]);


                //    }

                YourSavedSchoolComparisons = new ObservableCollection<ComparisonDetail>(data);
                }


            
        }

        private void SavedSchoolComparisonsFailure(System.Exception e)
        {
            var message = e.Message;
        }




        #endregion


        public void GetPopularComparisons()
        {
            try
            {
                List<ComparisonDetail> popularComparisons = new List<ComparisonDetail>();
                _schoolComparisonService.GetPopularSchoolComparisons(popularComparisons, PopularSchoolComparisonsSuccess, PopularSchoolComparisonsFailure);
            }
            catch (Exception ex)
            {
                string a = ex.Message;
            }
        }

        private void PopularSchoolComparisonsSuccess(ResponseMessage<object> response)
        {
            try{
            if (response.SuccessCode == 1)
            {
                List<ComparisonDetail> data = JsonConvert.DeserializeObject<List<ComparisonDetail>>(response.Data.ToString());

                //List<SchoolsComparison> comparisons = new List<SchoolsComparison>();
                //for (int i = 0; i < data.Count; i++)
                //{
                //    for (int j = 0; j < data[i].Schools.Count; j++)
                //    {

                //        comparisons.Add(data[i].Schools[j]);


                //    }

                PopularSchoolComparisons = new ObservableCollection<ComparisonDetail>(data.Take(2));
                }
        }

            catch (Exception ex)
            {
                Mvx.Error("Error in CompareschoolsViewModel.cs :PopularSchoolComparisonsSuccess: " + ex.ToString());
            }


            }
       

        private void PopularSchoolComparisonsFailure(System.Exception e)
        {
            var message = e.Message;
        }
        private MvxCommand<School> deleteCommand;

        public ICommand DeleteSchool
        {
            get
            {
                deleteCommand = deleteCommand ?? new MvxCommand<School>(item => DoDeleteCommand(item));
                return deleteCommand;
            }
        }

        private void DoDeleteCommand(School item)
        {
            try
            {
            SchoolDetailsToCompare.Remove(item);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in CompareschoolsViewModel.cs :DoDeleteCommand: " + ex.ToString());
            }

        }
        private MvxCommand<ComparisonDetail> showDetailComparisons;

        public ICommand ShowDetailComparisons
        {
            get
            {

                showDetailComparisons = showDetailComparisons ?? new MvxCommand<ComparisonDetail>(item => GoToSchoolComparisonDetails(item.Schools));
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
                string a = ex.Message;
            }
           
        }

        private void SchoolDetailsFailure(Exception e)
        {
            var message = e.Message;
        }

        private void SchoolDetailsSuccess(ResponseMessage<object> response)
        {
            try
            {
            if (response.SuccessCode == 1)
            {
                List<School> school = JsonConvert.DeserializeObject<List<School>>(response.Data.ToString());
                ShowViewModel<SchoolComparisonDetailsViewModel>(new SchoolComparisonDetailsViewModel(new SchoolComparisonService(new RestService()),null) { SchoolComparisonsDetails = new ObservableCollection<School>(school)});
                   // ShowViewModel<SchoolComparisonDetailsViewModel>(new SchoolComparisonDetailsViewModel(new SchoolComparisonService(new RestService())) { SchoolComparisonsDetails = new ObservableCollection<School>(school) });

            }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in CompareschoolsViewModel.cs :SchoolDetailsSuccess: " + ex.ToString());
            }
           
        }


        /// <summary>
        /// Backing field for my property.
        /// </summary>
        private string myProperty = "Mvx Ninja Coder!";

        /// <summary>
        ///  Backing field for my command.
        /// </summary>
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
                return this.myCommand ?? (this.myCommand = new MvxCommand(() => this.ShowViewModel<CompareschoolsViewModel>()));
            }
        }
    }
}

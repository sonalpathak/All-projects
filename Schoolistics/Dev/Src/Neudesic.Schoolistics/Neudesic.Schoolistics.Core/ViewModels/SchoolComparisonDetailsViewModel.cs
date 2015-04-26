// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the SchoolComparisonDetailsViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
using Cirrious.MvvmCross.ViewModels;
using Neudesic.Schoolistics.Core.Entities;
using Neudesic.Schoolistics.Core.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
using Newtonsoft.Json;
using System;
using Cirrious.MvvmCross.Plugins.Messenger;
namespace Neudesic.Schoolistics.Core.ViewModels
{
    /// <summary>
    /// Define the SchoolComparisonDetailsViewModel type.
    /// </summary>
    /// 



    public class SchoolComparisonDetailsViewModel : BaseViewModel
    {
        //public SchoolComparisonDetailsViewModel()
        //{
        //}
        public static List<School> schoolList;
        private static ObservableCollection<School> schoolComparisonsDetails;

        public ObservableCollection<School> SchoolComparisonsDetails
        {
            get
            { return schoolComparisonsDetails; }
            set
            {
                schoolComparisonsDetails = value;
                RaisePropertyChanged(() => SchoolComparisonsDetails);
            }
        }

        //public void Init(List<SchoolsComparison> SchoolComparisonsDetails)
        //{
        //    string a = "";
        //}
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

        IMvxMessenger messenger;


        private ISchoolComparisonService _schoolComparisonService;

        public SchoolComparisonDetailsViewModel(ISchoolComparisonService schoolComparisonService, IMvxMessenger _messenger)
        {
            _schoolComparisonService = schoolComparisonService;
            messenger = _messenger;
            GetSchoolDetails();
           
        }

       

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
            if (response.SuccessCode == 1)
            {
                List<School> data = JsonConvert.DeserializeObject<List<School>>(response.Data.ToString());
                SchoolDetails = new ObservableCollection<School>(data);

            }
        }

        private void AllSchoolDetailsFailure(System.Exception e)
        {
            var message = e.Message;
        }

        
        public string GetSchoolDetailsToCompare(List<string> schoolIdList)
        {
            List<School> schoolsList = new List<School>();
            //var schoolsList = SchoolDetails.Where(r => r.SchoolName == names).ToList();

            for (int i = 0; i < schoolIdList.Count; i++)
            {
                var schoolID = schoolIdList[i];

                var schoolDetailsTodisplay = SchoolDetails.FirstOrDefault(r => r.SchoolId == schoolID);
                schoolsList.Add(schoolDetailsTodisplay);
            }
            if (schoolComparisonsDetails == null)
            {
                if (schoolsList.Count < 7)
                {
                    schoolComparisonsDetails = new ObservableCollection<School>(schoolsList);
                    return "LessThan7";
                }
                else
                    schoolComparisonsDetails = new ObservableCollection<School>(schoolsList.Take(7));
                return "GreaterThan7";
            }
            else
            {

                foreach (var item in schoolsList)
                {
                    if (schoolComparisonsDetails.Count < 7)
                    {
                        if ((schoolComparisonsDetails.ToList().Where(p => p.SchoolId == item.SchoolId).FirstOrDefault() == null))
                        {
                            schoolComparisonsDetails.Add(item);

                        }
                        else
                            return "AlreadyExists";
                    }
                    else
                        return "GreaterThan7";


                }

                return "Success";
            }
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
            SchoolComparisonsDetails.Remove(item);

        }

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

      
    }
}

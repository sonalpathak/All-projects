using Neudesic.Schoolistics.Core.ViewModels;
using Neudesic.Schoolistics.WindowsStore.Usercontrols;
using System.Collections.Generic;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using System;
// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the SchoolComparisonDetailsView.xaml type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media.Animation;
using Neudesic.Schoolistics.Core.Services;
using Windows.UI.Xaml.Controls;
using Neudesic.Schoolistics.Core.Entities;
using Windows.UI.Xaml.Input;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml.Navigation;
using System.Linq;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.CrossCore;
using Cirrious.CrossCore;
namespace Neudesic.Schoolistics.WindowsStore.Views
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class SchoolComparisonDetailsView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SchoolComparisonDetailsView"/> class.
        /// </summary>
        List<School> schools = new List<School>();
        private School deletedSchool;
        MvxSubscriptionToken token;
       
        public SchoolComparisonDetailsView()
        {
            try
            {
            this.InitializeComponent();
                IMvxMessenger messenger = Mvx.GetSingleton<IMvxMessenger>();
            token = messenger.SubscribeOnMainThread<NavigationMessage<SchoolsComparison>>(MessageSubscribe);
          //  DataTransferManager dataTransferManagerForCompareSchools = DataTransferManager.GetForCurrentView();
                 }
            catch (Exception ex)
            {
                Mvx.Error("Error in SchoolComaparionsDetailsView.cs :SchoolComparisonDetailsView: " + ex.ToString());

            }
           
        }

        

        private Popup _addSchoolPopup;


        private void AddSchoolButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {
            _addSchoolPopup = null;
            OpenAddSchoolsPopup();
        }
            catch (Exception ex)
            {
                Mvx.Error("Error in SchoolComaparionsDetailsView.cs :SchoolComparisonDetailsView: " + ex.ToString());

            }
        }



        private void OpenAddSchoolsPopup()
        {
            try
            {
            if (_addSchoolPopup == null)
            {
                _addSchoolPopup = GetPopUp();
                _addSchoolPopup.IsOpen = true;
     return;
               
            }
     
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SchoolComaparionsDetailsView.cs :SchoolComparisonDetailsView: " + ex.ToString());

            }
   
            _addSchoolPopup.IsOpen = !_addSchoolPopup.IsOpen;
        }

        Popup popUp = new Popup();
        private Popup GetPopUp()
        {
            try{
            var addSchoolControl = new CompareSchoolsControl();
            addSchoolControl.DataContext = (SchoolComparisonDetailsViewModel)ViewModel;
            addSchoolControl._selectedSchoolsList += addSchoolControl__selectedSchoolsList;
            addSchoolControl._popUpCloseEvent += addSchoolControl__popUpCloseEvent;
            addSchoolControl.Height = Window.Current.Bounds.Height;
            popUp.Width = 400;
            popUp.Height = Window.Current.Bounds.Height;
            popUp.ChildTransitions = new TransitionCollection();
            popUp.ChildTransitions.Add(new PaneThemeTransition());
            popUp.Child = addSchoolControl;
            popUp.HorizontalOffset = Window.Current.Bounds.Width - popUp.Width;
            popUp.VerticalOffset = 0;
            popUp.IsLightDismissEnabled = true;
        }
            catch (Exception ex)
            {
                Mvx.Error("Error in SchoolComaparionsDetailsView.cs :SchoolComparisonDetailsView: " + ex.ToString());

            }
            return popUp;

        }

        void addSchoolControl__popUpCloseEvent(object sender, RoutedEventArgs e)
        {
            try
            {
            popUp.IsOpen = false;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SchoolComaparionsDetailsView.cs :addSchoolControl__popUpCloseEvent: " + ex.ToString());

            }

        }

        void addSchoolControl__selectedSchoolsList(object sender, RoutedEventArgs e)
        {
            try{
            var schoolIdList = sender as List<string>;
            var result = ((SchoolComparisonDetailsViewModel)ViewModel).GetSchoolDetailsToCompare(schoolIdList);
            if (result == "Success")
            {
                popUp.IsOpen = false;
            }
            else if (result == "LessThan7")
            {
                popUp.IsOpen = false;
            }
            else if (result == "GreaterThan7")
            {
                popUp.IsOpen = false;
                SchoolsExceedCount();
                stkAddSchoolButtonAppend.Visibility = Visibility.Collapsed;
            }
            else if (result == "AlreadyExists")
            {
                popUp.IsOpen = false;
                SchoolAlreadyExists();
            }
            else
            {

            }
        }
            catch (Exception ex)
            {
                Mvx.Error("Error in SchoolComaparionsDetailsView.cs :addSchoolControl__selectedSchoolsList: " + ex.ToString());

            }
        }

        private async void SchoolAlreadyExists()
        {
            try
            {
            var dialog = new MessageDialog("School already exists in comparisons.");
            dialog.Commands.Add(new UICommand("OK"));
            await dialog.ShowAsync();
        }
            catch (Exception ex)
            {
                Mvx.Error("Error in SchoolComaparionsDetailsView.cs :SchoolAlreadyExists: " + ex.ToString());

            }
        }



        private async void SchoolsExceedCount()
        {
            try
            {
            var dialog = new MessageDialog("You can compare only 7 schools.");
            dialog.Commands.Add(new UICommand("OK"));
            await dialog.ShowAsync();
        }
            catch (Exception ex)
            {
                Mvx.Error("Error in SchoolComaparionsDetailsView.cs :SchoolsExceedCount: " + ex.ToString());

            }
        }

            
//reset
        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            if (((SchoolComparisonDetailsViewModel)ViewModel).SchoolComparisonsDetails != null)
            {
                ((SchoolComparisonDetailsViewModel)ViewModel).SchoolComparisonsDetails.Clear();
                if (stkAddSchoolButtonAppend.Visibility == Visibility.Collapsed)
                stkAddSchoolButtonAppend.Visibility = Visibility.Visible;
                else
                { }
            }
        }
            catch (Exception ex)
            {
                Mvx.Error("Error in SchoolComaparionsDetailsView.cs :resetButton_Click: " + ex.ToString());

            }

        }

        private void saveComparisonsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            SaveComparisonsButton();
        }
            catch (Exception ex)
            {
                Mvx.Error("Error in SchoolComaparionsDetailsView.cs :saveComparisonsButton_Click: " + ex.ToString());

            }
        }

        private MessageDialog _dialog;
        private string isComparisonSaved;
        private async void SaveComparisonsButton()
        {
            try
            {
            _dialog = new MessageDialog("Do you want to save comparions?", "Save Comparisons");
            _dialog.Commands.Add(new UICommand("OK", new UICommandInvokedHandler(SaveComparisonCommandHandler)));
            await _dialog.ShowAsync();
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SchoolComaparionsDetailsView.cs :SaveComparisonsButton: " + ex.ToString());

            }

        }

        private void SaveComparisonCommandHandler(IUICommand command)
        {
            if (command.Label == "OK")
            {
                var comparisonList = ((SchoolComparisonDetailsViewModel)ViewModel).SchoolComparisonsDetails.ToList();
                ((SchoolComparisonDetailsViewModel)ViewModel).SaveComparisons(comparisonList);
                //if (isComparisonSaved == "Success")
                //{
                //    ShowComparisonSaved();
                //}
                //else
                //{
                //    ShowComparisonSavedFailed();
                //}

            }


        }

        private void MessageSubscribe(NavigationMessage<SchoolsComparison> messageData)
        {
            if (messageData.Message == "Success")
                ShowComparisonSaved();
            else
                ShowComparisonSavedFailed();
        }

        private async void ShowComparisonSaved()
        {
            _dialog = new MessageDialog("Your comparison is saved", "Save Comparisons");
            _dialog.Commands.Add(new UICommand("OK"));
            await _dialog.ShowAsync();
        }

        private async void ShowComparisonSavedFailed()
        {
            _dialog = new MessageDialog("Your comparison is not saved.Please check", "Error");
            _dialog.Commands.Add(new UICommand("OK"));
            await _dialog.ShowAsync();
        }
       

        private void deleteButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
  try
            {
var b = sender as Button;

            deletedSchool = b.DataContext as School;
            SchoolDeleteConfirmation();

            }
catch (Exception ex)
            {
                Mvx.Error("Error in SchoolComaparionsDetailsView.cs :deleteButton_Tapped: " + ex.ToString());

            }

           

        }
            
        private async void SchoolDeleteConfirmation()
        {
            _dialog = new MessageDialog("Do you want to delete this school?", "Delete School");
            _dialog.Commands.Add(new UICommand("OK", new UICommandInvokedHandler(DeleteCommandHandler)));
            await _dialog.ShowAsync();
        }
          
        private void DeleteCommandHandler(IUICommand command)
        {
            if (command.Label == "OK")
            {
                ((SchoolComparisonDetailsViewModel)ViewModel).DeleteSchool.Execute(deletedSchool);
            }

        }

        
        private void shareButton_Click(object sender, RoutedEventArgs e)
        {

            try{
            schools = ((SchoolComparisonDetailsViewModel)ViewModel).SchoolComparisonsDetails.ToList();
            //DataTransferManager.ShowShareUI();
        }
               catch (Exception ex)
            {
                Mvx.Error("Error in SchoolComaparionsDetailsView.cs :shareButton_Click: " + ex.ToString());

            }
        }

        //private void CompareschoolsView_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        //{
        //    try
        //    {
        //    var req = args.Request;
        //    req.Data.Properties.Title = "School Comparisons";
        //    string sharedData = null;
        //    foreach (var comparisons in schools)
        //    {
        //        if (sharedData != null)
        //        {
        //            sharedData = sharedData + "," + comparisons.SchoolName ;
        //        }
        //        else
        //            sharedData = comparisons.SchoolName;
        //    }
        //    req.Data.SetText("I have compared the following schools - " + sharedData);
        //    }
        //    catch (Exception ex)
        //    {
        //        Mvx.Error("Error in SchoolComaparionsDetailsView.cs :CompareschoolsView_DataRequested: " + ex.ToString());

        //}
       
           
        //}

        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    base.OnNavigatedTo(e);
        //    DataTransferManager.GetForCurrentView().DataRequested += CompareschoolsView_DataRequested;    
           
        //}

        //protected override void OnNavigatedFrom(NavigationEventArgs e)
        //{
        //    DataTransferManager.GetForCurrentView().DataRequested -= CompareschoolsView_DataRequested;
        //}


    }
}
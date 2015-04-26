// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the SearchresultsView.xaml type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Cirrious.CrossCore;
using Neudesic.Schoolistics.Core.Services;
using Neudesic.Schoolistics.Core.ViewModels;
using Neudesic.Schoolistics.WindowsStore.Usercontrols;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml;
using System;
using System.Collections.Generic;
using Windows.Devices.Geolocation;
using Neudesic.Schoolistics.Core.Entities;
using Windows.UI.Xaml.Controls;
using Cirrious.MvvmCross.Plugins.Messenger;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage.Streams;
using System.Text;
using Neudesic.Schoolistics.WindowsStore.Common;
using Neudesic.Schoolistics.WindowsStore.Helpers;
using Windows.UI.Popups;
using System.Threading.Tasks;
using Windows.UI.Core;
using Neudesic.Schoolistics.Core.Utils;
using Windows.UI.Xaml.Navigation;

namespace Neudesic.Schoolistics.WindowsStore.Views
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class SearchresultsView
    {
        private Popup refineSearchPopUp;
        private Popup savePopUp;
        private Popup refinePopUp;
        private FindSchoolsRefineSearchResultsControl refineSearch;
        private FindSchoolsSaveControl saveControl;
        private int selectedSchoolCount;
        MvxSubscriptionToken token;
        static School selectedSchoolData;
       // DataTransferManager dataTransferManager;

        public SearchresultsView()
        {
            try
            {
            this.InitializeComponent();

            IMvxMessenger messenger = Mvx.GetSingleton<IMvxMessenger>();
            token = messenger.SubscribeOnMainThread<NavigationMessage<object>>(MessageSubscribe);

            // dataTransferManager = DataTransferManager.GetForCurrentView();
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchresultsView.xaml.cs : SearchresultsView : " + ex.ToString());
            }
           
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
            base.OnNavigatedTo(e);
               // dataTransferManager.DataRequested += ShareLinkHandler;
        }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchresultsView.xaml.cs : OnNavigatedTo : " + ex.ToString());
            }
        }

        //protected override void OnNavigatedFrom(NavigationEventArgs e)
        //{
        //    try
        //    {
        //        dataTransferManager.DataRequested -= ShareLinkHandler;
        //    }
        //    catch (Exception ex)
        //    {
        //        Mvx.Error("Error in SearchresultsView.xaml.cs : OnNavigatedFrom : " + ex.ToString());
        //    }
        //}

        public async void MessageSubscribe(NavigationMessage<object> message)
        {
            try
            {
            if (message.Message == "CloseRefinePopUp")
            {
                refineSearchPopUp.IsOpen = false;
                return;
            }
            if (message.Message == "BookmarkedSchoolSuccess")
            {
                await ShowMessage("School Bookmarked Successfully");
                return;
            }
            if (message.Message == "BookmarkedSchoolFailure")
            {
                await ShowMessage("School Bookmarked Failed, please try after some time");
                return;
            }
            if (message.Message == "CheckingUserLogInBeforeBookmarking")
            {
                await ShowMessage("Please LogIn before bookmarking a school");
                return;
            }
           if (message.Message == "SaveSuccess")
            {
                // savePopUp.IsOpen = false;
                await ShowMessage(message.Data.ToString());
                return;
            }
           if (message.Message == "CheckingLoginBeforeSave")
           {
               await ShowMessage("Please LogIn before saving the search");
               return;
           }
           if (message.Message == "CheckingSaveSearchText")
           {
               await ShowMessage("Save search can't be empty");
               return;
           }
        }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchresultsView.xaml.cs : MessageSubscribe : " + ex.ToString());
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
                Mvx.Error("Error in SearchresultsView.xaml.cs : ShowMessage : " + ex.ToString());
                return null;
            }
        }

        private void FindSchoolsBottomAppBarControl_refineTappedEvent(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {
            this.BottomAppBar.IsOpen = false;
            if (refineSearchPopUp == null)
            {
                refineSearchPopUp = this.GetRefineSearchPopUp();
                refineSearchPopUp.IsOpen = true;
                return;
            }
            refineSearchPopUp.IsOpen = !refineSearchPopUp.IsOpen;
        }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchresultsView.xaml.cs : FindSchoolsBottomAppBarControl_refineTappedEvent : " + ex.ToString());
            }
        }

        public Popup GetRefineSearchPopUp()
        {
            try
            {
            refineSearch = new FindSchoolsRefineSearchResultsControl();
            refineSearch.DataContext = (SearchresultsViewModel)ViewModel;

            refineSearch.Width = 380;
            refineSearch.Height = Window.Current.Bounds.Height;

            refinePopUp = new Popup();
            refinePopUp.Width = 380;
            refinePopUp.Height = Window.Current.Bounds.Height;
            refinePopUp.ChildTransitions = new TransitionCollection();
            refinePopUp.ChildTransitions.Add(new PaneThemeTransition());
            refinePopUp.IsLightDismissEnabled = true;
            refinePopUp.Child = refineSearch;
            refinePopUp.HorizontalOffset = Window.Current.Bounds.Width - refinePopUp.Width;
            refinePopUp.VerticalOffset = 0;
            return refinePopUp;
        }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchresultsView.xaml.cs : GetRefineSearchPopUp : " + ex.ToString());
                return null;
            }
        }

        public Popup GetSavePopUp()
        {
            try
            {
            saveControl = new FindSchoolsSaveControl();
            saveControl.DataContext = (SearchresultsViewModel)ViewModel;
            saveControl.cancelSavePopUp += saveControl_cancelSavePopUp;

            saveControl.Height = Window.Current.Bounds.Height;
            saveControl.Width = Window.Current.Bounds.Width;

            savePopUp = new Popup();
            savePopUp.Width = Window.Current.Bounds.Width;
            savePopUp.Height = 120;
            savePopUp.IsLightDismissEnabled = true;
            savePopUp.Child = saveControl;
            savePopUp.VerticalOffset = 0;
            savePopUp.HorizontalOffset = 0;
            return savePopUp;
        }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchresultsView.xaml.cs : GetSavePopUp : " + ex.ToString());
                return null;
            }
        }

        private void saveControl_cancelSavePopUp(object sender, RoutedEventArgs e)
        {
            try
            {
            savePopUp.IsOpen = false;
        }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchresultsView.xaml.cs : saveControl_cancelSavePopUp : " + ex.ToString());
            }
        }

        private void FindSchoolsBottomAppBarControl_mapTappedEvent(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {
            this.BottomAppBar.IsOpen = false;
            ((SearchresultsViewModel)ViewModel).MapVisibility = true;
            ((SearchresultsViewModel)ViewModel).GridVisibility = false;
        }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchresultsView.xaml.cs : FindSchoolsBottomAppBarControl_mapTappedEvent : " + ex.ToString());
            }
        }

        private void FindSchoolsBottomAppBarControl_gridTappedEvent(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {
            this.BottomAppBar.IsOpen = false;
            ((SearchresultsViewModel)ViewModel).GridVisibility = true;
            ((SearchresultsViewModel)ViewModel).MapVisibility = false;
        }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchresultsView.xaml.cs : FindSchoolsBottomAppBarControl_gridTappedEvent : " + ex.ToString());
            }
        }

        private void FindSchoolsBottomAppBarControl_saveTappedEvent(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {
            this.BottomAppBar.IsOpen = false;
            if (savePopUp == null)
            {
                savePopUp = this.GetSavePopUp();
                savePopUp.IsOpen = true;
                return;
            }
            savePopUp.IsOpen = !savePopUp.IsOpen;
        }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchresultsView.xaml.cs : FindSchoolsBottomAppBarControl_saveTappedEvent : " + ex.ToString());
            }
        }

        private void SearchResultsGridViewControl_gridSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
            selectedSchoolCount = e.AddedItems.Count;
            selectedSchoolData = ((sender as GridView).SelectedItem as School);
            ((SearchresultsViewModel)ViewModel).SelectedSchoolData = selectedSchoolData;

            //this.appBarOnGridClick.Visibility = Visibility.Visible;
            //this.mainAppBar.Visibility = Visibility.Collapsed;
            this.BottomAppBar.IsOpen = true;
        }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchresultsView.xaml.cs : SearchResultsGridViewControl_gridSelectionChanged : " + ex.ToString());
            }
        }

        private void AppBar_Opened(object sender, object e)
        {
            try
            {
            if (selectedSchoolCount != 0)
            {
                this.appBarOnGridClick.Visibility = Visibility.Visible;
                this.mainAppBar.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.appBarOnGridClick.Visibility = Visibility.Collapsed;
                this.mainAppBar.Visibility = Visibility.Visible;
            }
        }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchresultsView.xaml.cs : AppBar_Opened : " + ex.ToString());
            }
        }

        private void gridViewUserControl_gridItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
            string schoolId = ((School)e.ClickedItem).SchoolId;
            ((SearchresultsViewModel)ViewModel).NavigateToSchoolDetails(schoolId);
        }
            catch (Exception ex)
        {
                Mvx.Error("Error in SearchresultsView.xaml.cs : gridViewUserControl_gridItemClick : " + ex.ToString());
            }
        }
            
        //private void appBarOnGridClick_shareRoute(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        // selectedSchoolData = ((School)e.OriginalSource);
        //        DataTransferManager.ShowShareUI();
        //    }
        //    catch (Exception ex)
        //    {
        //        Mvx.Error("Error in SearchresultsView.xaml.cs : appBarOnGridClick_shareRoute : " + ex.ToString());
        //    }
        //}


        //private void ShareLinkHandler(DataTransferManager sender, DataRequestedEventArgs e)
        //{
        //    try
        //    {
        //        // Set title and Description Properties
        //        School schoolData = selectedSchoolData;
        //        var request = e.Request;
        //        request.Data.Properties.Title = schoolData.SchoolName;
        //        request.Data.Properties.Description = schoolData.Address;
        //        request.Data.SetText(schoolData.SchoolName + "\n\t" + schoolData.Address);
        //    }
        //    catch (Exception ex)
        //    {
        //        Mvx.Error("Error in SearchresultsView.xaml.cs : ShareLinkHandler : " + ex.ToString());
        //    }
        //}


        private void mapViewUserControl_pushpinTappedEvent(object sender, RoutedEventArgs e)
        {
            try
            {
            string schoolId = SearchResultsMapViewControl.selectedSchool.SchoolId;
            ((SearchresultsViewModel)ViewModel).NavigateToSchoolDetails(schoolId);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchresultsView.xaml.cs : mapViewUserControl_pushpinTappedEvent : " + ex.ToString());
            }
        }

        private void appBarOnGridClick_compareRoute(object sender, RoutedEventArgs e)
        {
            try
            {
            if (AppData.SavedComparedSchools.Count < 7)
            {
                if (!AppData.SavedComparedSchools.Contains(selectedSchoolData))
                {
                    AppData.SavedComparedSchools.Add(selectedSchoolData);
                    ShowMessage("School saved for comparision");
                }
                    else
                    {
                        ShowMessage("You have already added this school for comparision");
                    }
            }
            else
            {
                ShowMessage("You have already saved 7 schools for comparision");
            }
        }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchresultsView.xaml.cs : appBarOnGridClick_compareRoute : " + ex.ToString());
            }
        }

        private void errorLocationPopUpUserControl_EventForClosePopUp(object sender, RoutedEventArgs e)
        {
            try
            {
            errorLocationPopUpUserControl.Visibility = Visibility.Collapsed;
        }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchresultsView.xaml.cs : errorLocationPopUpUserControl_EventForClosePopUp : " + ex.ToString());
            }
        }

        private void searchViewUserControl_ErrorPopupCollapsedEvent(object sender, RoutedEventArgs e)
        {
            try
            {
            errorLocationPopUpUserControl.Visibility = Visibility.Collapsed;
        }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchresultsView.xaml.cs : searchViewUserControl_ErrorPopupCollapsedEvent : " + ex.ToString());
            }
        }

        private void searchViewUserControl_ErrorPopupVisibilityEvent(object sender, RoutedEventArgs e)
        {
            try
            {
            errorLocationPopUpUserControl.Visibility = Visibility.Visible;
        }
            catch (Exception ex)
            {
                Mvx.Error("Error in SearchresultsView.xaml.cs : searchViewUserControl_ErrorPopupVisibilityEvent : " + ex.ToString());
            }
        }
    }
}
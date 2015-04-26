using Neudesic.Schoolistics.Core.Entities;
using Neudesic.Schoolistics.Core.ViewModels;
using Neudesic.Schoolistics.WindowsStore.Usercontrols;
using Windows.UI.Popups;
using Windows.ApplicationModel.DataTransfer;
using System.Text;
using Windows.Storage.Streams;
using System.Collections.Generic;
using Windows.UI.Xaml;
// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the SchoolDetailsView.xaml type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;
using Neudesic.Schoolistics.WindowsStore.Helpers;
using Neudesic.Schoolistics.Core.Utils;
using System.Threading.Tasks;
using System;
using Windows.UI.Core;
using Windows.UI.Xaml.Navigation;
using Cirrious.CrossCore;
namespace Neudesic.Schoolistics.WindowsStore.Views
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class SchoolDetailsView
    {
        static Popup _popup = new Popup();
        BottonAppBar appbar;
        rating_usercontrol rp;
        private NewsArticlePostCommentControl postComment;
        private Popup postCommentPopUp;
        static School selectedSchool;
        //DataTransferManager dataTransferManagerForSchools;
        /// <summary>
        /// Initializes a new instance of the <see cref="SchoolDetailsView"/> class.
        /// </summary>
        /// 

        CompareschoolsViewModel _viewmodel;
        public SchoolDetailsView()
        {
            try
            {
            this.InitializeComponent();
        }
            catch (Exception ex)
        {
                Mvx.Error("Error in SchoolDetailsView.cs :SchoolDetailsView: " + ex.ToString());

        }

          //  dataTransferManagerForSchools = DataTransferManager.GetForCurrentView();

        }


        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    base.OnNavigatedTo(e);
            
        //    DataTransferManager.GetForCurrentView().DataRequested += SchoolDetailsView_DataRequested;
        //}

        //protected override void OnNavigatedFrom(NavigationEventArgs e)
        //{
        //    DataTransferManager.GetForCurrentView().DataRequested -= SchoolDetailsView_DataRequested;
        //}

        //protected override void GoBack(object sender, RoutedEventArgs e)
        //{
        //     base.OnLostFocus(e);
        //    dataTransferManagerForSchools.DataRequested -= SchoolDetailsView_DataRequested;
        //}

      

        private void appbar_buttonTapped(object sender, TappedRoutedEventArgs e)
        {
            try
            {
            _popup = new Popup();
            rp = new rating_usercontrol();
            _popup.Child = rp;

            _popup.HorizontalOffset = 1040;
            _popup.VerticalOffset = 650;
            _popup.IsOpen = true;
        }
            catch (Exception ex)
        {
                Mvx.Error("Error in SchoolDetailsView.cs :SchoolDetailsView: " + ex.ToString());

        }

        }

        //void appbar_Rating_Button(object sender, RoutedEventArgs e)
        //{
        //    MessageDialog mess = new MessageDialog("wqjbdkejbc", "ndsbcns");
        //    mess.ShowAsync();
        //}





        public void Rating_Button(object sender, RoutedEventArgs e)
        {
            
            rating_usercontrol rp = new rating_usercontrol();
            _popup.Child = rp;

            _popup.HorizontalOffset = 1040;
            _popup.VerticalOffset = 650;
            _popup.IsOpen = true;
       
        }

        //private void star5_clicked_event(object sender, RoutedEventArgs e)
        //{

        //}

        private void BottonAppBar_RateTappedEventHandler(object sender, TappedRoutedEventArgs e)
        {
            try
            {
            _popup = new Popup();
            rating_usercontrol rp = new rating_usercontrol();
            rp.DataContext = this.DataContext;
            rp.star5_clicked_event += rp_star5_clicked_event;
            rp.star3_clicked_event += rp_star3_clicked_event;
            rp.star1_clicked_event += rp_star1_clicked_event;
            rp.star2_clicked_event += rp_star2_clicked_event;
            rp.star4_clicked_event += rp_star4_clicked_event;
            _popup.Child = rp;

            _popup.HorizontalOffset = 1040;
            _popup.VerticalOffset = 650;
            _popup.IsOpen = true;
        }
            catch (Exception ex)
            {
                Mvx.Error("Error in SchoolDetailsView.cs :BottonAppBar_RateTappedEventHandler : " + ex.ToString());

            }
        }

        private void rp_star4_clicked_event(object sender, RoutedEventArgs e)
        {
            //var details = ((SchoolDetailsViewModel)ViewModel).Details;
            //details.Rating = 4;
            //((SchoolDetailsViewModel)ViewModel).Details = details;
            ((SchoolDetailsViewModel)ViewModel).RatingNumber = 4;
            try
            {
                if (_popup.IsOpen)
                {
                    _popup.IsOpen = false;
        }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SchoolDetailsView.cs : rp_star4_clicked_event : " + ex.ToString());

            }
        }

        private void rp_star2_clicked_event(object sender, RoutedEventArgs e)
        {
            //var details = ((SchoolDetailsViewModel)ViewModel).Details;
            //details.Rating = 2;
            //((SchoolDetailsViewModel)ViewModel).Details = details;
            ((SchoolDetailsViewModel)ViewModel).RatingNumber = 2;
            try
            {
                if (_popup.IsOpen)
                {
                    _popup.IsOpen = false;
        }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SchoolDetailsView.cs : rp_star2_clicked_event : " + ex.ToString());

            }
        }

        private void rp_star1_clicked_event(object sender, RoutedEventArgs e)
        {
            //((SchoolDetailsViewModel)ViewModel).Details.Rating=1;
            //((SchoolDetailsViewModel)ViewModel).RatingNumber = 1;
            //details.Rating = 1;
            //detailsRating.Rating = 1;
            //  ((SchoolDetailsViewModel)ViewModel).Details = detailsRating;



            //var rating = ((SchoolDetailsViewModel)ViewModel).Details;
            //rating.Rating = 1;
            //((SchoolDetailsViewModel)ViewModel).Details = rating;

            ((SchoolDetailsViewModel)ViewModel).RatingNumber = 1;
            try
            {
                if (_popup.IsOpen)
                {
                    _popup.IsOpen = false;
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SchoolDetailsView.cs : rp_star1_clicked_event : " + ex.ToString());

            }

        }

        private void rp_star3_clicked_event(object sender, RoutedEventArgs e)
        {
            //var details = ((SchoolDetailsViewModel)ViewModel).Details;
            //details.Rating = 3;
            //((SchoolDetailsViewModel)ViewModel).Details = details;
            // ((SchoolDetailsViewModel)ViewModel).UserRating.Execute(3);z
            //((SchoolDetailsViewModel)ViewModel).Userrating.Execute(3);
            //((SchoolDetailsViewModel)ViewModel).UserRating.Execute(3);
            ((SchoolDetailsViewModel)ViewModel).RatingNumber = 3;
            try
            {
                if (_popup.IsOpen)
                {
                    _popup.IsOpen = false;
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SchoolDetailsView.cs : rp_star3_clicked_event : " + ex.ToString());

            }

        }

        private void rp_star5_clicked_event(object sender, RoutedEventArgs e)
        {
            //var details = ((SchoolDetailsViewModel)ViewModel).Details;
            //details.Rating = 5;
            //((SchoolDetailsViewModel)ViewModel).Details = details;
            ((SchoolDetailsViewModel)ViewModel).RatingNumber = 5;
            try
            {
                if (_popup.IsOpen)
                {
                    _popup.IsOpen = false;
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SchoolDetailsView.cs : rp_star5_clicked_event : " + ex.ToString());

            }
        }

        private void AppBar_Closed(object sender, object e)
        {
            try
            {
            if (_popup.IsOpen)
            {
                _popup.IsOpen = false;
            }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SchoolDetailsView.cs : AppBar_Closed : " + ex.ToString());

            }


        }

        private void LocationUserControl_loadedEvent(object sender, RoutedEventArgs e)
        {
           

        }

        private void BottonAppBar_SaveToCompare(object sender, TappedRoutedEventArgs e)
        {
            selectedSchool = ((SchoolDetailsViewModel)ViewModel).Details;
            try
            {
            if (AppData.SavedComparedSchools.Count < 7)
            {
                if (!AppData.SavedComparedSchools.Contains(selectedSchool))
                    {
                    AppData.SavedComparedSchools.Add(selectedSchool);
                        ShowMessage("You have added this school to comparison");
                    }
                else
                    ShowMessage("You have added this school to comparison before");
            }
            else
            {
                ShowMessage("You have exceedded the limit of adding 7 schools,you cant add more");
                }
            }

            catch (Exception ex)
            {
                Mvx.Error("Error in SchoolDetailsView.cs : BottonAppBar_SaveToCompare : " + ex.ToString());

            }
        }
        //private void CommandHandler_save(IUICommand command)
        //{

        //    if (command.Label == "OK")
        //    {
        //        ShowMessage("You have exceedded the limit of adding 7 schools,you cant add more");
        //    }


        //}


        Task ShowMessage(String errorMessage)
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

        //private void SaveComparisonsButton()
        //{
        //    _dialog = new MessageDialog("Do you want to save comparions?", "Save Comparisons");
        //    _dialog.Commands.Add(new UICommand("OK", new UICommandInvokedHandler(CommandHandler)));
        //    _dialog.Commands.Add(new UICommand("Cancel", new UICommandInvokedHandler(CommandHandler)));

        //}

        //private void CommandHandler(IUICommand command)
        //{


        //    //var comparisonList = ((CompareschoolsViewModel)ViewModel).SchoolDetailsToCompare;
        //    //((CompareschoolsViewModel)ViewModel).SaveComparisons(comparisonList);
        //}




        private void BottonAppBar_WriteAReview(object sender, TappedRoutedEventArgs e)
        {
            try
            {
            if (BottomBar.IsOpen)
            {
                BottomBar.IsOpen = false;

            }
            if (topAppBar.IsOpen)
            {
                topAppBar.IsOpen = false;
            }
            postComment = new NewsArticlePostCommentControl();
            postComment.DataContext = (SchoolDetailsViewModel)ViewModel;



            postComment.Width = 700;
            postComment.Height = Window.Current.Bounds.Height;

            postCommentPopUp = new Popup();
            postCommentPopUp.Width = 700;
            postCommentPopUp.Height = Window.Current.Bounds.Height;
            postCommentPopUp.ChildTransitions = new TransitionCollection();
            postCommentPopUp.ChildTransitions.Add(new PaneThemeTransition());
            postCommentPopUp.IsLightDismissEnabled = true;
            postCommentPopUp.Child = postComment;
            postCommentPopUp.HorizontalOffset = Window.Current.Bounds.Width - postCommentPopUp.Width;
            postCommentPopUp.VerticalOffset = 0;
            postCommentPopUp.IsOpen = true;
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SchoolDetailsView.cs : BottonAppBar_WriteAReview : " + ex.ToString());
            }


        }


        public MessageDialog _dialog { get; set; }

        private void BottonAppBar_Share(object sender, TappedRoutedEventArgs e)
        {
            try
            {
            selectedSchool = ((SchoolDetailsViewModel)ViewModel).Details;
            }

            //DataTransferManager.ShowShareUI();
            catch (Exception ex)
            {
                Mvx.Error("Error in SchoolDetailsView.cs : BottonAppBar_Share : " + ex.ToString());
            }
        }

        //private void SchoolDetailsView_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        //{

        //    var req = args.Request;
        //    req.Data.Properties.Title = selectedSchool.SchoolName;

        //    req.Data.Properties.Description = selectedSchool.Address + "Here's` the school rating" + selectedSchool.Rating;
        //    req.Data.SetText(selectedSchool.SchoolName + "\n\t" + selectedSchool.Address + "\n" + selectedSchool.Rating);

        //    //var reference = RandomAccessStreamReference.CreateFromUri(new Uri(selectedSchool.SchoolLogo)); 
        //    //req.Data.Properties.Thumbnail = reference;
        //    //req.Data.SetBitmap(reference);



        //}
        

    }
}
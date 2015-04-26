using Cirrious.MvvmCross.Plugins.Messenger;
using Neudesic.Schoolistics.Core.Entities;
// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the BookmarkedSchoolItemsPageViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
using Neudesic.Schoolistics.Core.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Neudesic.Schoolistics.Core.ViewModels
{
    /// <summary>
    /// Define the BookmarkedSchoolItemsPageViewModel type.
    /// </summary>
    public class BookmarkedSchoolItemsPageViewModel : BaseViewModel
    {
        private IBookmarkedSchoolService _bookmarkedSchoolService;
        public BookmarkedSchoolItemsPageViewModel(IMvxMessenger messenger, IBookmarkedSchoolService bookmarkedSchoolService)
        {
            _bookmarkedSchoolService = bookmarkedSchoolService;

            var cachedBookmarks = _bookmarkedSchoolService.GetCachedBookmarkedSchools();

            if (cachedBookmarks == null)
                _bookmarkedSchoolService.GetBookmarkedSchools(BookmarkedSchoolsSuccess, BookmarkedSchoolsFailure);
            else
                BookmarkedSchoolDetailsPage = new ObservableCollection<BookmarkedSchools>(cachedBookmarks);

        }



        private ObservableCollection<BookmarkedSchools> bookmarkedSchoolDetailsPage;
        public ObservableCollection<BookmarkedSchools> BookmarkedSchoolDetailsPage
        {
            get { return bookmarkedSchoolDetailsPage; }
            set { bookmarkedSchoolDetailsPage = value; RaisePropertyChanged(() => BookmarkedSchoolDetailsPage); }
        }

        public void BookmarkedSchoolsSuccess(ResponseMessage<Object> response)
        {
            if (response.SuccessCode == 1)
            {
                var bookmarkedschools = JsonConvert.DeserializeObject<List<BookmarkedSchools>>(response.Data.ToString());

                _bookmarkedSchoolService.SetBookmarkedSchools(bookmarkedschools);


                BookmarkedSchoolDetailsPage = new ObservableCollection<BookmarkedSchools>(bookmarkedschools);

            }
        }

        public void BookmarkedSchoolsFailure(Exception e)
        {
            var message = e.Message;
        }

    }
}

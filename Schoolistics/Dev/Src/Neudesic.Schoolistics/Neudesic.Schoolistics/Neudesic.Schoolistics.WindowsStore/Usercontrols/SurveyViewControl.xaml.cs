using Cirrious.CrossCore;
using Neudesic.Schoolistics.Core.Entities;
using Neudesic.Schoolistics.Core.Services;
using Neudesic.Schoolistics.Core.Utils;
using Neudesic.Schoolistics.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Neudesic.Schoolistics.WindowsStore.Usercontrols
{
    public sealed partial class SurveyViewControl : UserControl
    {
        
        public event ItemClickEventHandler listItemClicked;
        public SurveyViewControl()
        {
            try
            {
                this.InitializeComponent();
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SurveyViewControl.Xaml.cs : Initializemethod: " + ex.ToString());
            }
        }
        private async void SurveyoptionsListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (Utils.Username != null)
                {
                    var question = this.DataContext as Survey;
                    this.listItemClicked(question, e);
                }
                else
                {
                    await ShowMessage("Please login before you vote");
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SurveyViewControl.Xaml.cs : SurveyoptionsListView_ItemClick: " + ex.ToString());
            }
        }

        Task ShowMessage(String errorMessage)
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
    }
}

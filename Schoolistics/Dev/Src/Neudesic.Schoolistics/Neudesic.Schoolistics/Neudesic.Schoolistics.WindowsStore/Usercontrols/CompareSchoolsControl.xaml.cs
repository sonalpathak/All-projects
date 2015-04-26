using Neudesic.Schoolistics.Core.Entities;
using Neudesic.Schoolistics.Core.Services;
using Neudesic.Schoolistics.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Collections;
using Neudesic.Schoolistics.WindowsStore.Views;
using Cirrious.CrossCore;


// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Neudesic.Schoolistics.WindowsStore.Usercontrols
{
    public sealed partial class CompareSchoolsControl : UserControl
    {        
        public event RoutedEventHandler _selectedSchoolsList;

        public event RoutedEventHandler _popUpCloseEvent;

        public CompareSchoolsControl()
        {
            try
            {
                this.InitializeComponent();
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in CompareSchoolControl.cs :CompareSchoolConstructor: " + ex.ToString());

            }
        }

        private void SchoolsSelectedBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<string> schoolID = addSchoolListView.SelectedItems.Cast<School>()
                                     .Select(item => item.SchoolId)
                                     .ToList();
                this._selectedSchoolsList(schoolID, e);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in CompareSchoolControl.cs :SchoolsSelectedBtn_Click: " + ex.ToString());

            }

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this._popUpCloseEvent(sender, e);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in CompareSchoolControl.cs :CancelButton_Click: " + ex.ToString());

            }
        }    
       

        private void tbsearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var data = this.DataContext as CompareschoolsViewModel;
                if (tbsearchBox.Text.Length >= 3)
                {
                    var schoolList = data.SchoolDetails.Where(r => r.SchoolName.ToLower().Contains(tbsearchBox.Text)).ToList();
                    addSchoolListView.ItemsSource = schoolList;
                }
                else
                {
                    addSchoolListView.ItemsSource = data.SchoolDetails;
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in CompareSchoolControl.cs :tbsearchBox_TextChanged: " + ex.ToString());

            }

        }


        private void tbsearchBox_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            try
            {
                if (tbsearchBox.Text != null)
                {
                    tbsearchBox.Text = "";
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in CompareSchoolControl.cs :tbsearchBox_PointerEntered: " + ex.ToString());

            }
            
        }

        private void tbsearchBox_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            try
            {
                if (tbsearchBox.Text == null)
                {
                    tbsearchBox.Text = "Search for school";
                }
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in CompareSchoolControl.cs :tbsearchBox_PointerExited: " + ex.ToString());

            }
        }

        

     
    }
}

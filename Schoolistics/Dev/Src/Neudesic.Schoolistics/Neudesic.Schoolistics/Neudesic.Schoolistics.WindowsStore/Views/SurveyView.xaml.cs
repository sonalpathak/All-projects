// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the SurveyView.xaml type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
using Windows.UI.Xaml.Controls;
using De.TorstenMandelkow.MetroChart;
using Neudesic.Schoolistics.Core.ViewModels;
using Neudesic.Schoolistics.Core.Entities;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Cirrious.CrossCore;
using System;

namespace Neudesic.Schoolistics.WindowsStore.Views
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class SurveyView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SurveyView"/> class.
        /// </summary>
        public SurveyView()
        {
            try
            {
                this.InitializeComponent();
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SurveyView.Xaml.cs : Initializemethod: " + ex.ToString());
            }

        }
        private void SurveyViewControl_listItemClicked(object sender, ItemClickEventArgs e)
        {
            try
            {
                var clickedQuestion = sender as Survey;
                var Option = e.ClickedItem as SurveyOption;
                ((SurveyViewModel)ViewModel).VoteOnSurveys(clickedQuestion.SurveyId, Option.OptionId);
                ((SurveyViewModel)ViewModel).SaveUserVotedSurvey(clickedQuestion.SurveyId);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in SurveyView.Xaml.cs:SurveyViewControl_listItemClicked  : " + ex.ToString());
            }
        }
    }
}

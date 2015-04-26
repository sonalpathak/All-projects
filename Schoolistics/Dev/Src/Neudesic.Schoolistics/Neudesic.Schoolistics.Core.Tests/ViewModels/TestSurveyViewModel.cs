// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestSurveyViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Neudesic.Schoolistics.Core.Tests.ViewModels
{
    using Core.ViewModels;
    using Neudesic.Schoolistics.Core.Services;
    using NUnit.Framework;

    /// <summary>
    /// Defines the TestSurveyViewModel type.
    /// </summary>
    [TestFixture]
    public class TestSurveyViewModel : BaseTest
    {
        /// <summary>
        /// The SurveyViewModel.
        /// </summary>
        private SurveyViewModel surveyViewModel;

        /// <summary>
        /// Creates an instance of the object to test.
        /// To allow Ninja automatically create the unit tests
        /// this method should not be changed.
        /// </summary>
        public override void CreateTestableObject()
        {
            
            this.surveyViewModel = new SurveyViewModel(new SurveyPageService(new RestService()));
        }
    }
}

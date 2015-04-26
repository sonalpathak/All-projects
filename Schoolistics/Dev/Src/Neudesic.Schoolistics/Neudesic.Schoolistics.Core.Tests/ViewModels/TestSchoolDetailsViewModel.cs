// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestSchoolDetailsViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Neudesic.Schoolistics.Core.Tests.ViewModels
{
    using Core.ViewModels;

    using NUnit.Framework;

    /// <summary>
    /// Defines the TestSchoolDetailsViewModel type.
    /// </summary>
    [TestFixture]
    public class TestSchoolDetailsViewModel : BaseTest
    {
        /// <summary>
        /// The SchoolDetailsViewModel.
        /// </summary>
        private SchoolDetailsViewModel schoolDetailsViewModel;

        /// <summary>
        /// Creates an instance of the object to test.
        /// To allow Ninja automatically create the unit tests
        /// this method should not be changed.
        /// </summary>
        public override void CreateTestableObject()
        {
           // this.schoolDetailsViewModel = new SchoolDetailsViewModel();
        }
    }
}

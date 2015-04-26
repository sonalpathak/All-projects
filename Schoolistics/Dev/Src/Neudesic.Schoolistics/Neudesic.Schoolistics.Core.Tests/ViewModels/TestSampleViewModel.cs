// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestSampleViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Neudesic.Schoolistics.Core.Tests.ViewModels
{
    using Core.ViewModels;

    using NUnit.Framework;

    /// <summary>
    /// Defines the TestSampleViewModel type.
    /// </summary>
    [TestFixture]
    public class TestSampleViewModel : BaseTest
    {
        /// <summary>
        /// The SampleViewModel.
        /// </summary>
        private SampleViewModel sampleViewModel;

        /// <summary>
        /// Creates an instance of the object to test.
        /// To allow Ninja automatically create the unit tests
        /// this method should not be changed.
        /// </summary>
        public override void CreateTestableObject()
        {
            this.sampleViewModel = new SampleViewModel();
        }
    }
}

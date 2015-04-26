// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestSearchresultsViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Neudesic.Schoolistics.Core.Tests.ViewModels
{
    using Core.ViewModels;
    using Neudesic.Schoolistics.Core.Services;
    using NUnit.Framework;

    /// <summary>
    /// Defines the TestSearchresultsViewModel type.
    /// </summary>
    [TestFixture]
    public class TestSearchresultsViewModel : BaseTest
    {
        /// <summary>
        /// The first view model.
        /// </summary>
        private SearchresultsViewModel searchresultsViewModel;

        /// <summary>
        /// Creates an instance of the object to test.
        /// To allow Ninja automatically create the unit tests
        /// this method should not be changed.
        /// </summary>
        public override void CreateTestableObject()
        {
          //  this.searchresultsViewModel = new SearchresultsViewModel(new SearchResultsService());
        }

        ///// <summary>
        ///// Tests my property.
        ///// </summary>
        //[Test]
        //public void TestMyProperty()
        //{
        //    //// arrange
        //    bool changed = false;

        //    this.searchresultsViewModel.PropertyChanged += (sender, args) =>
        //        {
        //            if (args.PropertyName == "MyProperty")
        //            {
        //                changed = true;
        //            }
        //        };

        //    //// act
        //    this.searchresultsViewModel.MyProperty = "Hello MvvmCross";

        //    //// assert
        //    Assert.AreEqual(changed, true);
        //}

        ///// <summary>
        ///// Tests my command.
        ///// </summary>
        //[Test]
        //public void TestMyCommand()
        //{
        //    //// arrange

        //    //// act
        //    this.searchresultsViewModel.MyCommand.Execute(null);

        //    //// assert
        //}
    }
}

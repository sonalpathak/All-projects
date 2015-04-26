// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestCompareschoolsViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Neudesic.Schoolistics.Core.Tests.ViewModels
{
    using Core.ViewModels;
    using Neudesic.Schoolistics.Core.Services;
    using NUnit.Framework;

    /// <summary>
    /// Defines the TestCompareschoolsViewModel type.
    /// </summary>
    [TestFixture]
    public class TestCompareschoolsViewModel : BaseTest
    {
        /// <summary>
        /// The first view model.
        /// </summary>
        private CompareschoolsViewModel compareschoolsViewModel;

        /// <summary>
        /// Creates an instance of the object to test.
        /// To allow Ninja automatically create the unit tests
        /// this method should not be changed.
        /// </summary>
        public override void CreateTestableObject()
        {

            //this.compareschoolsViewModel = new CompareschoolsViewModel( new SchoolComparisonService(new RestService()),);
        }

        ///// <summary>
        ///// Tests my property.
        ///// </summary>
        //[Test]
        //public void TestMyProperty()
        //{
        //    //// arrange
        //    bool changed = false;

        //    this.compareschoolsViewModel.PropertyChanged += (sender, args) =>
        //        {
        //            if (args.PropertyName == "MyProperty")
        //            {
        //                changed = true;
        //            }
        //        };

        //    //// act
        //    this.compareschoolsViewModel.MyProperty = "Hello MvvmCross";

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
        //    this.compareschoolsViewModel.MyCommand.Execute(null);

        //    //// assert
        //}
    }
}

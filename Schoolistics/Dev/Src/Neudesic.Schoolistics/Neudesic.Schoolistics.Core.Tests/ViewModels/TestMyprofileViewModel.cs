// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestMyprofileViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Neudesic.Schoolistics.Core.Tests.ViewModels
{
    using Core.ViewModels;

    using NUnit.Framework;

    /// <summary>
    /// Defines the TestMyprofileViewModel type.
    /// </summary>
    [TestFixture]
    public class TestMyprofileViewModel : BaseTest
    {
        /// <summary>
        /// The first view model.
        /// </summary>
        private MyprofileViewModel myprofileViewModel;

        /// <summary>
        /// Creates an instance of the object to test.
        /// To allow Ninja automatically create the unit tests
        /// this method should not be changed.
        /// </summary>
        //public override void CreateTestableObject()
        //{
        //    this.myprofileViewModel = new MyprofileViewModel();
        //}

        ///// <summary>
        ///// Tests my property.
        ///// </summary>
        //[Test]
        //public void TestMyProperty()
        //{
        //    //// arrange
        //    bool changed = false;

        //    this.myprofileViewModel.PropertyChanged += (sender, args) =>
        //        {
        //            if (args.PropertyName == "MyProperty")
        //            {
        //                changed = true;
        //            }
        //        };

        //    //// act
        //    this.myprofileViewModel.MyProperty = "Hello MvvmCross";

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
        //    this.myprofileViewModel.MyCommand.Execute(null);

        //    //// assert
        //}
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestForgotpasswordViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Neudesic.Schoolistics.Core.Tests.ViewModels
{
    using Core.ViewModels;

    using NUnit.Framework;

    /// <summary>
    /// Defines the TestForgotpasswordViewModel type.
    /// </summary>
    [TestFixture]
    public class TestForgotpasswordViewModel : BaseTest
    {
        /// <summary>
        /// The first view model.
        /// </summary>
        private ForgotpasswordViewModel forgotpasswordViewModel;

        /// <summary>
        /// Creates an instance of the object to test.
        /// To allow Ninja automatically create the unit tests
        /// this method should not be changed.
        /// </summary>
        public override void CreateTestableObject()
        {
            this.forgotpasswordViewModel = new ForgotpasswordViewModel();
        }

        ///// <summary>
        ///// Tests my property.
        ///// </summary>
        //[Test]
        //public void TestMyProperty()
        //{
        //    //// arrange
        //    bool changed = false;

        //    this.forgotpasswordViewModel.PropertyChanged += (sender, args) =>
        //        {
        //            if (args.PropertyName == "MyProperty")
        //            {
        //                changed = true;
        //            }
        //        };

        //    //// act
        //    this.forgotpasswordViewModel.MyProperty = "Hello MvvmCross";

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
        //    this.forgotpasswordViewModel.MyCommand.Execute(null);

        //    //// assert
        //}
    }
}

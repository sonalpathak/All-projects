// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestRegistrationViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Neudesic.Schoolistics.Core.Tests.ViewModels
{
    using Core.ViewModels;

    using NUnit.Framework;

    /// <summary>
    /// Defines the TestRegistrationViewModel type.
    /// </summary>
    [TestFixture]
    public class TestRegistrationViewModel : BaseTest
    {
        /// <summary>
        /// The first view model.
        /// </summary>
        private RegistrationViewModel registrationViewModel;

        /// <summary>
        /// Creates an instance of the object to test.
        /// To allow Ninja automatically create the unit tests
        /// this method should not be changed.
        /// </summary>
        //public override void CreateTestableObject()
        //{
        //    this.registrationViewModel = new RegistrationViewModel();
        //}

        ///// <summary>
        ///// Tests my property.
        ///// </summary>
        //[Test]
        //public void TestMyProperty()
        //{
        //    //// arrange
        //    bool changed = false;

        //    this.registrationViewModel.PropertyChanged += (sender, args) =>
        //        {
        //            if (args.PropertyName == "MyProperty")
        //            {
        //                changed = true;
        //            }
        //        };

        //    //// act
        //    this.registrationViewModel.MyProperty = "Hello MvvmCross";

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
        //    this.registrationViewModel.MyCommand.Execute(null);

        //    //// assert
        //}
    }
}

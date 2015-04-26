// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestEditprofileViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Neudesic.Schoolistics.Core.Tests.ViewModels
{
    using Core.ViewModels;

    using NUnit.Framework;

    /// <summary>
    /// Defines the TestEditprofileViewModel type.
    /// </summary>
    [TestFixture]
    public class TestEditprofileViewModel : BaseTest
    {
        /// <summary>
        /// The first view model.
        /// </summary>
        private EditprofileViewModel editprofileViewModel;

        /// <summary>
        /// Creates an instance of the object to test.
        /// To allow Ninja automatically create the unit tests
        /// this method should not be changed.
        /// </summary>
        public override void CreateTestableObject()
        {
           // this.editprofileViewModel = new EditprofileViewModel();
        }

        ///// <summary>
        ///// Tests my property.
        ///// </summary>
        //[Test]
        //public void TestMyProperty()
        //{
        //    //// arrange
        //    bool changed = false;

        //    this.editprofileViewModel.PropertyChanged += (sender, args) =>
        //        {
        //            if (args.PropertyName == "MyProperty")
        //            {
        //                changed = true;
        //            }
        //        };

        //    //// act
        //    this.editprofileViewModel.MyProperty = "Hello MvvmCross";

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
        //    this.editprofileViewModel.MyCommand.Execute(null);

        //    //// assert
        //}
    }
}

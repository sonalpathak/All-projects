using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Runtime;

namespace NeuBeacon.Droid
{
    [Application(Debuggable = true, ManageSpaceActivity = typeof(Welcome))]
    public class NeuBeaconsApplication: Application
    {
        public ApplicationState ApplicationState { get; private set;}
        public NeuBeaconsApplication(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
            this.ApplicationState = new ApplicationState();
        }

        public override void OnCreate()
        {
            base.OnCreate();
        }
    }
}

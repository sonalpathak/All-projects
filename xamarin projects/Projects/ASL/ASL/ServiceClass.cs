using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace ASL
{
	[Service]
     class ServiceClass:Service
	{
		#region implemented abstract members of Service

		public override IBinder OnBind (Intent intent)
		{
			throw new NotImplementedException ();
		}

		#endregion

		public ServiceClass()
		{

		}

//		public override bool BindService (Intent service, IServiceConnection conn, Bind flags)
//		{
//			return base.BindService (service, conn, flags);
//		}

//		public IBinder bind(Intent i)
//		{
//			throw new UnsupportedOperationException("Not yet implemented");
//		}
		public override void OnCreate ()
		{
			base.OnCreate ();
			Toast.MakeText (this, "starting services", ToastLength.Long).Show ();
		}
		public override StartCommandResult OnStartCommand (Intent intent, StartCommandFlags flags, int startId)
		{
			Toast.MakeText (this, "starting services", ToastLength.Long).Show ();
			return base.OnStartCommand (intent, flags, startId);
		}
	}
}


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

namespace ApplicationStartOnBoot
{
	[BroadcastReceiver]
	[IntentFilter(new[]{Android.Content.Intent.ActionBootCompleted},Categories = new[]{Android.Content.Intent.CategoryDefault})]
	class ReceiveBoot:BroadcastReceiver
	{
		#region implemented abstract members of BroadcastReceiver
		public override void OnReceive (Context context, Intent intent)
		{
			Toast.MakeText (context, "Starting on boot", ToastLength.Long).Show ();
			if ((intent.Action != null) &&
				(intent.Action ==
					Android.Content.Intent.ActionBootCompleted))
			{ // Start the service or activity
//context.ApplicationContext.StartService(new Intent(context, typeof(MainActivity)));

				Android.Content.Intent start = new Android.Content.Intent(context, typeof(MainActivity));

// my activity name is MainActivity replace it with yours
				start.AddFlags(ActivityFlags.NewTask);
				context.ApplicationContext.StartActivity(start);
			
		}
		#endregion
	}
	}
}


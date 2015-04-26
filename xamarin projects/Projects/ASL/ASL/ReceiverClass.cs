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
using Android.Util;

namespace ASL
{
	[BroadcastReceiver]

	class ReceiverClass:BroadcastReceiver
	{
	    static string TAG = "BootCompletedReceiver";
		public override void OnReceive (Context context, Intent intent)
		{
			Intent service = new Intent (context, typeof(ServiceClass));
			context.StartService (service);
			Log.WriteLine (LogPriority.Info, TAG, "starting service");


			Intent i = new Intent (context, typeof(MainActivity));
			i.AddFlags (ActivityFlags.FromBackground);
			context.StartService (i);
		}
	}
}


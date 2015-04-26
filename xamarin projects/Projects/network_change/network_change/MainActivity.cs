using System;
//using System.Linq;
//using System.Text;
//using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Net.Wifi;




namespace network_change
{
	[Activity (Label = "network_change", MainLauncher = true)]
	public class MainActivity : Activity
	{
	

		private ConnectivityManager er=null;
		private ConnectivityReceiver receiver = null;
		private TextView txtNetworkInfo = null;

		//int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			var c=Context.ConnectivityService;
			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			txtNetworkInfo = (TextView)FindViewById (Resource.Id.txtNetworkInfo);

			// Get our button from the layout resource,
			// and attach an event to it
//			Button button = FindViewById<Button> (Resource.Id.myButton);
//			
//			button.Click += delegate {
//				button.Text = string.Format ("{0} clicks!", count++);
//			};
		}
	}
}



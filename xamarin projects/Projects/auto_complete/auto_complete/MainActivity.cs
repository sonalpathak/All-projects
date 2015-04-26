using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Util;

namespace auto_complete
{
	[Activity (Label = "auto_complete", MainLauncher = true)]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			Log.Info ("Beginning tag!!!","oncreate has started");
			base.OnCreate (bundle);


			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			TextView button = FindViewById<TextView> (Resource.Id.button1);
			
			button.Click += delegate {
				button.Text = string.Format ("{0} clicks!", count++);
			};
			Log.Info ("End tag!!!","Oncreate has just ended!");
		}
	}
}



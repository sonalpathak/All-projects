using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace layout
{
	[Activity (Label = "layout", MainLauncher = true)]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			//var layout = new LinearLayout (this);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			TextView tv = FindViewById<TextView> (Resource.Id.textView1);


			
			button.Click += delegate {
				button.Text = string.Format ("{0} clicks!", count++);
				//tv.Text="kalpana";
			};
		}
	}
}



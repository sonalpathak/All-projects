using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;

namespace xml_reader
{
	[Activity (Label = "xml_reader", MainLauncher = true)]
	public class MainActivity : Activity
	{
		//int count = 1;
		TextView tv;


		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			tv = FindViewById<TextView> (Resource.Id.textView1);
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += delegate {
				var sdCardPath=Android.OS.Environment.ExternalStorageDirectory.Path;
				var xmlFilePath=Path.Combine(sdCardPath,"SampleXmlFile.xml");

			};
		}
	}
}



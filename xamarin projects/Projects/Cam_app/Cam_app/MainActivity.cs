using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Cam_app
{
	//Java.IO.File _file;
	//Java.IO.File _dir;
	//ImageView _imageView;
	[Activity (Label = "Cam_app", MainLauncher = true)]
	public class MainActivity : Activity
	{
		Java.IO.File _file;
		Java.IO.File _dir;
		ImageView _imageView;
		int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			if(IsThereAnAppToTakePictures()){

			}
			private bool IsThereAnAppToTakePictures(){
			}

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += delegate {
				button.Text = string.Format ("{0} clicks!", count++);
			};
		}
	}
}



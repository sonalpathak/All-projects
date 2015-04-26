using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace face_detection
{
	[Activity (Label = "face_detection", MainLauncher = true)]
	public class MainActivity : Activity
	{
		//int count = 1;
		private static int Take_picture_code=10;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.take_picture);

			
			button.Click += delegate {
				openCamera();
				//button.Text = string.Format ("{0} clicks!", count++);
			};
		}
	
//		protected override void onActivityResult (int resultcode,int requestcode);
		private void openCamera()
		{
			Intent i = new Intent (Android.Provider.MediaStore.ActionImageCapture);
			StartActivityForResult (i, Take_picture_code);
		}
	
	}
}



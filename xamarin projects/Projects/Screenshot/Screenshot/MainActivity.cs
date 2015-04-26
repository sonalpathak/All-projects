using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
//using Android.Widget.LinearLayout;
using Android.OS;
using Android.Views.Animations;
using Android.Graphics.Drawables;
using Android.Graphics;




namespace Screenshot
{
	[Activity (Label = "Screenshot", MainLauncher = true)]
	public class MainActivity : Activity
	{
		LinearLayout l1;
		ImageView image;
		//	int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			l1 = ((LinearLayout)FindViewById (Resource.Id.linearlayout));
			//l1.SetBackgroundDrawable (true);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += delegate {

//				button.Text = string.Format ("{0} clicks!", count++);
				View view1 = l1.GetChildAt(0);

				bool x=view1.DrawingCacheEnabled;

				//	image.setDrawingCacheEnabled(true);
				Toast.MakeText(this,x.ToString(),ToastLength.Short).Show();
				Bitmap bm = view1.GetDrawingCache (true);
				Bitmap b=l1.GetDrawingCache(x);
				BitmapDrawable bitmapDrawable = new BitmapDrawable (bm);
				image = (ImageView)FindViewById (Resource.Id.imageView1);
				//image.SetBackgroundResource(bitmapDrawable);
				//image.setBackgroundDrawable(bitmapDrawable);


			};
		}


		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			var inflater = MenuInflater;
			inflater.Inflate(Resource.Menu.Main, menu);        
			return true;
		}

//		public override bool OnCreateOptionsMenu(Menu menu,MenuInflater mf)
//		{
//			MenuInflater().Inflate(Resource.menu.screen, menu);
//			return true;
//		}
	}
}



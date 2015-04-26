using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using Android.Content.Res;
using Java.IO;

namespace ASL
{
	[Activity (Label = "ASL",MainLauncher = true)]
	public class MainActivity : Activity
	{
		string mPath;

		public	void GetStream (string filePath)
		{
			Java.IO.FileInputStream fi = new Java.IO.FileInputStream (filePath);
			fi.Read ();
		}

		public void saveBitmap (Bitmap bmp)
		{
			//Bitmap bmpimage = Utils;


			OutputStream output = null;
			var ImageName = DateTime.Now.Millisecond+","+ DateTime.Now.Hour+","+DateTime.Now.DayOfWeek+"-"+DateTime.Now.Month+"-"+DateTime.Now.Year;
			Toast.MakeText (this, ImageName, ToastLength.Long).Show ();


			var path=Android.OS.Environment.ExternalStorageDirectory.Path;
			var dir = Android.OS.Environment.DirectoryPictures;

			var filePath = System.IO.Path.Combine (path, ""+ImageName+".png");
			Toast.MakeText (this, filePath, ToastLength.Long).Show ();

			File fnew = new File (filePath);
			var val = System.IO.File.Exists (filePath);

			if (!val) {
				output = new FileOutputStream (filePath);
				//Bitmap b = BitmapFactory.DecodeFile (filePath);
				ByteArrayOutputStream b=new ByteArrayOutputStream();
				//out1=new FileOutputStream (new System.IO.Stream(filePath));
				var fo = new FileOutputStream (fnew);
				var newFo=new OutputStreamWriter(new Android.Runtime.OutputStreamInvoker(fo));
//				bmp.Compress(Bitmap.CompressFormat.Jpeg,90,fo);
//				fo.Flush ();
//				GetStream (filePath);


				//var file = new FileOutputStream (filePath);
				bmp.Compress (Bitmap.CompressFormat.Png, 90, new Android.Runtime.OutputStreamInvoker(fo));
				fo.Flush ();
				fo.Close ();
				GetStream (filePath);

			}
			//var path=Android.OS.Environment.GetFolderPath (System.Environment.SpecialFolder.ApplicationData);
		}

		//Bitmap bmp;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			mPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += delegate {
				LinearLayout lv=FindViewById<LinearLayout>(Resource.Id.LL);
				View v=LayoutInflater.Inflate(Resource.Layout.Main,null);
				View newView=v.RootView;
				View LinearView=lv.RootView;
				lv.DrawingCacheEnabled=true;
				newView.DrawingCacheEnabled=true;
				//DrawingCacheQuality=DrawingCacheQuality.High;
				if(lv.DrawingCacheEnabled)
				{
					//bmp=newView.DrawingCache;


					Bitmap bmp;
					bmp=Bitmap.CreateBitmap(lv.GetDrawingCache(lv.DrawingCacheEnabled));
					bmp.GetScaledHeight(10);
					saveBitmap(bmp);
					//newView.DrawingCacheEnabled=false;
				}



			};
		}
	}
}



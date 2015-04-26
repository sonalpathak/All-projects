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
using Android.Graphics;
using Java.IO;

namespace BackGroundService
{
	[Activity (Label = "service1")]			
	public class service1 : Activity
	{
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
				//GetStream (filePath);

			}
			//var path=Android.OS.Environment.GetFolderPath (System.Environment.SpecialFolder.ApplicationData);
		}
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			View v=Window.DecorView.RootView;
			v.DrawingCacheEnabled=true;
			v.Layout (0, 0, 1000, 100);
			//newView.DrawingCacheEnabled=true;
			//DrawingCacheQuality=DrawingCacheQuality.High;
			if(v.DrawingCacheEnabled)
			{
				//bmp=newView.DrawingCache;


				Bitmap bmp;
				Bitmap b=v.GetDrawingCache (true);
				bmp=Bitmap.CreateBitmap(v.GetDrawingCache(v.DrawingCacheEnabled));
				bmp.GetScaledHeight(10);
				saveBitmap(bmp);
				//v.DrawingCacheEnabled=false;
			}




			// Create your application here
		}

		protected override void OnResume ()
		{
			base.OnResume ();
			View v=Window.DecorView.RootView;
			v.DrawingCacheEnabled=true;
			v.Layout (0, 0, 1000, 100);
			//newView.DrawingCacheEnabled=true;
			//DrawingCacheQuality=DrawingCacheQuality.High;
			if(v.DrawingCacheEnabled)
			{
				//bmp=newView.DrawingCache;


				Bitmap bmp;
				Bitmap b=v.GetDrawingCache (true);
				bmp=Bitmap.CreateBitmap(v.GetDrawingCache(v.DrawingCacheEnabled));
				bmp.GetScaledHeight(10);
				saveBitmap(bmp);
				//v.DrawingCacheEnabled=false;
			}
		}


	}
}


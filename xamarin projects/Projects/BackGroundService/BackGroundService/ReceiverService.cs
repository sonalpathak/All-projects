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
using System.Threading;
using Android.Graphics;
using Java.Lang;

namespace BackGroundService
{
	[Service]
	class ReceiverService:Service
	{
		////Timer timer;
		Bitmap bmp;
		//Activity context;
		Context context;
		System.Threading.Timer _timer;
	

		#region implemented abstract members of Service
		public override IBinder OnBind (Intent intent)
		{
			throw new NotImplementedException ();
		}
		#endregion

		public void ScreenShot ()
		{


			//_timer = new Timer (x, null, 5 * 60, 4000);


			_timer = new System.Threading.Timer ((o) => {
				Intent i=new Intent(this,typeof(service1));
				i.SetFlags(ActivityFlags.NewTask);
//				i.SetClass(this.ApplicationContext,typeof(service1));
				//i.SetFlags(ActivityFlags.FromBackground);
				//	i.AddFlags(ActivityFlags.NewTask);
				//StartActivityForResult();

				StartActivity(i);
                Log.Debug ("SimpleService", "hello from simple service");}
            , null, 0, 4000);




			//var s = Service.UserService;
			//View v = (View)iV.Parent;

			//View v=this.DecorView.RootView;
			//View v =this.Window.DecorView.RootView;

			//View v = new View (this.ApplicationContext);

//			var newView=v.RootView;
//			newView.DrawingCacheEnabled = true;
//			if (newView.DrawingCacheEnabled) {
//				bmp = Bitmap.CreateBitmap (newView.GetDrawingCache (newView.DrawingCacheEnabled));
//			}
			//newView.
		}
//		public override void OnResume()
//		{
//			ScreenShot ();
//		}

//		public override void OnRebind (Intent intent)
//		{
//			base.OnRebind (intent);
//			ScreenShot ();
//		}

		public override void OnStart (Intent intent, int startId)
		{
			base.OnStart (intent, startId);
			Log.Debug ("Receiver Service", "Starting service...");
			ScreenShot ();
//
//			new Handler ().PostDelayed (new Runnable()
//				{
//					//protected Override void Runtime.Run()
////					{
////					Intent i1=new Intent(this,typeof(service1));
////					i.SetFlags(ActivityFlags.NewTask);
//////				
////
////					StartActivity(i1);
////					}
//
//				},4000);
		}


	}
}


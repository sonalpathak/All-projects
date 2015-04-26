using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.GoogleMaps;
using Android.gms.Maps;


namespace Maps_App
{
	[Activity (Label = "Maps_App", MainLauncher = true)]
	public class MainActivity : MapActivity
	{
		//int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
		
			  var mapFragment = new MapFragment ();
			FragmentTransaction fragmentTx = this.FragmentManager.BeginTransaction();
			fragmentTx.Add (Resource.Id.linearLayout1, mapFragment);
			fragmentTx.Commit ();

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
		}
		protected override bool IsRouteDisplayed
		{
			get
			{return false; }
		}
	}
}



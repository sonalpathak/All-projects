using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Locations;
using System.Collections.Generic;
using System.Linq;

namespace schoolistic_v1
{
	[Activity (Label = "schoolistic_v1", MainLauncher = true)]
	public class MainActivity : Activity,GoogleMap.IOnMapClickListener,ILocationListener
	{
		private MapFragment _mf;
		public GoogleMap gmap;
		string placesearchstr;
		Location location;
		LocationManager _lm;
		string name;



		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			_lm = (LocationManager)GetSystemService (LocationService);
			var c = new Criteria {
				Accuracy = Accuracy.Medium
			};
			var acceptableprovider = _lm.GetProviders (c, false);
			Location l1=_lm.GetLastKnownLocation(acceptableprovider.First());


			InitMapFragment ();
			SetupMapIfNeeded();
			//gmap.SetOnMapClickListener (this);
		}
		private void InitMapFragment()
		{
			_mf = FragmentManager.FindFragmentById (Resource.Id.frameLayout1) as MapFragment;
			if (_mf == null) {
				GoogleMapOptions op = new GoogleMapOptions ();
				op.InvokeMapType (GoogleMap.MapTypeNormal);
				FragmentTransaction fragTx = FragmentManager.BeginTransaction();
				_mf = MapFragment.NewInstance(op);
				fragTx.Add(Resource.Id.frameLayout1, _mf, "map");
				fragTx.Commit();
				place ();

			}

		}
		private void place()
		{
			placesearchstr= "https://maps.googleapis.com/maps/api/place/nearbysearch/" +

			                "json?location=" + location.Latitude + "," + location.Longitude +

			                "&radius=5000&sensor=true" +

			                "&types=food|store|police|hospital" + "&name=" + name +

			                "&key=AIzaSyB3mBmihJrXPapCoe-2PCAKJbG3KwIXrjs";
		}

		public void OnMapClick(LatLng arg)
		{
			MarkerOptions moptions = new MarkerOptions ().SetPosition (arg );
			gmap.AddMarker (moptions);
		}
//		private void AddMarkertomap ()
//		{
//			MarkerOptions moptions = new MarkerOptions ().SetPosition (LatLng );
//			gmap.AddMarker (moptions);
//		}
		private void  SetupMapIfNeeded()
		{
			if (gmap == null) {
				gmap = _mf.Map;
				if (gmap != null) {
					gmap.SetOnMapClickListener (this);
					//AddMarkertomap ();
					//	OnMapClick ();
				}
			}
		}
		public void OnLocationChanged(Location location)
		{
//			_currentLocation = location;
//			if (_currentLocation == null)
//			{
//				_latitudeText.Text = "Unable to determine your location.";
//				_longitudeText.Text = "";
//
//			}
//			else
//			{
//				_latitudeText.Text = String.Format("{0}", _currentLocation.Latitude);
//				_longitudeText.Text = string.Format ("{0}",_currentLocation.Longitude);
//
//			}
		}

		public void OnProviderDisabled(string provider) { }

		public void OnProviderEnabled(string provider) {
			Toast.MakeText (this, "kajdnjncd dmc ", ToastLength.Long);
		}

		public void OnStatusChanged(string provider, Availability status, Bundle extras) { }
	

	
	
}
}



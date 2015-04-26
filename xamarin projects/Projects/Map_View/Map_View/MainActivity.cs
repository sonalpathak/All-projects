using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Provider;



namespace Map_View
{
	[Activity (Label = "Map_View", MainLauncher = true)]
	public class MainActivity :Activity
	{
		private static readonly LatLng place1 = new LatLng(50.897778, 3.013333);
		private static readonly LatLng place2 = new LatLng(50.379444, 2.773611);
		private GoogleMap _map;
		private MapFragment _mapFragment;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			SetContentView(Resource.Layout.Main);

			InitMapFragment();

			SetupMapIfNeeded(); // It's not gauranteed that the map will be available at this point.

			SetupAnimateToButton();
			SetupZoomInButton();
			SetupZoomOutButton();
		}

		protected override void OnResume()
		{
			base.OnResume();
			SetupMapIfNeeded();
		}

		private void InitMapFragment()
		{
			_mapFragment = FragmentManager.FindFragmentByTag("map") as MapFragment;
			if (_mapFragment == null)
			{
				GoogleMapOptions mapOptions = new GoogleMapOptions()
					.InvokeMapType(GoogleMap.MapTypeSatellite)
					.InvokeZoomControlsEnabled(false)
					.InvokeCompassEnabled(true);

				FragmentTransaction fragTx = FragmentManager.BeginTransaction();
				_mapFragment = MapFragment.NewInstance(mapOptions);
				fragTx.Add(Resource.Id.map, _mapFragment, "map");
				fragTx.Commit();
			}
		}

		private void SetupAnimateToButton()
		{
			Button animateButton = FindViewById<Button>(Resource.Id.animateButton);
			animateButton.Click += (sender, e) =>{
				// Move the camera to theplace1 Memorial in Belgium.
				CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
				builder.Target(place1);
				builder.Zoom(18);
				builder.Bearing(155);
				builder.Tilt(25);
				CameraPosition cameraPosition = builder.Build();

				// AnimateCamera provides a smooth, animation effect while moving
				// the camera to the the position.
				_map.AnimateCamera(CameraUpdateFactory.NewCameraPosition(cameraPosition));
			};
		}

		private void SetupMapIfNeeded()
		{
			if (_map == null)
			{
				_map = _mapFragment.Map;
				if (_map != null)
				{
					MarkerOptions marker1 = new MarkerOptions();
					marker1.SetPosition(place2);
					marker1.SetTitle("place2");
					_map.AddMarker(marker1);

					MarkerOptions marker2 = new MarkerOptions();
					marker2.SetPosition(place1);
					marker2.SetTitle("place1");
					_map.AddMarker(marker2);

					// We create an instance of CameraUpdate, and move the map to it.
					CameraUpdate cameraUpdate = CameraUpdateFactory.NewLatLngZoom(place2, 15);
					_map.MoveCamera(cameraUpdate);
				}
			}
		}

		private void SetupZoomInButton()
		{
			Button zoomInButton = FindViewById<Button>(Resource.Id.zoomInButton);
			zoomInButton.Click += (sender, e) => { _map.AnimateCamera(CameraUpdateFactory.ZoomIn()); };
		}

		private void SetupZoomOutButton()
		{
			Button zoomOutButton = FindViewById<Button>(Resource.Id.zoomOutButton);
			zoomOutButton.Click += (sender, e) => { _map.AnimateCamera(CameraUpdateFactory.ZoomOut()); };
		}
	}
}




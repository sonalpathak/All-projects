using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Locations;
using Android.Util;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Text;
using Android.Media;
using Android.Telephony;
using Android.Content.PM;
using Android.Provider;

namespace MySecurity
{
	[Activity (Label = "MySecurity", MainLauncher = true)]
	public class MainActivity : Activity,ILocationListener
	{
		private Location _currentLocation;
		private LocationManager _locationManager;
		private TextView _latitudeText;
		private TextView _longitudeText;
	    private string _locationProvider;
		protected MediaPlayer player;
    	int count;
		protected override void OnCreate(Bundle bundle)
		{
			player = MediaPlayer.Create (this,Resource.Raw.police_alarm);
			base.OnCreate(bundle);
			SetContentView(Resource.Layout.Main);

			//_addressText = FindViewById<TextView>(Resource.Id.textView3);
			_latitudeText = FindViewById<TextView>(Resource.Id.txtLatitude);
			_longitudeText = FindViewById<TextView> (Resource.Id.txtLongitude);
			_locationManager = (LocationManager)GetSystemService(LocationService);

			var criteriaForLocationService = new Criteria
			{
				Accuracy = Accuracy.Medium
			};
			var acceptableLocationProviders = _locationManager.GetProviders (criteriaForLocationService, false);
			Location l=_locationManager.GetLastKnownLocation(acceptableLocationProviders.First());

			if (l == null) {
				_latitudeText.Text = 17.4417641.ToString();
				_longitudeText.Text = 78.3807515.ToString();
				OnLocationChanged (l);
			} else {
				_latitudeText.Text = l.Latitude.ToString();
				_longitudeText.Text = l.Longitude.ToString();
			}

			FindViewById<TextView>(Resource.Id.button1).Click += CallPolice_OnClick;
			FindViewById<TextView> (Resource.Id.button2).Click += alarm_click;
			FindViewById<TextView> (Resource.Id.contact).Click += contact;
			var sendsosIntent = FindViewById<Button> (Resource.Id.button3);

            
			sendsosIntent.Click += (sender, e) => {

			
				if(_latitudeText.Text=="")
				{
					Toast.MakeText(this,"Gps is looking for your latitude and longitude positions kindly hold on",ToastLength.Short).Show();
				}
				else {
					_locationManager = (LocationManager)GetSystemService(LocationService);

					var criteriaForLocationService1 = new Criteria
					{
						Accuracy = Accuracy.Medium
					};
					var acceptableLocationProviders1 = _locationManager.GetProviders (criteriaForLocationService1, false);
					Location l1=_locationManager.GetLastKnownLocation(acceptableLocationProviders1.First());



					string lo="http://maps.google.com/maps?q="+l1.Latitude+","+l1.Longitude+"&mode=driving";

					ISharedPreferences p = GetSharedPreferences ("Contacts", FileCreationMode.WorldReadable);
					String val=p.GetString("number","");
					ISharedPreferences p2 = GetSharedPreferences ("Contacts2", FileCreationMode.WorldReadable);
					String val2=p2.GetString("number2","");
					var s=val;
					var s2=val2;

					Intent i=new Intent(Android.Content.Intent.ActionView);
					i.PutExtra("address",s+";"+s2);
					i.PutExtra("sms_body",lo);
					i.SetType("vnd.android-dir/mms-sms");
					StartActivity(i);

			}

            };

			InitializeLocationManager();
		}

		private void Sos_Click(object sender,EventArgs args)
		{

			var smsUri = Android.Net.Uri.Parse("smsto:1234567890");
			var smsIntent = new Intent (Intent.ActionSendto, smsUri);
			smsIntent.PutExtra("please help!",_currentLocation);
			//smsIntent.PutExtra ("sms_body", _currentLocation);  
			StartActivity (smsIntent);
		}


		private void InitializeLocationManager()
		{
			_locationManager = (LocationManager)GetSystemService(LocationService);

			var criteriaForLocationService = new Criteria
			{
				Accuracy = Accuracy.Medium
			};
			var acceptableLocationProviders = _locationManager.GetProviders (criteriaForLocationService, false);

			if (acceptableLocationProviders.Any())
			{
				_locationProvider = acceptableLocationProviders.First();

			}
			else
			{
				_locationProvider = String.Empty;
			}
		}




		protected override void OnResume()
		{
			base.OnResume();
			_locationManager.RequestLocationUpdates(_locationProvider, 0, 0, this);
		}

		protected override void OnPause()
		{
			base.OnPause();
			_locationManager.RemoveUpdates(this);
		}






		private void contact(object s,EventArgs e)
		{
			StartActivity (typeof(AddContacts));
//			var intent = new Intent(Intent.ActionPick, ContactsContract.Contacts.ContentUri);
//			var intent2 = new Intent (Intent.ActionPick, ContactsContract.CommonDataKinds.Phone.ContentUri);
//			StartActivityForResult(intent2,110);
		}
		private void  CallPolice_OnClick(object sender, EventArgs eventArgs)
		{
			var uri = Android.Net.Uri.Parse ("tel:100");
			var intent = new Intent (Intent.ActionView, uri);  
			StartActivity (intent);
		}
		private void alarm_click(object s,EventArgs ea)
		{
			StartActivity (typeof(alarm));
		}

		public void OnLocationChanged(Location location)
		{
			_currentLocation = location;
			if (_currentLocation == null)
			{
				_latitudeText.Text = "Unable to determine your location.";
				_longitudeText.Text = "";

				}
			else
			{
				_latitudeText.Text = String.Format("{0}", _currentLocation.Latitude);
				_longitudeText.Text = string.Format ("{0}",_currentLocation.Longitude);

			}
		}

		public void OnProviderDisabled(string provider) { }

		public void OnProviderEnabled(string provider) {
			Toast.MakeText (this, "kajdnjncd dmc ", ToastLength.Long);
		 }

		public void OnStatusChanged(string provider, Availability status, Bundle extras) { }
		public void OnCompletion(MediaPlayer p)
		{
			p.Stop ();
			p.Release ();
		}
		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			if(requestCode == 100)
			{
					if (data != null)
				{
					var cursor1 = ManagedQuery(data.Data, null, null, null, null);
					if (cursor1.Count > 0) {
						cursor1.MoveToNext ();
					}
				}
				else
				{
					Toast.MakeText(this, "No contact picked", ToastLength.Long).Show();
				}
			}

			if (requestCode == 110) {
				//count = 0;
				if (data != null)
				{
					var cursor2 = ManagedQuery(data.Data, null, null, null, null);
					if(cursor2.Count > 0)
					{


						cursor2.MoveToNext();
						Toast.MakeText(this, "Got contact " +cursor2.GetString(cursor2.GetColumnIndex(ContactsContract.CommonDataKinds.Phone.Number))+ cursor2.GetString(cursor2.GetColumnIndex(ContactsContract.ContactsColumns.DisplayName)), ToastLength.Short).Show();	

						if (count == 0) {
							ISharedPreferences p = GetSharedPreferences ("Contacts", FileCreationMode.WorldReadable);
							var name = cursor2.GetString (cursor2.GetColumnIndex (ContactsContract.ContactsColumns.DisplayName));
							var number = cursor2.GetString (cursor2.GetColumnIndex (ContactsContract.CommonDataKinds.Phone.Number));	
							ISharedPreferencesEditor e = p.Edit ();
							e.PutString (name, number);
							e.PutString ("name_contact", name);
							e.PutString ("number", number);
							e.Commit ();
							String val = p.GetString (name, "");
							Toast.MakeText (this, val + name, ToastLength.Short).Show ();
						
							//break;
						} 
						if(count==1) {
							ISharedPreferences p = GetSharedPreferences ("Contacts2", FileCreationMode.WorldReadable);
							var name = cursor2.GetString (cursor2.GetColumnIndex (ContactsContract.ContactsColumns.DisplayName));
							var number = cursor2.GetString (cursor2.GetColumnIndex (ContactsContract.CommonDataKinds.Phone.Number));	
							ISharedPreferencesEditor e = p.Edit ();
							e.PutString (name, number);
							e.PutString ("name_contact", name);
							e.PutString ("number2", number);
							e.Commit ();
							String val = p.GetString (name, "");
							Toast.MakeText (this, val + name, ToastLength.Short).Show ();
						    Toast.MakeText (this, "count is now 1", ToastLength.Short).Show();
						}

					}	
					count++;
				}
				else
				{
					Toast.MakeText(this, "No contact picked", ToastLength.Long).Show();
				}
			}
		}


	}
}



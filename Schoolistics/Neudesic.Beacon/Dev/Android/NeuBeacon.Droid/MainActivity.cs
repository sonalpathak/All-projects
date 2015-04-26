using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using RadiusNetworks.IBeaconAndroid;
//using Region = RadiusNetworks.IBeaconAndroid.Region;
using Color = Android.Graphics.Color;
using Android.Support.V4.App;
using NeuBeacons.Core;
using Android.Graphics.Drawables;
using System.Collections.Generic;

namespace NeuBeacon.Droid
{
	//[Activity(Label = "Neu Beacons", MainLauncher = false, LaunchMode = LaunchMode.SingleTask)]
    [Activity(Label = "Neu Beacons")]
	public class MainActivity : LayoutActivity, IBeaconConsumer
	{
		//private const string UUID = "e4C8A4FCF68B470D959F29382AF72CE7";
		private const string regiodId = "";
        //First is for AboutNeudesic, Carrers and TechQuiz
        private List<string> UUIDs = new List<string>() { "2F234454-CF6D-4A0F-ADF2-F4911BA9FFA6", "E2C56DB5-DFFB-48D2-B060-D0F5A71096E0", "5A4BCFCE-174E-4BAC-A814-092E77F6B7E5" };

		bool _paused;
		IBeaconManager _iBeaconManager;
		MonitorNotifier _monitorNotifier;
		RangeNotifier _rangeNotifier;
		Region _monitoringRegion;
		Region _rangingRegion;
		EditText _textBeaconId;
		EditText _textBeaconName;
		EditText _textBeaconDescription;
		Button _saveButton;
		bool _newBeaconDetected;
		IBeacon _iBeacon;
        private AnimationDrawable _aboutGlowDrawable;
        private AnimationDrawable _neuCareersGlowDrawable;
        private AnimationDrawable _newQuizDrawable;
        private string detectedUUID;

		int _previousProximity;


		public MainActivity()
		{
			_iBeaconManager = IBeaconManager.GetInstanceForApplication(this);

			_monitorNotifier = new MonitorNotifier();
			_rangeNotifier = new RangeNotifier();

			_monitoringRegion = new Region(regiodId, null, null, null);
			_rangingRegion = new Region(regiodId, null, null, null);
		}

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			SetContentView(Resource.Layout.Main);

            _iBeaconManager.Bind(this);

            _monitorNotifier.EnterRegionComplete += EnteredRegion;
            _monitorNotifier.ExitRegionComplete += ExitedRegion;

            _rangeNotifier.DidRangeBeaconsInRegionComplete += RangingBeaconsInRegion;

            // Load the animation from resources
            _aboutGlowDrawable = (AnimationDrawable)Resources.GetDrawable(Resource.Drawable.aboutglow);
            _neuCareersGlowDrawable = (AnimationDrawable)Resources.GetDrawable(Resource.Drawable.careersglow);
            _newQuizDrawable = (AnimationDrawable)Resources.GetDrawable(Resource.Drawable.quizglow);

            ImageView imageViewAboutNeudesic = FindViewById<ImageView>(Resource.Id.imageAboutNeudesic);
            imageViewAboutNeudesic.SetImageDrawable(_aboutGlowDrawable);

            imageViewAboutNeudesic.Click += imageViewAboutNeudesic_Click;

            ImageView imageViewNeuCareers = FindViewById<ImageView>(Resource.Id.imageViewNeuCareers);
            imageViewNeuCareers.SetImageDrawable(_neuCareersGlowDrawable);
            imageViewNeuCareers.Click += imageViewNeuCareers_Click;

            ImageView imageViewQuiz = FindViewById<ImageView>(Resource.Id.imageViewQuiz);
            imageViewQuiz.SetImageDrawable(_newQuizDrawable);
            imageViewQuiz.Click += imageViewQuiz_Click;

            _aboutGlowDrawable.Stop();
            _neuCareersGlowDrawable.Stop();
            _newQuizDrawable.Stop();

            FindViewById<Button>(Resource.Id.button1).Click += (sender, args) => _aboutGlowDrawable.Start();
            FindViewById<Button>(Resource.Id.button2).Click += (sender, args) => _neuCareersGlowDrawable.Start();
            FindViewById<Button>(Resource.Id.button3).Click += (sender, args) => _newQuizDrawable.Start();
		}

        void imageViewQuiz_Click(object sender, EventArgs e)
        {
            if (this._newQuizDrawable.IsRunning)
            {
                StartActivity(typeof(QuizActivity));
            }
        }

        void imageViewNeuCareers_Click(object sender, EventArgs e)
        {
            if (this._neuCareersGlowDrawable.IsRunning)
            {
                StartActivity(typeof(CareerActivity));
            }
        }

        void imageViewAboutNeudesic_Click(object sender, EventArgs e)
        {
            if (this._aboutGlowDrawable.IsRunning)
            {
                StartActivity(typeof(About));
            }
        }

		protected override void OnResume()
		{
			base.OnResume();
			_paused = false;
		}

		protected override void OnPause()
		{
			base.OnPause();
			_paused = true;
		}

		void EnteredRegion(object sender, MonitorEventArgs e)
		{
            if (_paused)
			{
				//ShowNotification();
			}
		}

		void ExitedRegion(object sender, MonitorEventArgs e)
		{
		}

		void RangingBeaconsInRegion(object sender, RangeEventArgs e)
		{
			if (e.Beacons.Count > 0)
			{
				var newDetectedBeacon = e.Beacons.FirstOrDefault();
                if (detectedUUID == newDetectedBeacon.ProximityUuid)
                {
					return;
				}

                if (!UUIDs.Contains(newDetectedBeacon.ProximityUuid.ToUpper()))
                {
                    return;
                }

                switch ((ProximityType)newDetectedBeacon.Proximity)
				{
				case ProximityType.Immediate:
					UpdateDisplay(newDetectedBeacon);
					break;
                    //case ProximityType.Near:
                    //    UpdateDisplay(newDetectedBeacon);
                    //    break;
					//case ProximityType.Far:
                    //    UpdateDisplay(beacon.ProximityUuid);
                    //    break;
					//case ProximityType.Unknown:
                    //    UpdateDisplay(beacon.ProximityUuid);
                    //    break;
				}

                //_previousProximity = newDetectedBeacon.Proximity;
                //_iBeacon = newDetectedBeacon;

                detectedUUID = newDetectedBeacon.ProximityUuid;


			}
		}

		#region IBeaconConsumer impl
		public void OnIBeaconServiceConnect()
		{
			_iBeaconManager.SetMonitorNotifier(_monitorNotifier);
			_iBeaconManager.SetRangeNotifier(_rangeNotifier);

			_iBeaconManager.StartMonitoringBeaconsInRegion(_monitoringRegion);
			_iBeaconManager.StartRangingBeaconsInRegion(_rangingRegion);
		}
		#endregion

		private void UpdateDisplay(IBeacon ibeacon)
		{
			RunOnUiThread(() =>
				{
                    if (String.Compare(ibeacon.ProximityUuid, UUIDs[0], true) == 0)
                    {
                        _aboutGlowDrawable.Start();
                        _neuCareersGlowDrawable.Stop();
                        _newQuizDrawable.Stop();
                    }
                    else if (String.Compare(ibeacon.ProximityUuid, UUIDs[1], true) == 0)
                    {
                        _aboutGlowDrawable.Stop();
                        _neuCareersGlowDrawable.Start();
                        _newQuizDrawable.Stop();
                    }
                    else if (String.Compare(ibeacon.ProximityUuid, UUIDs[2], true) == 0)
					{
                        _aboutGlowDrawable.Stop();
                        _neuCareersGlowDrawable.Stop();
                        _newQuizDrawable.Start();
					}
					else
					{
                        _aboutGlowDrawable.Stop();
                        _neuCareersGlowDrawable.Stop();
                        _newQuizDrawable.Stop();
					}
				});
		}
		private void ShowNotification()
		{
			var resultIntent = new Intent(this, typeof(MainActivity));
			resultIntent.AddFlags(ActivityFlags.ReorderToFront);
			var pendingIntent = PendingIntent.GetActivity(this, 0, resultIntent, PendingIntentFlags.UpdateCurrent);
			var notificationId = Resource.String.beacon_notification;

			var builder = new NotificationCompat.Builder(this)
                .SetSmallIcon(Resource.Drawable.n_logo)
				.SetContentTitle(this.GetText(Resource.String.app_label))
				.SetContentText(this.GetText(Resource.String.beacon_notification))
				.SetContentIntent(pendingIntent)
				.SetAutoCancel(true);

			var notification = builder.Build();

			var notificationManager = (NotificationManager)GetSystemService(NotificationService);
			notificationManager.Notify(notificationId, notification);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();

			_monitorNotifier.EnterRegionComplete -= EnteredRegion;
			_monitorNotifier.ExitRegionComplete -= ExitedRegion;

			_rangeNotifier.DidRangeBeaconsInRegionComplete -= RangingBeaconsInRegion;

			_iBeaconManager.StopMonitoringBeaconsInRegion(_monitoringRegion);
			_iBeaconManager.StopRangingBeaconsInRegion(_rangingRegion);
			_iBeaconManager.UnBind(this);
		}
	}
}
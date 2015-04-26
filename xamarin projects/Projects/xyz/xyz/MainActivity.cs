using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;

namespace xyz
{
	[Activity (Label = "xyz", MainLauncher = true)]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			Button b = FindViewById<Button> (Resource.Id.button1);
			
			button.Click += delegate {
				button.Text = string.Format ("{0} clicks!", count++);
			};
		
			b.Click+=delegate {
//				string col="#aaaaaa";
//				Color m = new Color(col);
////				m.GetSaturation();
//				Color m =Android.Resource.Color.Black;
//
//		
				b.SetBackgroundColor(Color.ParseColor("#ffffff"));
				//b.Background=col;
				};
		}
		}
	}




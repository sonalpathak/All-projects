using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Auto_message
{
	[Activity (Label = "Auto_message", MainLauncher = true)]
	public class MainActivity :TabActivity
	{
		int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			TabHost.TabSpec tab;

			tab = TabHost.NewTabSpec ("All contacts");
			tab.SetIndicator ("All contacts");
			Intent i = new Intent (this, typeof(AllContact));

			tab.SetContent (i);
			TabHost.AddTab (tab);

			tab = TabHost.NewTabSpec ("Particular number");
			tab.SetIndicator ("Particular number");
			Intent i1=new Intent(this,typeof(SingleContact));
			tab.SetContent (i1);
			TabHost.AddTab (tab);


			tab = TabHost.NewTabSpec ("Settings");
			tab.SetIndicator ("Settings");
			Intent i2 = new Intent (this,typeof(settings));





		
//			Button button = FindViewById<Button> (Resource.Id.myButton);
//			
//			button.Click += delegate {
//				button.Text = string.Format ("{0} clicks!", count++);
//			};
		}
	}
}



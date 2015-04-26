using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace AndroidTabLayout
{
	[Activity (Label = "AndroidTabLayout", MainLauncher = true)]
	public class MainActivity :TabActivity
	{
		//int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			TabHost.TabSpec con;
			//con = TabHost.NewTabSpec ("tab_test0").SetIndicator ("Contacts").SetContent (new Intent(this,typeof(contacts)));
			//TabHost.AddTab (con);

	con = TabHost.NewTabSpec ("tab_test0");
	con.SetIndicator ("Contacts");
			Intent i = new Intent (this, typeof(contacts));
			con.SetContent (i);
			TabHost.AddTab (con);

			con = TabHost.NewTabSpec ("tab_spec1");
			con.SetIndicator ("social networking");
			Intent i1 = new Intent (this, typeof(All_mail));
			con.SetContent (i1);
			TabHost.AddTab (con);


			//con = TabHost.NewTabSpec("tab_test1").SetIndicator("PHONE CONTACTS").SetContent(i);
			//TabHost.AddTab(con);


			con = TabHost.NewTabSpec("tab_test2").SetIndicator("MESSENGER").SetContent(Resource.Id.textview2);
			TabHost.AddTab(con);
//
//			con = TabHost.NewTabSpec("tab_test3").SetIndicator("SOCIAL NETWORKING").SetContent(Resource.Id.textview3);
//			TabHost.AddTab(con);

			TabHost.CurrentTab = 0;  

			// Get our button from the layout resource,
			// and attach an event to it
			//Button button = FindViewById<Button> (Resource.Id.myButton);
			
			/*button.Click += delegate {
				button.Text = string.Format ("{0} clicks!", count++);
			};*/
		}
	}
}



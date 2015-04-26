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
using Android.Telephony;
using Android.Provider;


namespace AndroidTabLayout
{
	[Activity (Label = "contacts")]
	class All_mail:ListActivity
	{
		List<string> email;
		protected override void OnCreate(Bundle b)
		{
			base.OnCreate (b);
			//SetContentView (Resource.Layout.ContactView);
			var uri = ContactsContract.Contacts.ContentUri;

			//projection shows which rows to return
			string[] proj = {
				ContactsContract.Contacts.InterfaceConsts.Id,
				ContactsContract.Contacts.InterfaceConsts.DisplayName
			};
			var cursor = ManagedQuery (ContactsContract.CommonDataKinds.Email.ContentUri, null, ContactsContract.Contacts.InterfaceConsts.Id, null, null);


			email=new List<string>();
			if(cursor.MoveToFirst())
			{
				do {
					email.Add (cursor.GetString (cursor.GetColumnIndex ("DATA1")));
				} while(cursor.MoveToNext ());
			}
			ListAdapter = new ArrayAdapter<string> (this, Resource.Layout.ContactView, email);
		}
//		protected override void OnListItemClick(ListView l,View v,int position,long id)
//		{
//			var t = Convert.ToInt32 (id)+1;
//			var uri_1 = ContactsContract.Contacts.ContentUri;
//			var uri = Android.Net.Uri.WithAppendedPath (uri_1, t.ToString ());
//			Intent i = new Intent (Intent.ActionView, uri);
//			StartActivity (i);
//		}


	}
}





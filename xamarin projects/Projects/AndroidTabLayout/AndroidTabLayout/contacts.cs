using System;

using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;

using Android.Telephony;
using Android.Provider;
using System.Collections.Generic;

namespace AndroidTabLayout
{
	[Activity (Label = "contacts")]
	class contacts:ListActivity
	{
		List<string> contact;
		protected override void OnCreate(Bundle b)
		{
			base.OnCreate (b);
			//SetContentView (Resource.Layout.ContactView);
			var uri = ContactsContract.Contacts.ContentUri;

			//projection shows which rows to return
			string[] proj = { ContactsContract.Contacts.InterfaceConsts.Id, ContactsContract.Contacts.InterfaceConsts.DisplayName};

			var cursor = ManagedQuery (uri, null, null, null, null, null);
		
			//		var s = ContactsContract.ContactsColumns.HasPhoneNumber;

			//var cursor1 = ManagedQuery (uri, ContactsContract.Contacts.InterfaceConsts.DisplayName, null, null, null);
//			if (cursor.GetInt(cursor.GetColumnIndex(s))==1) {
//				var phonenumber = ManagedQuery (ContactsContract.CommonDataKinds.Phone.ContentUri, null, "Contact_id" + "=" + ContactsContract.Contacts.InterfaceConsts.Id, null, null);
//				while(phonenumber.MoveToNext())
//				{
//					var number = phonenumber.GetString (phonenumber.GetColumnIndex (ContactsContract.CommonDataKinds.Phone.Number));
//					Log.Info ("", number);
//					//string[] proj3 =number;
//
//				}
//
//				}
			//	string[] proj2 = { number};
//						if(cursor.GetInt(cursor.GetColumnIndex(ContactsContract.Contacts.InterfaceConsts.HasPhoneNumber))==1)
//			{
//				var phonenumber = ManagedQuery (ContactsContract.CommonDataKinds.Phone.ContentUri, null, "Contact_id" + "=" + ContactsContract.Contacts.InterfaceConsts.Id, null, null);
//				while(phonenumber.MoveToNext())
//				{
//					var number = phonenumber.GetString (phonenumber.GetColumnIndex (ContactsContract.CommonDataKinds.Phone.Number));
//				}
//			
//			
//						}
			//string[] proj2={ContactsContract.CommonDataKinds.Phone.Number};
			//string[] proj1 = { ContactsContract.Contacts.InterfaceConsts.Id, ContactsContract.Contacts.InterfaceConsts.DisplayName,ContactsContract.CommonDataKinds.Phone.Number};

			contact=new List<string>();



//			if(cursor.MoveToFirst())
//			{
//				do {
//					contact.Add (cursor.GetString (cursor.GetColumnIndex (proj [1])));
//				} while(cursor.MoveToNext ());
//			}
			if (cursor.Count >= 0) {

				while (cursor.MoveToNext ()) {
					if (cursor.GetInt (cursor.GetColumnIndex (ContactsContract.ContactsColumns.HasPhoneNumber)) == 1) {
						contact.Add (cursor.GetString (cursor.GetColumnIndex (proj [1])));
//						do {
//							contact.Add (cursor.GetString (cursor.GetColumnIndex (proj [1])));
//						} while(cursor.MoveToNext ());
					}
				}
			}




			ListAdapter = new ArrayAdapter<string> (this, Resource.Layout.ContactView, contact);
		}
		protected override void OnListItemClick(ListView l,View v,int position,long id)
		{
			int t = Convert.ToInt32 (id+1);
			var uri_1 = ContactsContract.Contacts.ContentUri;
			var uri = Android.Net.Uri.WithAppendedPath (uri_1, t.ToString ());
			Intent i = new Intent (Intent.ActionView, uri);
			StartActivity (i);
		}


	}
}


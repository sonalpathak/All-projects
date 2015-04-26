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
	[Activity (Label = "social_networking")]
	class number:ListActivity
	{
		List<string> mail;
		protected override void OnCreate(Bundle b)
		{
			base.OnCreate (b);
			var email_cursor = ManagedQuery (ContactsContract.CommonDataKinds.Email.ContentUri, null, ContactsContract.Contacts.InterfaceConsts.Id, null, null);
			while (email_cursor.MoveToNext ()) {
				var email = email_cursor.GetString (email_cursor.GetColumnIndex (ContactsContract.CommonDataKinds.Email.Address));
				mail = new List<string> ();

			}
		}
	}
}


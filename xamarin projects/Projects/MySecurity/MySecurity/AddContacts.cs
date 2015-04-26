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
using Xamarin.Contacts;
using Android.Telephony;
using Android.Content.PM;
using Android.Provider;

namespace MySecurity
{

	[Activity (Label = "AddContact", MainLauncher = false)]
	public class AddContacts : Activity
	{
		int count=0;
		TextView name1;
		TextView name2;
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView (Resource.Layout.contacts);
			Button save = FindViewById<Button> (Resource.Id.saveButton);
			FindViewById<Button> (Resource.Id.cancelButton).Click+=cancel;
			name1 = FindViewById<TextView> (Resource.Id.name1);
			name2 = FindViewById<TextView> (Resource.Id.name2);
			FindViewById<Button> (Resource.Id.contacts).Click+=contact;
			ISharedPreferences sharedpreference1 = GetSharedPreferences ("Contacts", FileCreationMode.WorldReadable);
			string ContactName1=sharedpreference1.GetString("name_contact_1","");
			string ContactNumber1 = sharedpreference1.GetString ("number_1", "");
			name1.Text = ContactName1+ ContactNumber1;
			ISharedPreferences sharedpreference2 = GetSharedPreferences ("Contacts2", FileCreationMode.WorldReadable);
			string ContactName2=sharedpreference2.GetString("name_contact_2","");
			string ContactNumber2 = sharedpreference2.GetString ("number_2", "");
			name2.Text = ContactName2 + ContactNumber2;
         	save.Click += delegate {
				StartActivity(typeof(MainActivity));
			};

		}
		private void contact(object s,EventArgs e)
		{
			Intent intent2 = new Intent (Intent.ActionPick, ContactsContract.CommonDataKinds.Phone.ContentUri);

			StartActivityForResult(intent2,110);
		}
		private void cancel(object s,EventArgs e)
		{
			ISharedPreferences p1 = GetSharedPreferences ("Contacts", FileCreationMode.WorldReadable);
			ISharedPreferencesEditor ed1 = p1.Edit ();
			string val = p1.GetString ("name_contact_1", "");
			ISharedPreferences p2 = GetSharedPreferences ("Contacts2", FileCreationMode.WorldReadable);
			ISharedPreferencesEditor ed2 = p2.Edit ();
			string val2 = p2.GetString ("name_contact_2", "");

			if (val != "" && val2 != "") {
				ed2.Clear ();
				ed2.Remove ("name_contact_2").Commit ();
				ed2.Remove ("number_2").Commit ();
				name2.Text = "";
			}
			if (val != "" && val2 == "") {
				ed1.Remove ("name_contact_1").Commit ();
				ed1.Remove ("number_1").Commit ();
				name1.Text = "";
				count = 0;
			}
			if (val == "" && val2 == "") {
				Toast.MakeText (this, "no numbers to be deleted", ToastLength.Short).Show ();
			}
			if (val == "" && val2 != "") {
				ed2.Remove ("name_contact_2").Commit ();
				ed2.Remove ("number_2").Commit ();
				name2.Text = "";

			}
		}
		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			if (requestCode == 110) {

				if (data != null)
				{
					var cursor2 = ManagedQuery(data.Data, null, null, null, null);
					if(cursor2.Count > 0)
					{
                         cursor2.MoveToNext();

						if (count == 0) {
						
							ISharedPreferences sharedpreference1 = GetSharedPreferences ("Contacts", FileCreationMode.WorldReadable);
							var name = cursor2.GetString (cursor2.GetColumnIndex (ContactsContract.ContactsColumns.DisplayName));
							var number = cursor2.GetString (cursor2.GetColumnIndex (ContactsContract.CommonDataKinds.Phone.Number));	
							ISharedPreferencesEditor e = sharedpreference1.Edit ();
							e.PutString (name, number);
							e.PutString ("name_contact_1", name);
							e.PutString ("number_1", number);
							e.Commit ();
							string ContactName_1 = sharedpreference1.GetString (name, "");
//							var n = name;
							name1.Text = name+ ContactName_1;


						} 
						if(count==1) {
							ISharedPreferences sharedpreference2 = GetSharedPreferences ("Contacts2", FileCreationMode.WorldReadable);
							var name = cursor2.GetString (cursor2.GetColumnIndex (ContactsContract.ContactsColumns.DisplayName));
							var number = cursor2.GetString (cursor2.GetColumnIndex (ContactsContract.CommonDataKinds.Phone.Number));	
							ISharedPreferencesEditor e = sharedpreference2.Edit ();
							e.PutString (name, number);
							e.PutString ("name_contact_2", name);
							e.PutString ("number_2", number);
							e.Commit ();
							string ContactName_2 = sharedpreference2.GetString (name, "");
//							var n = name;
							name2.Text = name+ ContactName_2;

						}
						if (count == 2) {
                         Toast.MakeText (this, "can't add more than 2 contacts", ToastLength.Short).Show ();
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


//using System;
//using Android.App;
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
//		ListView listview;
//		List<string> contactList;
		int count=0;
		//int count2;
		TextView name1;
		TextView name2;
		//TextView name3;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView (Resource.Layout.contacts);
			Button save = FindViewById<Button> (Resource.Id.saveButton);
			FindViewById<Button> (Resource.Id.cancelButton).Click+=cancel;
			name1 = FindViewById<TextView> (Resource.Id.name1);
			name2 = FindViewById<TextView> (Resource.Id.name2);
			//name3 = FindViewById<TextView> (Resource.Id.name3);
			FindViewById<Button> (Resource.Id.contacts).Click+=contact;
//			FindViewById<Button>(Resource.Id.
			ISharedPreferences p1 = GetSharedPreferences ("Contacts", FileCreationMode.WorldReadable);
			string val=p1.GetString("name_contact","");
			string val1 = p1.GetString ("number", "");
			name1.Text = val+ val1;
			ISharedPreferences p2 = GetSharedPreferences ("Contacts2", FileCreationMode.WorldReadable);
			string val2=p2.GetString("name_contact2","");
			string val2_2 = p2.GetString ("number2", "");
			name2.Text = val2 + val2_2;
//			ISharedPreferences p3 = GetSharedPreferences ("Contacts3", FileCreationMode.WorldReadable);
//			string val3=p2.GetString("name_contact3","");
//			string val3_2 = p2.GetString ("number3", "");
//			name3.Text = val3 + val3_2;


		
			save.Click += delegate {
				StartActivity(typeof(MainActivity));
			
			};

		
		}
		private void contact(object s,EventArgs e)
		{
			//var intent = new Intent(Intent.ActionPick, ContactsContract.Contacts.ContentUri);
			Intent intent2 = new Intent (Intent.ActionPick, ContactsContract.CommonDataKinds.Phone.ContentUri);
			//StartActivityForResult(intent, 100);
			StartActivityForResult(intent2,110);
		}
		private void cancel(object s,EventArgs e)
		{
			ISharedPreferences p1 = GetSharedPreferences ("Contacts", FileCreationMode.WorldReadable);
			ISharedPreferencesEditor ed1 = p1.Edit ();
			string val=p1.GetString("name_contact","");
			ISharedPreferences p2 = GetSharedPreferences ("Contacts2", FileCreationMode.WorldReadable);
			ISharedPreferencesEditor ed2 = p2.Edit ();
			string val2 = p2.GetString("name_contact2","");
//			ISharedPreferences p3 = GetSharedPreferences ("Contacts2", FileCreationMode.WorldReadable);
//			ISharedPreferencesEditor ed3 = p3.Edit ();
//			string val3 = p3.GetString("name_contact3","");

//			if (count2 == 0 && val2=="") {
////				ISharedPreferences p2 = GetSharedPreferences ("Contacts2", FileCreationMode.WorldReadable);
////				ISharedPreferencesEditor ed1 = p2.Edit ();
////				ed1.Clear ();
//				ed1.Remove ("name_contact").Commit ();
//				ed1.Remove ("number").Commit ();
//				name1.Text = "";
//			}
			if (val != "" && val2 != "") {
				ed2.Clear ();
				ed2.Remove ("name_contact2").Commit ();
				ed2.Remove ("number2").Commit ();
				name2.Text = "";
			}
			if (val != "" && val2 == "") {
				ed1.Remove ("name_contact").Commit ();
				ed1.Remove ("number").Commit ();
				name1.Text = "";
				count = 0;
			}
			if (val == "" && val2 == "") {
				Toast.MakeText (this, "no numbers to be deleted", ToastLength.Short).Show ();
			}
			if (val == "" && val2 != "") {
				ed2.Remove ("name_contact2").Commit ();
				ed2.Remove ("number2").Commit ();
				name2.Text = "";

			}
//			if (count2 == 1 && val !="") {
//
//				ed2.Clear ();
//				ed2.Remove ("name_contact2").Commit ();
//				ed2.Remove ("number2").Commit ();
//				name2.Text = "";
//			}
//			count2++;





		}
		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			// Request 150 comes from inserting or editing a new contact
//			if(requestCode == 100)
//			{
//				if (data != null)
//				{
//					var cursor1 = ManagedQuery(data.Data, null, null, null, null);
//					if (cursor1.Count > 0) {
//						cursor1.MoveToNext ();
//					}
//				}
//				else
//				{
//					Toast.MakeText(this, "No contact picked", ToastLength.Long).Show();
//				}
//			}


			// Request 100 comes from contact picker
//			if(requestCode == 110)
//			{
//
////			
////	
//				if (data != null)
//				{
//					var cursor1 = ManagedQuery(data.Data, null, null, null, null);
//					if(cursor1.Count > 0)
//					{
//						cursor1.MoveToNext();
//						//Toast.MakeText(this, "Got contact " + cursor1.GetString(cursor1.GetColumnIndex(ContactsContract.ContactsColumns.DisplayName)), ToastLength.Long).Show();		
//					}	
//				}
//				else
//				{
//					Toast.MakeText(this, "No contact picked", ToastLength.Long).Show();
//				}
//			}

			if (requestCode == 110) {
				//count = 0;
				if (data != null)
				{
					var cursor2 = ManagedQuery(data.Data, null, null, null, null);
					if(cursor2.Count > 0)
					{
                         cursor2.MoveToNext();
						//	count = 0;
						//Toast.MakeText(this, "Got contact " +cursor2.GetString(cursor2.GetColumnIndex(ContactsContract.CommonDataKinds.Phone.Number))+ cursor2.GetString(cursor2.GetColumnIndex(ContactsContract.ContactsColumns.DisplayName)), ToastLength.Short).Show();	
						//void call(){
						if (count == 0) {
							//void CallLog(){
							ISharedPreferences p = GetSharedPreferences ("Contacts", FileCreationMode.WorldReadable);
							var name = cursor2.GetString (cursor2.GetColumnIndex (ContactsContract.ContactsColumns.DisplayName));
							var number = cursor2.GetString (cursor2.GetColumnIndex (ContactsContract.CommonDataKinds.Phone.Number));	


							ISharedPreferencesEditor e = p.Edit ();
							e.PutString (name, number);
							e.PutString ("name_contact", name);
							e.PutString ("number", number);
							e.Commit ();
							string val = p.GetString (name, "");
//							Toast.MakeText (this, val + name, ToastLength.Short).Show ();
							var n = name;
							name1.Text = name+ val;
								//}


							//break;
						} 
						if(count==1) {
							ISharedPreferences p = GetSharedPreferences ("Contacts2", FileCreationMode.WorldReadable);
							var name = cursor2.GetString (cursor2.GetColumnIndex (ContactsContract.ContactsColumns.DisplayName));
							var number = cursor2.GetString (cursor2.GetColumnIndex (ContactsContract.CommonDataKinds.Phone.Number));	


							ISharedPreferencesEditor e = p.Edit ();
							e.PutString (name, number);
									e.PutString ("name_contact2", name);
							e.PutString ("number2", number);
							e.Commit ();
							string val = p.GetString (name, "");
//							Toast.MakeText (this, val + name, ToastLength.Short).Show ();
//
//							Toast.MakeText (this, "count is now 1", ToastLength.Short).Show();
							var n = name;
							name2.Text = name+ val;

						}
						if (count == 2) {
//							ISharedPreferences p = GetSharedPreferences ("Contacts3", FileCreationMode.WorldReadable);
//							var name = cursor2.GetString (cursor2.GetColumnIndex (ContactsContract.ContactsColumns.DisplayName));
//							var number = cursor2.GetString (cursor2.GetColumnIndex (ContactsContract.CommonDataKinds.Phone.Number));	
//
//
//							ISharedPreferencesEditor e = p.Edit ();
//							e.PutString (name, number);
//							e.PutString ("name_contact3", name);
//							e.PutString ("number3", number);
//							e.Commit ();
//							string val = p.GetString (name, "");
//							Toast.MakeText (this, val + name, ToastLength.Short).Show ();
//
//							Toast.MakeText (this, "count is now 1", ToastLength.Short).Show();
//							var n = name;
//							name2.Text = name+ val;
							Toast.MakeText (this, "can't add more than 2 contacts", ToastLength.Short).Show ();
						}
//						if (count == 2) {
//							count = -1;
////							Toast.MakeText (this, "you cannot stored more than 2 contacts", ToastLength.Short).Show();
////							ISharedPreferences p = GetSharedPreferences ("Contacts", FileCreationMode.WorldReadable);
////							var name = cursor2.GetString (cursor2.GetColumnIndex (ContactsContract.ContactsColumns.DisplayName));
////							var number = cursor2.GetString (cursor2.GetColumnIndex (ContactsContract.CommonDataKinds.Phone.Number));	
////
////
////							ISharedPreferencesEditor e = p.Edit ();
////							e.PutString (name, number);
////							e.PutString ("name_contact", name);
////							e.PutString ("number", number);
////							e.Commit ();
////							String val = p.GetString (name, "");
////							Toast.MakeText (this, val + name, ToastLength.Short).Show ();
////							var n = name;
////							name1.Text = name+ val;
//
//						}






							//}	
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


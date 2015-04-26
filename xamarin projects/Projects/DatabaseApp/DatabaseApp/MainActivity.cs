using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Database;

namespace DatabaseApp
{
	[Activity (Label = "DatabaseApp", MainLauncher = true)]
	public class MainActivity : Activity
	{
		MyDatabase mydb;

		void GetCursorView (string sColumn, string str)
		{
			ICursor c = mydb.GetRecord (sColumn, str);
			if (c != null) {
				ListView lview = FindViewById<ListView> (Resource.Id.lvTemp);
				string[] from = new string[]{ "_id", "Name", "Age", "Country" };
				int[] to = new int[]{ Resource.Id.tvIdShow,Resource.Id.tvPersonShow ,Resource.Id.tvAgeShow, Resource.Id.tvCountryShow };
				SimpleCursorAdapter adapter = new SimpleCursorAdapter (this, Resource.Layout.record_view, c, from, to);
				lview.Adapter = adapter;
			} else {

			}
		}

		void ListView_click (object sender, AdapterView.ItemClickEventArgs e)
		{
			TextView tvIdshow=e.View.FindViewById<TextView> (Resource.Id.tvIdShow);
			TextView tvNameShow = e.View.FindViewById<TextView> (Resource.Id.tvPersonShow);
			TextView tvCountryShow = e.View.FindViewById<TextView> (Resource.Id.tvCountryShow);
			var val=tvIdshow.Text;
		}

		//int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			mydb = new MyDatabase ("studentDb");

			// Get our button from the layout resource,
			// and attach an event to it
			EditText name = FindViewById<EditText> (Resource.Id.txtName);
			EditText age = FindViewById<EditText> (Resource.Id.txtAge);
			EditText country = FindViewById<EditText> (Resource.Id.txtCountry);
			TextView msg = FindViewById<TextView> (Resource.Id.tvMsg);
			ImageButton addbutton = FindViewById<ImageButton> (Resource.Id.imgAdd);
			ImageButton updateButton = FindViewById<ImageButton> (Resource.Id.imgEdit);
			ImageButton deleteButton = FindViewById<ImageButton> (Resource.Id.imgDelete);
			ImageButton findButton = FindViewById<ImageButton> (Resource.Id.imgFind);
			deleteButton.SetImageResource( Resource.Drawable.delete);
			addbutton.SetImageResource (Resource.Drawable.add);
			updateButton.SetImageResource (Resource.Drawable.save);
			findButton.SetImageResource (Resource.Drawable.find);
			findButton.Click += delegate {
				string sColumn="";
				if(name.Text.Trim()!=null)
				{
					sColumn="by Name";
					GetCursorView(sColumn,name.Text.Trim());

				}
				else if(age.Text.Trim()!=null)
				{
					sColumn="by Age";
					GetCursorView(sColumn,age.Text.Trim());
					msg.Text=mydb.Message;
				}
				else if(country.Text.Trim()!=null)
				{
					sColumn="by Country";
					GetCursorView(sColumn,country.Text.Trim());
				}
				else{
					sColumn="All";
					//GetCursorView();
				}
				msg.Text="Search"+sColumn+".";
			
			};
			deleteButton.Click += delegate {
				//mydb.DeleteRecord(1);
				int id=-1;
				int.TryParse(msg.Text,out id);
				mydb.DeleteRecord(id); 

			};
			updateButton.Click += delegate {
				mydb.UpdateDatabase(1,"sonalpathak",22,"India");
			};
			//Button button = FindViewById<Button> (Resource.Id.imgAdd);
			
			addbutton.Click += delegate {
//				button.Text = string.Format ("{0} clicks!", count++);
				mydb.AddRecord(name.Text,int.Parse(age.Text),country.Text);
				name.Text=age.Text=country.Text="";
				//msg=mydb.Message;


				//mydb.AddRecord("sonal",22,"India");
			};

			ListView lv = FindViewById<ListView> (Resource.Id.lvTemp);
			lv.ItemClick += new EventHandler<AdapterView.ItemClickEventArgs> (ListView_click);
		  

		}
	}
}



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

namespace NeuBeacon.Droid
{
    [Activity(Label = "Profile")]
    public class Profile : LayoutActivity
    {
		EditText name;
		EditText email;
		EditText mobile;
        int mobilenum=1;
        bool techrole;
        bool managerrole;
        bool salesrole;
		ImageView salesButton;
		ImageView managerButton;
		ImageView techButton;
        int result;
        protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Profile);
			 techButton = FindViewById<ImageView> (Resource.Id.techguru);
			techButton.Clickable = true;
			techButton.Click += (sender, e) => {
			techButton.SetImageResource (Resource.Drawable.tech_sel);
			managerButton.Clickable=false;
			salesButton.Clickable=false;
			techrole = true;
			};
			 managerButton = FindViewById<ImageView> (Resource.Id.mindfulmanager);
			managerButton.Clickable = true;
			managerButton.Click += (sender, e) => {
		    managerButton.SetImageResource (Resource.Drawable.manager_sel);
		    managerrole = true;
			salesButton.Clickable=false;
			techButton.Clickable=false;
			};

			salesButton = FindViewById<ImageView> (Resource.Id.salesguy);
			salesButton.Clickable = true;
			salesButton.Click += (sender, e) => {
			salesButton.SetImageResource (Resource.Drawable.sales_sel);
			salesrole = true;
			managerButton.Clickable=false;
			techButton.Clickable=false;
			};
            name = FindViewById<EditText>(Resource.Id.name);
           
            email = FindViewById<EditText>(Resource.Id.email);
            
            mobile = FindViewById<EditText>(Resource.Id.phone);
           
            int result;
            
          int.TryParse(mobile.Text, out result);
            mobilenum = result;
			Button button = FindViewById<Button> (Resource.Id.navigateMain);

            button.Click += delegate
            {
                if (name.Text == "" || email.Text == "" )
                {
                    Toast.MakeText(this, "No field can be empty", ToastLength.Short).Show();

                }

                //  button.Text = string.Format("{0} clicks!", count++);
                else 
                {
                    (this.ApplicationContext as NeuBeaconsApplication).ApplicationState.Email = email.Text;
                    (this.ApplicationContext as NeuBeaconsApplication).ApplicationState.Nama = name.Text;
                  //  (this.ApplicationContext as NeuBeaconsApplication).ApplicationState.Phone = mobilenum;

                    StartActivity(typeof(MainActivity));
                }
			};

			
           
		}


            // Create your application here
        
        
    }
}
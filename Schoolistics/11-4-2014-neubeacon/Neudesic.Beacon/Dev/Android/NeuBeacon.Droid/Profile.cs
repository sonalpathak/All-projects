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
using System.Text.RegularExpressions;

namespace NeuBeacon.Droid
{
    [Activity(Label = "Profile")]
    public class Profile : Activity
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
            addToDb();
            
			};
			 managerButton = FindViewById<ImageView> (Resource.Id.mindfulmanager);
			managerButton.Clickable = true;
			managerButton.Click += (sender, e) => {
		    managerButton.SetImageResource (Resource.Drawable.manager_sel);
		    managerrole = true;
			salesButton.Clickable=false;
			techButton.Clickable=false;
            addToDb();
            
			};

			salesButton = FindViewById<ImageView> (Resource.Id.salesguy);
			salesButton.Clickable = true;
			salesButton.Click += (sender, e) => {
			salesButton.SetImageResource (Resource.Drawable.sales_sel);
			salesrole = true;
			managerButton.Clickable=false;
			techButton.Clickable=false;
            addToDb();
            
			};
            
			
           
		}
        static Regex ValidEmailRegex = CreateValidEmailRegex();

        
        private static Regex CreateValidEmailRegex()
        {
            string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return new Regex(validEmailPattern, RegexOptions.IgnoreCase);
        }

        public static bool EmailIsValid(string emailAddress)
        {
            bool isValid = ValidEmailRegex.IsMatch(emailAddress);

            return isValid;
        }

        public void addToDb()
        {
            name = FindViewById<EditText>(Resource.Id.name);

            email = FindViewById<EditText>(Resource.Id.email);

            mobile = FindViewById<EditText>(Resource.Id.phone);

            int result;

            int.TryParse(mobile.Text, out result);
            mobilenum = result;         

           
                if (name.Text == "" || email.Text == "")
                {
                    Toast.MakeText(this, "No field can be empty", ToastLength.Short).Show();
                    techButton.SetImageResource(Resource.Drawable.tech);
                    salesButton.SetImageResource(Resource.Drawable.sales);
                    managerButton.SetImageResource(Resource.Drawable.manager);
                    salesButton.Clickable = true; 
                    techButton.Clickable = true;
                    managerButton.Clickable = true;
                }

                //  button.Text = string.Format("{0} clicks!", count++);
                else
                {
                    
                    bool isTrue = EmailIsValid(email.Text);
                    if (isTrue == true)
                    {
                        (this.ApplicationContext as NeuBeaconsApplication).ApplicationState.Email = email.Text;
                        (this.ApplicationContext as NeuBeaconsApplication).ApplicationState.Nama = name.Text;
                        StartActivity(typeof(MainActivity));
                    }
                    else { Toast.MakeText(this, "Invalid Email", ToastLength.Short).Show();
                    techButton.SetImageResource(Resource.Drawable.tech);
                    salesButton.SetImageResource(Resource.Drawable.sales);
                    managerButton.SetImageResource(Resource.Drawable.manager);
                    salesButton.Clickable = true;
                    techButton.Clickable = true;
                    managerButton.Clickable = true;
                    
                    }
                   
                    //  (this.ApplicationContext as NeuBeaconsApplication).ApplicationState.Phone = mobilenum;
                    
                   
                       
                    
                   
                }
            

 
        }


            // Create your application here
        
        
    }
}
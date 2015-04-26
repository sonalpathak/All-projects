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
    [Activity(Label = "Career")]
    public class CareerActivity : LayoutActivity
    {
        Dictionary<string, string> dictGroup=new Dictionary<string,string>();
       


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Career);
           
                List<string> lstChild = new List<string>();
               
                  

                    dictGroup.Add("Super Architect","Hi Super Architect");
                    dictGroup.Add("Smart UXer","Hi Smart UXer");
                    dictGroup.Add("BI builder","Hi BI builder");
                    dictGroup.Add("Clouduable","Hi Cloudude");
                    dictGroup.Add("Mast Manager","Hi Mast Manager");
                    dictGroup.Add("Super saler","Hi Super saler");
                    dictGroup.Add("Mobile Genie","Hi Mobile Genie");
                    dictGroup.Add("Fresh $ new","Hi Fresh & new");
           
            var lstKeys = new List<string>(dictGroup.Keys);
            var ctlExListBox = FindViewById<ExpandableListView>(Resource.Id.ctlExListBox);
            ctlExListBox.SetAdapter(new NeuCareerAdapterClass(this, dictGroup));

            var button = FindViewById<Button>(Resource.Id.button1);
            // Create your application here
            button.Click += (sender, eventArgs) =>
            {
                Coins = Coins + 1;
                if (Coins == 8)
                {
                    StartActivity(typeof(QuizActivity));
                }
            };

            // Create your application here
        }
        
    }
}
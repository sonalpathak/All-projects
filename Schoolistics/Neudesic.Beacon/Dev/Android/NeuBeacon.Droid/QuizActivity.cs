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
    [Activity(Label = "Quiz")]
    public class QuizActivity : LayoutActivity
    {        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Quiz);

            var button = FindViewById<Button>(Resource.Id.button1);
            // Create your application here
            button.Click += (sender, eventArgs) =>
            {
                Coins = Coins + 1;
                if (Coins == 4)
                {
                    StartActivity(typeof(CareerActivity));
                }
            };
        }
    }
}
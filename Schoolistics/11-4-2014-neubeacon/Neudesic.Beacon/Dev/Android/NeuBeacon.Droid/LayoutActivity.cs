using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RadiusNetworks.IBeaconAndroid;

namespace NeuBeacon.Droid
{
    [Activity(Label = "Layout Activity")]
    public class LayoutActivity : Activity, INotifyPropertyChanged
    {

        IMenu _menu;
        public event PropertyChangedEventHandler PropertyChanged;

        public int Coins
        {
            get
            {
                return (this.ApplicationContext as NeuBeaconsApplication).ApplicationState.Coins;
            }
            set
            {
                if ((this.ApplicationContext as NeuBeaconsApplication).ApplicationState.Coins == value)
                {
                    return;
                }
                (this.ApplicationContext as NeuBeaconsApplication).ApplicationState.Coins = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Coins"));
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            PropertyChanged += LayoutActivity_PropertyChanged;
            // Create your application here
        }

        protected override void OnResume()
        {
            base.OnResume();
            UpdateCoinCount();
        }

        private void UpdateCoinCount()
        {
            if (_menu != null)
            {
                var coinMenuItem = _menu.FindItem(Resource.Id.coinMenuItem);
                coinMenuItem.SetTitle(Coins.ToString());
            }
        }

        void LayoutActivity_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Coins":
                    UpdateCoinCount();
                    break;
                default:
                    break;
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            var inflater = MenuInflater;            
            inflater.Inflate(Resource.Menu.menus, menu);
            _menu = menu;
            UpdateCoinCount();
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.coinMenuItem:
                    Android.Widget.Toast.MakeText(this, "Coin clicked", Android.Widget.ToastLength.Long).Show();
                    break;
                case Resource.Id.homeMenuItem:
                    Intent mainActivityIntent = new Intent(this, typeof(MainActivity));
                    mainActivityIntent.AddFlags(ActivityFlags.ReorderToFront);
                    this.StartActivity(mainActivityIntent);
                    break;                
                default:
                    break;
            }
            return base.OnContextItemSelected(item);
        }        
    }


}
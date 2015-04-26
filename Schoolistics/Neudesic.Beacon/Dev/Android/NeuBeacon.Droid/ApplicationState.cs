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
    public class ApplicationState
    {
        public int Coins { get; set; }
        public string Email { get; set; }
        public string Nama { get; set; }
        public int Phone { get; set; }
        public string Role { get; set; }
    }
}
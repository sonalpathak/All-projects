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
    public class QuizQuestions:Java.Lang.Object
    {
        public string Name { get; set; }
        public int image { get; set; }
        public IList<Questions> Items { get; set; }
    }
}
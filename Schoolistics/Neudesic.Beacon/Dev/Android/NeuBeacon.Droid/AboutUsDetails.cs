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
   public class AboutUsDetails:Java.Lang.Object
    {

        public string Name { get; set; }
        public IList<string> Items { get; set; }
       //public Question()
       //{
       //}
       //public static List<Question> NeuData()
       //{
       //    var neuDataList = new List<Question>();
       //    neuDataList.Add(new Question("Values", "Neudesic1"));
       //    neuDataList.Add(new Question("Passion", "Neudesic1"));
       //    neuDataList.Add(new Question("Discipline", "Neudesic1"));
       //    neuDataList.Add(new Question("Integrity", "Neudesic1"));
       //    neuDataList.Add(new Question("Teaming", "Neudesic1"));
       //    neuDataList.Add(new Question("Innovation", "Neudesic1"));
       //    return neuDataList;


       //}
       //public string Id { get; set; }
       //public string Val { get; set; }
       //public Question(string newid,string value)
       //{
       //    Id = newid;
       //    Val = value;

       //}
    }
}
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;

//namespace NeuBeacon.Droid
//{
//[Activity(Label = "About Neudesic", MainLauncher = false)]
//   public  class AboutNeudesic:Activity
//    {
//    Dictionary<string, List<string>> dictGroup = new Dictionary<string, List<string>>();
//    //need to uncomment it
//   // Dictionary<string, string> dictGroup = new Dictionary<string, string>();
//    List<string> lstkeys;
//   //Question q = new Question();

//    protected override void OnCreate(Bundle savedInstanceState)
//    {
//        base.OnCreate(savedInstanceState);
//      //  SetContentView(Resource.Layout.About);
//        //var listView = FindViewById<ExpandableListView>(Resource.Id.myExpandableListview);
//        //listView.SetAdapter(new AboutUsDataAdapter(this, Data.NeuData()));

//        //need to uncomment it
//        //List<string> lstChild = new List<string>();



//        //dictGroup.Add("Super Architect", "Hi Super Architect");
//        //dictGroup.Add("Smart UXer", "Hi Smart UXer");
//        //dictGroup.Add("BI builder", "Hi BI builder");
//        //dictGroup.Add("Clouduable", "Hi Cloudude");
//        //dictGroup.Add("Mast Manager", "Hi Mast Manager");
//        //dictGroup.Add("Super saler", "Hi Super saler");
//        //dictGroup.Add("Mobile Genie", "Hi Mobile Genie");
//        //dictGroup.Add("Fresh $ new", "Hi Fresh & new");

//        //var lstKeys = new List<string>(dictGroup.Keys);
//        //var ctlExListBox = FindViewById<ExpandableListView>(Resource.Id.ctlExListBox);
//        //ctlExListBox.SetAdapter(new AboutUsDataAdapter(this,dictGroup)); 

//        CreateExpendableListData();



//    }

//   public void CreateExpendableListData()
//    {
//        for (int iGroup = 1; iGroup <= 3; iGroup++)
//        {
//            var lstChild = new List<string>();
//            for (int iChild = 1; iChild <= 3; iChild++)
//            {
//                lstChild.Add(string.Format("Group {0} Child {1}", iGroup, iChild));
//            }
//            dictGroup.Add(string.Format("Group {0}", iGroup), lstChild);
//        }
//       List<string> lstKeys = new List<string>(dictGroup.Keys);
//        var ctlExListBox = FindViewById<ExpandableListView>(Resource.Id.ctlExListBox);
//        ctlExListBox.SetAdapter(new AboutUsDataAdapter(this, dictGroup)); 

//    }
//    //public override void OnCreate(bundle b)
//    //{
//    //    base.OnCreate(b);
//    //setContentView(Resource.Layout.About);
//    //}
    
//    }
//}
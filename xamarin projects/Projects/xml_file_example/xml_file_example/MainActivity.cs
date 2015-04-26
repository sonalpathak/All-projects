using Android.App;
using Android.OS;
using Android.Widget;
using System.Xml;
using System;
using Android.Content;
using Android.Runtime;
using Android.Views;
using System.IO;
using System.Collections;
using System.Xml.Linq;
using System.Text;
using System.Linq;
using System.Xml.Schema;
//using System.Linq;
using System.Drawing;
using System.Threading;





namespace xml_file_example
{
	[Activity (Label = "xml_file_example", MainLauncher = true)]
	public class MainActivity : Activity
	{
		//int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			ArrayList ar = new ArrayList ();
			var doc = XDocument.Load ("http://www.globalbackwaters.com/SampleXmlFile.xml");
			var val = (from r in doc.Descendants ("gallery")
			         from a in r.Elements ("image")
			         select a.Value);

			foreach (string s in val) {
				ar.Add (s);
			}
			var gridview=FindViewById<GridView> (Resource.Id.gridView1);
			ImageAdapter a=new ImageView(this);



			// Get our button from the layout resource,
			// and attach an event to it
			//Button button = FindViewById<Button> (Resource.Id.myButton);

			/*button.Click += delegate {
				button.Text = string.Format ("{0} clicks!", doc);
			};*/
		}
	}
}



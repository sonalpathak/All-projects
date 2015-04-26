using System;
using Android.App;
using Android.Views;
using Android.Appwidget;
using Android.Widget;
using Android.Content;
using System.Collections;

namespace xml_file_example
{
	public class ImageAdapter:BaseAdapter
	{
		private readonly Context c;
		private ArrayList Array;
		public ImageAdapter (Context c)
		{
		}
		public override int Count {
			get {
				return Array.Count;
			}
		}

		public override Object GetItem(int position)
		{
			return null;
		}
		public override long GetItemId(int position)
		{
			return 10;
		}
}
}


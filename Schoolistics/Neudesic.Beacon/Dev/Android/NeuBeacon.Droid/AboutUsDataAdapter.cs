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
using Android.Util;

namespace NeuBeacon.Droid
{
    public class AboutUsDataAdapter : BaseExpandableListAdapter
    {
        private readonly Activity _context;
        private readonly IList<AboutUsDetails> _groups;
        

        public AboutUsDataAdapter(Activity activity, IList<AboutUsDetails> quest)
        {
            _context = activity;
            _groups = quest;

        }
//        public AboutUsDataAdapterActivity(Activity activity,Dictionary<string, List<string>> dictGroup)
//{
//    _dictGroup = dictGroup;
//    _activity = activity;
//    _lstGroupID = dictGroup.Keys.ToList();
	
//}
        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            return _groups[groupPosition].Items[childPosition];

        }

        public override long GetChildId(int groupPosition, int childPosition)
        {
            return (groupPosition * _groups.Count) + childPosition;

        }

        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            //if (convertView == null)
                
            //    convertView = _context.LayoutInflater.Inflate(Resource.Layout.Child, null);

            //var textBox = convertView.FindViewById<TextView>(Resource.Id.txtLarge);
            //textBox.Text = _groups[groupPosition].Items[childPosition];
            ////textBox.SetText(item, TextView.BufferType.Normal);

            //return convertView;





            var view = (TextView)(convertView ?? new TextView(_context));

            view.Text = _groups[groupPosition].Items[childPosition];
            view.SetWidth(32);
            view.SetHeight(200);
            view.SetTextColor(Android.Graphics.Color.White);
           // view.SetTextSize(TypedValue.ComplexToDimensionPixelSize,
           //Resource.));
           // view.SetTextSize(TypedValue.ComplexToDimensionPixelSize, 18);
                
           

            view.Click += delegate
            {
                //Toast.MakeText(_context, "Item clicked", ToastLength.Short).Show();
            };

            return view;


        }

        public override int GetChildrenCount(int groupPosition)
        {
            //return 1;
            return _groups[groupPosition].Items.Count;
        }

        public override Java.Lang.Object GetGroup(int groupPosition)
        {
            return _groups[groupPosition];


        }

        public override long GetGroupId(int groupPosition)
        {
            return groupPosition;
        }

        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
           var view = (TextView)(convertView ?? new TextView(_context));
            

            view.Text = _groups[groupPosition].Name;

            return view;


        }

        public override int GroupCount
        {
            get { return _groups.Count; }
        }

        public override bool HasStableIds
        {
            get { return true; }
        }

        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return true;
        }
    }
}
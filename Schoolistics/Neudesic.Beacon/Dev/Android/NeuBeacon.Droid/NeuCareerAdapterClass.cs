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

   public class NeuCareerAdapterClass:BaseExpandableListAdapter
    {
        Dictionary<string, string> _dictGroup = null;
        List<string> _lstGroupID = null;
        Activity _activity;

        public NeuCareerAdapterClass (Activity activity,Dictionary<string, string> dictGroup)
        {
	        _dictGroup = dictGroup;
	        _activity = activity;
	        _lstGroupID = dictGroup.Keys.ToList();
	
        }

        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            return _dictGroup[_lstGroupID[groupPosition]][childPosition];
        }

        public override long GetChildId(int groupPosition, int childPosition)
        {
            return childPosition;
        }

        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            var item = _dictGroup[_lstGroupID[groupPosition]];

            if (convertView == null)
                convertView = _activity.LayoutInflater.Inflate(Resource.Layout.NeucareerChild, null);

            var textBox = convertView.FindViewById<TextView>(Resource.Id.txtSmall);
            textBox.SetText(item, TextView.BufferType.Normal);

            return convertView;
        }

        public override int GetChildrenCount(int groupPosition)
        {
            return 1;
        }

        public override Java.Lang.Object GetGroup(int groupPosition)
        {
            return _lstGroupID[groupPosition];
        }

        public override long GetGroupId(int groupPosition)
        {
            return groupPosition;
        }

        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            var item = _lstGroupID[groupPosition];

            if (convertView == null)
                convertView = _activity.LayoutInflater.Inflate(Resource.Layout.NeuCareerList, null);

            var textBox = convertView.FindViewById<TextView>(Resource.Id.txtLarge);
            textBox.SetText(item, TextView.BufferType.Normal);

            return convertView;
        }

        public override int GroupCount
        {
            get
            {
                return _dictGroup.Count;
            }
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
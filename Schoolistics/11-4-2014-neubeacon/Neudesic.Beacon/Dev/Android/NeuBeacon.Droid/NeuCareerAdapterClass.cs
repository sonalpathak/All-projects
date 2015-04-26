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
using Android.Views.Animations;

namespace NeuBeacon.Droid
{

   public class NeuCareerAdapterClass:BaseExpandableListAdapter
    {
        List<CareerList> _dictGroup = null;
       
        Activity _activity;

        public NeuCareerAdapterClass(Activity activity, List<CareerList> dictGroup)
        {
	        _dictGroup = dictGroup;
	        _activity = activity;  
           
	
        }

        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            return _dictGroup[groupPosition].CareerChild[childPosition];
        }

        public override long GetChildId(int groupPosition, int childPosition)
        {
            return (groupPosition * GroupCount) + childPosition;
        }

        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            var item = _dictGroup[groupPosition].CareerChild[childPosition];

            if (convertView == null)             
               
                
               convertView = _activity.LayoutInflater.Inflate(Resource.Layout.NeucareerChild, null);
            

            var textBox = convertView.FindViewById<TextView>(Resource.Id.txtSmall);
            textBox.SetText(item, TextView.BufferType.Normal);

            

            return convertView;
        }

        public override int GetChildrenCount(int groupPosition)
        {
            return _dictGroup[groupPosition].CareerChild.Count;
        }

        public override Java.Lang.Object GetGroup(int groupPosition)
        {
            return _dictGroup[groupPosition].Career[groupPosition];
        }

        public override long GetGroupId(int groupPosition)
        {
            return groupPosition;
        }

        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            
            var item = _dictGroup[groupPosition].Career;
            

            if (convertView == null)
            convertView = _activity.LayoutInflater.Inflate(Resource.Layout.NeuCareerList, null);          
            var textBox = convertView.FindViewById<TextView>(Resource.Id.txtLarge);
            textBox.SetText(item, TextView.BufferType.Normal);
            ImageView indicator=convertView.FindViewById<ImageView>(Resource.Id.sru);      
                
            
               
                if (isExpanded)
                {
                    indicator.SetImageResource(Resource.Drawable.Neu_Career_select);
                    //collectCoins();
                    
                }
                //else
                //{

                //    indicator.SetImageResource(Resource.Drawable.Neu_Career_select);
                   
                //}
            
           
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
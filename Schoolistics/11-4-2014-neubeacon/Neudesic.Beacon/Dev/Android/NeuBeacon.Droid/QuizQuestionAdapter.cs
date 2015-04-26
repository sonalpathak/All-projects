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
   public  class QuizQuestionAdapter:BaseExpandableListAdapter
    {

        private readonly Activity _context;
        private readonly IList<QuizQuestions> _groups;
        public QuizQuestionAdapter(Activity activity,IList<QuizQuestions> qlist)
        {
            _context = activity;
            _groups = qlist;
        }
        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            //return _groups[groupPosition].Items[childPosition];
            return _groups[groupPosition].Items[childPosition].ToString();

        }

        public override long GetChildId(int groupPosition, int childPosition)
        {
            return (groupPosition * _groups.Count) + childPosition;

        }

        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            //var item = _dictGroup[_lstGroupID[groupPosition]];

            //if (convertView == null)
            //    convertView = _activity.LayoutInflater.Inflate(Resource.Layout.NeucareerChild, null);

            //var textBox = convertView.FindViewById<TextView>(Resource.Id.txtSmall);
            //textBox.SetText(item, TextView.BufferType.Normal);
            
            //return convertView;

            var item1= _groups[groupPosition].Items[childPosition].Question;
            var item2 = _groups[groupPosition].Items[childPosition].Option1;
            var item3 = _groups[groupPosition].Items[childPosition].Option2;
            var item4 = _groups[groupPosition].Items[childPosition].Option3;
            var item5 = _groups[groupPosition].Items[childPosition].Option4;
            var item6 = _groups[groupPosition].Items[childPosition].Answer;
            if (convertView == null)
                convertView = _context.LayoutInflater.Inflate(Resource.Layout.QuizQuestionChild, null);
            var textbox=convertView.FindViewById<TextView>(Resource.Id.question);
            var textbox2 = convertView.FindViewById<TextView>(Resource.Id.answer);
            var radiobutton1 = convertView.FindViewById<RadioButton>(Resource.Id.option1);
            var radiobutton2 = convertView.FindViewById<RadioButton>(Resource.Id.option2);
            var radiobutton3 = convertView.FindViewById<RadioButton>(Resource.Id.option3);
            var radiobutton4 = convertView.FindViewById<RadioButton>(Resource.Id.option4);
            textbox.SetText(item1, TextView.BufferType.Normal);
            textbox2.SetText(item6, TextView.BufferType.Normal);
            radiobutton1.SetText(item2, TextView.BufferType.Normal);
            radiobutton2.SetText(item3, TextView.BufferType.Normal);
            radiobutton3.SetText(item4, TextView.BufferType.Normal);
            radiobutton4.SetText(item5, TextView.BufferType.Normal);

            return convertView;
            


    //uncomment
            //var view = (TextView)(convertView ?? new TextView(_context));

            //view.Text = _groups[groupPosition].Items[childPosition].ToString();
           

            //view.SetTextColor(Android.Graphics.Color.White);
     //view.Click += delegate
            //{
            //    //Toast.MakeText(_context, "Item clicked", ToastLength.Short).Show();
            //};

            //return view;
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
           // var item1 = _groups[groupPosition].Name;
           // var item2 = _groups[groupPosition].image;
           // if (convertView == null)
           //     convertView = _context.LayoutInflater.Inflate(Resource.Layout.Quiz, null);
           // var textbox = convertView.FindViewById<TextView>(Resource.Id.text);
           // var imagebox = convertView.FindViewById<ImageView>(Resource.Id.icon);
           //textbox.SetText(item1, TextView.BufferType.Normal);
           //imagebox.SetBackgroundResource(Resource.Drawable.n_logo);
           
            
            //return convertView;

            var view = (TextView)(convertView ?? new TextView(_context));

            view.SetTextColor(Android.Graphics.Color.White);
            //view.Click+=viewclicked;

          
            view.Text = _groups[groupPosition].Name;
            view.SetBackgroundResource(Resource.Drawable.n_logo);
            
            view.SetHeight(40);
            return view;


        }

        private void viewclicked(object sender, EventArgs e)
        {


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
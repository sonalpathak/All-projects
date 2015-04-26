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
    [Activity(Label = "Career")]
    public class CareerActivity : LayoutActivity
    {
       
       


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Career);
            var careerlist = new List<CareerList>
            {
                new CareerList{
                    Career="Techie",
                    CareerChild=new List<string>
                    {
                       "Hi"
                    }
                },
 
                 new CareerList{
                    Career="Ux",
                    CareerChild=new List<string>
                    {
                        "Hello"+
                        "Hello Ux"
                    }
                },
                new CareerList{
                    Career="Techie",
                    CareerChild=new List<string>
                    {
                       "Hi"
                    }
                },new CareerList{
                    Career="Techie",
                    CareerChild=new List<string>
                    {
                       "Hi"
                    }
                },new CareerList{
                    Career="Techie",
                    CareerChild=new List<string>
                    {
                       "Hi"
                    }
                },new CareerList{
                    Career="Techie",
                    CareerChild=new List<string>
                    {
                       "Hi"
                    }
                },new CareerList{
                    Career="Techie",
                    CareerChild=new List<string>
                    {
                       "Hi"
                    }
                },new CareerList{
                    Career="Techie",
                    CareerChild=new List<string>
                    {
                       "Hi"
                    }
                }

            };
 
                
            var ctlExListBox = FindViewById<ExpandableListView>(Resource.Id.ctlExListBox);

            ctlExListBox.GroupExpand += incrementandChangeCoins;
                      
            ctlExListBox.SetAdapter(new NeuCareerAdapterClass(this, careerlist));
          //  ctlExListBox.GroupClick += incrementandChangeCoins;
        }

        private void incrementandChangeCoins(object sender, ExpandableListView.GroupExpandEventArgs e)
        {
            Coins = Coins + 1;
        }



        //public void incrementandChangeCoins()
        //{
        //    var ctlExListBox = FindViewById<ExpandableListView>(Resource.Id.ctlExListBox);
        //    ctlExListBox.GroupClick += (object sender, ExpandableListView.GroupClickEventArgs e) => { Coins = Coins + 1; };
            
        //}

           
             //  if (Coins == 8)
             //{
             //       StartActivity(typeof(QuizActivity));
             //}            
            
       

              
           
            // Create your application here
        }

        
    }

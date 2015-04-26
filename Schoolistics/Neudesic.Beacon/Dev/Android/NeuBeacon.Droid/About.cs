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
    [Activity(Label = "About Neudesic", MainLauncher = false)]
    public class About: Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.About);
            var group = new List<AboutUsDetails>
            {
                new AboutUsDetails{
                    Name="Neudesic core values",
                    Items=new List<string>
                    {
                        "When you see your journey from a different perspective, your destination becomes clear. At Neudesic, our unique perspective comes from years of experience helping clients achieve their business goals and gain a competitive advantage. And we use that experience to help put your business on a clear path to success.Neudesic leverages innovative technologies and proven development methods to quickly deliver quality, reliable solutions to help you overcome your unique business challenges. Our alliances with Microsoft*, SAP, Dell and other leading technology companies assure your solution will align with your existing and future technology investments.From cloud computing and big data to social collaboration and mobility, Neudesic delivers the capabilities to help you build a strategy that will put your business on top. And we’ll help you stay there with innovative business technology solutions, products and services tailored to your unique business needs.Microsoft Partner (Gold ISV) and Microsoft National Systems Integrator (NSI)",
                        //@"C:\Users\sonal.pathak\Desktop\Expandable_trial\exp\exp\ExpandableListViewExample\Resources\drawable\Icon.png"
                    }
                },

                 new AboutUsDetails{
                    Name="Passion",
                    Items=new List<string>
                    {
                        "sfsadnmbsjamfbcmdsbfmndsbmnfcdsnm fcdsfur destination becomes clear. At Neudesic, our unique perspective comes from years of experience helping clients achieve their business goals and gain a competitive advantage. And we use that experience to help put your business on a clear path to success.Neudesic leverages innovative technologies and proven development methods to quickly deliver quality, reliable solutions to help you overcome your unique business challenges. Our alliances with Microsoft*, SAP, Dell and other leading technology companies assure your solution will align with your existing and future technology investments.From cloud computing and big data to social collaboration and mobility, Neudesic delivers the capabilities to help you build a strategy that will put your business on top. And we’ll help you stay there with innovative business technology solutions, products and services tailored to your unique business needs.Microsoft Partner (Gold ISV) and Microsoft National Systems Integrator (NSI)",
                        @"C:\Users\sonal.pathak\Desktop\Expandable_trial\exp\exp\ExpandableListViewExample\Resources\drawable\Icon.png"
                    }
                }
            };

            var ctlExListBox = FindViewById<ExpandableListView>(Resource.Id.ctlExListBox);
            ctlExListBox.SetAdapter(new AboutUsDataAdapter(this, group)); 
        }
        
    }
}
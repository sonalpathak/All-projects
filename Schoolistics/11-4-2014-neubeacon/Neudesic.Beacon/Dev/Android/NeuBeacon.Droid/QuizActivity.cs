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
    [Activity(Label = "Quiz")]
    public class QuizActivity : LayoutActivity
    {        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            

            SetContentView(Resource.Layout.Quiz);
           // var img = FindViewById<ImageView>(Resource.Id.icon);
           // img.SetBackgroundResource(Resource.Drawable.n_logo);
                 //android:src="@drawable/Icon"
           
            var quizq = new List<QuizQuestions>
            {
                
                new QuizQuestions{
                    Name="C#",
                    
                    Items=new List<Questions>
                    {
                        new Questions{
                    Question="Which of the following statements are TRUE about the .NET CLR?",
                    Option1="It provides a language-neutral development & execution environment.",
                    Option2="It ensures that an application would not be able to access memory that it is not authorized to access.",
                    Option3="It provides services to run managed applications.",
                    Option4="The resources are garbage collected.",
                    Answer="It provides services to run unmanaged applications"

                },

                      new Questions{
                    Question="Which of the following statements is correct about Managed Code?",
                    Option1="Managed code is the code that is compiled by the JIT compilers.",
                    Option2="Managed code is the code where resources are Garbage Collected.",
                    Option3="Managed code is the code that runs on top of Windows",
                    Option4="Managed code is the code that is written to target the services of the CLR",
                    Answer="Managed code is the code that is written to target the services of the CLR"

                },
                 new Questions{
                    Question="Which of the following utilities can be used to compile managed assemblies into processor-specific native code?",
                    Option1="gacutil",
                    Option2="ngen",
                    Option3="sn",
                    Option4="dumpbin",
                    Answer="ngen"

                },
                  new Questions{
                    Question="Choose the correct statements about the LINQ?",
                    Option1="The main concept behind the linq is query",
                    Option2="linq make use of foreach loop to execute the query",
                    Option3="It is not required that linq should make use of IEnumerable interface",
                    Option4="None of the mentioned",
                    Answer="a, b"

                },
                   new Questions{
                    Question="What is Recursion in CSharp defined as?",
                    Option1="Recursion another form of class",
                    Option2="Recursion another process of defining a method that calls other methods repeatedly",
                    Option3="Recursion is a process of defining a method that calls itself repeatedly",
                    Option4="Recursion is a process of defining a method that calls other methods which in turn call again this method",
                    Answer="Recursion is a process of defining a method that calls itself repeatedly"

                }
      

                    }
                },



                  new QuizQuestions{
                    Name="Java",
                    Items=new List<Questions>
                    {
                        new Questions{
                    Question="What is the name of the method used to start a thread execution?",
                    Option1="init()",
                    Option2="start()",
                    Option3="run()",
                    Option4="resume()",
                    Answer="start()"

                },

                       new Questions{
                    Question="Which class does not override the equals() and hashCode() methods, inheriting them directly from class Object?",
                    Option1="java.lang.String",
                    Option2="java.lang.Double",
                    Option3="java.lang.StringBuffer",
                    Option4="java.lang.Character",
                    Answer="java.lang.StringBuffer"

                },
                 new Questions{
                    Question="Which collection class allows you to grow or shrink its size and provides indexed access to its elements, but whose methods are not synchronized?",
                    Option1="java.util.HashSet",
                    Option2="java.util.LinkedHashSet",
                    Option3="java.util.List",
                    Option4="java.util.ArrayList",
                    Answer="java.util.ArrayList"

                },
                  new Questions{
                    Question="Which is a reserved word in the Java programming language?",
                    Option1="method",
                    Option2="native",
                    Option3="subclasses",
                    Option4="reference",
                    Answer="native"

                },
                new Questions{
                    Question="Which is a valid keyword in java?",
                    Option1="interface",
                    Option2="string",
                    Option3="Float",
                    Option4="unsigned",
                    Answer="interface"

                },
    

                    }
                },

                new QuizQuestions{
                    Name="Android",
                      Items=new List<Questions>
                      {
                          new Questions
                          {
                    Question="Android doesn't support which format.",
                    Option1="MP4",
                    Option2="MPEG",
                    Option3="AVI",
                    Option4="MIDI",
                    Answer="interface"
                          },
                             new Questions
                          {
                    Question="The OD 1.5 version is called?",
                    Option1="Apple pie",
                    Option2="Lemon tart",
                    Option3="Cupcake",
                    Option4="donut",
                    Answer="cup cake"
                          },
                             new Questions
                          {
                    Question="Which piece of code used in Android is not open source?",
                    Option1="Keypad driver",
                    Option2="WiFi? driver",
                    Option3="Audio driver",
                    Option4="Power management",
                    Answer="WiFi? driver"
                          },
                             new Questions
                          {
                    Question="What does the .apk extension stand for?",
                    Option1="Application Package",
                    Option2="Application Program Kit",
                    Option3="Android Proprietary Kit",
                    Option4="Android Package",
                    Answer="Application Package"
                          },
                             new Questions
                          {
                    Question="What is the name of the program that converts Java byte code into Dalvik byte code?",
                    Option1="Android Interpretive Compiler",
                    Option2="Dalvik Converter",
                    Option3="Dex compiler",
                    Option4="Mobile Interpretive Compiler",
                    Answer="Dex compiler"
                          }
                      }
                }
                

            };
            //var quiz = new List<Questions>
            //{
            //    new Questions{
            //        Question="",
            //        Option1="",
            //        Option2="",
            //        Option3="",
            //        Option4="",
            //        Answer=""

            //    }
            //};

            //var button = FindViewById<Button>(Resource.Id.button1);
            // Create your application here
            //button.Click += (sender, eventArgs) =>
            //{
            //    Coins = Coins + 1;
            //    if (Coins == 4)
            //    {
            //        StartActivity(typeof(CareerActivity));
            //    }
            //};
            var ctlExListBox = FindViewById<ExpandableListView>(Resource.Id.ctlExListBox);
            ctlExListBox.SetAdapter(new QuizQuestionAdapter(this, quizq)); 

        }
    }
}
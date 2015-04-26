﻿using Cirrious.CrossCore;
using Neudesic.Schoolistics.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Neudesic.Schoolistics.WindowsStore.Usercontrols
{
    public sealed partial class FindSchoolsSaveControl : UserControl
    {
        public event RoutedEventHandler cancelSavePopUp;

        public FindSchoolsSaveControl()
        {
            try
            {
                this.InitializeComponent();
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in FindSchoolsSaveControl.xaml.cs : FindSchoolsSaveControl : " + ex.ToString());
            }
        }

        private void contentButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cancelSavePopUp(sender, e);
            }
            catch (Exception ex)
            {
                Mvx.Error("Error in FindSchoolsSaveControl.xaml.cs : contentButton_Click : " + ex.ToString());
            }
        }
    }
}

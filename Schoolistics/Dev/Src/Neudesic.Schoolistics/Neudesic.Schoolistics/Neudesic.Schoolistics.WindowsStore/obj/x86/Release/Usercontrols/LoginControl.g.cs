﻿

#pragma checksum "C:\Users\NeuAdmin\Desktop\Schoolistics\Dev\Src\Neudesic.Schoolistics\Neudesic.Schoolistics\Neudesic.Schoolistics.WindowsStore\Usercontrols\LoginControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D68E9E1B91D493F90A6B0A58EC3E4FE9"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Neudesic.Schoolistics.WindowsStore.Usercontrols
{
    partial class LoginControl : global::Windows.UI.Xaml.Controls.UserControl, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 47 "..\..\..\Usercontrols\LoginControl.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.facebookLogin_Tapped;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 48 "..\..\..\Usercontrols\LoginControl.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.twitterLogin_Tapped;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 49 "..\..\..\Usercontrols\LoginControl.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.google_Tapped;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 50 "..\..\..\Usercontrols\LoginControl.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.signUp_Tapped;
                 #line default
                 #line hidden
                break;
            case 5:
                #line 19 "..\..\..\Usercontrols\LoginControl.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.GoBack;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


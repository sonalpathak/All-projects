﻿

#pragma checksum "C:\Users\NeuAdmin\Desktop\Schoolistics\Dev\Src\Neudesic.Schoolistics\Neudesic.Schoolistics\Neudesic.Schoolistics.WindowsStore\Usercontrols\SearchResultsGridViewControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "2CFFEA0BE84BF6E06D3B03546FEDDD69"
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
    partial class SearchResultsGridViewControl : global::Windows.UI.Xaml.Controls.UserControl, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 24 "..\..\..\Usercontrols\SearchResultsGridViewControl.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.Selector)(target)).SelectionChanged += this.itemGridView_SelectionChanged;
                 #line default
                 #line hidden
                #line 27 "..\..\..\Usercontrols\SearchResultsGridViewControl.xaml"
                ((global::Windows.UI.Xaml.Controls.ListViewBase)(target)).ItemClick += this.itemGridView_ItemClick;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}



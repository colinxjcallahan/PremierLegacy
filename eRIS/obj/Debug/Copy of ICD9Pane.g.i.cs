﻿#pragma checksum "C:\Documents and Settings\bennie.wilson\My Documents\Visual Studio 2010\Projects\eRIS\eRIS\Copy of ICD9Pane.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "5016812397532BCD23CC24B4378396BC"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;
using Telerik.Windows.Controls;


namespace eRIS {
    
    
    public partial class ICD9Pane : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.UserControl _ICD9Pane;
        
        internal System.Windows.Controls.Grid MainGrid;
        
        internal System.Windows.Controls.TextBox sCode;
        
        internal System.Windows.Controls.TextBox sDescription;
        
        internal System.Windows.Controls.Button btnSearch;
        
        internal Telerik.Windows.Controls.RadGridView gvDetails;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/eRIS;component/Copy%20of%20ICD9Pane.xaml", System.UriKind.Relative));
            this._ICD9Pane = ((System.Windows.Controls.UserControl)(this.FindName("_ICD9Pane")));
            this.MainGrid = ((System.Windows.Controls.Grid)(this.FindName("MainGrid")));
            this.sCode = ((System.Windows.Controls.TextBox)(this.FindName("sCode")));
            this.sDescription = ((System.Windows.Controls.TextBox)(this.FindName("sDescription")));
            this.btnSearch = ((System.Windows.Controls.Button)(this.FindName("btnSearch")));
            this.gvDetails = ((Telerik.Windows.Controls.RadGridView)(this.FindName("gvDetails")));
        }
    }
}


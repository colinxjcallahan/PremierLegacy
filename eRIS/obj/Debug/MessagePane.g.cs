﻿#pragma checksum "C:\Projects\PremierLegacy\eRIS\MessagePane.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6E4CA8303ED1D1E0912FD8CC9356148A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
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
    
    
    public partial class MessagePane : Telerik.Windows.Controls.RadDocumentPane {
        
        internal Telerik.Windows.Controls.RadDocumentPane _MessagePane;
        
        internal Telerik.Windows.Controls.RadGridView gvTotals;
        
        internal Telerik.Windows.Controls.RadGridView gvDetails;
        
        internal Telerik.Windows.Controls.RadExpander optionsExpander;
        
        internal Telerik.Windows.Controls.RadCalendar SelectDate;
        
        internal System.Windows.Controls.Button btnRefresh;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/eRIS;component/MessagePane.xaml", System.UriKind.Relative));
            this._MessagePane = ((Telerik.Windows.Controls.RadDocumentPane)(this.FindName("_MessagePane")));
            this.gvTotals = ((Telerik.Windows.Controls.RadGridView)(this.FindName("gvTotals")));
            this.gvDetails = ((Telerik.Windows.Controls.RadGridView)(this.FindName("gvDetails")));
            this.optionsExpander = ((Telerik.Windows.Controls.RadExpander)(this.FindName("optionsExpander")));
            this.SelectDate = ((Telerik.Windows.Controls.RadCalendar)(this.FindName("SelectDate")));
            this.btnRefresh = ((System.Windows.Controls.Button)(this.FindName("btnRefresh")));
        }
    }
}


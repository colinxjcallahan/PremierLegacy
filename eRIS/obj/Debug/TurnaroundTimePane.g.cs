﻿#pragma checksum "C:\Projects\PremierLegacy\eRIS\TurnaroundTimePane.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "F2A39779BBA930B460E56D54E06AB265"
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
    
    
    public partial class TurnaroundTimePane : Telerik.Windows.Controls.RadDocumentPane {
        
        internal Telerik.Windows.Controls.RadDocumentPane _TurnaroundTimePane;
        
        internal Telerik.Windows.Controls.RadChart TaTChart;
        
        internal Telerik.Windows.Controls.RadExpander optionsExpander;
        
        internal Telerik.Windows.Controls.RadCalendar chartDate;
        
        internal System.Windows.Controls.Button btnRefresh;
        
        internal Telerik.Windows.Controls.RadGridView gvDetails;
        
        internal System.Windows.Controls.ComboBox cmbAggregation;
        
        internal System.Windows.Controls.Button btnDraw;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/eRIS;component/TurnaroundTimePane.xaml", System.UriKind.Relative));
            this._TurnaroundTimePane = ((Telerik.Windows.Controls.RadDocumentPane)(this.FindName("_TurnaroundTimePane")));
            this.TaTChart = ((Telerik.Windows.Controls.RadChart)(this.FindName("TaTChart")));
            this.optionsExpander = ((Telerik.Windows.Controls.RadExpander)(this.FindName("optionsExpander")));
            this.chartDate = ((Telerik.Windows.Controls.RadCalendar)(this.FindName("chartDate")));
            this.btnRefresh = ((System.Windows.Controls.Button)(this.FindName("btnRefresh")));
            this.gvDetails = ((Telerik.Windows.Controls.RadGridView)(this.FindName("gvDetails")));
            this.cmbAggregation = ((System.Windows.Controls.ComboBox)(this.FindName("cmbAggregation")));
            this.btnDraw = ((System.Windows.Controls.Button)(this.FindName("btnDraw")));
        }
    }
}


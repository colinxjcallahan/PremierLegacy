﻿#pragma checksum "C:\Documents and Settings\bennie.wilson\My Documents\Visual Studio 2010\Projects\eRIS\eRIS\Copy of StudiesReadPerDayPane.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "30D069ED50342082B60E644BCA870F9C"
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
using Telerik.Windows.Controls.Charting;


namespace eRIS {
    
    
    public partial class StudiesReadPerDayPane : Telerik.Windows.Controls.RadDocumentPane {
        
        internal Telerik.Windows.Controls.RadChart ReadByDateChart;
        
        internal Telerik.Windows.Controls.Charting.ChartArea ReadByDateChartArea;
        
        internal Telerik.Windows.Controls.RadExpander optionsExpander;
        
        internal Telerik.Windows.Controls.RadCalendar chartDate;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/eRIS;component/Copy%20of%20StudiesReadPerDayPane.xaml", System.UriKind.Relative));
            this.ReadByDateChart = ((Telerik.Windows.Controls.RadChart)(this.FindName("ReadByDateChart")));
            this.ReadByDateChartArea = ((Telerik.Windows.Controls.Charting.ChartArea)(this.FindName("ReadByDateChartArea")));
            this.optionsExpander = ((Telerik.Windows.Controls.RadExpander)(this.FindName("optionsExpander")));
            this.chartDate = ((Telerik.Windows.Controls.RadCalendar)(this.FindName("chartDate")));
            this.btnRefresh = ((System.Windows.Controls.Button)(this.FindName("btnRefresh")));
        }
    }
}


﻿#pragma checksum "C:\Projects\eRIS\eRIS\ReportsCodedPerDayPane.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "86A803107AC17C2D683247ED74982254"
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
using Telerik.Windows.Controls.Charting;


namespace eRIS {
    
    
    public partial class ReportsCodedPerDayPane : Telerik.Windows.Controls.RadDocumentPane {
        
        internal Telerik.Windows.Controls.RadDocumentPane _ReportsCodedPerDayPane;
        
        internal Telerik.Windows.Controls.RadChart ToCodeNumChart;
        
        internal Telerik.Windows.Controls.Charting.ChartArea ToCodeNumChartArea;
        
        internal Telerik.Windows.Controls.RadChart ToCodeDaysChart;
        
        internal Telerik.Windows.Controls.Charting.ChartArea ToCodeDaysChartArea;
        
        internal Telerik.Windows.Controls.RadChart CodedByDateChart;
        
        internal Telerik.Windows.Controls.Charting.ChartArea CodedByDateChartArea;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/eRIS;component/ReportsCodedPerDayPane.xaml", System.UriKind.Relative));
            this._ReportsCodedPerDayPane = ((Telerik.Windows.Controls.RadDocumentPane)(this.FindName("_ReportsCodedPerDayPane")));
            this.ToCodeNumChart = ((Telerik.Windows.Controls.RadChart)(this.FindName("ToCodeNumChart")));
            this.ToCodeNumChartArea = ((Telerik.Windows.Controls.Charting.ChartArea)(this.FindName("ToCodeNumChartArea")));
            this.ToCodeDaysChart = ((Telerik.Windows.Controls.RadChart)(this.FindName("ToCodeDaysChart")));
            this.ToCodeDaysChartArea = ((Telerik.Windows.Controls.Charting.ChartArea)(this.FindName("ToCodeDaysChartArea")));
            this.CodedByDateChart = ((Telerik.Windows.Controls.RadChart)(this.FindName("CodedByDateChart")));
            this.CodedByDateChartArea = ((Telerik.Windows.Controls.Charting.ChartArea)(this.FindName("CodedByDateChartArea")));
            this.optionsExpander = ((Telerik.Windows.Controls.RadExpander)(this.FindName("optionsExpander")));
            this.chartDate = ((Telerik.Windows.Controls.RadCalendar)(this.FindName("chartDate")));
            this.btnRefresh = ((System.Windows.Controls.Button)(this.FindName("btnRefresh")));
        }
    }
}


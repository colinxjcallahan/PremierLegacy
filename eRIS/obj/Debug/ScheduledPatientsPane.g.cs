﻿#pragma checksum "C:\Projects\PremierLegacy\eRIS\ScheduledPatientsPane.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "56A46B84A577A5746A77714AC0745C01"
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
    
    
    public partial class ScheduledPatientsPane : Telerik.Windows.Controls.RadDocumentPane {
        
        internal Telerik.Windows.Controls.RadDocumentPane _ScheduledPatientsPane;
        
        internal System.Windows.Controls.StackPanel ChartPanel;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/eRIS;component/ScheduledPatientsPane.xaml", System.UriKind.Relative));
            this._ScheduledPatientsPane = ((Telerik.Windows.Controls.RadDocumentPane)(this.FindName("_ScheduledPatientsPane")));
            this.ChartPanel = ((System.Windows.Controls.StackPanel)(this.FindName("ChartPanel")));
            this.optionsExpander = ((Telerik.Windows.Controls.RadExpander)(this.FindName("optionsExpander")));
            this.chartDate = ((Telerik.Windows.Controls.RadCalendar)(this.FindName("chartDate")));
            this.btnRefresh = ((System.Windows.Controls.Button)(this.FindName("btnRefresh")));
        }
    }
}


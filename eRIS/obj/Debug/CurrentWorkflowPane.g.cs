﻿#pragma checksum "C:\Documents and Settings\bennie.wilson\My Documents\Visual Studio 2010\Projects\eRIS\eRIS\CurrentWorkflowPane.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "43E6A0882E4D3A7B8351EAFA69572BB7"
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
    
    
    public partial class CurrentWorkflowPane : Telerik.Windows.Controls.RadDocumentPane {
        
        internal Telerik.Windows.Controls.RadDocumentPane _CurrentWorkflowPane;
        
        internal Telerik.Windows.Controls.RadChart radChart;
        
        internal Telerik.Windows.Controls.Charting.ChartArea mainChart;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/eRIS;component/CurrentWorkflowPane.xaml", System.UriKind.Relative));
            this._CurrentWorkflowPane = ((Telerik.Windows.Controls.RadDocumentPane)(this.FindName("_CurrentWorkflowPane")));
            this.radChart = ((Telerik.Windows.Controls.RadChart)(this.FindName("radChart")));
            this.mainChart = ((Telerik.Windows.Controls.Charting.ChartArea)(this.FindName("mainChart")));
        }
    }
}


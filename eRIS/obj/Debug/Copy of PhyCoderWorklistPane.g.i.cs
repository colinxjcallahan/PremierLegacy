﻿#pragma checksum "C:\Documents and Settings\bennie.wilson\My Documents\Visual Studio 2010\Projects\eRIS\eRIS\Copy of PhyCoderWorklistPane.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "36D3AC2B8573E1F4C35F8ED7D067B8E9"
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
    
    
    public partial class PhyCoderWorklistPane : Telerik.Windows.Controls.RadDocumentPane {
        
        internal Telerik.Windows.Controls.RadDocumentPane _PhyCoderWorklistPane;
        
        internal Telerik.Windows.Controls.RadGridView gvAvailableToCode;
        
        internal Telerik.Windows.Controls.RadRadioButton bMissingDemo;
        
        internal System.Windows.Controls.TextBlock MissingDemo;
        
        internal Telerik.Windows.Controls.RadRadioButton bMissingOrder;
        
        internal System.Windows.Controls.TextBlock MissingOrder;
        
        internal Telerik.Windows.Controls.RadRadioButton bMissingReport;
        
        internal System.Windows.Controls.TextBlock MissingReport;
        
        internal Telerik.Windows.Controls.RadRadioButton bPending;
        
        internal System.Windows.Controls.TextBlock Pending;
        
        internal Telerik.Windows.Controls.RadRadioButton bPendingMe;
        
        internal System.Windows.Controls.TextBlock PendingMe;
        
        internal System.Windows.Controls.CheckBox xCoding;
        
        internal System.Windows.Controls.TextBox xLocation;
        
        internal System.Windows.Controls.TextBox xDate;
        
        internal System.Windows.Controls.TextBox xExamNumber;
        
        internal System.Windows.Controls.TextBox xPatient;
        
        internal System.Windows.Controls.TextBox xMRN;
        
        internal System.Windows.Controls.TextBox xStatus;
        
        internal System.Windows.Controls.TextBox xModality;
        
        internal System.Windows.Controls.TextBox xRecordLimit;
        
        internal System.Windows.Controls.TextBox xDateLimit;
        
        internal System.Windows.Controls.Button btnClear;
        
        internal System.Windows.Controls.Button btnExport;
        
        internal System.Windows.Controls.Button btnSearch;
        
        internal Telerik.Windows.Controls.RadGridView gvCoded;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/eRIS;component/Copy%20of%20PhyCoderWorklistPane.xaml", System.UriKind.Relative));
            this._PhyCoderWorklistPane = ((Telerik.Windows.Controls.RadDocumentPane)(this.FindName("_PhyCoderWorklistPane")));
            this.gvAvailableToCode = ((Telerik.Windows.Controls.RadGridView)(this.FindName("gvAvailableToCode")));
            this.bMissingDemo = ((Telerik.Windows.Controls.RadRadioButton)(this.FindName("bMissingDemo")));
            this.MissingDemo = ((System.Windows.Controls.TextBlock)(this.FindName("MissingDemo")));
            this.bMissingOrder = ((Telerik.Windows.Controls.RadRadioButton)(this.FindName("bMissingOrder")));
            this.MissingOrder = ((System.Windows.Controls.TextBlock)(this.FindName("MissingOrder")));
            this.bMissingReport = ((Telerik.Windows.Controls.RadRadioButton)(this.FindName("bMissingReport")));
            this.MissingReport = ((System.Windows.Controls.TextBlock)(this.FindName("MissingReport")));
            this.bPending = ((Telerik.Windows.Controls.RadRadioButton)(this.FindName("bPending")));
            this.Pending = ((System.Windows.Controls.TextBlock)(this.FindName("Pending")));
            this.bPendingMe = ((Telerik.Windows.Controls.RadRadioButton)(this.FindName("bPendingMe")));
            this.PendingMe = ((System.Windows.Controls.TextBlock)(this.FindName("PendingMe")));
            this.xCoding = ((System.Windows.Controls.CheckBox)(this.FindName("xCoding")));
            this.xLocation = ((System.Windows.Controls.TextBox)(this.FindName("xLocation")));
            this.xDate = ((System.Windows.Controls.TextBox)(this.FindName("xDate")));
            this.xExamNumber = ((System.Windows.Controls.TextBox)(this.FindName("xExamNumber")));
            this.xPatient = ((System.Windows.Controls.TextBox)(this.FindName("xPatient")));
            this.xMRN = ((System.Windows.Controls.TextBox)(this.FindName("xMRN")));
            this.xStatus = ((System.Windows.Controls.TextBox)(this.FindName("xStatus")));
            this.xModality = ((System.Windows.Controls.TextBox)(this.FindName("xModality")));
            this.xRecordLimit = ((System.Windows.Controls.TextBox)(this.FindName("xRecordLimit")));
            this.xDateLimit = ((System.Windows.Controls.TextBox)(this.FindName("xDateLimit")));
            this.btnClear = ((System.Windows.Controls.Button)(this.FindName("btnClear")));
            this.btnExport = ((System.Windows.Controls.Button)(this.FindName("btnExport")));
            this.btnSearch = ((System.Windows.Controls.Button)(this.FindName("btnSearch")));
            this.gvCoded = ((Telerik.Windows.Controls.RadGridView)(this.FindName("gvCoded")));
            this.gvDetails = ((Telerik.Windows.Controls.RadGridView)(this.FindName("gvDetails")));
        }
    }
}


﻿#pragma checksum "C:\Documents and Settings\bennie.wilson\My Documents\Visual Studio 2010\Projects\eRIS\eRIS\BioImagingPane.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "86A28E791FE8641398E28B0782E5194A"
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
    
    
    public partial class BioImagingPane : Telerik.Windows.Controls.RadDocumentPane {
        
        internal Telerik.Windows.Controls.RadDocumentPane _BioImagingPane;
        
        internal System.Windows.Controls.TextBox xLocation;
        
        internal System.Windows.Controls.TextBox xDate;
        
        internal System.Windows.Controls.TextBox xMRN;
        
        internal System.Windows.Controls.TextBox xSSN;
        
        internal System.Windows.Controls.TextBox xPatient;
        
        internal System.Windows.Controls.TextBox xDoctor;
        
        internal System.Windows.Controls.TextBox xRecordLimit;
        
        internal System.Windows.Controls.Button btnClear;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/eRIS;component/BioImagingPane.xaml", System.UriKind.Relative));
            this._BioImagingPane = ((Telerik.Windows.Controls.RadDocumentPane)(this.FindName("_BioImagingPane")));
            this.xLocation = ((System.Windows.Controls.TextBox)(this.FindName("xLocation")));
            this.xDate = ((System.Windows.Controls.TextBox)(this.FindName("xDate")));
            this.xMRN = ((System.Windows.Controls.TextBox)(this.FindName("xMRN")));
            this.xSSN = ((System.Windows.Controls.TextBox)(this.FindName("xSSN")));
            this.xPatient = ((System.Windows.Controls.TextBox)(this.FindName("xPatient")));
            this.xDoctor = ((System.Windows.Controls.TextBox)(this.FindName("xDoctor")));
            this.xRecordLimit = ((System.Windows.Controls.TextBox)(this.FindName("xRecordLimit")));
            this.btnClear = ((System.Windows.Controls.Button)(this.FindName("btnClear")));
            this.btnSearch = ((System.Windows.Controls.Button)(this.FindName("btnSearch")));
            this.gvDetails = ((Telerik.Windows.Controls.RadGridView)(this.FindName("gvDetails")));
        }
    }
}


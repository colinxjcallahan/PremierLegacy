﻿#pragma checksum "C:\Projects\eRIS\eRIS\RadiologistXrefPane.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "3A250724CD1EBEE19FBECEEA7E8C483C"
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
    
    
    public partial class RadiologistXrefPane : Telerik.Windows.Controls.RadDocumentPane {
        
        internal Telerik.Windows.Controls.RadDocumentPane _RadiologistXrefPane;
        
        internal Telerik.Windows.Controls.RadComboBox cbFacility;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/eRIS;component/RadiologistXrefPane.xaml", System.UriKind.Relative));
            this._RadiologistXrefPane = ((Telerik.Windows.Controls.RadDocumentPane)(this.FindName("_RadiologistXrefPane")));
            this.cbFacility = ((Telerik.Windows.Controls.RadComboBox)(this.FindName("cbFacility")));
            this.gvDetails = ((Telerik.Windows.Controls.RadGridView)(this.FindName("gvDetails")));
        }
    }
}


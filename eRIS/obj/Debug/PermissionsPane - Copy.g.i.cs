﻿#pragma checksum "C:\Projects\eRIS\eRIS\PermissionsPane - Copy.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "05168B851BEF97606BD11D2DB942A2B3"
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
    
    
    public partial class PermissionsPane : Telerik.Windows.Controls.RadDocumentPane {
        
        internal Telerik.Windows.Controls.RadDocumentPane _PermissionsPane;
        
        internal System.Windows.Controls.Grid MainGrid;
        
        internal Telerik.Windows.Controls.RadGridView gvDetails;
        
        internal System.Windows.Controls.Grid DetailGrid;
        
        internal System.Windows.Controls.TextBlock detailTitle;
        
        internal System.Windows.Controls.CheckBox isAdmin;
        
        internal System.Windows.Controls.CheckBox isRISAdmin;
        
        internal System.Windows.Controls.CheckBox isManager;
        
        internal System.Windows.Controls.CheckBox isRadiologist;
        
        internal System.Windows.Controls.CheckBox isRadiologistADI;
        
        internal Telerik.Windows.Controls.RadComboBox RadGroup;
        
        internal System.Windows.Controls.CheckBox isTech;
        
        internal System.Windows.Controls.CheckBox isClerk;
        
        internal System.Windows.Controls.CheckBox isTelerad;
        
        internal System.Windows.Controls.CheckBox isPhysician;
        
        internal System.Windows.Controls.CheckBox isPatient;
        
        internal System.Windows.Controls.CheckBox isCoder;
        
        internal System.Windows.Controls.CheckBox isCoderAssignable;
        
        internal System.Windows.Controls.CheckBox isCoderLimited;
        
        internal System.Windows.Controls.CheckBox isFollowUpEnabled;
        
        internal System.Windows.Controls.TextBox shortName;
        
        internal System.Windows.Controls.Button btnUpdate;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/eRIS;component/PermissionsPane%20-%20Copy.xaml", System.UriKind.Relative));
            this._PermissionsPane = ((Telerik.Windows.Controls.RadDocumentPane)(this.FindName("_PermissionsPane")));
            this.MainGrid = ((System.Windows.Controls.Grid)(this.FindName("MainGrid")));
            this.gvDetails = ((Telerik.Windows.Controls.RadGridView)(this.FindName("gvDetails")));
            this.DetailGrid = ((System.Windows.Controls.Grid)(this.FindName("DetailGrid")));
            this.detailTitle = ((System.Windows.Controls.TextBlock)(this.FindName("detailTitle")));
            this.isAdmin = ((System.Windows.Controls.CheckBox)(this.FindName("isAdmin")));
            this.isRISAdmin = ((System.Windows.Controls.CheckBox)(this.FindName("isRISAdmin")));
            this.isManager = ((System.Windows.Controls.CheckBox)(this.FindName("isManager")));
            this.isRadiologist = ((System.Windows.Controls.CheckBox)(this.FindName("isRadiologist")));
            this.isRadiologistADI = ((System.Windows.Controls.CheckBox)(this.FindName("isRadiologistADI")));
            this.RadGroup = ((Telerik.Windows.Controls.RadComboBox)(this.FindName("RadGroup")));
            this.isTech = ((System.Windows.Controls.CheckBox)(this.FindName("isTech")));
            this.isClerk = ((System.Windows.Controls.CheckBox)(this.FindName("isClerk")));
            this.isTelerad = ((System.Windows.Controls.CheckBox)(this.FindName("isTelerad")));
            this.isPhysician = ((System.Windows.Controls.CheckBox)(this.FindName("isPhysician")));
            this.isPatient = ((System.Windows.Controls.CheckBox)(this.FindName("isPatient")));
            this.isCoder = ((System.Windows.Controls.CheckBox)(this.FindName("isCoder")));
            this.isCoderAssignable = ((System.Windows.Controls.CheckBox)(this.FindName("isCoderAssignable")));
            this.isCoderLimited = ((System.Windows.Controls.CheckBox)(this.FindName("isCoderLimited")));
            this.isFollowUpEnabled = ((System.Windows.Controls.CheckBox)(this.FindName("isFollowUpEnabled")));
            this.shortName = ((System.Windows.Controls.TextBox)(this.FindName("shortName")));
            this.btnUpdate = ((System.Windows.Controls.Button)(this.FindName("btnUpdate")));
        }
    }
}


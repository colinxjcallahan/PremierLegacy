﻿#pragma checksum "C:\Projects\eRIS\eRIS\PhyCoderOrdersPane.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "48DC8FA161E2C3E0C0E9537CDC054A12"
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
    
    
    public partial class PhyCoderOrdersPane : Telerik.Windows.Controls.RadDocumentPane {
        
        internal System.Windows.Controls.Grid MainGrid;
        
        internal System.Windows.Controls.TextBlock dLocation;
        
        internal System.Windows.Controls.TextBlock dMRN;
        
        internal System.Windows.Controls.TextBlock dVisit;
        
        internal System.Windows.Controls.TextBlock dExam;
        
        internal System.Windows.Controls.TextBlock dProcedure;
        
        internal System.Windows.Controls.TextBlock dAdmitDate;
        
        internal System.Windows.Controls.TextBlock dOrder;
        
        internal System.Windows.Controls.TextBlock dPerform;
        
        internal System.Windows.Controls.TextBlock dIns1;
        
        internal System.Windows.Controls.TextBlock dIns2;
        
        internal System.Windows.Controls.TextBlock dIns3;
        
        internal System.Windows.Controls.TextBlock dStatus1;
        
        internal System.Windows.Controls.TextBlock dStatus2;
        
        internal System.Windows.Controls.Button btnDelete;
        
        internal System.Windows.Controls.TabControl tabControl;
        
        internal System.Windows.Controls.Button btnNextOrder;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/eRIS;component/PhyCoderOrdersPane.xaml", System.UriKind.Relative));
            this.MainGrid = ((System.Windows.Controls.Grid)(this.FindName("MainGrid")));
            this.dLocation = ((System.Windows.Controls.TextBlock)(this.FindName("dLocation")));
            this.dMRN = ((System.Windows.Controls.TextBlock)(this.FindName("dMRN")));
            this.dVisit = ((System.Windows.Controls.TextBlock)(this.FindName("dVisit")));
            this.dExam = ((System.Windows.Controls.TextBlock)(this.FindName("dExam")));
            this.dProcedure = ((System.Windows.Controls.TextBlock)(this.FindName("dProcedure")));
            this.dAdmitDate = ((System.Windows.Controls.TextBlock)(this.FindName("dAdmitDate")));
            this.dOrder = ((System.Windows.Controls.TextBlock)(this.FindName("dOrder")));
            this.dPerform = ((System.Windows.Controls.TextBlock)(this.FindName("dPerform")));
            this.dIns1 = ((System.Windows.Controls.TextBlock)(this.FindName("dIns1")));
            this.dIns2 = ((System.Windows.Controls.TextBlock)(this.FindName("dIns2")));
            this.dIns3 = ((System.Windows.Controls.TextBlock)(this.FindName("dIns3")));
            this.dStatus1 = ((System.Windows.Controls.TextBlock)(this.FindName("dStatus1")));
            this.dStatus2 = ((System.Windows.Controls.TextBlock)(this.FindName("dStatus2")));
            this.btnDelete = ((System.Windows.Controls.Button)(this.FindName("btnDelete")));
            this.tabControl = ((System.Windows.Controls.TabControl)(this.FindName("tabControl")));
            this.btnNextOrder = ((System.Windows.Controls.Button)(this.FindName("btnNextOrder")));
        }
    }
}


﻿#pragma checksum "C:\Documents and Settings\bennie.wilson\My Documents\Visual Studio 2010\Projects\eRIS\eRIS\LoginImagesPane.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6F1EF9E53E2D9C04CD650C572A63A56B"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.225
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
    
    
    public partial class LoginImagesPane : Telerik.Windows.Controls.RadDocumentPane {
        
        internal Telerik.Windows.Controls.RadDocumentPane _LoginImagesPane;
        
        internal System.Windows.Controls.Grid MainGrid;
        
        internal System.Windows.Controls.Image imageLeft;
        
        internal System.Windows.Controls.Image imageRight;
        
        internal Telerik.Windows.Controls.RadUpload radUploadLeft;
        
        internal System.Windows.Controls.TextBox userName;
        
        internal System.Windows.Controls.PasswordBox password;
        
        internal System.Windows.Controls.Button btnLogin;
        
        internal Telerik.Windows.Controls.RadUpload radUploadRight;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/eRIS;component/LoginImagesPane.xaml", System.UriKind.Relative));
            this._LoginImagesPane = ((Telerik.Windows.Controls.RadDocumentPane)(this.FindName("_LoginImagesPane")));
            this.MainGrid = ((System.Windows.Controls.Grid)(this.FindName("MainGrid")));
            this.imageLeft = ((System.Windows.Controls.Image)(this.FindName("imageLeft")));
            this.imageRight = ((System.Windows.Controls.Image)(this.FindName("imageRight")));
            this.radUploadLeft = ((Telerik.Windows.Controls.RadUpload)(this.FindName("radUploadLeft")));
            this.userName = ((System.Windows.Controls.TextBox)(this.FindName("userName")));
            this.password = ((System.Windows.Controls.PasswordBox)(this.FindName("password")));
            this.btnLogin = ((System.Windows.Controls.Button)(this.FindName("btnLogin")));
            this.radUploadRight = ((Telerik.Windows.Controls.RadUpload)(this.FindName("radUploadRight")));
        }
    }
}


﻿#pragma checksum "C:\Documents and Settings\bennie.wilson\My Documents\Visual Studio 2010\Projects\Dashboard\Dashboard\LoginImages.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "F85F39486552F7B5989EC1436669C0F3"
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


namespace Dashboard {
    
    
    public partial class LoginImages : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid LoginGrid;
        
        internal System.Windows.Controls.TextBox userName;
        
        internal System.Windows.Controls.PasswordBox password;
        
        internal System.Windows.Controls.Button btnLogin;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/Dashboard;component/LoginImages.xaml", System.UriKind.Relative));
            this.LoginGrid = ((System.Windows.Controls.Grid)(this.FindName("LoginGrid")));
            this.userName = ((System.Windows.Controls.TextBox)(this.FindName("userName")));
            this.password = ((System.Windows.Controls.PasswordBox)(this.FindName("password")));
            this.btnLogin = ((System.Windows.Controls.Button)(this.FindName("btnLogin")));
        }
    }
}


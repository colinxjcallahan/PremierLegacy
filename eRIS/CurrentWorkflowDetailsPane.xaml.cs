using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;
using System.ComponentModel;
using System.ServiceModel;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using eRIS;
using eRIS.eRISServiceReference;

namespace eRIS
{
    public partial class CurrentWorkflowDetailsPane : RadDocumentPane
    {
        private MainPage pPage = Application.Current.RootVisual as MainPage;
        public CurrentWorkflowDetailsPane(string chart)
        {
            InitializeComponent();
            // Set header
            Header = chart + " at " + DateTime.Now.ToShortTimeString();
            // Retrieve details
            eRISServiceClient client = new eRISServiceClient();
            client.GetCurrentWorkflowDetailsCompleted += new EventHandler<GetCurrentWorkflowDetailsCompletedEventArgs>(client_GetCurrentWorkflowDetailsCompleted);
            client.GetCurrentWorkflowDetailsAsync(chart);
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Req Data");
        }
        // Event fired when result arrives
        void client_GetCurrentWorkflowDetailsCompleted(object sender, GetCurrentWorkflowDetailsCompletedEventArgs e)
        {
            // Load grid view with result
            gvDetails.ItemsSource = e.Result;
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Rcvd Data");
        }
    }
}


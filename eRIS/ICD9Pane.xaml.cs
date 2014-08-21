using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Data;
using System.ComponentModel;
using System.ServiceModel;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using eRIS;
using eRIS.eRISServiceReference;

namespace eRIS
{
    public partial class ICD9Pane : UserControl
    {
        private MainPage pPage = Application.Current.RootVisual as MainPage;
        public string selCode = "";
        public ICD9Pane()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            doSearch();
        }
        private void doSearch()
        {
            eRISServiceClient client = new eRISServiceClient();
            client.ListICD9Completed += new EventHandler<ListICD9CompletedEventArgs>(client_ListICD9Completed);
            client.ListICD9Async(sCode.Text+"%", "%"+sDescription.Text+"%");
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Req Data");
        }

        // Event fired when results arrive for current totals
        void client_ListICD9Completed(object sender, ListICD9CompletedEventArgs e)
        {
            // Load the grid with the results
            gvDetails.ItemsSource= e.Result;
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Rcvd Data");
        }
        private void sCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                doSearch();
            }
        }
        private void gvDetails_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            // Get the selected item from the grid
            sListICD9Result item = gvDetails.SelectedItem as sListICD9Result;
            // Save selected item
            selCode = item.Code.TrimEnd();
            // Close window
            RadWindow window = this.ParentOfType<RadWindow>();
            window.Close();            
        }
    }
}

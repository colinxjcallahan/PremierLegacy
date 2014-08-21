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
    public partial class UserActivityPane : RadDocumentPane
    {
        private MainPage pPage = Application.Current.RootVisual as MainPage;
        public UserActivityPane()
        {
            InitializeComponent();
            begDate.SelectedDate = pPage.getToday();
            endDate.SelectedDate = pPage.getToday();
            // Seed the chart the first time
            updateList();
        }
        // Event fired when result arrives
        void client_GetUsersLogPerDayCompleted(object sender, GetUsersLogPerDayCompletedEventArgs e)
        {
            // Load grid view with result
            gvDetails.ItemsSource = e.Result;
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Rcvd Data");
        }
        private void updateList()
        {
            // Build end datetime
            DateTime endDateTime = new DateTime(endDate.SelectedDate.Value.Ticks + 863999900000);
            // Retrieve details
            eRISServiceClient client = new eRISServiceClient();
            client.GetUsersLogPerDayCompleted += new EventHandler<GetUsersLogPerDayCompletedEventArgs>(client_GetUsersLogPerDayCompleted);
            client.GetUsersLogPerDayAsync(begDate.SelectedDate.Value, endDateTime);
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Req Data");
        }
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            updateList();
        }
    }
}



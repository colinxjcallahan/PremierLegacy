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
    public partial class MessagePane : RadDocumentPane
    {
        private MainPage pPage = Application.Current.RootVisual as MainPage;
        private System.Windows.Threading.DispatcherTimer myDispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        private DateTime begDate = new DateTime();
        private DateTime endDate = new DateTime();
        public MessagePane()
        {
            InitializeComponent();
            //
            SelectDate.SelectionChanged += new Telerik.Windows.Controls.SelectionChangedEventHandler(SelectDate_SelectionChanged);
            SelectDate.SelectedDate = pPage.getToday();
            // Close the expander
            this.optionsExpander.IsExpanded = false;
            // Initialize the timer
            myDispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 60000);
            myDispatcherTimer.Tick += new EventHandler(refreshList);
            // Seed the chart the first time
            updateList();
        }
        // Fires every five minutes
        public void refreshList(object o, EventArgs sender)
        {
            updateList();
        }
        // Event fired when result arrives
        void client_GetMessagesLogPerDayCompleted(object sender, GetMessagesLogPerDayCompletedEventArgs e)
        {
            // Load grid view with result
            gvDetails.ItemsSource = e.Result;
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Rcvd Data");
            // Update the header timestamp
            if (begDate == pPage.getToday() && endDate == pPage.getToday())
            {
                Header = "Msgs at " + DateTime.Now.ToShortTimeString();
            }
            else
            {
                if (begDate == endDate)
                {
                    Header = "Msgs " + begDate.ToShortDateString();
                }
                else
                {
                    Header = "Msgs " + begDate.ToShortDateString() + " - " + endDate.ToShortDateString();
                }
            }
        }
        // Event fired when result arrives
        void client_MessagesSummaryCompleted(object sender, MessagesSummaryCompletedEventArgs e)
        {
            // Load grid view with result
            gvTotals.ItemsSource = e.Result;
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Rcvd Totals");
        }
        private void updateList()
        {
            // Always reset the timer
            if (myDispatcherTimer.IsEnabled == true)
            {
                myDispatcherTimer.Stop();
            }
            // Build end datetime
            DateTime endDateTime = new DateTime(endDate.Ticks + 863999900000);
            // Retrieve details
            eRISServiceClient client = new eRISServiceClient();
            client.GetMessagesLogPerDayCompleted += new EventHandler<GetMessagesLogPerDayCompletedEventArgs>(client_GetMessagesLogPerDayCompleted);
            client.GetMessagesLogPerDayAsync(begDate, endDateTime);
            // Retrieve details
            client.MessagesSummaryCompleted += new EventHandler<MessagesSummaryCompletedEventArgs>(client_MessagesSummaryCompleted);
            client.MessagesSummaryAsync(begDate, endDateTime);
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Req Data");
            // Start a timer to refresh the chart
            if (begDate == pPage.getToday() && endDate == pPage.getToday())
            {
                myDispatcherTimer.Start();
            }
        }
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            // Close options
            this.optionsExpander.IsExpanded = false;
            //
            updateList();
        }
        private void SelectDate_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (SelectDate.SelectedDates.Count > 0)
            {
                begDate = SelectDate.SelectedDates.ElementAt(0);
                endDate = SelectDate.SelectedDates.ElementAt(SelectDate.SelectedDates.Count - 1);
            }
            else
            {
                begDate = endDate = pPage.getToday();
            }
        }
        public void closeMe()
        {
            if (myDispatcherTimer.IsEnabled == true)
            {
                myDispatcherTimer.Stop();
            }
            this.RemoveFromParent();
        }
    }
}



using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
using System.Windows.Data;
using System.ComponentModel;
using System.ServiceModel;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Charting;
using Telerik.Windows.Data;
using eRIS;
using eRIS.eRISServiceReference;

namespace eRIS
{
    public partial class FirstLastStudyPane : RadDocumentPane
    {
        private MainPage pPage = Application.Current.RootVisual as MainPage;
        private ObservableCollection<fFirstLastAppointmentResult> cResult = new ObservableCollection<fFirstLastAppointmentResult>();
        private DateTime begDate = new DateTime();
        private DateTime endDate = new DateTime();
        public FirstLastStudyPane()
        {
            InitializeComponent();
            //
            chartDate.SelectionChanged += new Telerik.Windows.Controls.SelectionChangedEventHandler(chartDate_SelectionChanged);
            chartDate.SelectedDate = pPage.getToday();
            if (this.optionsExpander != null)
            {
                this.optionsExpander.IsExpanded = true;
            }
        }
        // Fires every five minutes
        public void refreshChart(object o, EventArgs sender)
        {
            updateChart();
        }
        // Event fired when results arrive for current totals
        void client_GetFirstLastAppointmentCompleted(object sender, GetFirstLastAppointmentCompletedEventArgs e)
        {
            // Capture the result for later use
            cResult = e.Result;
            // ClearValue the Panel 
            ChartPanel.Children.Clear();
            TextBlock tb = new TextBlock();
            tb.Text = "Average Time of First/Last Appointment (hours in military time)";
            tb.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            tb.FontSize = 18;
            tb.FontWeight = System.Windows.FontWeights.Bold;
            tb.Height = 36;
            ChartPanel.Children.Add(tb);
            // Loop through each site
            string lastLocation = "";
            foreach (var item in cResult)
            {
                if (item.Location != lastLocation)
                {
                    if (lastLocation != "")
                    {
                        // Add this site
                        addChart(lastLocation);
                    }
                    lastLocation = item.Location;
                }
            }
            if (lastLocation != "")
            {
                // Add this site
                addChart(lastLocation);
            }
            // Update the header timestamp
            Header = "First/Last Appt " + begDate.ToShortDateString() + " - " + endDate.ToShortDateString();
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Rcvd Data");
        }
        private void addChart(string Location)
        {
            RadChart chart = new RadChart();
            chart.Height = 300;
            chart.Width = 600;
            chart.DefaultView.ChartLegend.Visibility = System.Windows.Visibility.Visible;
            chart.DefaultView.ChartArea.EnableAnimations = false;
            // Build the chart
            DataSeries rangeSeries = new DataSeries();
            rangeSeries.Definition = new RangeSeriesDefinition();
            rangeSeries.LegendLabel = "Appt Times";
            // Add datapoints
            foreach (var item in cResult)
            {
                if (item.Location == Location)
                {
                    rangeSeries.Add(new DataPoint() { Low = item.FirstAppt.Value, High = item.LastAppt.Value, XCategory = item.Modality });
                }
            }
            chart.DefaultView.ChartArea.DataSeries.Clear();
            chart.DefaultView.ChartArea.DataSeries.Add(rangeSeries);
            chart.DefaultView.ChartArea.AxisY.AutoRange = false;
            chart.DefaultView.ChartArea.AxisY.Step = 1;
            chart.DefaultView.ChartArea.AxisY.MinValue = 6;
            chart.DefaultView.ChartArea.AxisY.MaxValue = 20;
            chart.DefaultView.ChartArea.AxisX.Title = Location.TrimEnd();
            //
            ChartPanel.Children.Add(chart);
        }
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
       { 
            updateChart();
        }
        private void updateChart()
        {
            // Build end datetime
            DateTime endDateTime = new DateTime(endDate.Ticks + 863999900000);
            eRISServiceClient client = new eRISServiceClient();
            client.GetFirstLastAppointmentCompleted += new EventHandler<GetFirstLastAppointmentCompletedEventArgs>(client_GetFirstLastAppointmentCompleted);
            client.GetFirstLastAppointmentAsync(begDate, endDateTime);
            // Close options
            this.optionsExpander.IsExpanded = false;
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Req Data");
        }
        private void chartDate_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (chartDate.SelectedDates.Count > 0)
            {
                begDate = chartDate.SelectedDates.ElementAt(0);
                endDate = chartDate.SelectedDates.ElementAt(chartDate.SelectedDates.Count - 1);
            }
            else
            {
                begDate = endDate = pPage.getToday();
            }
        }
    }
}

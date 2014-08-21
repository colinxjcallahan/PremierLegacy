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
    public partial class CompletedPerTechPane : RadDocumentPane
    {
        private MainPage pPage = Application.Current.RootVisual as MainPage;
        private ObservableCollection<fCompletedPerTechResult> cResult = new ObservableCollection<fCompletedPerTechResult>();
        private DateTime begDate = new DateTime();
        private DateTime endDate = new DateTime();
        public CompletedPerTechPane()
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
        void client_GetCompletedPerTechCompleted(object sender, GetCompletedPerTechCompletedEventArgs e)
        {
            // Capture the result for later use
            cResult = e.Result;
            // ClearValue the Panel 
            ChartPanel.Children.Clear();
            TextBlock tb = new TextBlock();
            tb.Text = "Studies Completed Per Technologist with Average Minutes";
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
            Header = "Per Tech " + begDate.ToShortDateString() + " - " + endDate.ToShortDateString();
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Rcvd Data");
        }
        private void addChart(string Location)
        {
            RadChart chart = new RadChart();
            chart.Height = 400;
            chart.Width = 800;
            chart.DefaultView.ChartLegend.Visibility = System.Windows.Visibility.Visible;
            chart.DefaultView.ChartArea.EnableAnimations = false;
            // Build the chart
            DataSeries barSeries = new DataSeries();
            BarSeriesDefinition bD = new BarSeriesDefinition();
            bD.LabelSettings.LabelDisplayMode = LabelDisplayMode.Inside;
            bD.LabelSettings.Distance = 1;
            barSeries.Definition = bD;
            barSeries.LegendLabel = "Studies";
            DataSeries lineSeries = new DataSeries();
            lineSeries.Definition = new LineSeriesDefinition();
            lineSeries.LegendLabel = "Avg Minutes";
             // Sum totals
            double maxTotals = 0;
            foreach (var item in cResult)
            {
                if (item.Location == Location)
                {
                    barSeries.Add(new DataPoint() { YValue = item.Studies.Value, XCategory = item.Clerk.ToString() });
                    lineSeries.Add(new DataPoint() { YValue = item.ExamTime.Value, XCategory = item.Clerk.ToString() });
                    if (item.Studies > maxTotals)
                    {
                        maxTotals = item.Studies.Value;
                    }
                    if (item.ExamTime > maxTotals)
                    {
                        maxTotals = item.ExamTime.Value;
                    }
                }
            }
            chart.DefaultView.ChartArea.DataSeries.Clear();
            chart.DefaultView.ChartArea.DataSeries.Add(barSeries);
            chart.DefaultView.ChartArea.DataSeries.Add(lineSeries);
            // Set the max Y axis height
            chart.DefaultView.ChartArea.AxisY.StripLinesVisibility = System.Windows.Visibility.Collapsed;
            chart.DefaultView.ChartArea.AxisY.Visibility = System.Windows.Visibility.Collapsed;
            chart.DefaultView.ChartArea.AxisY.AutoRange = false;
            chart.DefaultView.ChartArea.AxisX.Title = Location.TrimEnd();
            chart.DefaultView.ChartArea.AxisX.LabelRotationAngle = 90;
            chart.DefaultView.ChartArea.AxisY.MaxValue = maxTotals;
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
            client.GetCompletedPerTechCompleted += new EventHandler<GetCompletedPerTechCompletedEventArgs>(client_GetCompletedPerTechCompleted);
            client.GetCompletedPerTechAsync(begDate, endDateTime);
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

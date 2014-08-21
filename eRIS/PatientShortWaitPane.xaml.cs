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
    public partial class PatientShortWaitPane : RadDocumentPane
    {
        private MainPage pPage = Application.Current.RootVisual as MainPage;
        private ObservableCollection<fPatientShortWaitResult> cResult = new ObservableCollection<fPatientShortWaitResult>();
        private DateTime begDate = new DateTime();
        private DateTime endDate = new DateTime();
        public PatientShortWaitPane()
        {
            InitializeComponent();
            //
            chartDate.SelectionChanged += new Telerik.Windows.Controls.SelectionChangedEventHandler(chartDate_SelectionChanged);
            chartDate.SelectedDate = pPage.getToday();
            //
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
        void client_GetPatientShortWaitCompleted(object sender, GetPatientShortWaitCompletedEventArgs e)
        {
            // Capture the result for later use
            cResult = e.Result;
            // ClearValue the Panel 
            ChartPanel.Children.Clear();
            TextBlock tb = new TextBlock();
            tb.Text = "Percentage of studies with wait shorter than 15 minutes";
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
                        doModality(lastLocation);
                    }
                    lastLocation = item.Location;
                }
            }
            if (lastLocation != "")
            {
                // Add this site
                doModality(lastLocation);
            }
            // Update the header timestamp
            Header = "Short Wait " + begDate.ToShortDateString() + " - " + endDate.ToShortDateString();
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Rcvd Data");
        }
        private void doModality(string Location)
        {
            // Loop through each site
            string lastModality = "";
            foreach (var item in cResult)
            {
                if (item.Location == Location)
                {
                    if (item.Modality != lastModality)
                    {

                        if (lastModality != "")
                        {
                            // Add this site
                            addChart(Location, lastModality);
                        }
                        lastModality = item.Modality;
                    }
                }
            }
            if (lastModality != "")
            {
                // Add this site
                addChart(Location, lastModality);
            }
        }
        private void addChart(string Location, string Modality)
        {
            RadChart chart = new RadChart();
            chart.Height = 200;
            chart.Width = 800;
            chart.DefaultView.ChartLegend.Visibility = System.Windows.Visibility.Visible;
            chart.DefaultView.ChartArea.EnableAnimations = false;
            // Build the chart
            DataSeries barSeries = new DataSeries();
            BarSeriesDefinition bD = new BarSeriesDefinition();
            bD.LabelSettings.LabelDisplayMode = LabelDisplayMode.Inside;
            bD.LabelSettings.Distance = 1;
            barSeries.Definition = bD;
            barSeries.LegendLabel = "% Regular";
            DataSeries barSeries2 = new DataSeries();
            BarSeriesDefinition bD2 = new BarSeriesDefinition();
            bD2.LabelSettings.LabelDisplayMode = LabelDisplayMode.Inside;
            bD2.LabelSettings.Distance = 1;
            barSeries2.Definition = bD2;
            barSeries2.LegendLabel = "% STAT";
            DataSeries lineSeries = new DataSeries();
            lineSeries.Definition = new LineSeriesDefinition();
            lineSeries.LegendLabel = "Total Regular";
            DataSeries lineSeries2 = new DataSeries();
            lineSeries2.Definition = new LineSeriesDefinition();
            lineSeries2.LegendLabel = "Total STAT";
            // Sum totals
            double maxTotals = 0;
            foreach (var item in cResult)
            {
                if (item.Location == Location && item.Modality == Modality)
                {
                    if (item.Stat == 'N')
                    {
                        barSeries.Add(new DataPoint() { YValue = item.PercentWaiting.Value, XCategory = item.HourToDisplay.ToString() });
                        lineSeries.Add(new DataPoint() { YValue = item.Exams.Value, XCategory = item.HourToDisplay.ToString() });
                    }
                    else
                    {
                        barSeries2.Add(new DataPoint() { YValue = item.PercentWaiting.Value, XCategory = item.HourToDisplay.ToString() });
                        lineSeries2.Add(new DataPoint() { YValue = item.Exams.Value, XCategory = item.HourToDisplay.ToString() });
                    }
                    if (item.PercentWaiting > maxTotals)
                    {
                        maxTotals = item.PercentWaiting.Value;
                    }
                    if (item.Exams > maxTotals)
                    {
                        maxTotals = item.Exams.Value;
                    }
                }
            }
            chart.DefaultView.ChartArea.DataSeries.Clear();
            chart.DefaultView.ChartArea.DataSeries.Add(barSeries);
            chart.DefaultView.ChartArea.DataSeries.Add(lineSeries);
            chart.DefaultView.ChartArea.DataSeries.Add(barSeries2);
            chart.DefaultView.ChartArea.DataSeries.Add(lineSeries2);
            // Set the max Y axis height
            chart.DefaultView.ChartArea.AxisY.StripLinesVisibility = System.Windows.Visibility.Collapsed;
            chart.DefaultView.ChartArea.AxisY.Visibility = System.Windows.Visibility.Collapsed;
            chart.DefaultView.ChartArea.AxisY.AutoRange = false;
            chart.DefaultView.ChartArea.AxisX.Title = Location.TrimEnd() + " - " + Modality.TrimEnd();
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
            client.GetPatientShortWaitCompleted += new EventHandler<GetPatientShortWaitCompletedEventArgs>(client_GetPatientShortWaitCompleted);
            client.GetPatientShortWaitAsync(begDate, endDateTime);
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

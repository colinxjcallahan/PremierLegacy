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
    public partial class ReportsCodedPerDayPane : RadDocumentPane
    {
        private MainPage pPage = Application.Current.RootVisual as MainPage;
        private System.Windows.Threading.DispatcherTimer myDispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        private ObservableCollection<sReportsCodedPerDayResult> cResult = new ObservableCollection<sReportsCodedPerDayResult>();
        private DateTime begDate = new DateTime();
        private DateTime endDate = new DateTime();
        public ReportsCodedPerDayPane()
        {
            InitializeComponent();
            //
            chartDate.SelectionChanged += new Telerik.Windows.Controls.SelectionChangedEventHandler(chartDate_SelectionChanged);
            chartDate.SelectedDate = pPage.getToday();
            // Close the expander
            this.optionsExpander.IsExpanded = false;
            // Initialize the timer
            myDispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 300000);
            myDispatcherTimer.Tick += new EventHandler(refreshChart);
            // Seed the chart the first time
            updateChart();
        }
        // Fires every five minutes
        public void refreshChart(object o, EventArgs sender)
        {
            updateChart();
        }
        // Event fired when results arrive for current totals
        void client_GetReportsCodedPerDayCompleted(object sender, GetReportsCodedPerDayCompletedEventArgs e)
        {
            // Capture the result for later use
            cResult = e.Result;
            // Build the chart
            DataSeries barSeries = new DataSeries();
            BarSeriesDefinition bD = new HorizontalBarSeriesDefinition();
            bD.LabelSettings.LabelDisplayMode = LabelDisplayMode.Inside;
            bD.LabelSettings.Distance = 1;
            bD.ShowItemToolTips = true;
            barSeries.Definition = bD;
            DataSeries barSeries2 = new DataSeries();
            BarSeriesDefinition bD2 = new HorizontalBarSeriesDefinition();
            bD2.LabelSettings.LabelDisplayMode = LabelDisplayMode.Inside;
            bD2.LabelSettings.Distance = 1;
            bD2.ShowItemToolTips = true;
            barSeries2.Definition = bD2;
            // Sum totals
            double maxTotals = 0;
            double currentTotals = 0;
            double currentTotals2 = 0;
            string lastBreak = "";
            foreach (var item in cResult)
            {
                if (item.Coder == lastBreak)
                {
                    currentTotals += item.Reports.Value;
                    currentTotals2 += item.Charges.Value;
                }
                else
                {
                    if (lastBreak != "")
                    {
                        barSeries.Add(new DataPoint() { YValue = currentTotals, XCategory = lastBreak });
                        barSeries2.Add(new DataPoint() { YValue = currentTotals2, XCategory = lastBreak });
                        if (currentTotals > maxTotals)
                        {
                            maxTotals = currentTotals;
                        }
                        if (currentTotals2 > maxTotals)
                        {
                            maxTotals = currentTotals2;
                        }
                    }
                    lastBreak = item.Coder;
                    currentTotals = item.Reports.Value;
                    currentTotals2 = item.Charges.Value;
                }
            }
            if (lastBreak != "")
            {
                barSeries.Add(new DataPoint() { YValue = currentTotals, XCategory = lastBreak });
                barSeries2.Add(new DataPoint() { YValue = currentTotals2, XCategory = lastBreak });
            }
            if (currentTotals > maxTotals)
            {
                maxTotals = currentTotals;
            }
            if (currentTotals2 > maxTotals)
            {
                maxTotals = currentTotals2;
            }
            CodedByDateChartArea.DataSeries.Clear();
            CodedByDateChartArea.DataSeries.Add(barSeries);
            CodedByDateChartArea.DataSeries.Add(barSeries2);
            // Set the max Y axis height
            CodedByDateChartArea.AxisY.MaxValue = maxTotals;
            // Update the header timestamp
            if (begDate == pPage.getToday() && endDate == pPage.getToday())
            {
                Header = "Coded at " + DateTime.Now.ToShortTimeString();
            } else {
                if (begDate == endDate)
                {
                    Header = "Coded " + begDate.ToShortDateString();
                } else {
                    Header = "Coded " + begDate.ToShortDateString() + " - " + endDate.ToShortDateString();
                }
            }
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Rcvd sReportsCodedPerDay");
        }
        // Event fired when results arrive for current totals
        void client_GetToCodeCompleted(object sender, GetToCodeCompletedEventArgs e)
        {
            // Build the chart
            DataSeries barSeries = new DataSeries();
            BarSeriesDefinition bD = new BarSeriesDefinition();
            bD.LabelSettings.LabelDisplayMode = LabelDisplayMode.Inside;
            bD.LabelSettings.Distance = 1;
            barSeries.Definition = bD;
            // Sum totals
            decimal maxTotals = 0;
            decimal currentTotals = 0;
            string lastBreak = "";
            foreach (var item in e.Result)
            {
                if (item.Location == lastBreak)
                {
                    currentTotals += item.Reports.Value;
                }
                else
                {
                    if (lastBreak != "")
                    {
                        barSeries.Add(new DataPoint() { YValue = System.Convert.ToDouble(currentTotals), XCategory = lastBreak });
                        if (currentTotals > maxTotals)
                        {
                            maxTotals = currentTotals;
                        }
                    }
                    lastBreak = item.Location;
                    currentTotals = item.Reports.Value;
                }
            }
            if (lastBreak != "")
            {
                barSeries.Add(new DataPoint() { YValue = System.Convert.ToDouble(currentTotals), XCategory = lastBreak });
                if (currentTotals > maxTotals)
                {
                    maxTotals = currentTotals;
                }
            }
            ToCodeNumChartArea.DataSeries.Clear();
            ToCodeNumChartArea.DataSeries.Add(barSeries);
            // Set the max Y axis height
            ToCodeNumChartArea.AxisY.MaxValue = System.Convert.ToDouble(maxTotals);
            // Build the second chart
            DataSeries barSeries2 = new DataSeries();
            BarSeriesDefinition bD2 = new BarSeriesDefinition();
            bD2.LabelSettings.LabelDisplayMode = LabelDisplayMode.Inside;
            bD2.LabelSettings.Distance = 1;
            barSeries2.Definition = bD2;
            // Sum totals
            maxTotals = 0;
            currentTotals = 0;
            lastBreak = "";
            foreach (var item in e.Result)
            {
                if (item.Location == lastBreak)
                {
                    currentTotals += item.Age.Value;
                }
                else
                {
                    if (lastBreak != "")
                    {
                        barSeries2.Add(new DataPoint() { YValue = System.Convert.ToDouble(currentTotals), XCategory = lastBreak });
                        if (currentTotals > maxTotals)
                        {
                            maxTotals = currentTotals;
                        }
                    }
                    lastBreak = item.Location;
                    currentTotals = item.Age.Value;
                }
            }
            if (lastBreak != "")
            {
                barSeries2.Add(new DataPoint() { YValue = System.Convert.ToDouble(currentTotals), XCategory = lastBreak });
                if (currentTotals > maxTotals)
                {
                    maxTotals = currentTotals;
                }
            }
            ToCodeDaysChart.DefaultView.ChartTitle.Content = "Average Days";
            ToCodeDaysChartArea.DataSeries.Clear();
            ToCodeDaysChartArea.DataSeries.Add(barSeries2);
            // Set the max Y axis height
            ToCodeDaysChartArea.AxisY.MaxValue = System.Convert.ToDouble(maxTotals);
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Rcvd sToCode");
        }
        private void ReadByDateChartArea_ItemToolTipOpening(ItemToolTip2D tooltip, ItemToolTipEventArgs e)
        {
            //
            RadChart chartA = new RadChart();
            chartA.Height = 250;
            chartA.Width = 800;
            chartA.DefaultView.ChartLegend.Visibility = System.Windows.Visibility.Collapsed;
            chartA.DefaultView.ChartArea.AxisX.LabelRotationAngle = 90;
            chartA.DefaultView.ChartArea.EnableAnimations = false;
            // Build the chart
            DataSeries barSeries3 = new DataSeries();
            BarSeriesDefinition bD3 = new BarSeriesDefinition();
            bD3.LabelSettings.LabelDisplayMode = LabelDisplayMode.Inside;
            bD3.LabelSettings.Distance = 1;
            barSeries3.Definition = bD3;
            DataSeries barSeries2 = new DataSeries();
            BarSeriesDefinition bD2 = new BarSeriesDefinition();
            bD2.LabelSettings.LabelDisplayMode = LabelDisplayMode.Inside;
            bD2.LabelSettings.Distance = 1;
            barSeries2.Definition = bD2;
            // Sort results
            CollectionViewSource sortedResult = new CollectionViewSource();
            sortedResult.SortDescriptions.Add(new SortDescription("Location", ListSortDirection.Ascending));
            sortedResult.Source = cResult;
            // Sum totals
            double maxTotals = 0;
            double currentTotals = 0;
            double currentTotals2 = 0;
            string lastBreak = "";
            foreach (sReportsCodedPerDayResult item in sortedResult.View)
            {
                if (item.Coder == e.DataPoint.XCategory)
                {
                    if (item.Location == lastBreak)
                    {
                        currentTotals += item.Reports.Value;
                        currentTotals2 += item.Charges.Value;
                    }
                    else
                    {
                        if (lastBreak != "")
                        {
                            barSeries3.Add(new DataPoint() { YValue = currentTotals, XCategory = lastBreak });
                            barSeries2.Add(new DataPoint() { YValue = currentTotals2, XCategory = lastBreak });
                            if (currentTotals > maxTotals)
                            {
                                maxTotals = currentTotals;
                            }
                            if (currentTotals2 > maxTotals)
                            {
                                maxTotals = currentTotals2;
                            }
                        }
                        lastBreak = item.Location;
                        currentTotals = item.Reports.Value;
                        currentTotals2 = item.Charges.Value;
                    }
                }
            }
            if (lastBreak != "")
            {
                barSeries3.Add(new DataPoint() { YValue = currentTotals, XCategory = lastBreak });
                barSeries2.Add(new DataPoint() { YValue = currentTotals2, XCategory = lastBreak });
                if (currentTotals > maxTotals)
                {
                    maxTotals = currentTotals;
                }
                if (currentTotals2 > maxTotals)
                {
                    maxTotals = currentTotals2;
                }
            }
            //
            chartA.DefaultView.ChartArea.DataSeries.Clear();
            chartA.DefaultView.ChartArea.DataSeries.Add(barSeries3);
            chartA.DefaultView.ChartArea.DataSeries.Add(barSeries2);
            // Set the max Y axis height
            chartA.DefaultView.ChartArea.AxisY.StripLinesVisibility = System.Windows.Visibility.Collapsed;
            chartA.DefaultView.ChartArea.AxisY.Visibility = System.Windows.Visibility.Collapsed;
            chartA.DefaultView.ChartArea.AxisY.AutoRange = false;
            chartA.DefaultView.ChartArea.AxisY.MaxValue = maxTotals;
             //
            StackPanel panel = new StackPanel();
            panel.Children.Add(chartA);

            tooltip.Content = panel;
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Tooltip " + e.DataPoint.XCategory);
        }
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
       {
            // Close options
            this.optionsExpander.IsExpanded = false;
            //
            updateChart();
        }
        private void updateChart()
        {
            // Always reset the timer
            if (myDispatcherTimer.IsEnabled == true)
            {
                myDispatcherTimer.Stop();
            }
            // Build end datetime
            DateTime endDateTime = new DateTime(endDate.Ticks + 863999900000);
            eRISServiceClient client = new eRISServiceClient();
            client.GetReportsCodedPerDayCompleted += new EventHandler<GetReportsCodedPerDayCompletedEventArgs>(client_GetReportsCodedPerDayCompleted);
            client.GetReportsCodedPerDayAsync(begDate, endDateTime);
            //
            client.GetToCodeCompleted += new EventHandler<GetToCodeCompletedEventArgs>(client_GetToCodeCompleted);
            client.GetToCodeAsync();
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Req Data");
            // Start a timer to refresh the chart
            if (begDate == pPage.getToday() && endDate == pPage.getToday())
            {
                myDispatcherTimer.Start();
            }
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
        private void RadDocumentPane_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // Stop the timer if pane is removed
            myDispatcherTimer.Stop();
        }
    }
}

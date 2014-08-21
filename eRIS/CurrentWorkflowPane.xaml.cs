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
    public partial class CurrentWorkflowPane : RadDocumentPane
    {
        private MainPage pPage = Application.Current.RootVisual as MainPage;
        private ObservableCollection<fCurrentWorkflowResult> cResult = new ObservableCollection<fCurrentWorkflowResult>();
        System.Windows.Threading.DispatcherTimer myDispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        public CurrentWorkflowPane()
        {
            InitializeComponent();
            // Seed the chart the first time
            object sender = new object();
            EventArgs e = new EventArgs();
            refreshChart(sender, e);
            // Start a timer to refresh the chart
            myDispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 300000);
            myDispatcherTimer.Tick += new EventHandler(refreshChart);
            myDispatcherTimer.Start();
        }
        // Fires every five minutes
        public void refreshChart(object o, EventArgs sender)
        {
            eRISServiceClient client = new eRISServiceClient();
            client.GetCurrentWorkflowCompleted += new EventHandler<GetCurrentWorkflowCompletedEventArgs>(client_GetCurrentWorkflowCompleted);
            client.GetCurrentWorkflowAsync();
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Req Data");
        }
        // Event fired when results arrive for current totals
        void client_GetCurrentWorkflowCompleted(object sender, GetCurrentWorkflowCompletedEventArgs e)
        {
            // Capture the result for later use
            cResult = e.Result;
            // Build the chart
            DataSeries barSeries = new DataSeries();
            BarSeriesDefinition bD = new BarSeriesDefinition();
            bD.LabelSettings.LabelDisplayMode = LabelDisplayMode.Inside;
            bD.LabelSettings.Distance = 1;
            bD.ShowItemToolTips = true;
            barSeries.Definition = bD;
            // Sum totals
            double maxTotals = 0;
            double currentTotals = 0;
            string lastBreak = "";
            foreach (var item in cResult)
            {
                if (item.chart == lastBreak)
                {
                    currentTotals += item.totals.Value;
                }
                else
                {
                    if (lastBreak != "") 
                    {
                        barSeries.Add(new DataPoint() { YValue = currentTotals, XCategory = lastBreak });
                        if (currentTotals > maxTotals)
                        {
                            maxTotals = currentTotals;
                        }
                    }
                    lastBreak = item.chart;
                    currentTotals = item.totals.Value;
                }
            }
            if (lastBreak != "")
            {
                barSeries.Add(new DataPoint() { YValue = currentTotals, XCategory = lastBreak });
            }
            if (currentTotals > maxTotals)
            {
                maxTotals = currentTotals;
            }
            mainChart.DataSeries.Clear();
            mainChart.DataSeries.Add(barSeries);
            // Set the max Y axis height
            mainChart.AxisY.MaxValue = maxTotals;
            // Update the header timestamp
            Header = "Workflow at " + DateTime.Now.ToShortTimeString();
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Rcvd Data");
        }
        private void mainChart_ItemClick(object sender, ChartItemClickEventArgs e)
        { 
            pPage.openPanel(e.DataPoint.XCategory, "CurrentWorkflowDetailsPane");
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Clicked " + e.DataPoint.XCategory);
        }
        private void mainChart_ItemToolTipOpening(ItemToolTip2D tooltip, ItemToolTipEventArgs e)
        {
            RadChart chart = new RadChart();
            chart.Height = 300;
            chart.Width = 600;
            chart.DefaultView.ChartLegend.Visibility = System.Windows.Visibility.Collapsed;
            chart.DefaultView.ChartTitle.Content = e.DataPoint.XCategory;
            chart.DefaultView.ChartArea.AxisX.LabelRotationAngle = 90;
            chart.DefaultView.ChartArea.EnableAnimations = false;
            //
            DataSeries barSeries = new DataSeries();
            BarSeriesDefinition bD = new BarSeriesDefinition();
            bD.LabelSettings.LabelDisplayMode = LabelDisplayMode.Inside;
            bD.LabelSettings.Distance = 1;
            barSeries.Definition = bD;
            // Sort results
            CollectionViewSource sortedResult = new CollectionViewSource();
            sortedResult.SortDescriptions.Add(new SortDescription("location", ListSortDirection.Ascending));
            sortedResult.Source = cResult;
            // Sum totals
            double maxTotals = 0;
            double currentTotals = 0;
            string lastBreak = "";
            foreach (fCurrentWorkflowResult item in sortedResult.View)
            {
                if (item.chart == e.DataPoint.XCategory)
                {
                    if (item.location == lastBreak)
                    {
                        currentTotals += item.totals.Value;
                    }
                    else
                    {
                        if (lastBreak != "")
                        {
                            barSeries.Add(new DataPoint() { YValue = currentTotals, XCategory = lastBreak });
                            if (currentTotals > maxTotals)
                            {
                                maxTotals = currentTotals;
                            }
                        }
                        lastBreak = item.location;
                        currentTotals = item.totals.Value;
                    }
                }
            }
            if (lastBreak != "")
            {
                barSeries.Add(new DataPoint() { YValue = currentTotals, XCategory = lastBreak });
                if (currentTotals > maxTotals)
                {
                    maxTotals = currentTotals;
                }
            }
            chart.DefaultView.ChartArea.DataSeries.Clear();
            chart.DefaultView.ChartArea.DataSeries.Add(barSeries);
            // Set the max Y axis height
            chart.DefaultView.ChartArea.AxisY.StripLinesVisibility = System.Windows.Visibility.Collapsed;
            chart.DefaultView.ChartArea.AxisY.Visibility = System.Windows.Visibility.Collapsed;
            chart.DefaultView.ChartArea.AxisY.AutoRange = false;
            chart.DefaultView.ChartArea.AxisY.MaxValue = maxTotals;
            //
            tooltip.Content = chart;
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Tooltip " + e.DataPoint.XCategory);
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

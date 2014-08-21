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
using Telerik.Windows.Controls.GridView;
using eRIS;
using eRIS.eRISServiceReference;

namespace eRIS
{
    public partial class TurnaroundTimePane : RadDocumentPane
    {
        private MainPage pPage = Application.Current.RootVisual as MainPage;
        private DateTime begDate = new DateTime();
        private DateTime endDate = new DateTime();
        public TurnaroundTimePane()
        {
            InitializeComponent();
            // Set the default date to
            chartDate.SelectedDate = pPage.getToday();
        }
        // Event fired when results arrive for current totals
        void client_GetTurnaroundTimeCompleted(object sender, GetTurnaroundTimeCompletedEventArgs e)
        {
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Rcvd Data");
            // Load grid view with result
            gvDetails.ItemsSource = e.Result;
            // Add default groupings
            gvDetails.GroupDescriptors.Clear();
            GroupDescriptor groupDesc = new GroupDescriptor();
            groupDesc.Member = "Location";
            gvDetails.GroupDescriptors.Add(groupDesc);
            groupDesc = new GroupDescriptor();
            groupDesc.Member = "Modality";
            gvDetails.GroupDescriptors.Add(groupDesc);
            // Bind the chart
            bindChart();
        }
        // Bind the chart
        public void bindChart()
        {
            // Close options
            //this.optionsExpander.IsExpanded = false;
            // Reset the chart
            TaTChart.ItemsSource = null;
            TaTChart.SeriesMappings.Clear();
            // Build the chart
            TaTChart.DefaultView.ChartLegend.Visibility = System.Windows.Visibility.Visible;
            TaTChart.DefaultView.ChartTitle.Content = "Minutes Between Patient Arrival and Report Signed";
            TaTChart.DefaultView.ChartArea.AxisY.StripLinesVisibility = System.Windows.Visibility.Collapsed;
            TaTChart.DefaultView.ChartArea.AxisY.Visibility = System.Windows.Visibility.Collapsed;
            TaTChart.DefaultView.ChartArea.AxisY.AutoRange = true;
            TaTChart.DefaultView.ChartArea.AxisY.ExtendDirection = AxisExtendDirection.None;
            TaTChart.DefaultView.ChartArea.EnableAnimations = false;
            // Map the bound data
            SeriesMapping mapping = new SeriesMapping();
            ChartAggregateFunction aggFunct = (ChartAggregateFunction)Enum.Parse(typeof(ChartAggregateFunction), (this.cmbAggregation.SelectedItem as ComboBoxItem).Content as string, true);
            mapping.ItemMappings.Add(new ItemMapping("Minutes", DataPointMember.YValue, aggFunct));
            HorizontalBarSeriesDefinition bD = new HorizontalBarSeriesDefinition();
            bD.LabelSettings.LabelDisplayMode = LabelDisplayMode.Inside;
            bD.LabelSettings.Distance = 1;
            mapping.SeriesDefinition = bD;
            // Sort
            foreach (SortDescriptor sDescriptor in gvDetails.SortDescriptors)
            {
                mapping.SortDescriptors.Add(new ChartSortDescriptor(sDescriptor.Member, sDescriptor.SortDirection));
            }
            // Grouping
            foreach (GroupDescriptor descriptor in gvDetails.GroupDescriptors)
            {
                mapping.GroupingSettings.GroupDescriptors.Add(new ChartGroupDescriptor(descriptor.Member));
            }
            if (gvDetails.GroupDescriptors.Count > 1)
            {
                GroupDescriptor lastGroupDescriptor = (GroupDescriptor)gvDetails.GroupDescriptors[gvDetails.GroupDescriptors.Count - 1];
                mapping.ItemMappings.Add(new ItemMapping(lastGroupDescriptor.Member, DataPointMember.XCategory));
            }
            //
            TaTChart.SeriesMappings.Add(mapping);
            TaTChart.ItemsSource = gvDetails.Items;
            // Update the header timestamp
            if (begDate == pPage.getToday() && endDate == pPage.getToday())
            {
                Header = "TaT at " + DateTime.Now.ToShortTimeString();
            }
            else
            {
                if (begDate == endDate)
                {
                    Header = "TaT " + begDate.ToShortDateString();
                }
                else
                {
                    Header = "TaT " + begDate.ToShortDateString() + " - " + endDate.ToShortDateString();
                }
            }
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
            client.GetTurnaroundTimeCompleted += new EventHandler<GetTurnaroundTimeCompletedEventArgs>(client_GetTurnaroundTimeCompleted);
            client.GetTurnaroundTimeAsync(begDate, endDateTime);
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
        private void btnDraw_Click(object sender, RoutedEventArgs e)
        {
            // Do the charts
            bindChart();
        }

        private void gvDetails_Grouped(object sender, GridViewGroupedEventArgs e)
        {
            // Do the charts
            bindChart();
        }

        private void gvDetails_Sorted(object sender, GridViewSortedEventArgs e)
        {
            // Do the charts
            bindChart();
        }

        private void gvDetails_Filtered(object sender, GridViewFilteredEventArgs e)
        {
            // Do the charts
            bindChart();
        }
    }
}

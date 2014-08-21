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
    public partial class StudiesReadPerDayPane : RadDocumentPane
    {
        private MainPage pPage = Application.Current.RootVisual as MainPage;
        private System.Windows.Threading.DispatcherTimer myDispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        private ObservableCollection<sStudiesReadPerDayResult> cResult = new ObservableCollection<sStudiesReadPerDayResult>();
        private DateTime begDate = new DateTime();
        private DateTime endDate = new DateTime();
        private int SelRadGroup = 0;
        public StudiesReadPerDayPane()
        {
            InitializeComponent();
            //
            chartDate.SelectionChanged += new Telerik.Windows.Controls.SelectionChangedEventHandler(chartDate_SelectionChanged);
            chartDate.SelectedDate = pPage.getToday();
            // Populate RadGroup
            SelRadGroup = System.Convert.ToInt32(pPage.currentLogin.RadGroup);
            if (pPage.currentLogin.RadGroup == 1)
            {
                RadGroupLit.Visibility = System.Windows.Visibility.Visible;
                RadGroup.Visibility = System.Windows.Visibility.Visible;
                eRISServiceClient client = new eRISServiceClient();
                client.GetRadGroupCompleted += new EventHandler<GetRadGroupCompletedEventArgs>(client_GetRadGroupCompleted);
                client.GetRadGroupAsync();
            }
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
        // Event fired when results arrive for RadGroup
        void client_GetRadGroupCompleted(object sender, GetRadGroupCompletedEventArgs e)
        {
            // Load the combo box
            RadGroup.ItemsSource = e.Result;
            RadGroup.SelectedValue = pPage.currentLogin.RadGroup;
        }
        // Event fired when results arrive for current totals
        void client_GetStudiesReadPerDayCompleted(object sender, GetStudiesReadPerDayCompletedEventArgs e)
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
            //
            barSeries.Add(new DataPoint() { YValue = 0, XCategory = "A" });
            //barSeries2.Add(new DataPoint() { YValue = 0, XCategory = "A" });
            // Sum totals
            double maxTotals = 0;
            double TotalRVUs = 0;
            double MaxRVUs = 0;
            double TotalRadiologists = 0;
            double currentTotals = 0;
            double currentTotals2 = 0;
            double rad1 = 0;
            double rad2 = 0;
            double rad3 = 0;
            double ChartRVUs = 0;
            double ChartRadiologists = 0;
            string lastBreak = "";
            foreach (var item in cResult)
            {
                if (item.Rad == lastBreak)
                {
                    currentTotals += item.Exams.Value;
                    currentTotals2 += item.RVUs.Value;
                    TotalRVUs += item.RVUs.Value;
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
                        if (currentTotals2 > MaxRVUs)
                        {
                            MaxRVUs = currentTotals2;
                        }
                    }
                    // accumulate lowest three rads
                    if ((currentTotals2 <= rad1) || (rad1 == 0))
                    {
                        rad3 = rad2;
                        rad2 = rad1;
                        rad1 = currentTotals2;
                    }
                    else
                        if ((currentTotals2 <= rad2) || (rad2 == 0))
                        {
                            rad3 = rad2;
                            rad2 = currentTotals2;
                        }
                        else if ((currentTotals2 <= rad3) || (rad3 == 0))
                        {
                            rad3 = currentTotals2;
                        }
                    //
                    lastBreak = item.Rad;
                    currentTotals = item.Exams.Value;
                    currentTotals2 = item.RVUs.Value;
                    TotalRVUs += item.RVUs.Value;
                    TotalRadiologists += 1;
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
            if (currentTotals2 > MaxRVUs)
            {
                MaxRVUs = currentTotals2;
            }
            //
            barSeries.Add(new DataPoint() { YValue = 0, XCategory = "Z" });
            //barSeries2.Add(new DataPoint() { YValue = 0, XCategory = "Z" });

            ReadByDateChartArea.DataSeries.Clear();
            ReadByDateChartArea.DataSeries.Add(barSeries);
            ReadByDateChartArea.DataSeries.Add(barSeries2);
            // Set the max Y axis height
            ReadByDateChartArea.AxisY.MaxValue = maxTotals;
            // Calculate the Chart totals
            if (TotalRadiologists > 29)
            {
                ChartRadiologists = TotalRadiologists - 3;
                ChartRVUs = TotalRVUs - rad1 - rad2 - rad3;
            }
            else if (TotalRadiologists > 19)
            {
                ChartRadiologists = TotalRadiologists - 2;
                ChartRVUs = TotalRVUs - rad1 - rad2;
            }
            else if (TotalRadiologists > 9)
            {
                ChartRadiologists = TotalRadiologists - 1;
                ChartRVUs = TotalRVUs - rad1;
            }
            else
            {
                ChartRadiologists = TotalRadiologists;
                ChartRVUs = TotalRVUs;
            }
            // Set screen values
            xRadiologists.Text = TotalRadiologists.ToString();
            xRVU.Text = TotalRVUs.ToString();
            if (TotalRadiologists > 0 && TotalRVUs > 0)
                xAverage.Text = (TotalRVUs / TotalRadiologists).ToString();
            else
                xAverage.Text = "";
            xRadiologists2.Text = ChartRadiologists.ToString();
            xRVU2.Text = ChartRVUs.ToString();
            if (ChartRadiologists > 0 && ChartRVUs > 0)
                xAverage2.Text = (ChartRVUs / ChartRadiologists).ToString();
            else
                xAverage2.Text = "";
            if (pPage.currentLogin.isAdmin == true)
            {
                xRadiologistsLit.Visibility = System.Windows.Visibility.Visible;
                xRadiologists.Visibility = System.Windows.Visibility.Visible;
                xRVULit.Visibility = System.Windows.Visibility.Visible;
                xRVU.Visibility = System.Windows.Visibility.Visible;
                xAverageLit.Visibility = System.Windows.Visibility.Visible;
                xAverage.Visibility = System.Windows.Visibility.Visible;
                xRadiologists2Lit.Visibility = System.Windows.Visibility.Visible;
                xRadiologists2.Visibility = System.Windows.Visibility.Visible;
                xRVU2Lit.Visibility = System.Windows.Visibility.Visible;
                xRVU2.Visibility = System.Windows.Visibility.Visible;
                xAverage2Lit.Visibility = System.Windows.Visibility.Visible;
                xAverage2.Visibility = System.Windows.Visibility.Visible;
            }
            // Update the zones
            ReadByDateChartArea.Annotations.Clear();
            MarkedZone redZone = new MarkedZone();
            redZone.StartY = 0;
            if (ChartRadiologists > 0 && ChartRVUs > 0)
                redZone.EndY = (ChartRVUs / ChartRadiologists) * 0.8;
            else
                redZone.EndY = 0;
            redZone.Background = new SolidColorBrush(Color.FromArgb(255, 248, 109, 90));
            ReadByDateChartArea.Annotations.Add(redZone);
            MarkedZone yellowZone = new MarkedZone();
            yellowZone.StartY = redZone.EndY;
            if (ChartRadiologists > 0 && ChartRVUs > 0)
                yellowZone.EndY = (ChartRVUs / ChartRadiologists) * 1.4;
            else
                yellowZone.EndY = 0;
            yellowZone.Background = new SolidColorBrush(Color.FromArgb(255, 234, 244, 81));
            ReadByDateChartArea.Annotations.Add(yellowZone);
            MarkedZone greenZone = new MarkedZone();
            greenZone.StartY = yellowZone.EndY;
            if (ChartRadiologists > 0 && ChartRVUs > 0)
                greenZone.EndY = MaxRVUs;
            else
                greenZone.EndY = 0;
            greenZone.Background = new SolidColorBrush(Color.FromArgb(255, 154, 216, 70));
            ReadByDateChartArea.Annotations.Add(greenZone);
            // Update the header timestamp
            if (begDate == pPage.getToday() && endDate == pPage.getToday())
            {
                Header = "Read at " + DateTime.Now.ToShortTimeString();
            } else {
                if (begDate == endDate)
                {
                    Header = "Read " + begDate.ToShortDateString();
                } else {
                    Header = "Read " + begDate.ToShortDateString() + " - " + endDate.ToShortDateString();
                }
            }
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Rcvd sStudiesReadPerDay");
        }
        private void ReadByDateChartArea_ItemClick(object sender, ChartItemClickEventArgs e)
        {
            pPage.openPanel("Read by " + e.DataPoint.XCategory + " on " + begDate.ToString(), "CurrentWorkflowDetailsPane");
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Clicked " + e.DataPoint.XCategory);
        }
        private void ReadByDateChartArea_ItemToolTipOpening(ItemToolTip2D tooltip, ItemToolTipEventArgs e)
        {
            RadChart chart = new RadChart();
            chart.Height = 250;
            chart.Width = 800;
            chart.DefaultView.ChartLegend.Visibility = System.Windows.Visibility.Collapsed;
            chart.DefaultView.ChartTitle.Content = e.DataPoint.XCategory;
            chart.DefaultView.ChartArea.AxisX.LabelRotationAngle = 90;
            chart.DefaultView.ChartArea.EnableAnimations = false;
            // Build the chart
            DataSeries barSeries = new DataSeries();
            BarSeriesDefinition bD = new BarSeriesDefinition();
            bD.LabelSettings.LabelDisplayMode = LabelDisplayMode.Inside;
            bD.LabelSettings.Distance = 1;
            barSeries.Definition = bD;
            DataSeries barSeries2 = new DataSeries();
            BarSeriesDefinition bD2 = new BarSeriesDefinition();
            bD2.LabelSettings.LabelDisplayMode = LabelDisplayMode.Inside;
            bD2.LabelSettings.Distance = 1;
            barSeries2.Definition = bD2;
            // Sort results
            CollectionViewSource sortedResult = new CollectionViewSource();
            sortedResult.SortDescriptions.Add(new SortDescription("Modality", ListSortDirection.Ascending));
            sortedResult.Source = cResult;
            // Sum totals
            double maxTotals = 0;
            double currentTotals = 0;
            double currentTotals2 = 0;
            string lastBreak = "";
            foreach (sStudiesReadPerDayResult item in sortedResult.View)
            {
                if (item.Rad == e.DataPoint.XCategory)
                {
                    if (item.Modality == lastBreak)
                    {
                        currentTotals += item.Exams.Value;
                        currentTotals2 += item.RVUs.Value;
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
                        lastBreak = item.Modality;
                        currentTotals = item.Exams.Value;
                        currentTotals2 = item.RVUs.Value;
                    }
                }
            }
            if (lastBreak != "")
            {
                barSeries.Add(new DataPoint() { YValue = currentTotals, XCategory = lastBreak });
                barSeries2.Add(new DataPoint() { YValue = currentTotals2, XCategory = lastBreak });
                pPage.appendLog(this.GetType().ToString(), lastBreak);
            }
            if (currentTotals > maxTotals)
            {
                maxTotals = currentTotals;
            }
            if (currentTotals2 > maxTotals)
            {
                maxTotals = currentTotals2;
            }
            chart.DefaultView.ChartArea.DataSeries.Clear();
            chart.DefaultView.ChartArea.DataSeries.Add(barSeries);
            chart.DefaultView.ChartArea.DataSeries.Add(barSeries2);
            // Set the max Y axis height
            chart.DefaultView.ChartArea.AxisY.StripLinesVisibility = System.Windows.Visibility.Collapsed;
            chart.DefaultView.ChartArea.AxisY.Visibility = System.Windows.Visibility.Collapsed;
            chart.DefaultView.ChartArea.AxisY.AutoRange = false;
            chart.DefaultView.ChartArea.AxisY.MaxValue = maxTotals;
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
            DataSeries barSeries4 = new DataSeries();
            BarSeriesDefinition bD4 = new BarSeriesDefinition();
            bD4.LabelSettings.LabelDisplayMode = LabelDisplayMode.Inside;
            bD4.LabelSettings.Distance = 1;
            barSeries4.Definition = bD4;
            // Sort results
            sortedResult = new CollectionViewSource();
            sortedResult.SortDescriptions.Add(new SortDescription("Location", ListSortDirection.Ascending));
            sortedResult.Source = cResult;
            // Sum totals
            maxTotals = 0;
            currentTotals = 0;
            currentTotals2 = 0;
            lastBreak = "";
            foreach (sStudiesReadPerDayResult item in sortedResult.View)
            {
                if (item.Rad == e.DataPoint.XCategory)
                {
                    if (item.Location == lastBreak)
                    {
                        currentTotals += item.Exams.Value;
                        currentTotals2 += item.RVUs.Value;
                    }
                    else
                    {
                        if (lastBreak != "")
                        {
                            barSeries3.Add(new DataPoint() { YValue = currentTotals, XCategory = lastBreak });
                            barSeries4.Add(new DataPoint() { YValue = currentTotals2, XCategory = lastBreak });
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
                        currentTotals = item.Exams.Value;
                        currentTotals2 = item.RVUs.Value;
                    }
                }
            }
            if (lastBreak != "")
            {
                barSeries3.Add(new DataPoint() { YValue = currentTotals, XCategory = lastBreak });
                barSeries4.Add(new DataPoint() { YValue = currentTotals2, XCategory = lastBreak });
            }
            if (currentTotals > maxTotals)
            {
                maxTotals = currentTotals;
            }
            if (currentTotals2 > maxTotals)
            {
                maxTotals = currentTotals2;
            }
            chartA.DefaultView.ChartArea.DataSeries.Clear();
            chartA.DefaultView.ChartArea.DataSeries.Add(barSeries3);
            chartA.DefaultView.ChartArea.DataSeries.Add(barSeries4);
            // Set the max Y axis height
            chartA.DefaultView.ChartArea.AxisY.StripLinesVisibility = System.Windows.Visibility.Collapsed;
            chartA.DefaultView.ChartArea.AxisY.Visibility = System.Windows.Visibility.Collapsed;
            chartA.DefaultView.ChartArea.AxisY.AutoRange = false;
            chartA.DefaultView.ChartArea.AxisY.MaxValue = maxTotals;
            //
            StackPanel panel = new StackPanel();
            panel.Children.Add(chart);
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
            client.GetStudiesReadPerDayCompleted += new EventHandler<GetStudiesReadPerDayCompletedEventArgs>(client_GetStudiesReadPerDayCompleted);
            client.GetStudiesReadPerDayAsync(begDate, endDateTime, SelRadGroup);
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
        public void closeMe()
        {
            if (myDispatcherTimer.IsEnabled == true)
            {
                myDispatcherTimer.Stop();
            }
            this.RemoveFromParent();
        }

        private void RadGroup_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangedEventArgs e)
        {
            SelRadGroup = System.Convert.ToInt32(RadGroup.SelectedValue);
        }
    }
}

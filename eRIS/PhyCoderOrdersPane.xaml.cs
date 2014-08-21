using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Data;
using System.Windows.Navigation;
using System.ComponentModel;
using System.ServiceModel;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using eRIS;
using eRIS.eRISServiceReference;

namespace eRIS
{
    public partial class PhyCoderOrdersPane : RadDocumentPane
    {
        private MainPage pPage = Application.Current.RootVisual as MainPage;
        public sGetOrderResult currentDetail = new sGetOrderResult();
        public TextBox tb;
        public PhyCoderOrdersPane(string id)
        {
            InitializeComponent();
            //
            // Cleanup
            tabControl.Items.Clear();
            //
            initScreen(id);
        }
        public void initScreen(string id)
        {
            // Cleanup
            tabControl.Items.Clear();
            // Retrieve details
            eRISServiceClient client = new eRISServiceClient();
            client.GetOrderCompleted += new EventHandler<GetOrderCompletedEventArgs>(client_GetOrderCompleted);
            client.GetOrderAsync(System.Convert.ToInt32(id));
        }
        // Event fired when result arrives
        void client_GetOrderCompleted(object sender, GetOrderCompletedEventArgs e)
        {
            // Load result
            currentDetail = e.Result.First();
            //
            dLocation.Text = currentDetail.LocationCode;
            dMRN.Text = currentDetail.PatientMRN;
            dExam.Text = currentDetail.ExamNumber.ToString();
            dProcedure.Text = currentDetail.ProcedureName;
            dVisit.Text = currentDetail.VisitNumber;
            dMRN.Text = currentDetail.PatientMRN;
            dOrder.Text = currentDetail.OrderedDate.ToString();
            dPerform.Text = currentDetail.PerformedDate.ToString();
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Rcvd Data");
            // Retrieve demographics
            eRISServiceClient client = new eRISServiceClient();
            client.GetDemographicsCompleted += new EventHandler<GetDemographicsCompletedEventArgs>(client_GetDemographicsCompleted);
            client.GetDemographicsAsync(currentDetail.LocationCode, currentDetail.PatientMRN, currentDetail.VisitNumber);
            // Retrieve orders
            client.GetOrdersCompleted += new EventHandler<GetOrdersCompletedEventArgs>(client_GetOrdersCompleted);
            client.GetOrdersAsync(currentDetail.LocationCode, currentDetail.PatientMRN, currentDetail.VisitNumber);
            // Retrieve additional reports
            client.GetReportsCompleted += new EventHandler<GetReportsCompletedEventArgs>(client_GetReportsCompleted);
            client.GetReportsAsync(currentDetail.LocationCode, currentDetail.PatientMRN, currentDetail.ExamNumber, currentDetail.VisitNumber);
        }
        // Event fired when result arrives
        void client_GetDemographicsCompleted(object sender, GetDemographicsCompletedEventArgs e)
        {
            // Load result
            sGetDemographicsResult gd = new sGetDemographicsResult();
            if (e.Result.Count() > 0)
            {
                gd = e.Result.First();
                dAdmitDate.Text = gd.AdmitDate.ToString();
                dIns1.Text = gd.Insurance1;
                dIns2.Text = gd.Insurance2;
                dIns3.Text = gd.Insurance3;
            }
            else
            {
                dAdmitDate.Text = "";
                dIns1.Text = "";
                dIns2.Text = "";
                dIns3.Text = "";
            }
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Rcvd Demo");
        }
        // Event fired when result arrives
        void client_GetOrdersCompleted(object sender, GetOrdersCompletedEventArgs e)
        {
            // Build gridview
            RadGridView gv = new RadGridView();
            gv.Name = "iOrders";
            gv.VerticalAlignment = VerticalAlignment.Stretch;
            gv.HorizontalAlignment = HorizontalAlignment.Left;
            gv.MaxWidth = 550;
            gv.AutoGenerateColumns = false;
            gv.CanUserSelect = true;
            gv.SelectionMode = System.Windows.Controls.SelectionMode.Multiple;
            gv.IsReadOnly = false;
            gv.CanUserReorderColumns = false;
            gv.CanUserFreezeColumns = false;
            gv.IsFilteringAllowed = false;
            gv.ShowGroupPanel = false;
            gv.RowIndicatorVisibility = Visibility.Collapsed;
            // Add columns
            GridViewDataColumn c0 = new GridViewDataColumn();
            c0.DataMemberBinding = new Binding("ID");
            c0.IsVisible = false;
            gv.Columns.Add(c0);
            GridViewSelectColumn cs = new GridViewSelectColumn();
            gv.Columns.Add(cs);
            GridViewDataColumn c1 = new GridViewDataColumn();
            c1.DataMemberBinding = new Binding("Status");
            c1.Header = "S";
            gv.Columns.Add(c1);
            GridViewDataColumn c2 = new GridViewDataColumn();
            c2.DataMemberBinding = new Binding("ExamNumber");
            c2.Header = "Exam Nbr";
            gv.Columns.Add(c2);
            GridViewDataColumn c3 = new GridViewDataColumn();
            c3.DataMemberBinding = new Binding("OrderedDate");
            c3.Header = "Ordered";
            gv.Columns.Add(c3);
            GridViewDataColumn c4 = new GridViewDataColumn();
            c4.DataMemberBinding = new Binding("ProcedureName");
            c4.Header = "Procedure Name";
            gv.Columns.Add(c4);
            GridViewDataColumn c5 = new GridViewDataColumn();
            c5.DataMemberBinding = new Binding("Status1");
            c5.Header = "POS";
            gv.Columns.Add(c5);
            GridViewDataColumn c6 = new GridViewDataColumn();
            c6.DataMemberBinding = new Binding("OriginalExamNumber");
            c6.Header = "On Report";
            gv.Columns.Add(c6);
            // Add result
            gv.ItemsSource = e.Result;
            // Loop through results
            dStatus1.Text = "";
            dStatus2.Text = "";
            foreach (sGetOrdersResult item in gv.Items)
            {
                if (item.ExamNumber == currentDetail.ExamNumber)
                {
                    // Extract status
                    dStatus1.Text = item.Status1;
                    dStatus2.Text = item.Status2;
                }
                if (item.ExamNumber == currentDetail.ExamNumber || item.OriginalExamNumber == currentDetail.ExamNumber)
                {
                    // Mark as corresponding order
                    gv.SelectedItems.Add(item);
                }
            }
            // Add event
            gv.SelectionChanged += new EventHandler<SelectionChangeEventArgs>(gv_SelectionChanged);
            // Add gridview to tab
            tabControl.Items.Add(new TabItem { Header = "Orders", Content = gv });
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Rcvd Orders");
        }
        private void gv_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            // Get the unselected items from the grid
            foreach (sGetOrdersResult item in e.RemovedItems)
            {
                eRISServiceClient client = new eRISServiceClient();
                client.SetExamNumberAsync(Convert.ToInt32(currentDetail.ID), "0");
                // Log status
                pPage.appendLog(this.GetType().ToString(), "UnMatched " + item.ExamNumber.ToString());
            }
            // Get the selected items from the grid
            foreach (sGetOrdersResult item in e.AddedItems)
            {
                if (currentDetail.ExamNumber != item.ExamNumber)
                {
                    eRISServiceClient client = new eRISServiceClient();
                    client.SetExamNumberAsync(Convert.ToInt32(currentDetail.ID), item.ExamNumber);
                    // Log status
                    pPage.appendLog(this.GetType().ToString(), "Matched " + item.ExamNumber.ToString());
                }
            }
        }
        // Event fired when result arrives
        void client_GetReportsCompleted(object sender, GetReportsCompletedEventArgs e)
        {
            foreach (var item in e.Result)
            {
                // Create tab control
                TabControl tab = new TabControl();
                tab.MinWidth = 540;
                tab.MaxWidth = 540;
                tab.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                tab.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                tab.TabStripPlacement = System.Windows.Controls.Dock.Bottom;
                // Add HTML tab for Report
                RadHtmlPlaceholder htmlReport = new RadHtmlPlaceholder();
                htmlReport.Width = 540;
                htmlReport.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                htmlReport.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                htmlReport.HtmlSource = "<font size=\"2\">" + item.Report.Replace("^", "<br />") + "</font>";
                tab.Items.Add(new TabItem { Header = "Report", Content = htmlReport });
                // add coding
                UserControl pane = new PhyCoderSubPane(item);
                tab.Items.Add(new TabItem { Header = "Details", Content = pane });
                tabControl.Items.Add(new TabItem { Header = "+"+item.Status, Content = tab });
            }
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Rcvd Reports");
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            // Update report
            eRISServiceClient client = new eRISServiceClient();
            client.MarkDeletedAsync(currentDetail.LocationCode, currentDetail.ExamNumber, pPage.currentLogin.ID);
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Deleted " + currentDetail.ID);
            //
            refreshWorklist();
        }
        private void refreshWorklist() 
        {
            bool closeMe = false;
            // Refresh worklist
            RadPaneGroup pg = pPage.WorkspacePanes;
            foreach (RadPane pane in pg.EnumeratePanes())
            {
                if (pane.Title.ToString() == "Coder Worklist")
                {
                    PhyCoderWorklistPane pcwp = pane as PhyCoderWorklistPane;
                    // Check to see if we need to reload
                    if (pcwp.bMissingReport.IsChecked == true)
                    {
                        pcwp.reloadCodingPane = true;
                    }
                    else
                    {
                        closeMe = true;
                        pcwp.reloadCodingPane = false;
                    }
                    pcwp.refreshWorklist2(this);
                }
            }
            // Check to see if we need to close this pane
            if (closeMe == true)
            {
                // Close this page
                this.RemoveFromParent();
            }
        }
        private void btnNextOrder_Click(object sender, RoutedEventArgs e)
        {
            //
            refreshWorklist();
        }
    }
}

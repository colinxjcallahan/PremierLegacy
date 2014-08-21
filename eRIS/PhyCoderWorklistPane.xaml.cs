using System;
using System.Linq;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using eRIS.eRISServiceReference;

namespace eRIS
{
    public partial class PhyCoderWorklistPane : RadDocumentPane
    {
        private MainPage pPage = Application.Current.RootVisual as MainPage;
        private System.Windows.Threading.DispatcherTimer myDispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        private string SQL;
        private bool firstTime;
        public bool reloadCodingPane;
        private PhyCoderOrdersPane PCOP;
        private long lastID = 0;
        private long xCoderID = 0;
        public PhyCoderWorklistPane()
        {
            InitializeComponent();
            // Initialize the timer
            myDispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 300000);
            myDispatcherTimer.Tick += new EventHandler(refreshTotals);
            //
            resetSelection();
            // Hide stuff if isCoderLimited
            if (pPage.currentLogin.isCoderLimited == true)
            {
                gvAvailableToCode.Visibility = System.Windows.Visibility.Collapsed;
                bMissingDemo.Visibility = System.Windows.Visibility.Collapsed;
                bMissingInsurance.Visibility = System.Windows.Visibility.Collapsed;
                bMissingOrder.Visibility = System.Windows.Visibility.Collapsed;
                bMissingReport.Visibility = System.Windows.Visibility.Collapsed;
                bPending.Visibility = System.Windows.Visibility.Collapsed;
                gvCoded.Visibility = System.Windows.Visibility.Collapsed;
            }
            // Seed the totals the first time
            updateTotals();
        }
        // Refresh the totals
        void refreshTotals(object o, EventArgs sender)
        {
            updateTotals();
        }
        // Event fired when results arrive
        void client_PhyCoderReportsCompleted(object sender, PhyCoderReportsCompletedEventArgs e)
        {
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Rcvd List");
            // Load combo with result
            gvDetails.ItemsSource = e.Result;
            //
            updateTotals();
        }
        // Event fired when results arrive
        void client_PhyCoderOrdersCompleted(object sender, PhyCoderOrdersCompletedEventArgs e)
        {
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Rcvd List");
            // Load combo with result
            gvDetails.ItemsSource = e.Result;
            if (e.Result.Count > 0)
            {
                // Get the selected item from the grid
                Order item = e.Result.First() as Order;
                // Turn off reload if we get the same report 
                if (item.ID == lastID)
                {
                    // Close the coding pane
                    if (reloadCodingPane == true)
                    {
                        if (bMissingReport.IsChecked == true)
                            PCOP.RemoveFromParent();
                        reloadCodingPane = false;
                    }
                }
                // Save the ID for next time
                lastID = item.ID;
                // Check to see if we need to load PhyCoderCodingPane
                if (reloadCodingPane == true)
                {
                    // Reset flag
                    reloadCodingPane = false;
                    // Open new panel
                    if (bMissingReport.IsChecked == true)
                        PCOP.initScreen(item.ID.ToString());
                    // Log status
                    pPage.appendLog(this.GetType().ToString(), item.ID.ToString());
                }
            }
            else
            {
                lastID = 0;
                if (reloadCodingPane == true)
                {
                    reloadCodingPane = false;
                    // Close the coding pane
                    if (bMissingReport.IsChecked == true)
                        PCOP.RemoveFromParent();
                }
            }
            //
            updateTotals();
        }
        // Event fired when results arrive
        void client_CoderTotalsCompleted(object sender, CoderTotalsCompletedEventArgs e)
        {
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Rcvd Totals");
            // Clear gridviews
            gvAvailableToCode.Items.Clear();
            gvCoded.Items.Clear();
            // Load results
            foreach (var item in e.Result)
            {
                switch (item.ID)
                {
                    case "AvailableToCode":
                        gvAvailableToCode.Items.Add(item);
                        break;
                    case "MissingDemographics":
                        MissingDemo.Text = item.Records.ToString();
                        break;
                    case "MissingInsurance":
                        MissingInsurance.Text = item.Records.ToString();
                        break;
                    case "MissingOrder":
                        MissingOrder.Text = item.Records.ToString();
                        break;
                    case "MissingReport":
                        MissingReport.Text = item.Records.ToString();
                        break;
                    case "Pending":
                        Pending.Text = item.Records.ToString();
                        break;
                    case "IPending":
                        PendingMe.Text = item.Records.ToString();
                        break;
                    case "CodedToday":
                        gvCoded.Items.Add(item);
                        break;
                }
            }
        }
        public void refreshWorklist2(PhyCoderOrdersPane pane)
        {
            PCOP = pane;
            updateList();
        }
        public void updateList()
        {

            SQL = buildSQL();

            // Save the SQL statement for later use
            if (xCoding.IsChecked == false)
            {
                pPage.LastSQL = "";
                pPage.CodingLocation = "";
            }
            else
            {
                pPage.LastSQL = SQL;
                pPage.CodingLocation = xLocation.Text.ToUpper();
            }

            eRISServiceClient client = new eRISServiceClient();

            if (bMissingReport.IsChecked == true)
            {
                client.PhyCoderOrdersCompleted += new EventHandler<PhyCoderOrdersCompletedEventArgs>(client_PhyCoderOrdersCompleted);
                client.PhyCoderOrdersAsync(SQL);
            }
            else
            {
                client.PhyCoderReportsCompleted += new EventHandler<PhyCoderReportsCompletedEventArgs>(client_PhyCoderReportsCompleted);
                client.PhyCoderReportsAsync(SQL);
            }
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Req List");
        }
        public string buildSQL()
        {
            DateTime begDate = new DateTime();
            begDate = pPage.getToday();
            DateTime endDate = new DateTime(begDate.Ticks + 863999900000);

            string rFields = "r.ID, r.LocationCode, r.ExamNumber, r.PatientMRN, r.PatientName, r.RadName, r.SignedDate, r.OrderedDate, r.PerformedDate, r.Status, r.Modality, c.FirstName + ' ' + c.LastName AS ICD1, r.CodedDate, a.FirstName + ' ' + a.LastName AS ICD2, r.AssignedDate FROM Reports r LEFT JOIN VRIS_DOTNET.dbo.Personnel c ON (r.CodedBy = c.ID) LEFT JOIN VRIS_DOTNET.dbo.Personnel a ON (r.AssignedTo = a.ID) WHERE ";
            string oFields = "ID, LocationCode, ExamNumber, PatientMRN, PatientName, OrderedDate, PerformedDate, Status FROM Orders o WHERE ";
            string sortOrder = "";

            SQL = "SELECT ";
            firstTime = false;

            if (xRecordLimit.Text.Length > 0)
            {
                SQL = SQL + "TOP " + xRecordLimit.Text + " ";
            }
            if (xCoding.IsChecked == true)
            {
                SQL = SQL + rFields + "r.Status = 'N' AND hasDemographics = 'Y' AND hasOrder = 'Y' ";
                sortOrder = " ORDER BY CONVERT(DATETIME, FLOOR(CONVERT(FLOAT, r.PerformedDate))) ASC, r.PatientName ASC ";
            }
            else if (bMissingDemo.IsChecked == true)
            {
                SQL = SQL + rFields + "r.Status = 'N' AND hasDemographics = 'N' ";
                sortOrder = " ORDER BY r.PerformedDate ASC";
            }
            else if (bMissingInsurance.IsChecked == true)
            {
                SQL = SQL + rFields + "r.Status = 'T' ";
                sortOrder = " ORDER BY r.PerformedDate ASC";
            }
            else if (bMissingOrder.IsChecked == true)
            {
                SQL = SQL + rFields + "r.Status = 'N' AND hasOrder = 'N' ";
                sortOrder = " ORDER BY r.PerformedDate ASC";
            }
            else if (bMissingReport.IsChecked == true)
            {
                SQL = SQL + oFields + "o.Status = 'N' AND DATEDIFF(hour, PerformedDate, GetDate()) > 148 AND hasReport = 'N' AND ISNULL(o.OriginalExamNumber,'0') = '0' ";
                sortOrder = " ORDER BY o.OrderedDate ASC";
            }
            else if (bPending.IsChecked == true)
            {
                SQL = SQL + rFields + "(r.Status = 'P') ";
                sortOrder = " ORDER BY r.AssignedDate ASC";
            }
            else if (bPendingMe.IsChecked == true)
            {
                SQL = SQL + rFields + "(r.Status = 'P') AND (r.AssignedTo = " + pPage.currentLogin.ID.ToString() + ") ";
                sortOrder = " ORDER BY r.AssignedDate ASC";
            }
            else if (xCoderID != 0)
            {
                SQL = SQL + rFields + "(r.Status = 'B' OR r.Status = 'C') AND (r.CodedDate BETWEEN '" + begDate.ToString() + "' AND '" + endDate.ToString() + "')  AND (r.CodedBy = " + xCoderID + ") ";
                sortOrder = " ORDER BY r.CodedDate DESC";
            }
            else
            {
                firstTime = true;
                SQL = SQL + rFields;
                if (xStatus.Text.Length > 0)
                {
                    if (bMissingReport.IsChecked == true)
                        buildWHERE("o.Status = '" + xStatus.Text.ToUpper() + "' ");
                    else
                        buildWHERE("r.Status = '" + xStatus.Text.ToUpper() + "' ");
                }
            }
            if (xLocation.Text.Length > 0)
            {
                if (xLocation.Text.ToUpper() == "HOS")
                    buildWHERE("LocationCode IN ('HEN','HRZ','SKY','UMC','WMC')");
                else
                    buildWHERE("LocationCode='" + xLocation.Text.ToUpper() + "'");
            }
            if (xDate.Text.Length > 0)
            {
                buildWHERE("(PerformedDate Between '" + xDate.Text + " 00:00:00.000' AND '" + xDate.Text + " 23:59:59.000')");
            }
            if (xExamNumber.Text.Length > 0)
            {
                buildWHERE("ExamNumber='" + xExamNumber.Text.TrimEnd() + "'");
            }
            if (xPatient.Text.Length > 0)
            {
                buildWHERE("PatientName LIKE '" + xPatient.Text.ToUpper().Replace("'", "''") + "%'");
            }
            if (xMRN.Text.Length > 0)
            {
                buildWHERE("PatientMRN='" + xMRN.Text.ToUpper() + "'");
            }
            if (xModality.Text.Length > 0)
            {
                buildWHERE("Modality='" + xModality.Text.ToUpper() + "'");
            }
            if (xDateLimit.Text.Length > 0)
            {
                buildWHERE("(PerformedDate < '" + xDateLimit.Text + " 23:59:59.000')");
            }
            SQL = SQL + sortOrder;
            //
            return SQL;
        }
        private void buildWHERE(string WHERE)
        {
            if (firstTime == false)
            {
                SQL = SQL + "AND ";
            }
            else
            {
                firstTime = false;
            }
            SQL = SQL + WHERE + " ";
        }
        public void updateTotals()
        {
            // Always reset the timer
            if (myDispatcherTimer.IsEnabled == true)
            {
                myDispatcherTimer.Stop();
            }
            //
            eRISServiceClient client = new eRISServiceClient(); 
            client.CoderTotalsCompleted += new EventHandler<CoderTotalsCompletedEventArgs>(client_CoderTotalsCompleted);
            client.CoderTotalsAsync(pPage.currentLogin.ID);
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Req Totals");
            // Restart the timer
            myDispatcherTimer.Start();
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            updateList();
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            // Reset criteria
            resetSelection();
            xStatus.Text = "";
            clearButtons();
        }

        private void clearButtons()
        {
            // Clear selected button
            bMissingDemo.IsChecked = false;
            bMissingInsurance.IsChecked = false;
            bMissingOrder.IsChecked = false;
            bMissingReport.IsChecked = false;
            bPending.IsChecked = false;
            bPendingMe.IsChecked = false;
        }
        private void resetSelection()
        {
            // Clear search fields
            xCoding.IsChecked = false;
            xStatus.Text = "N";
            xDate.Text = "";
            xDateLimit.Text = "";
            xExamNumber.Text = "";
            xLocation.Text = "";
            xMRN.Text = "";
            xPatient.Text = "";
            xModality.Text = "";
            xRecordLimit.Text = "50";
            xCoderID = 0;
        }
        private void bMissingDemo_Click(object sender, RoutedEventArgs e)
        {
            resetSelection();
            updateList();
        }
        private void bMissingInsurance_Click(object sender, RoutedEventArgs e)
        {
            resetSelection();
            updateList();
        }
        private void bMissingOrder_Click(object sender, RoutedEventArgs e)
        {
            resetSelection();
            updateList();
        }
        private void bMissingReport_Click(object sender, RoutedEventArgs e)
        {
            resetSelection();
            updateList();
        }
        private void bCodedByAll_Click(object sender, RoutedEventArgs e)
        {
            resetSelection();
            updateList();
        }
        private void gvDetails_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            if (bMissingReport.IsChecked == true)
            {
                foreach (Order item in e.AddedItems)
                {
                    // Open new panel
                    pPage.openPanel(item.ID.ToString(), "PhyCoderOrdersPane");
                    // Save the ID for later use
                    lastID = item.ID;
                    // Log status
                    pPage.appendLog(this.GetType().ToString(), item.ID.ToString());
                    // Unselect all
                    gvDetails.SelectedItems.Clear();
                } 
            }
            else
            {
                foreach (Report item in e.AddedItems)
                {
                    // Set the status to pending
                    if (item.Status == 'N')
                    {
                        eRISServiceClient client = new eRISServiceClient();
                        client.SetPendingAsync(Convert.ToInt32(item.ID), pPage.currentLogin.ID);
                    }
                    pPage.openPanel(item.ID.ToString(), "PhyCoderCodingPane");
                    // Log status
                    pPage.appendLog(this.GetType().ToString(), item.ID.ToString());
                    // Unselect all
                    gvDetails.SelectedItems.Clear();
                }
            }
        }
        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            string extension = "csv";
            SaveFileDialog dialog = new SaveFileDialog()
            {
                DefaultExt = extension,
                Filter = String.Format("{1} files (*.{0})|*.{0}|All files (*.*)|*.*", extension, "Excel"),
                FilterIndex = 1
            };
            if (dialog.ShowDialog() == true)
            {
                using (Stream stream = dialog.OpenFile())
                {
                    gvDetails.Export(stream,
                     new GridViewExportOptions()
                     {
                         Format = ExportFormat.Csv,
                         ShowColumnHeaders = true,
                         ShowColumnFooters = false,
                         ShowGroupFooters = false,
                     });
                }
            }
        }
        private void bPending_Click(object sender, RoutedEventArgs e)
        {
            resetSelection();
            updateList();
        }
        private void bPendingMe_Click(object sender, RoutedEventArgs e)
        {
            resetSelection();
            updateList();
        }
        private void gvAvailableToCode_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            foreach (sCoderTotalsResult item in e.AddedItems)
           {
                //
                resetSelection();
                clearButtons();
                // Update items
                xCoding.IsChecked = true;
                xLocation.Text = item.Who;
                xRecordLimit.Text = "10";
                updateList();
                // Unselect all
                gvAvailableToCode.SelectedItems.Clear();
            }
        }
        private void gvCoded_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            foreach (sCoderTotalsResult item in e.AddedItems)
            {
                // Update items
                resetSelection();
                clearButtons();
                xCoderID = System.Convert.ToInt32(item.WhoID);
                xRecordLimit.Text = "2000";
                updateList();
                // Unselect all
                gvCoded.SelectedItems.Clear();
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
    public class gvCodedStyle : StyleSelector
    {
        private MainPage pPage = Application.Current.RootVisual as MainPage;
        public override Style SelectStyle(object item, DependencyObject container)
        {
            if (item is sCoderTotalsResult)
            {
                sCoderTotalsResult r = item as sCoderTotalsResult;
                if (r.WhoID == pPage.currentLogin.ID)
                {
                    return SelCoderStyle;
                }
                else
                {
                    return UnSelCoderStyle;
                }
            }
            return null;
        }
        public Style SelCoderStyle { get; set; }
        public Style UnSelCoderStyle { get; set; }
    }
    public class gvAvailableStyle : StyleSelector
    {
        private MainPage pPage = Application.Current.RootVisual as MainPage;
        public override Style SelectStyle(object item, DependencyObject container)
        {
            if (item is sCoderTotalsResult)
            {
                //
                sCoderTotalsResult r = item as sCoderTotalsResult;
                //
                // Refresh worklist
                RadPaneGroup pg = pPage.WorkspacePanes;
                foreach (RadPane pane in pg.EnumeratePanes())
                {
                    if (pane.Title.ToString() == "Coder Worklist")
                    {
                        PhyCoderWorklistPane pcwp = pane as PhyCoderWorklistPane;
                        if (r.Who == pcwp.xLocation.Text)
                            return SelAvailStyle;
                        else
                            return UnSelAvailStyle;
                    }
                }
            }
            return null;
        }
        public Style SelAvailStyle { get; set; }
        public Style UnSelAvailStyle { get; set; }
    } 
}

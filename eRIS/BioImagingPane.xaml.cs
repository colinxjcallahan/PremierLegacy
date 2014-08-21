using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.IO;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using eRIS.eRISServiceReference;

namespace eRIS
{
    public partial class BioImagingPane : RadDocumentPane
    {
        private MainPage pPage = Application.Current.RootVisual as MainPage;
        private string SQL;
        private bool firstTime;
        public BioImagingPane()
        {
            InitializeComponent();
        }
        // Event fired when results arrive
        void client_BioImagingReportsCompleted(object sender, BioImagingReportsCompletedEventArgs e)
        {
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Rcvd List");
            // Load combo with result
            gvDetails.ItemsSource = e.Result;
        }
        public void updateList()
        {
            DateTime begDate = new DateTime();
            begDate = pPage.getToday();
            DateTime endDate = new DateTime(begDate.Ticks + 863999900000);

            eRISServiceClient client = new eRISServiceClient();

            string sortOrder = "";

            SQL = "SELECT ";

            if (xRecordLimit.Text.Length > 0)
            {
                SQL = SQL + "TOP " + xRecordLimit.Text + " ";
            }

            SQL = SQL + "* FROM bAppointments WHERE ";

            firstTime = true;
            
            if (xLocation.Text.Length > 0)
            {
                buildWHERE("SiteCode='" + xLocation.Text.ToUpper() + "'");
            }
            if (xDate.Text.Length > 0)
            {
                buildWHERE("(PerformedDate Between '" + xDate.Text + " 00:00:00.000' AND '" + xDate.Text + " 23:59:59.000')");
            }
            if (xSSN.Text.Length > 0)
            {
                buildWHERE("ssn=" + xSSN.Text);
            }
            if (xPatient.Text.Length > 0)
            {
                buildWHERE("last LIKE '%" + xPatient.Text.Replace("'", "''") + "%' OR first LIKE '%" + xPatient.Text.Replace("'", "''") + "%' ");
            } 
            if (xDoctor.Text.Length > 0)
            {
                buildWHERE("listname LIKE '%" + xDoctor.Text.Replace("'", "''") + "%'");
            }
            if (xMRN.Text.Length > 0)
            {
                buildWHERE("PatientID='" + xMRN.Text.ToUpper() + "'");
            }
            SQL = SQL + sortOrder;
 
            client.BioImagingReportsCompleted += new EventHandler<BioImagingReportsCompletedEventArgs>(client_BioImagingReportsCompleted);
            client.BioImagingReportsAsync(SQL);

            // Log status
            pPage.appendLog(this.GetType().ToString(), "Req List");
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
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            updateList();
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            // Reset criteria
            resetSelection();
        }
        private void resetSelection()
        {
            // Clear search fields
            xDate.Text = "";
            xSSN.Text = "";
            xLocation.Text = "";
            xMRN.Text = "";
            xPatient.Text = "";
            xDoctor.Text = "";
            xLocation.Text = "";
            xRecordLimit.Text = "250";
        }
        private void gvDetails_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            // Get the selected item from the grid
            bAppointment item = gvDetails.SelectedItem as bAppointment;

            if (item.reportid.TrimEnd() == "")
            {
                MessageBox.Show("There is no report for this patient visit.");
            }
            else
            {
                RadWindow reportWindow = new RadWindow();
                reportWindow.Header = "Report for " + item.last + " on " + item.PerformedDate.ToString();
                reportWindow.Width = 650;
                reportWindow.Height = 650;
                reportWindow.WindowStartupLocation = Telerik.Windows.Controls.WindowStartupLocation.CenterScreen;
                RadHtmlPlaceholder htmlPage = new RadHtmlPlaceholder();
                htmlPage.SourceUrl = new Uri("https://eris.premierradiology.com/ServeDocument.ashx?doc=BioImagingReport&id=" + item.reportid);
                reportWindow.Content = htmlPage;
                reportWindow.ShowDialog();
                // Log status
                pPage.appendLog(this.GetType().ToString(), "Viewed " + item.reportid);
            }
        }

        private void xLocation_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                updateList();
            }
        }

        private void xDate_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                updateList();
            }
        }

        private void xMRN_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                updateList();
            }
        }

        private void xSSN_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                updateList();
            }
        }

        private void xPatient_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                updateList();
            }
        }

        private void xDoctor_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                updateList();
            }
        }

        private void xRecordLimit_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                updateList();
            }
        }
    }
}

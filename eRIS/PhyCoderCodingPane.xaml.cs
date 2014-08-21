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
using System.Windows.Browser;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using eRIS;
using eRIS.eRISServiceReference;

namespace eRIS
{
    public partial class PhyCoderCodingPane : RadDocumentPane
    {
        private MainPage pPage = Application.Current.RootVisual as MainPage;
        private eRISServiceClient client = new eRISServiceClient();
        public sGetReportResult currentDetail = new sGetReportResult();
        public TextBox tb;
        public String currentPlaceOfService;
        public PhyCoderCodingPane(string id)
        {
            InitializeComponent();
            // Set up combobox
            iAssignTo.ItemsSource = pPage.allUsers.Where(sGetUsersResult => sGetUsersResult.isCoderAssignable == true);
            iRad.ItemsSource = pPage.allUsers.Where(sGetUsersResult => sGetUsersResult.isRadiologistADI == true);
            // Set event handlers
            client.GetReportCompleted += new EventHandler<GetReportCompletedEventArgs>(client_GetReportCompleted);
            client.GetDemographicsCompleted += new EventHandler<GetDemographicsCompletedEventArgs>(client_GetDemographicsCompleted);
            client.GetOrdersCompleted += new EventHandler<GetOrdersCompletedEventArgs>(client_GetOrdersCompleted);
            client.GetReportsCompleted += new EventHandler<GetReportsCompletedEventArgs>(client_GetReportsCompleted);
            client.GetDuplicateReportsCompleted += new EventHandler<GetDuplicateReportsCompletedEventArgs>(client_GetDuplicateReportsCompleted);
            client.GetExamCommentsCompleted += new EventHandler<GetExamCommentsCompletedEventArgs>(client_GetExamCommentsCompleted);
            client.GetExamDocumentsCompleted += new EventHandler<GetExamDocumentsCompletedEventArgs>(client_GetExamDocumentsCompleted);
            client.UpdateReportCompleted += new EventHandler<UpdateReportCompletedEventArgs>(client_UpdateReportCompleted);
            client.SetRadCompleted += new EventHandler<SetRadCompletedEventArgs>(client_SetRadCompleted);
            // Retrieve details
            client.GetReportAsync(System.Convert.ToInt32(id));
        }
        // Event fired when result arrives
        void client_GetReportCompleted(object sender, GetReportCompletedEventArgs e)
        {
            // Load result
            if (e.Result.Count < 1)
            {
                // Close the coding pane
                RemoveFromParent();
            }
            else
            {
                currentDetail = e.Result.First();
                loadScreen();
            }
        }
        // Event fired when result arrives
        void client_UpdateReportCompleted(object sender, UpdateReportCompletedEventArgs e)
        {
            // Load result
            if (e.Result.Count < 1)
            {
                // Close the coding pane
                RemoveFromParent();
            }
            else
            {
                // Extract the first result
                sUpdateReportResult eDetail = e.Result.First();
                // Transfer fields
                currentDetail.AssignedDate = eDetail.AssignedDate;
                currentDetail.AssignedTo = eDetail.AssignedTo;
                currentDetail.Charge1 = eDetail.Charge1;
                currentDetail.Charge10 = eDetail.Charge10;
                currentDetail.Charge11 = eDetail.Charge11;
                currentDetail.Charge12 = eDetail.Charge12;
                currentDetail.Charge13 = eDetail.Charge13;
                currentDetail.Charge14 = eDetail.Charge14;
                currentDetail.Charge15 = eDetail.Charge15;
                currentDetail.Charge2 = eDetail.Charge2;
                currentDetail.Charge3 = eDetail.Charge3;
                currentDetail.Charge4 = eDetail.Charge4;
                currentDetail.Charge5 = eDetail.Charge5;
                currentDetail.Charge6 = eDetail.Charge6;
                currentDetail.Charge7 = eDetail.Charge7;
                currentDetail.Charge8 = eDetail.Charge8;
                currentDetail.Charge9 = eDetail.Charge9;
                currentDetail.CheckForAfterHours = eDetail.CheckForAfterHours;
                currentDetail.CheckForSameDayReports = eDetail.CheckForSameDayReports;
                currentDetail.CheckForPQRI = eDetail.CheckForPQRI;
                currentDetail.CodedBy = eDetail.CodedBy;
                currentDetail.CodedDate = eDetail.CodedDate;
                currentDetail.DictatedDate = eDetail.DictatedDate;
                currentDetail.Dosage1 = eDetail.Dosage1;
                currentDetail.Dosage10 = eDetail.Dosage10;
                currentDetail.Dosage11 = eDetail.Dosage11;
                currentDetail.Dosage12 = eDetail.Dosage12;
                currentDetail.Dosage13 = eDetail.Dosage13;
                currentDetail.Dosage14 = eDetail.Dosage14;
                currentDetail.Dosage15 = eDetail.Dosage15;
                currentDetail.Dosage2 = eDetail.Dosage2;
                currentDetail.Dosage3 = eDetail.Dosage3;
                currentDetail.Dosage4 = eDetail.Dosage4;
                currentDetail.Dosage5 = eDetail.Dosage5;
                currentDetail.Dosage6 = eDetail.Dosage6;
                currentDetail.Dosage7 = eDetail.Dosage7;
                currentDetail.Dosage8 = eDetail.Dosage8;
                currentDetail.Dosage9 = eDetail.Dosage9;
                currentDetail.ExamNumber = eDetail.ExamNumber;
                currentDetail.hasDemographics = eDetail.hasDemographics;
                currentDetail.hasOrder = eDetail.hasOrder;
                currentDetail.ICD1 = eDetail.ICD1;
                currentDetail.ICD10 = eDetail.ICD10;
                currentDetail.ICD11 = eDetail.ICD11;
                currentDetail.ICD12 = eDetail.ICD12;
                currentDetail.ICD13 = eDetail.ICD13;
                currentDetail.ICD14 = eDetail.ICD14;
                currentDetail.ICD15 = eDetail.ICD15;
                currentDetail.ICD2 = eDetail.ICD2;
                currentDetail.ICD3 = eDetail.ICD3;
                currentDetail.ICD4 = eDetail.ICD4;
                currentDetail.ICD5 = eDetail.ICD5;
                currentDetail.ICD6 = eDetail.ICD6;
                currentDetail.ICD7 = eDetail.ICD7;
                currentDetail.ICD8 = eDetail.ICD8;
                currentDetail.ICD9 = eDetail.ICD9;
                currentDetail.ID = eDetail.ID;
                currentDetail.LocationCode = eDetail.LocationCode;
                currentDetail.Modality = eDetail.Modality;
                currentDetail.Modifier1 = eDetail.Modifier1;
                currentDetail.Modifier10 = eDetail.Modifier10;
                currentDetail.Modifier11 = eDetail.Modifier11;
                currentDetail.Modifier12 = eDetail.Modifier12;
                currentDetail.Modifier13 = eDetail.Modifier13;
                currentDetail.Modifier14 = eDetail.Modifier14;
                currentDetail.Modifier15 = eDetail.Modifier15;
                currentDetail.Modifier2 = eDetail.Modifier2;
                currentDetail.Modifier3 = eDetail.Modifier3;
                currentDetail.Modifier4 = eDetail.Modifier4;
                currentDetail.Modifier5 = eDetail.Modifier5;
                currentDetail.Modifier6 = eDetail.Modifier6;
                currentDetail.Modifier7 = eDetail.Modifier7;
                currentDetail.Modifier8 = eDetail.Modifier8;
                currentDetail.Modifier9 = eDetail.Modifier9;
                currentDetail.Notes = eDetail.Notes;
                currentDetail.OrderedDate = eDetail.OrderedDate;
                currentDetail.PatientMRN = eDetail.PatientMRN;
                currentDetail.PatientName = eDetail.PatientName;
                currentDetail.PerformedDate = eDetail.PerformedDate;
                currentDetail.Price1 = eDetail.Price1;
                currentDetail.Price10 = eDetail.Price10;
                currentDetail.Price11 = eDetail.Price11;
                currentDetail.Price12 = eDetail.Price12;
                currentDetail.Price13 = eDetail.Price13;
                currentDetail.Price14 = eDetail.Price14;
                currentDetail.Price15 = eDetail.Price15;
                currentDetail.Price2 = eDetail.Price2;
                currentDetail.Price3 = eDetail.Price3;
                currentDetail.Price4 = eDetail.Price4;
                currentDetail.Price5 = eDetail.Price5;
                currentDetail.Price6 = eDetail.Price6;
                currentDetail.Price7 = eDetail.Price7;
                currentDetail.Price8 = eDetail.Price8;
                currentDetail.Price9 = eDetail.Price9;
                currentDetail.RadID = eDetail.RadID;
                currentDetail.RadName = eDetail.RadName;
                currentDetail.RadReview = eDetail.RadReview;
                currentDetail.Report = eDetail.Report;
                currentDetail.SignedDate = eDetail.SignedDate;
                currentDetail.Status = eDetail.Status;
                currentDetail.TotalCharges = eDetail.TotalCharges;
                currentDetail.TotalRVU = eDetail.TotalRVU;
                currentDetail.Unit1 = eDetail.Unit1;
                currentDetail.Unit10 = eDetail.Unit10;
                currentDetail.Unit11 = eDetail.Unit11;
                currentDetail.Unit12 = eDetail.Unit12;
                currentDetail.Unit13 = eDetail.Unit13;
                currentDetail.Unit14 = eDetail.Unit14;
                currentDetail.Unit15 = eDetail.Unit15;
                currentDetail.Unit2 = eDetail.Unit2;
                currentDetail.Unit3 = eDetail.Unit3;
                currentDetail.Unit4 = eDetail.Unit4;
                currentDetail.Unit5 = eDetail.Unit5;
                currentDetail.Unit6 = eDetail.Unit6;
                currentDetail.Unit7 = eDetail.Unit7;
                currentDetail.Unit8 = eDetail.Unit8;
                currentDetail.Unit9 = eDetail.Unit9;
                currentDetail.VisitNumber = eDetail.VisitNumber;
                // Load the screen
                loadScreen();
            }
        }
        public void loadScreen()
        {
            // Cleanup
            tabControl.Items.Clear();

            //
            dLocation.Text = currentDetail.LocationCode;
            dMRN.Text = currentDetail.PatientMRN;
            dExam.Text = currentDetail.ExamNumber.ToString();
            dVisit.Text = currentDetail.VisitNumber;
            if (currentDetail.OrderedDate.Value.Year > 1900)
            {
                dOrder.Text = currentDetail.OrderedDate.ToString();
                dOrderLbl.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                dOrderLbl.Visibility = System.Windows.Visibility.Collapsed;
            }
            dPerform.Text = currentDetail.PerformedDate.ToString();
            dReport.Text = currentDetail.SignedDate.ToString();
            iCharge1.Text = currentDetail.Charge1.TrimEnd();
            iCharge2.Text = currentDetail.Charge2.TrimEnd();
            iCharge3.Text = currentDetail.Charge3.TrimEnd();
            iCharge4.Text = currentDetail.Charge4.TrimEnd();
            iCharge5.Text = currentDetail.Charge5.TrimEnd();
            iCharge6.Text = currentDetail.Charge6.TrimEnd();
            iCharge7.Text = currentDetail.Charge7.TrimEnd();
            iCharge8.Text = currentDetail.Charge8.TrimEnd();
            iCharge9.Text = currentDetail.Charge9.TrimEnd();
            iCharge10.Text = currentDetail.Charge10.TrimEnd();
            iCharge11.Text = currentDetail.Charge11.TrimEnd();
            iCharge12.Text = currentDetail.Charge12.TrimEnd();
            iCharge13.Text = currentDetail.Charge13.TrimEnd();
            iCharge14.Text = currentDetail.Charge14.TrimEnd();
            iCharge15.Text = currentDetail.Charge15.TrimEnd();
            iModifier1.Text = currentDetail.Modifier1.TrimEnd();
            iModifier2.Text = currentDetail.Modifier2.TrimEnd();
            iModifier3.Text = currentDetail.Modifier3.TrimEnd();
            iModifier4.Text = currentDetail.Modifier4.TrimEnd();
            iModifier5.Text = currentDetail.Modifier5.TrimEnd();
            iModifier6.Text = currentDetail.Modifier6.TrimEnd();
            iModifier7.Text = currentDetail.Modifier7.TrimEnd();
            iModifier8.Text = currentDetail.Modifier8.TrimEnd();
            iModifier9.Text = currentDetail.Modifier9.TrimEnd();
            iModifier10.Text = currentDetail.Modifier10.TrimEnd();
            iModifier11.Text = currentDetail.Modifier11.TrimEnd();
            iModifier12.Text = currentDetail.Modifier12.TrimEnd();
            iModifier13.Text = currentDetail.Modifier13.TrimEnd();
            iModifier14.Text = currentDetail.Modifier14.TrimEnd();
            iModifier15.Text = currentDetail.Modifier15.TrimEnd();
            iICD1.Text = currentDetail.ICD1.TrimEnd();
            iICD2.Text = currentDetail.ICD2.TrimEnd();
            iICD3.Text = currentDetail.ICD3.TrimEnd();
            iICD4.Text = currentDetail.ICD4.TrimEnd();
            iICD5.Text = currentDetail.ICD5.TrimEnd();
            iICD6.Text = currentDetail.ICD6.TrimEnd();
            iICD7.Text = currentDetail.ICD7.TrimEnd();
            iICD8.Text = currentDetail.ICD8.TrimEnd();
            iICD9.Text = currentDetail.ICD9.TrimEnd();
            iICD10.Text = currentDetail.ICD10.TrimEnd();
            iICD11.Text = currentDetail.ICD11.TrimEnd();
            iICD12.Text = currentDetail.ICD12.TrimEnd();
            iICD13.Text = currentDetail.ICD13.TrimEnd();
            iICD14.Text = currentDetail.ICD14.TrimEnd();
            iICD15.Text = currentDetail.ICD15.TrimEnd();
            iUnit1.Text = currentDetail.Unit1.ToString();
            iUnit2.Text = currentDetail.Unit2.ToString();
            iUnit3.Text = currentDetail.Unit3.ToString();
            iUnit4.Text = currentDetail.Unit4.ToString();
            iUnit5.Text = currentDetail.Unit5.ToString();
            iUnit6.Text = currentDetail.Unit6.ToString();
            iUnit7.Text = currentDetail.Unit7.ToString();
            iUnit8.Text = currentDetail.Unit8.ToString();
            iUnit9.Text = currentDetail.Unit9.ToString();
            iUnit10.Text = currentDetail.Unit10.ToString();
            iUnit11.Text = currentDetail.Unit11.ToString();
            iUnit12.Text = currentDetail.Unit12.ToString();
            iUnit13.Text = currentDetail.Unit13.ToString();
            iUnit14.Text = currentDetail.Unit14.ToString();
            iUnit15.Text = currentDetail.Unit15.ToString();
            iPrice1.Text = nzd(currentDetail.Price1.ToString());
            iPrice2.Text = nzd(currentDetail.Price2.ToString());
            iPrice3.Text = nzd(currentDetail.Price3.ToString());
            iPrice4.Text = nzd(currentDetail.Price4.ToString());
            iPrice5.Text = nzd(currentDetail.Price5.ToString());
            iPrice6.Text = nzd(currentDetail.Price6.ToString());
            iPrice7.Text = nzd(currentDetail.Price7.ToString());
            iPrice8.Text = nzd(currentDetail.Price8.ToString());
            iPrice9.Text = nzd(currentDetail.Price9.ToString());
            iPrice10.Text = nzd(currentDetail.Price10.ToString());
            iPrice11.Text = nzd(currentDetail.Price11.ToString());
            iPrice12.Text = nzd(currentDetail.Price12.ToString());
            iPrice13.Text = nzd(currentDetail.Price13.ToString());
            iPrice14.Text = nzd(currentDetail.Price14.ToString());
            iPrice15.Text = nzd(currentDetail.Price15.ToString());
            if (!Convert.IsDBNull(currentDetail.Dosage1))
                iDosage1.Text = Convert.ToString(currentDetail.Dosage1);
            else
                iDosage1.Text = "";
            if (!Convert.IsDBNull(currentDetail.Dosage2))
                iDosage2.Text = Convert.ToString(currentDetail.Dosage2);
            else
                iDosage2.Text = ""; 
            if (!Convert.IsDBNull(currentDetail.Dosage3))
                iDosage3.Text = Convert.ToString(currentDetail.Dosage3);
            else
                iDosage3.Text = "";
            if (!Convert.IsDBNull(currentDetail.Dosage4))
                iDosage4.Text = Convert.ToString(currentDetail.Dosage4);
            else
                iDosage4.Text = "";
            if (!Convert.IsDBNull(currentDetail.Dosage5))
                iDosage5.Text = Convert.ToString(currentDetail.Dosage5);
            else
                iDosage5.Text = "";
            if (!Convert.IsDBNull(currentDetail.Dosage6))
                iDosage6.Text = Convert.ToString(currentDetail.Dosage6);
            else
                iDosage6.Text = "";
            if (!Convert.IsDBNull(currentDetail.Dosage7))
                iDosage7.Text = Convert.ToString(currentDetail.Dosage7);
            else
                iDosage7.Text = "";
            if (!Convert.IsDBNull(currentDetail.Dosage8))
                iDosage8.Text = Convert.ToString(currentDetail.Dosage8);
            else
                iDosage8.Text = "";
            if (!Convert.IsDBNull(currentDetail.Dosage9))
                iDosage9.Text = Convert.ToString(currentDetail.Dosage9);
            else
                iDosage9.Text = "";
            if (!Convert.IsDBNull(currentDetail.Dosage10))
                iDosage10.Text = Convert.ToString(currentDetail.Dosage10);
            else
                iDosage10.Text = "";
            if (!Convert.IsDBNull(currentDetail.Dosage11))
                iDosage11.Text = Convert.ToString(currentDetail.Dosage11);
            else
                iDosage11.Text = "";
            if (!Convert.IsDBNull(currentDetail.Dosage12))
                iDosage12.Text = Convert.ToString(currentDetail.Dosage12);
            else
                iDosage12.Text = "";
            if (!Convert.IsDBNull(currentDetail.Dosage13))
                iDosage13.Text = Convert.ToString(currentDetail.Dosage13);
            else
                iDosage13.Text = "";
            if (!Convert.IsDBNull(currentDetail.Dosage14))
                iDosage14.Text = Convert.ToString(currentDetail.Dosage14);
            else
                iDosage14.Text = "";
            if (!Convert.IsDBNull(currentDetail.Dosage15))
                iDosage15.Text = Convert.ToString(currentDetail.Dosage15);
            else
                iDosage15.Text = "";
            iNotes.Text = currentDetail.Notes;

            // Add HTML tab for Report
            //RadHtmlPlaceholder htmlReport = new RadHtmlPlaceholder();
            //htmlReport.Width = 540;
            //htmlReport.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            //htmlReport.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            //htmlReport.HtmlSource = "<font size=\"2\">" + currentDetail.Report.Replace("^", "<br />") + "</font><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />";
            //tabControl.Items.Add(new TabItem { Header = "Report", Content = htmlReport });

            TextBox txtReport = new TextBox();
            txtReport.Width = 540;
            txtReport.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            txtReport.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            txtReport.TextWrapping = TextWrapping.Wrap;
            txtReport.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            txtReport.Text = currentDetail.Report.Replace("^", Environment.NewLine).Replace("<br>", Environment.NewLine).Replace("<table>", "").Replace("</table>", "").Replace("<tr>", "").Replace("</tr>", "").Replace("<td colspan='4'>", "").Replace("</td>", "").Replace("<tr style='line-height: 6pt;'>", "").Replace("<td align='center' colspan='4'>", "").Replace("&nbsp;", " ").Replace("<strong>", "").Replace("</strong>", "");
            tabControl.Items.Add(new TabItem { Header = "Report", Content = txtReport });
            
            //
            if (currentDetail.CheckForPQRI == true)
            {
                iPQRI.Visibility = System.Windows.Visibility.Visible;
            } 
            else 
            {
                iPQRI.Visibility = System.Windows.Visibility.Collapsed;
            }
            if (currentDetail.CheckForSameDayReports == true)
            {
                iSameDay.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                iSameDay.Visibility = System.Windows.Visibility.Collapsed;
            }

            if (currentDetail.CheckForAfterHours == true)
            {
                iAfterHours.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                iAfterHours.Visibility = System.Windows.Visibility.Collapsed;
            }
            // Defualt to current user if none selected
            if ((System.Convert.IsDBNull(currentDetail.AssignedTo) == true) || (currentDetail.AssignedTo == 0))
            {
                iAssignTo.SelectedValue = pPage.currentLogin.ID;
            }
            else
            {
                iAssignTo.SelectedValue = System.Convert.ToInt32(currentDetail.AssignedTo);
            }
            //
            iRad.SelectedValue = System.Convert.ToInt32(currentDetail.RadID);
            //
            iCodedBy.Text = "";
            iCodedByHdr.Text = "";
            if (!Convert.IsDBNull(currentDetail.CodedBy))
            {
                try
                {
                    sGetUsersResult i = pPage.allUsers.First(sGetUsersResult => sGetUsersResult.ID == currentDetail.CodedBy);
                    iCodedBy.Text = i.Name;
                    iCodedByHdr.Text = "Coded By:";
                }
                catch {}
            }
            //
            switch(currentDetail.Status.ToString()) {
                case "N":
                    Header = "New-" + currentDetail.PatientName;
                    updateButtons("On");
                    break;
                case "P":
                    Header = "Pending-" + currentDetail.PatientName;
                    updateButtons("On");
                    break;
                case "B":
                    Header = "Billed-" + currentDetail.PatientName;
                    //updateButtons("Off");
                    updateButtons("On");
                    break;
                case "C":
                    Header = "Coded-" + currentDetail.PatientName;
                    updateButtons("On");
                    break;
                case "D":
                    Header = "Deleted-" + currentDetail.PatientName;
                    updateButtons("On");
                    break;
                case "T":
                    Header = "Awaiting Insurance-" + currentDetail.PatientName;
                    updateButtons("Off");
                    break;
                default:
                    Header = currentDetail.Status.ToString() + "-" + currentDetail.PatientName;
                    break;
            }
            if (currentDetail.hasDemographics == 'N' || currentDetail.hasOrder == 'N') {
                btnFinal.Visibility = System.Windows.Visibility.Collapsed;
            }
            // Hide stuff if isCoderLimited
            if (pPage.currentLogin.isCoderLimited == true)
            {
                btnDelete.Visibility = System.Windows.Visibility.Collapsed;
                btnFinal.Visibility = System.Windows.Visibility.Collapsed;
            }
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Rcvd Data");
            // Retrieve demographics
            client.GetDemographicsAsync(currentDetail.LocationCode, currentDetail.PatientMRN, currentDetail.VisitNumber);
            // Retrieve orders
            client.GetOrdersAsync(currentDetail.LocationCode, currentDetail.PatientMRN, currentDetail.VisitNumber);
            // Retrieve additional reports
            client.GetReportsAsync(currentDetail.LocationCode, currentDetail.PatientMRN, currentDetail.ExamNumber, currentDetail.VisitNumber);
            // Retrieve duplicate reports
            client.GetDuplicateReportsAsync(System.Convert.ToInt32(currentDetail.ID));
            // Retrieve scanned documents and comments from RIS
            switch (currentDetail.LocationCode.TrimEnd()) {
                case "CLI":
                case "ASC":
                case "MDI":
                case "PMJ":
                case "OUT":
                    client.GetExamCommentsAsync(System.Convert.ToInt32(currentDetail.ExamNumber));
                    client.GetExamDocumentsAsync(currentDetail.PatientMRN, System.Convert.ToInt32(currentDetail.ExamNumber));
                    break;
            }
            // Make sure we have a good radiologist
            if (currentDetail.RadID == 0)
            {
                MessageBox.Show("The radiologist must be a valid ADI radiologist!");
                iRad.Focus();
            }
        }
        // Print the report
        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            //     
            string currentReport = "<html><head></head><body onload=\"window.print();window.close();\">";
            if (currentDetail.LocationCode == "UMC")
            {
                currentReport += "<table><tr><td valign='top'><strong>University Medical Center</strong><br />1411 W. Baddour Parkway<br />Lebanon, TN 37087</td><td valign='top'><strong>Patient: ";
                string[] name = currentDetail.PatientName.Split('^');
                currentReport += name[0] + ", " + name[1];
                currentReport += "</strong><br />MRN: " + currentDetail.PatientMRN + "<br />Date of Service: " + currentDetail.PerformedDate + "<br />Date of Birth: " + dDOB.Text + "</td></tr></table>";
            }
            currentReport += "<p>" + currentDetail.Report.Replace("^", "<br />") + "</p></body></html>";

            HtmlPage.Window.Invoke("popupWindow", currentReport);
        }
        // Toggle buttons
        void updateButtons(string Status)
        {
            if (Status == "Off") {
                btnPending.Visibility = System.Windows.Visibility.Collapsed;
                btnClear.Visibility = System.Windows.Visibility.Collapsed;
                btnFinal.Visibility = System.Windows.Visibility.Collapsed;
            } else {
                btnPending.Visibility = System.Windows.Visibility.Visible;
                btnClear.Visibility = System.Windows.Visibility.Visible;
                btnFinal.Visibility = System.Windows.Visibility.Visible;
            }
        }
        string nzd(string Value)
        {
            if (Convert.IsDBNull(Value))
                return "0.00";
            if (Value == "")
                return "0.00";
            return Value;
        }
        // Event fired when result arrives
        void client_GetDemographicsCompleted(object sender, GetDemographicsCompletedEventArgs e)
        {
            // Load result
            sGetDemographicsResult gd = new sGetDemographicsResult();
            dAdmitDate.Text = "";
            dDOB.Text = "";
            dIns1.Text = "";
            dIns2.Text = "";
            dIns3.Text = "";
            dPreAuth.Text = "";
            iVIP.Visibility = System.Windows.Visibility.Collapsed;

            if (e.Result.Count() > 0)
            {
                gd = e.Result.First();
                if (gd.AdmitDate.Value.Year > 1900)
                {
                    dAdmitDate.Text = gd.AdmitDate.ToString();
                    dAdmitLbl.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    dAdmitLbl.Visibility = System.Windows.Visibility.Collapsed;
                }
                int years = DateTime.Now.Year - gd.DOB.Value.Year; 
                if (DateTime.Now.Month < gd.DOB.Value.Month || (DateTime.Now.Month == gd.DOB.Value.Month && DateTime.Now.Day < gd.DOB.Value.Day)) 
                    years--;
                dDOB.Text = gd.DOB.Value.ToShortDateString() + " (" + years.ToString() + ")";
                dIns1.Text = gd.Insurance1;
                dIns2.Text = gd.Insurance2;
                dIns3.Text = gd.Insurance3;
                dPreAuth.Text = gd.PreAuthorization;
                if (gd.VIP == true)
                    iVIP.Visibility = System.Windows.Visibility.Visible;
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
            currentPlaceOfService = "";
            foreach (sGetOrdersResult item in gv.Items)
            {
                if (item.ExamNumber == currentDetail.ExamNumber)
                {
                    // Extract status
                    dLocation.Text = currentDetail.LocationCode.TrimEnd()+" - "+item.Status2.TrimEnd();
                    currentPlaceOfService = item.Status2.TrimEnd();
                    dStatus1.Text = item.Status1;
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
        // Event fired when result arrives
        void client_GetExamCommentsCompleted(object sender, GetExamCommentsCompletedEventArgs e)
        {
            // Build gridview
            RadGridView gv = new RadGridView();
            gv.Name = "iComments";
            gv.VerticalAlignment = VerticalAlignment.Stretch;
            gv.HorizontalAlignment = HorizontalAlignment.Stretch;
            gv.MaxWidth = 550;
            gv.AutoGenerateColumns = false;
            gv.CanUserSelect = false;
            gv.IsReadOnly = true;
            gv.CanUserReorderColumns = false;
            gv.CanUserFreezeColumns = false;
            gv.IsFilteringAllowed = false;
            gv.ShowGroupPanel = false;
            gv.RowIndicatorVisibility = Visibility.Collapsed;
            // Add columns
            GridViewDataColumn c0 = new GridViewDataColumn();
            c0.DataMemberBinding = new Binding("TextDate");
            c0.Header = "Date/Time";
            gv.Columns.Add(c0);
            GridViewDataColumn c1 = new GridViewDataColumn();
            c1.DataMemberBinding = new Binding("AdditionalText");
            c1.Header = "Comment";
            gv.Columns.Add(c1);
            GridViewDataColumn c2 = new GridViewDataColumn();
            c2.DataMemberBinding = new Binding("ShortName");
            c2.Header = "Entered By";
            gv.Columns.Add(c2);
            // Add result
            gv.ItemsSource = e.Result;
            // Add gridview to tab
            tabControl.Items.Add(new TabItem { Header = "Comments", Content = gv });
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Rcvd Comments");
        }
        // Event fired when result arrives
        void client_GetExamDocumentsCompleted(object sender, GetExamDocumentsCompletedEventArgs e)
        {
            // Build gridview
            RadGridView gv = new RadGridView();
            gv.Name = "iDocuments";
            gv.VerticalAlignment = VerticalAlignment.Stretch;
            gv.HorizontalAlignment = HorizontalAlignment.Stretch;
            gv.MaxWidth = 550;
            gv.AutoGenerateColumns = false;
            gv.CanUserSelect = true;
            gv.IsReadOnly = true;
            gv.CanUserReorderColumns = false;
            gv.CanUserFreezeColumns = false;
            gv.IsFilteringAllowed = false;
            gv.ShowGroupPanel = false;
            gv.RowIndicatorVisibility = Visibility.Collapsed;
            // Add columns
            GridViewDataColumn c0 = new GridViewDataColumn();
            c0.DataMemberBinding = new Binding("Key");
            c0.Header = "File Name";
            gv.Columns.Add(c0);
            GridViewDataColumn c1 = new GridViewDataColumn();
            c1.DataMemberBinding = new Binding("Value");
            c1.Header = "Description";
            gv.Columns.Add(c1);
            // Add result
            gv.ItemsSource = e.Result;
            // Add event
            gv.SelectionChanged += new EventHandler<SelectionChangeEventArgs>(iDocuments_SelectionChanged);
            // Add gridview to tab
            tabControl.Items.Add(new TabItem { Header = "Documents", Content = gv });
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Rcvd Documents");
        }
        private void iDocuments_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            foreach (KeyValuePair<string, string> item in e.AddedItems)
            {
                RadWindow reportWindow = new RadWindow();
                reportWindow.Header = item.Value;
                reportWindow.Width = 650;
                reportWindow.Height = 650;
                reportWindow.WindowStartupLocation = Telerik.Windows.Controls.WindowStartupLocation.CenterScreen;
                RadHtmlPlaceholder htmlPage = new RadHtmlPlaceholder();
                htmlPage.SourceUrl = new Uri("https://eris.premierradiology.com/ServeDocument.ashx?doc=SynapseDocument&id=" + item.Key);
                reportWindow.Content = htmlPage;
                reportWindow.ShowDialog();
            }
        }
        private void gv_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            // Get the unselected items from the grid
            foreach (sGetOrdersResult item in e.RemovedItems)
            {
                client.SetExamNumberAsync(Convert.ToInt32(item.ID), "0");
                // Log status
                pPage.appendLog(this.GetType().ToString(), "UnMatched " + item.ExamNumber.ToString());
            }
            // Get the selected items from the grid
            foreach (sGetOrdersResult item in e.AddedItems)
            {
                client.SetExamNumberAsync(Convert.ToInt32(item.ID), currentDetail.ExamNumber);
                // Log status
                pPage.appendLog(this.GetType().ToString(), "Matched " + item.ExamNumber.ToString());
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
                //RadHtmlPlaceholder htmlReport = new RadHtmlPlaceholder();
                //htmlReport.Width = 540;
                //htmlReport.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                //htmlReport.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                //htmlReport.HtmlSource = "<font size=\"2\">" + item.Report.Replace("^", "<br />") + "</font><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />";
                //tab.Items.Add(new TabItem { Header = "Report", Content = htmlReport });

                TextBox txtReport = new TextBox();
                txtReport.Width = 530;
                txtReport.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                txtReport.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                txtReport.TextWrapping = TextWrapping.Wrap;
                txtReport.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                txtReport.Text = item.Report.Replace("^", Environment.NewLine).Replace("<br>", Environment.NewLine).Replace("<table>", "").Replace("</table>", "").Replace("<tr>", "").Replace("</tr>", "").Replace("<td colspan='4'>", "").Replace("</td>", "").Replace("<tr style='line-height: 6pt;'>", "").Replace("<td align='center' colspan='4'>", "").Replace("&nbsp;", " ").Replace("<strong>", "").Replace("</strong>", "");
                tab.Items.Add(new TabItem { Header = "Report", Content = txtReport });

                // Add coding
                UserControl pane = new PhyCoderSubPane(item);
                tab.Items.Add(new TabItem { Header = "Details", Content = pane });
                tabControl.Items.Add(new TabItem { Header = "+" + item.Status, Content = tab });
            }
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Rcvd Reports");
        }
        // Event fired when result arrives
        void client_GetDuplicateReportsCompleted(object sender, GetDuplicateReportsCompletedEventArgs e)
        {
            string Hdr = "";
            foreach (var item in e.Result)
            {
                // Add HTML tab for Report
                //RadHtmlPlaceholder htmlReport = new RadHtmlPlaceholder();
                //htmlReport.Width = 540;
                //htmlReport.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                //htmlReport.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                //htmlReport.HtmlSource = "<font size=\"2\">" + item.Report.Replace("^", "<br />") + "</font><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />";
                if (item.Report.Contains("ADDENDUM"))
                {
                    Hdr = "Addendum";
                }
                else
                {
                    Hdr = "Dup Rpt";
                }
                //tabControl.Items.Add(new TabItem { Header = Hdr, Content = htmlReport });

                TextBox txtReport = new TextBox();
                txtReport.Width = 540;
                txtReport.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                txtReport.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                txtReport.TextWrapping = TextWrapping.Wrap;
                txtReport.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                txtReport.Text = item.Report.Replace("^", Environment.NewLine).Replace("<br>", Environment.NewLine).Replace("<table>", "").Replace("</table>", "").Replace("<tr>", "").Replace("</tr>", "").Replace("<td colspan='4'>", "").Replace("</td>", "").Replace("<tr style='line-height: 6pt;'>", "").Replace("<td align='center' colspan='4'>", "").Replace("&nbsp;", " ").Replace("<strong>", "").Replace("</strong>", "").Replace("<STRONG>", "").Replace("</STRONG>", "").Replace("</P>", "").Replace("<P style=\"MARGIN-TOP: 1px; MARGIN-BOTTOM: 1px\">", "");
                tabControl.Items.Add(new TabItem { Header = Hdr, Content = txtReport });

            }
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Rcvd Dup Reports");
        }
        private void btnPending_Click(object sender, RoutedEventArgs e)
        {
            if (System.Convert.ToInt32(iAssignTo.SelectedValue) == 0)
            {
                // Do not let them mark Pending without assigning to someone
                MessageBox.Show("You cannot mark a report Pending without assigning it to someone!");
            } 
            else 
            {
                // Update report
                client.UpdateReportAsync(System.Convert.ToInt32(currentDetail.ID), 'P', iCharge1.Text, iCharge2.Text, iCharge3.Text, iCharge4.Text, iCharge5.Text, iCharge6.Text, iCharge7.Text, iCharge8.Text, iCharge9.Text, iCharge10.Text, iCharge11.Text, iCharge12.Text, iCharge13.Text, iCharge14.Text, iCharge15.Text, iModifier1.Text, iModifier2.Text, iModifier3.Text, iModifier4.Text, iModifier5.Text, iModifier6.Text, iModifier7.Text, iModifier8.Text, iModifier9.Text, iModifier10.Text, iModifier11.Text, iModifier12.Text, iModifier13.Text, iModifier14.Text, iModifier15.Text, pPage.currentLogin.ID, iNotes.Text, iICD1.Text, iICD2.Text, iICD3.Text, iICD4.Text, iICD5.Text, iICD6.Text, iICD7.Text, iICD8.Text, iICD9.Text, iICD10.Text, iICD11.Text, iICD12.Text, iICD13.Text, iICD14.Text, iICD15.Text, System.Convert.ToInt32(iUnit1.Text), System.Convert.ToInt32(iUnit2.Text), System.Convert.ToInt32(iUnit3.Text), System.Convert.ToInt32(iUnit4.Text), System.Convert.ToInt32(iUnit5.Text), System.Convert.ToInt32(iUnit6.Text), System.Convert.ToInt32(iUnit7.Text), System.Convert.ToInt32(iUnit8.Text), System.Convert.ToInt32(iUnit9.Text), System.Convert.ToInt32(iUnit10.Text), System.Convert.ToInt32(iUnit11.Text), System.Convert.ToInt32(iUnit12.Text), System.Convert.ToInt32(iUnit13.Text), System.Convert.ToInt32(iUnit14.Text), System.Convert.ToInt32(iUnit15.Text), System.Convert.ToInt32(iAssignTo.SelectedValue), System.Convert.ToDecimal(iPrice1.Text), System.Convert.ToDecimal(iPrice2.Text), System.Convert.ToDecimal(iPrice3.Text), System.Convert.ToDecimal(iPrice4.Text), System.Convert.ToDecimal(iPrice5.Text), System.Convert.ToDecimal(iPrice6.Text), System.Convert.ToDecimal(iPrice7.Text), System.Convert.ToDecimal(iPrice8.Text), System.Convert.ToDecimal(iPrice9.Text), System.Convert.ToDecimal(iPrice10.Text), System.Convert.ToDecimal(iPrice11.Text), System.Convert.ToDecimal(iPrice12.Text), System.Convert.ToDecimal(iPrice13.Text), System.Convert.ToDecimal(iPrice14.Text), System.Convert.ToDecimal(iPrice15.Text), iDosage1.Text, iDosage2.Text, iDosage3.Text, iDosage4.Text, iDosage5.Text, iDosage6.Text, iDosage7.Text, iDosage8.Text, iDosage9.Text, iDosage10.Text, iDosage11.Text, iDosage12.Text, iDosage13.Text, iDosage14.Text, iDosage15.Text, pPage.CodingLocation);
            }
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            // Update report
            client.UpdateReportAsync(System.Convert.ToInt32(currentDetail.ID), 'N', iCharge1.Text, iCharge2.Text, iCharge3.Text, iCharge4.Text, iCharge5.Text, iCharge6.Text, iCharge7.Text, iCharge8.Text, iCharge9.Text, iCharge10.Text, iCharge11.Text, iCharge12.Text, iCharge13.Text, iCharge14.Text, iCharge15.Text, iModifier1.Text, iModifier2.Text, iModifier3.Text, iModifier4.Text, iModifier5.Text, iModifier6.Text, iModifier7.Text, iModifier8.Text, iModifier9.Text, iModifier10.Text, iModifier11.Text, iModifier12.Text, iModifier13.Text, iModifier14.Text, iModifier15.Text, pPage.currentLogin.ID, iNotes.Text, iICD1.Text, iICD2.Text, iICD3.Text, iICD4.Text, iICD5.Text, iICD6.Text, iICD7.Text, iICD8.Text, iICD9.Text, iICD10.Text, iICD11.Text, iICD12.Text, iICD13.Text, iICD14.Text, iICD15.Text, System.Convert.ToInt32(iUnit1.Text), System.Convert.ToInt32(iUnit2.Text), System.Convert.ToInt32(iUnit3.Text), System.Convert.ToInt32(iUnit4.Text), System.Convert.ToInt32(iUnit5.Text), System.Convert.ToInt32(iUnit6.Text), System.Convert.ToInt32(iUnit7.Text), System.Convert.ToInt32(iUnit8.Text), System.Convert.ToInt32(iUnit9.Text), System.Convert.ToInt32(iUnit10.Text), System.Convert.ToInt32(iUnit11.Text), System.Convert.ToInt32(iUnit12.Text), System.Convert.ToInt32(iUnit13.Text), System.Convert.ToInt32(iUnit14.Text), System.Convert.ToInt32(iUnit15.Text), 0, System.Convert.ToDecimal(iPrice1.Text), System.Convert.ToDecimal(iPrice2.Text), System.Convert.ToDecimal(iPrice3.Text), System.Convert.ToDecimal(iPrice4.Text), System.Convert.ToDecimal(iPrice5.Text), System.Convert.ToDecimal(iPrice6.Text), System.Convert.ToDecimal(iPrice7.Text), System.Convert.ToDecimal(iPrice8.Text), System.Convert.ToDecimal(iPrice9.Text), System.Convert.ToDecimal(iPrice10.Text), System.Convert.ToDecimal(iPrice11.Text), System.Convert.ToDecimal(iPrice12.Text), System.Convert.ToDecimal(iPrice13.Text), System.Convert.ToDecimal(iPrice14.Text), System.Convert.ToDecimal(iPrice15.Text), iDosage1.Text, iDosage2.Text, iDosage3.Text, iDosage4.Text, iDosage5.Text, iDosage6.Text, iDosage7.Text, iDosage8.Text, iDosage9.Text, iDosage10.Text, iDosage11.Text, iDosage12.Text, iDosage13.Text, iDosage14.Text, iDosage15.Text, pPage.CodingLocation);
        }
        private void btnFinal_Click(object sender, RoutedEventArgs e)
        {
            const string eMissingICD = "You must enter an ICD9 code for this charge line!";
            // Set the textbox to the first field
            tb = iCharge1;
            // Make sure the required fields are entered
            if (iCharge1.Text.TrimEnd() == "")
                tb = iICD1;
            if (iCharge1.Text.TrimEnd() != "" && iICD1.Text.TrimEnd() == "")
                tb = iICD1;
            if (iCharge2.Text.TrimEnd() != "" && iICD2.Text.TrimEnd() == "")
                tb = iICD2;
            if (iCharge3.Text.TrimEnd() != "" && iICD3.Text.TrimEnd() == "")
                tb = iICD3;
            if (iCharge4.Text.TrimEnd() != "" && iICD4.Text.TrimEnd() == "")
                tb = iICD4;
            if (iCharge5.Text.TrimEnd() != "" && iICD5.Text.TrimEnd() == "")
                tb = iICD5;
            if (iCharge6.Text.TrimEnd() != "" && iICD6.Text.TrimEnd() == "")
                tb = iICD6;
            if (iCharge7.Text.TrimEnd() != "" && iICD7.Text.TrimEnd() == "")
                tb = iICD7;
            if (iCharge8.Text.TrimEnd() != "" && iICD8.Text.TrimEnd() == "")
                tb = iICD8;
            if (iCharge9.Text.TrimEnd() != "" && iICD9.Text.TrimEnd() == "")
                tb = iICD9;
            if (iCharge10.Text.TrimEnd() != "" && iICD10.Text.TrimEnd() == "")
                tb = iICD10;
            if (iCharge11.Text.TrimEnd() != "" && iICD11.Text.TrimEnd() == "")
                tb = iICD11;
            if (iCharge12.Text.TrimEnd() != "" && iICD12.Text.TrimEnd() == "")
                tb = iICD12;
            if (iCharge13.Text.TrimEnd() != "" && iICD13.Text.TrimEnd() == "")
                tb = iICD13;
            if (iCharge14.Text.TrimEnd() != "" && iICD14.Text.TrimEnd() == "")
                tb = iICD14;
            if (iCharge15.Text.TrimEnd() != "" && iICD15.Text.TrimEnd() == "")
                tb = iICD15;
            //
            if (tb != iCharge1)
            {
                MessageBox.Show(eMissingICD);
                tb.Focus();
                return;
            }

            const string eMissingDosage = "You must enter the NDC in Box 19 for this charge line!";
            // Set the textbox to the first field
            tb = iCharge1;
            // Make sure the required fields are entered
            if (iCharge1.Text.TrimEnd() != "")
                if (iCharge1.Text.Substring(0, 1) == "A" || iCharge1.Text.Substring(0, 1) == "J" || iCharge1.Text.Substring(0, 1) == "Q")
                    if (iDosage1.Text.TrimEnd() == "")
                        tb = iDosage1;
            if (iCharge2.Text.TrimEnd() != "")
                if (iCharge2.Text.Substring(0, 1) == "A" || iCharge2.Text.Substring(0, 1) == "J" || iCharge2.Text.Substring(0, 1) == "Q")
                    if (iDosage2.Text.TrimEnd() == "")
                        tb = iDosage2;
            if (iCharge3.Text.TrimEnd() != "")
                if (iCharge3.Text.Substring(0, 1) == "A" || iCharge3.Text.Substring(0, 1) == "J" || iCharge3.Text.Substring(0, 1) == "Q")
                    if (iDosage3.Text.TrimEnd() == "")
                        tb = iDosage3;
            if (iCharge4.Text.TrimEnd() != "")
                if (iCharge4.Text.Substring(0, 1) == "A" || iCharge4.Text.Substring(0, 1) == "J" || iCharge4.Text.Substring(0, 1) == "Q")
                    if (iDosage4.Text.TrimEnd() == "")
                        tb = iDosage4;
            if (iCharge5.Text.TrimEnd() != "")
                if (iCharge5.Text.Substring(0, 1) == "A" || iCharge5.Text.Substring(0, 1) == "J" || iCharge5.Text.Substring(0, 1) == "Q")
                    if (iDosage5.Text.TrimEnd() == "")
                        tb = iDosage5;
            if (iCharge6.Text.TrimEnd() != "")
                if (iCharge6.Text.Substring(0, 1) == "A" || iCharge6.Text.Substring(0, 1) == "J" || iCharge6.Text.Substring(0, 1) == "Q")
                    if (iDosage6.Text.TrimEnd() == "")
                        tb = iDosage6;
            if (iCharge7.Text.TrimEnd() != "")
                if (iCharge7.Text.Substring(0, 1) == "A" || iCharge7.Text.Substring(0, 1) == "J" || iCharge7.Text.Substring(0, 1) == "Q")
                    if (iDosage7.Text.TrimEnd() == "")
                        tb = iDosage7;
            if (iCharge8.Text.TrimEnd() != "")
                if (iCharge8.Text.Substring(0, 1) == "A" || iCharge8.Text.Substring(0, 1) == "J" || iCharge8.Text.Substring(0, 1) == "Q")
                    if (iDosage8.Text.TrimEnd() == "")
                        tb = iDosage8;
            if (iCharge9.Text.TrimEnd() != "")
                if (iCharge9.Text.Substring(0, 1) == "A" || iCharge9.Text.Substring(0, 1) == "J" || iCharge9.Text.Substring(0, 1) == "Q")
                    if (iDosage9.Text.TrimEnd() == "")
                        tb = iDosage9;
            if (iCharge10.Text.TrimEnd() != "")
                if (iCharge10.Text.Substring(0, 1) == "A" || iCharge10.Text.Substring(0, 1) == "J" || iCharge10.Text.Substring(0, 1) == "Q")
                    if (iDosage10.Text.TrimEnd() == "")
                        tb = iDosage10;
            if (iCharge11.Text.TrimEnd() != "")
                if (iCharge11.Text.Substring(0, 1) == "A" || iCharge11.Text.Substring(0, 1) == "J" || iCharge11.Text.Substring(0, 1) == "Q")
                    if (iDosage11.Text.TrimEnd() == "")
                        tb = iDosage11;
            if (iCharge12.Text.TrimEnd() != "")
                if (iCharge12.Text.Substring(0, 1) == "A" || iCharge12.Text.Substring(0, 1) == "J" || iCharge12.Text.Substring(0, 1) == "Q")
                    if (iDosage12.Text.TrimEnd() == "")
                        tb = iDosage12;
            if (iCharge13.Text.TrimEnd() != "")
                if (iCharge13.Text.Substring(0, 1) == "A" || iCharge13.Text.Substring(0, 1) == "J" || iCharge13.Text.Substring(0, 1) == "Q")
                    if (iDosage13.Text.TrimEnd() == "")
                        tb = iDosage13;
            if (iCharge14.Text.TrimEnd() != "")
                if (iCharge14.Text.Substring(0, 1) == "A" || iCharge14.Text.Substring(0, 1) == "J" || iCharge14.Text.Substring(0, 1) == "Q")
                    if (iDosage14.Text.TrimEnd() == "")
                        tb = iDosage14;
            if (iCharge15.Text.TrimEnd() != "")
                if (iCharge15.Text.Substring(0, 1) == "A" || iCharge15.Text.Substring(0, 1) == "J" || iCharge15.Text.Substring(0, 1) == "Q")
                    if (iDosage15.Text.TrimEnd() == "")
                        tb = iDosage15;
            if (tb != iCharge1)
            {
                MessageBox.Show(eMissingDosage);
                tb.Focus();
                return;
            }

            // Lancaster charges cannot have a 26 modifier
            if (currentPlaceOfService == "NCTTN")
            {
                const string eBadModifier = "You cannot enter a 26 modifier on this charge line!";
                // Set the textbox to the first field
                tb = iCharge1;
                // Check the modifier entered
                if (iModifier1.Text.TrimEnd() == "26")
                    tb = iModifier1;
                if (iModifier2.Text.TrimEnd() == "26")
                    tb = iModifier2;
                if (iModifier3.Text.TrimEnd() == "26")
                    tb = iModifier3;
                if (iModifier4.Text.TrimEnd() == "26")
                    tb = iModifier4;
                if (iModifier5.Text.TrimEnd() == "26")
                    tb = iModifier5;
                if (iModifier6.Text.TrimEnd() == "26")
                    tb = iModifier6;
                if (iModifier7.Text.TrimEnd() == "26")
                    tb = iModifier7;
                if (iModifier8.Text.TrimEnd() == "26")
                    tb = iModifier8;
                if (iModifier9.Text.TrimEnd() == "26")
                    tb = iModifier9;
                if (iModifier10.Text.TrimEnd() == "26")
                    tb = iModifier10;
                if (iModifier11.Text.TrimEnd() == "26")
                    tb = iModifier11;
                if (iModifier12.Text.TrimEnd() == "26")
                    tb = iModifier12;
                if (iModifier13.Text.TrimEnd() == "26")
                    tb = iModifier13;
                if (iModifier14.Text.TrimEnd() == "26")
                    tb = iModifier14;
                if (iModifier15.Text.TrimEnd() == "26")
                    tb = iModifier15;
                //
                if (tb != iCharge1)
                {
                    MessageBox.Show(eBadModifier);
                    tb.Focus();
                    return;
                }
            }

            if (currentDetail.RadID == 0)
            {
                // Make sure we have a good radiologist
                MessageBox.Show("The radiologist must be a valid ADI radiologist!");
                iRad.Focus();
                return;
            }

            // Build list of G codes
            List<string> qmList = new List<string>();
	        qmList.Add ("G8907");
	        qmList.Add ("G8908");
	        qmList.Add ("G8909");
	        qmList.Add ("G8910");
	        qmList.Add ("G8911");
	        qmList.Add ("G8912");
	        qmList.Add ("G8913");
	        qmList.Add ("G8914");
	        qmList.Add ("G8915");
	        qmList.Add ("G8916");
	        qmList.Add ("G8917");
	        qmList.Add ("G8918");
            //
            const string eBadICD = "You cannot enter more than one diagnostic code on this charge line!";
            // Set the textbox to the first field
            tb = iCharge1;
            // ASC G codes can only have one ICD9
            if ((qmList.IndexOf(iCharge1.Text.Trim()) != -1) && (iICD1.Text.Contains(",")))
                tb = iICD1;
            if ((qmList.IndexOf(iCharge2.Text.Trim()) != -1) && (iICD2.Text.Contains(",")))
                tb = iICD2;
            if ((qmList.IndexOf(iCharge3.Text.Trim()) != -1) && (iICD3.Text.Contains(",")))
                tb = iICD3;
            if ((qmList.IndexOf(iCharge4.Text.Trim()) != -1) && (iICD4.Text.Contains(",")))
                tb = iICD4;
            if ((qmList.IndexOf(iCharge5.Text.Trim()) != -1) && (iICD5.Text.Contains(",")))
                tb = iICD5;
            if ((qmList.IndexOf(iCharge6.Text.Trim()) != -1) && (iICD6.Text.Contains(",")))
                tb = iICD6;
            if ((qmList.IndexOf(iCharge7.Text.Trim()) != -1) && (iICD7.Text.Contains(",")))
                tb = iICD7;
            if ((qmList.IndexOf(iCharge8.Text.Trim()) != -1) && (iICD8.Text.Contains(",")))
                tb = iICD8;
            if ((qmList.IndexOf(iCharge9.Text.Trim()) != -1) && (iICD9.Text.Contains(",")))
                tb = iICD9;
            if ((qmList.IndexOf(iCharge10.Text.Trim()) != -1) && (iICD10.Text.Contains(",")))
                tb = iICD10;
            if ((qmList.IndexOf(iCharge11.Text.Trim()) != -1) && (iICD11.Text.Contains(",")))
                tb = iICD11;
            if ((qmList.IndexOf(iCharge12.Text.Trim()) != -1) && (iICD12.Text.Contains(",")))
                tb = iICD12;
            if ((qmList.IndexOf(iCharge13.Text.Trim()) != -1) && (iICD13.Text.Contains(",")))
                tb = iICD13;
            if ((qmList.IndexOf(iCharge14.Text.Trim()) != -1) && (iICD14.Text.Contains(",")))
                tb = iICD14;
            if ((qmList.IndexOf(iCharge15.Text.Trim()) != -1) && (iICD15.Text.Contains(",")))
                tb = iICD15; 
            //
            if (tb != iCharge1)
            {
                MessageBox.Show(eBadICD);
                tb.Focus();
                return;
            }
            // Update report
            client.UpdateReportAsync(System.Convert.ToInt32(currentDetail.ID), 'C', iCharge1.Text, iCharge2.Text, iCharge3.Text, iCharge4.Text, iCharge5.Text, iCharge6.Text, iCharge7.Text, iCharge8.Text, iCharge9.Text, iCharge10.Text, iCharge11.Text, iCharge12.Text, iCharge13.Text, iCharge14.Text, iCharge15.Text, iModifier1.Text, iModifier2.Text, iModifier3.Text, iModifier4.Text, iModifier5.Text, iModifier6.Text, iModifier7.Text, iModifier8.Text, iModifier9.Text, iModifier10.Text, iModifier11.Text, iModifier12.Text, iModifier13.Text, iModifier14.Text, iModifier15.Text, pPage.currentLogin.ID, iNotes.Text, iICD1.Text, iICD2.Text, iICD3.Text, iICD4.Text, iICD5.Text, iICD6.Text, iICD7.Text, iICD8.Text, iICD9.Text, iICD10.Text, iICD11.Text, iICD12.Text, iICD13.Text, iICD14.Text, iICD15.Text, System.Convert.ToInt32(iUnit1.Text), System.Convert.ToInt32(iUnit2.Text), System.Convert.ToInt32(iUnit3.Text), System.Convert.ToInt32(iUnit4.Text), System.Convert.ToInt32(iUnit5.Text), System.Convert.ToInt32(iUnit6.Text), System.Convert.ToInt32(iUnit7.Text), System.Convert.ToInt32(iUnit8.Text), System.Convert.ToInt32(iUnit9.Text), System.Convert.ToInt32(iUnit10.Text), System.Convert.ToInt32(iUnit11.Text), System.Convert.ToInt32(iUnit12.Text), System.Convert.ToInt32(iUnit13.Text), System.Convert.ToInt32(iUnit14.Text), System.Convert.ToInt32(iUnit15.Text), 0, System.Convert.ToDecimal(iPrice1.Text), System.Convert.ToDecimal(iPrice2.Text), System.Convert.ToDecimal(iPrice3.Text), System.Convert.ToDecimal(iPrice4.Text), System.Convert.ToDecimal(iPrice5.Text), System.Convert.ToDecimal(iPrice6.Text), System.Convert.ToDecimal(iPrice7.Text), System.Convert.ToDecimal(iPrice8.Text), System.Convert.ToDecimal(iPrice9.Text), System.Convert.ToDecimal(iPrice10.Text), System.Convert.ToDecimal(iPrice11.Text), System.Convert.ToDecimal(iPrice12.Text), System.Convert.ToDecimal(iPrice13.Text), System.Convert.ToDecimal(iPrice14.Text), System.Convert.ToDecimal(iPrice15.Text), iDosage1.Text, iDosage2.Text, iDosage3.Text, iDosage4.Text, iDosage5.Text, iDosage6.Text, iDosage7.Text, iDosage8.Text, iDosage9.Text, iDosage10.Text, iDosage11.Text, iDosage12.Text, iDosage13.Text, iDosage14.Text, iDosage15.Text, pPage.CodingLocation);

            // Set focus elsewhere the disable button
            tb.Focus();
            updateButtons("Off");
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            // Update report
            client.UpdateReportAsync(System.Convert.ToInt32(currentDetail.ID), 'D', iCharge1.Text, iCharge2.Text, iCharge3.Text, iCharge4.Text, iCharge5.Text, iCharge6.Text, iCharge7.Text, iCharge8.Text, iCharge9.Text, iCharge10.Text, iCharge11.Text, iCharge12.Text, iCharge13.Text, iCharge14.Text, iCharge15.Text, iModifier1.Text, iModifier2.Text, iModifier3.Text, iModifier4.Text, iModifier5.Text, iModifier6.Text, iModifier7.Text, iModifier8.Text, iModifier9.Text, iModifier10.Text, iModifier11.Text, iModifier12.Text, iModifier13.Text, iModifier14.Text, iModifier15.Text, pPage.currentLogin.ID, iNotes.Text, iICD1.Text, iICD2.Text, iICD3.Text, iICD4.Text, iICD5.Text, iICD6.Text, iICD7.Text, iICD8.Text, iICD9.Text, iICD10.Text, iICD11.Text, iICD12.Text, iICD13.Text, iICD14.Text, iICD15.Text, System.Convert.ToInt32(iUnit1.Text), System.Convert.ToInt32(iUnit2.Text), System.Convert.ToInt32(iUnit3.Text), System.Convert.ToInt32(iUnit4.Text), System.Convert.ToInt32(iUnit5.Text), System.Convert.ToInt32(iUnit6.Text), System.Convert.ToInt32(iUnit7.Text), System.Convert.ToInt32(iUnit8.Text), System.Convert.ToInt32(iUnit9.Text), System.Convert.ToInt32(iUnit10.Text), System.Convert.ToInt32(iUnit11.Text), System.Convert.ToInt32(iUnit12.Text), System.Convert.ToInt32(iUnit13.Text), System.Convert.ToInt32(iUnit14.Text), System.Convert.ToInt32(iUnit15.Text), 0, System.Convert.ToDecimal(iPrice1.Text), System.Convert.ToDecimal(iPrice2.Text), System.Convert.ToDecimal(iPrice3.Text), System.Convert.ToDecimal(iPrice4.Text), System.Convert.ToDecimal(iPrice5.Text), System.Convert.ToDecimal(iPrice6.Text), System.Convert.ToDecimal(iPrice7.Text), System.Convert.ToDecimal(iPrice8.Text), System.Convert.ToDecimal(iPrice9.Text), System.Convert.ToDecimal(iPrice10.Text), System.Convert.ToDecimal(iPrice11.Text), System.Convert.ToDecimal(iPrice12.Text), System.Convert.ToDecimal(iPrice13.Text), System.Convert.ToDecimal(iPrice14.Text), System.Convert.ToDecimal(iPrice15.Text), iDosage1.Text, iDosage2.Text, iDosage3.Text, iDosage4.Text, iDosage5.Text, iDosage6.Text, iDosage7.Text, iDosage8.Text, iDosage9.Text, iDosage10.Text, iDosage11.Text, iDosage12.Text, iDosage13.Text, iDosage14.Text, iDosage15.Text, pPage.CodingLocation);
        }
        private void tb_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ContextMenu.Visibility = Visibility.Collapsed;
            ContextMenu.IsOpen = false; 
        }
        private void tb_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true; 
        }
        private void tb_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(null);
            e.Handled = true;
            ContextMenu.Visibility = Visibility.Visible;
            ContextMenu.IsOpen = true;
            ContextMenu.SetValue(Canvas.LeftProperty, (double)p.X);
            ContextMenu.SetValue(Canvas.TopProperty, (double)p.Y); 
        }
        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            HyperlinkButton b = (HyperlinkButton)sender;

            try
            {
                switch (b.Content.ToString())
                {
                    case "Cut Selected":
                        Telerik.Windows.Controls.Clipboard.SetText(tb.SelectedText);
                        tb.SelectedText = "";
                        tb.Focus();
                        break;
                    case "Copy Selected":
                        Telerik.Windows.Controls.Clipboard.SetText(tb.SelectedText);
                        tb.Focus();
                        break;
                    case "Select All and Copy":
                        tb.SelectAll();
                        Telerik.Windows.Controls.Clipboard.SetText(tb.SelectedText);
                        tb.Focus();
                        break;
                    case "Paste From Clipboard":
                        tb.SelectedText = Telerik.Windows.Controls.Clipboard.GetText();
                        break;
                    case "Lookup ICD9 Code":
                        UserControl pane = new ICD9Pane();
                        RadWindow icd9Window = new RadWindow();
                        icd9Window.Header = "Lookup ICD9 Codes";
                        icd9Window.Width = 413;
                        icd9Window.Height = 600;
                        icd9Window.WindowStartupLocation = Telerik.Windows.Controls.WindowStartupLocation.CenterScreen;
                        icd9Window.PreviewClosed += new EventHandler<WindowPreviewClosedEventArgs>(icd9Window_PreviewClosed);
                        icd9Window.Content = pane;
                        icd9Window.ShowDialog();
                        break;
                    default:
                        break;
                }
            }
            catch { }
            ContextMenu.Visibility = Visibility.Collapsed;
            ContextMenu.IsOpen = false;
        }
        private void icd9Window_PreviewClosed(object sender, WindowPreviewClosedEventArgs e)
        {
            RadWindow icd9Window = sender as RadWindow;
            ICD9Pane pane = icd9Window.Content as ICD9Pane;
            tb.SelectedText = pane.selCode;
        }
        private void iCharge1_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iCharge1;
        }
        private void iModifier1_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iModifier1;
        }
        private void iICD1_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iICD1;
        }
        private void iCharge2_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iCharge2;
        }
        private void iModifier2_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iModifier2;
        }
        private void iICD2_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iICD2;
        }
        private void iCharge3_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iCharge3;
        }
        private void iModifier3_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iModifier3;
        }
        private void iICD3_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iICD3;
        }
        private void iCharge4_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iCharge4;
        }
        private void iModifier4_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iModifier4;
        }
        private void iICD4_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iICD4;
        }
        private void iCharge5_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iCharge5;
        }
        private void iModifier5_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iModifier5;
        }
        private void iICD5_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iICD5;
        }
        private void iCharge6_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iCharge6;
        }
        private void iModifier6_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iModifier6;
        }
        private void iICD6_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iICD6;
        }
        private void iCharge7_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iCharge7;
        }
        private void iModifier7_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iModifier7;
        }
        private void iICD7_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iICD7;
        }
        private void iCharge8_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iCharge8;
        }
        private void iModifier8_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iModifier8;
        }
        private void iICD8_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iICD8;
        }
        private void iCharge9_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iCharge9;
        }
        private void iModifier9_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iModifier9;
        }
        private void iICD9_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iICD9;
        }
        private void iCharge10_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iCharge10;
        }
        private void iModifier10_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iModifier10;
        }
        private void iICD10_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iICD10;
        }
        private void iCharge11_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iCharge11;
        }
        private void iModifier11_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iModifier11;
        }
        private void iICD11_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iICD11;
        }
        private void iCharge12_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iCharge12;
        }
        private void iModifier12_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iModifier12;
        }
        private void iICD12_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iICD12;
        }
        private void iCharge13_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iCharge13;
        }
        private void iModifier13_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iModifier13;
        }
        private void iICD13_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iICD13;
        }
        private void iCharge14_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iCharge14;
        }
        private void iModifier14_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iModifier14;
        }
        private void iICD14_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iICD14;
        }
        private void iCharge15_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iCharge15;
        }
        private void iModifier15_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iModifier15;
        }
        private void iICD15_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iICD15;
        }
        private void iPrice1_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iPrice1;
        }
        private void iPrice2_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iPrice2;
        }
        private void iPrice3_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iPrice3;
        }
        private void iPrice4_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iPrice4;
        }
        private void iPrice5_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iPrice5;
        }
        private void iPrice6_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iPrice6;
        }
        private void iPrice7_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iPrice7;
        }
        private void iPrice8_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iPrice8;
        }
        private void iPrice9_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iPrice9;
        }
        private void iPrice10_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iPrice10;
        }
        private void iPrice11_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iPrice11;
        }
        private void iPrice12_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iPrice12;
        }
        private void iPrice13_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iPrice13;
        }
        private void iPrice14_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iPrice14;
        }
        private void iPrice15_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iPrice15;
        }
        private void iDosage1_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iDosage1;
        }
        private void iDosage2_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iDosage2;
        }
        private void iDosage3_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iDosage3;
        }
        private void iDosage4_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iDosage4;
        }
        private void iDosage5_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iDosage5;
        }
        private void iDosage6_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iDosage6;
        }
        private void iDosage7_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iDosage7;
        }
        private void iDosage8_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iDosage8;
        }
        private void iDosage9_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iDosage9;
        }
        private void iDosage10_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iDosage10;
        }
        private void iDosage11_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iDosage11;
        }
        private void iDosage12_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iDosage12;
        }
        private void iDosage13_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iDosage13;
        }
        private void iDosage14_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iDosage14;
        }
        private void iDosage15_GotFocus(object sender, RoutedEventArgs e)
        {
            tb = iDosage15;
        }
        private void iRad_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (iRad.SelectedIndex != -1)
            {
                client.SetRadAsync(System.Convert.ToInt32(currentDetail.ID), System.Convert.ToInt32(iRad.SelectedValue));
            }
        }
        // Event fired when results arrive
        void client_SetRadCompleted(object sender, SetRadCompletedEventArgs e)
        {
            if (e.Result == 0)
            {
                MessageBox.Show("You must select an ADI radiologist!");
                iRad.SelectedIndex = -1;
                iRad.Focus();
            }
        }
        private void iICD1_LostFocus(object sender, RoutedEventArgs e)
        {
            if (iCharge2.Text.Length > 0 && iICD2.Text.Length == 0)
                iICD2.Text = iICD1.Text;
            if (iCharge3.Text.Length > 0 && iICD3.Text.Length == 0)
                iICD3.Text = iICD1.Text;
            if (iCharge4.Text.Length > 0 && iICD4.Text.Length == 0)
                iICD4.Text = iICD1.Text;
            if (iCharge5.Text.Length > 0 && iICD5.Text.Length == 0)
                iICD5.Text = iICD1.Text;
            if (iCharge6.Text.Length > 0 && iICD6.Text.Length == 0)
                iICD6.Text = iICD1.Text;
            if (iCharge7.Text.Length > 0 && iICD7.Text.Length == 0)
                iICD7.Text = iICD1.Text;
            if (iCharge8.Text.Length > 0 && iICD8.Text.Length == 0)
                iICD8.Text = iICD1.Text;
            if (iCharge9.Text.Length > 0 && iICD9.Text.Length == 0)
                iICD9.Text = iICD1.Text;
            if (iCharge10.Text.Length > 0 && iICD10.Text.Length == 0)
                iICD10.Text = iICD1.Text;
            if (iCharge11.Text.Length > 0 && iICD11.Text.Length == 0)
                iICD11.Text = iICD1.Text;
            if (iCharge12.Text.Length > 0 && iICD12.Text.Length == 0)
                iICD12.Text = iICD1.Text;
            if (iCharge13.Text.Length > 0 && iICD13.Text.Length == 0)
                iICD13.Text = iICD1.Text;
            if (iCharge14.Text.Length > 0 && iICD14.Text.Length == 0)
                iICD14.Text = iICD1.Text;
            if (iCharge15.Text.Length > 0 && iICD15.Text.Length == 0)
                iICD15.Text = iICD1.Text;
        }
    }
}

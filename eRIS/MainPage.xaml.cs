using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ServiceModel;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.TreeView;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Controls.Charting;
using Telerik.Windows.Data;
using System.Text.RegularExpressions;
using eRIS.eRISServiceReference;

namespace eRIS
{
    public partial class MainPage : UserControl
    {
        public sLoginResult currentLogin;
        public ObservableCollection<sGetUsersResult> allUsers;
        public string LastSQL;
        public string CodingLocation;
        public MainPage()
        {
            InitializeComponent();
            // Set focus to first field
            Dispatcher.BeginInvoke(() => this.userName.Focus());
            // Request all users to cache
            eRISServiceClient client = new eRISServiceClient();
            client.GetUsersCompleted += new EventHandler<GetUsersCompletedEventArgs>(client_GetUsersCompleted);
            client.GetUsersAsync();
        }
        // Event fired when load is complete
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Set focus to eRIS
            System.Windows.Browser.HtmlPage.Plugin.Focus();
        }
        // Event fired when login results are returned
        void client_LoginCompleted(object sender, LoginCompletedEventArgs e)
        {
            if (e.Result.Count == 0)
            {
                RadWindow.Alert(new DialogParameters { Content = "The User Name or Password is incorrect.  Please try again." });
                // Log status
                appendLog(this.GetType().ToString(), "Login failed as " + userName.Text); 
                // Set focus
                userName.Focus();
            }
            else
            {
                // Process results
                currentLogin = e.Result.First();
                // Close the login panel
                Login.IsHidden = true;
                // Disable menu entries we don't have permission to see
                UpdateMenus(this.Menu.ItemsSource as Menus);
                // Unhide Menu
                Menu.Visibility = System.Windows.Visibility.Visible;
                // Load initial panels
                if (currentLogin.isAdmin == true)
                {
                }
                if (currentLogin.isClerk == true)
                {
                }
                if (currentLogin.isCoder == true)
                {
                    // Add PhyCoderWorklistPane
                    openPanel("Coder Worklist", "PhyCoderWorklistPane");
                }
                if (currentLogin.isManager == true)
                {
                    // Add CurrentWorkflowPage
                    openPanel("Current Workflow", "CurrentWorkflowPane");
                }
                if (currentLogin.isPatient == true)
                {
                }
                if (currentLogin.isPhysician == true)
                {
                }
                if (currentLogin.isRadiologist == true)
                {
                    // Add StudiesReadPerDayPane
                    openPanel("Studies Read Per Day", "StudiesReadPerDayPane");
                }
                if (currentLogin.isRISAdmin == true)
                {
                }
                if (currentLogin.isTech == true)
                {
                }
                if (currentLogin.isTelerad == true)
                {
                }
                // Log status
                appendLog(this.GetType().ToString(), "Login as " + userName.Text);
            }
        }
        // Event fired when combobox values arrive
        void client_GetUsersCompleted(object sender, GetUsersCompletedEventArgs e)
        {
            // Load result
            allUsers = e.Result;
            // Log status
            appendLog(this.GetType().ToString(), "Rcvd Users");
        }

        private void UpdateMenus(Collection<Menu> _menus)
        {
            foreach(Menu _menu in _menus)
            {
                if (checkRole(_menu.Role) == false)
                        _menu.Visibility = System.Windows.Visibility.Collapsed;
                if (_menu.SubMenu != null)
                    UpdateMenus(_menu.SubMenu);
            }
        }
        // Check permission on current object
        private bool checkRole(String[] roles)
        {
            foreach (string role in roles)
            {
                switch (role)
                {
                    case "Admin":
                        if (currentLogin.isAdmin == true)
                        {
                            return true;
                        }
                        break;
                    case "RISAdmin":
                        if (currentLogin.isRISAdmin == true)
                        {
                            return true;
                        }
                        break;
                    case "Manager":
                        if (currentLogin.isManager == true)
                        {
                            return true;
                        }
                        break;
                    case "Radiologist":
                        if (currentLogin.isRadiologist == true)
                        {
                            return true;
                        }
                        break;
                    case "Tech":
                        if (currentLogin.isTech == true)
                        {
                            return true;
                        }
                        break;
                    case "Clerk":
                        if (currentLogin.isClerk == true)
                        {
                            return true;
                        }
                        break;
                    case "Telerad":
                        if (currentLogin.isTelerad == true)
                        {
                            return true;
                        }
                        break;
                    case "Physician":
                        if (currentLogin.isPhysician == true)
                        {
                            return true;
                        }
                        break;
                    case "Patient":
                        if (currentLogin.isPatient == true)
                        {
                            return true;
                        }
                        break;
                    case "Coder":
                        if (currentLogin.isCoder == true)
                        {
                            return true;
                        }
                        break;
                    case "Everyone":
                        return true;
                }
            }
            return false;
        }
        // Open new document panel
        public void openPanel(string pTitle, string pName)
        {
            // Check for pane already open and set focus if open already
            RadPaneGroup pg = WorkspacePanes;
            foreach (RadPane pane in pg.EnumeratePanes())
            {
                if (pane.Name == "_" + pName)
                {
                    pane.IsSelected = true;
                    return;
                }
            }
            switch (pName)
            {
                case "AvgRegistrationPane":
                    RadDocumentPane AvgRegistrationPane = new AvgRegistrationPane();
                    WorkspacePanes.Items.Add(AvgRegistrationPane);
                    break;
                case "BioImagingPane":
                    RadDocumentPane BioImagingPane = new BioImagingPane();
                    WorkspacePanes.Items.Add(BioImagingPane);
                    break;
                case "CompletedPerTechPane":
                    RadDocumentPane CompletedPerTechPane = new CompletedPerTechPane();
                    WorkspacePanes.Items.Add(CompletedPerTechPane);
                    break;
                case "CurrentWorkflowPane":
                    RadDocumentPane CurrentWorkflowPane = new CurrentWorkflowPane();
                    WorkspacePanes.Items.Add(CurrentWorkflowPane);
                    break;
                case "CurrentWorkflowDetailsPane":
                    RadDocumentPane CurrentWorkflowDetailsPane = new CurrentWorkflowDetailsPane(pTitle);
                    WorkspacePanes.Items.Add(CurrentWorkflowDetailsPane);
                    break;
                case "FirstLastStudyPane":
                    RadDocumentPane FirstLastStudyPane = new FirstLastStudyPane();
                    WorkspacePanes.Items.Add(FirstLastStudyPane);
                    break;
                case "InterpretationTimePane":
                    RadDocumentPane InterpretationTimePane = new InterpretationTimePane();
                    WorkspacePanes.Items.Add(InterpretationTimePane);
                    break;
                case "LoginImagesPane":
                    RadDocumentPane LoginImagesPane = new LoginImagesPane();
                    WorkspacePanes.Items.Add(LoginImagesPane);
                    break;
                case "MessagePane":
                    RadDocumentPane MessagePane = new MessagePane();
                    WorkspacePanes.Items.Add(MessagePane);
                    break;
                case "OrderingPhysicianXrefPane":
                    RadDocumentPane OrderingPhysicianXrefPane = new OrderingPhysicianXrefPane();
                    WorkspacePanes.Items.Add(OrderingPhysicianXrefPane);
                    break;
                case "PatientLongWaitPane":
                    RadDocumentPane PatientLongWaitPane = new PatientLongWaitPane();
                    WorkspacePanes.Items.Add(PatientLongWaitPane);
                    break;
                case "PatientShortWaitPane":
                    RadDocumentPane PatientShortWaitPane = new PatientShortWaitPane();
                    WorkspacePanes.Items.Add(PatientShortWaitPane);
                    break;
                case "PatientWaitPane":
                    RadDocumentPane PatientWaitPane = new PatientWaitPane();
                    WorkspacePanes.Items.Add(PatientWaitPane);
                    break;
                case "PatientXrefPane":
                    RadDocumentPane PatientXrefPane = new PatientXrefPane();
                    WorkspacePanes.Items.Add(PatientXrefPane);
                    break;
                case "PermissionsPane":
                    RadDocumentPane PermissionsPane = new PermissionsPane();
                    PermissionsPane.Header = "Permissions";
                    WorkspacePanes.Items.Add(PermissionsPane);
                    break;
                case "PhyCoderCodingPane":
                    RadDocumentPane PhyCoderCodingPane = new PhyCoderCodingPane(pTitle);
                    WorkspacePanes.Items.Add(PhyCoderCodingPane);
                    break;
                case "PhyCoderOrdersPane":
                    RadDocumentPane PhyCoderOrdersPane = new PhyCoderOrdersPane(pTitle);
                    WorkspacePanes.Items.Add(PhyCoderOrdersPane);
                    break;
                case "PhyCoderWorklistPane":
                    RadDocumentPane PhyCoderWorklistPane = new PhyCoderWorklistPane();
                    PhyCoderWorklistPane.Header = "Coder Worklist";
                    WorkspacePanes.Items.Add(PhyCoderWorklistPane);
                    break;
                case "ProcedureXrefPane":
                    RadDocumentPane ProcedureXrefPane = new ProcedureXrefPane();
                    WorkspacePanes.Items.Add(ProcedureXrefPane);
                    break;
                case "ProviderXrefPane":
                    RadDocumentPane ProviderXrefPane = new ProviderXrefPane();
                    WorkspacePanes.Items.Add(ProviderXrefPane);
                    break;
                case "RadiologistXrefPane":
                    RadDocumentPane RadiologistXrefPane = new RadiologistXrefPane();
                    WorkspacePanes.Items.Add(RadiologistXrefPane);
                    break;
                case "RegisteredPerClerkPane":
                    RadDocumentPane RegisteredPerClerkPane = new RegisteredPerClerkPane();
                    WorkspacePanes.Items.Add(RegisteredPerClerkPane);
                    break;
                case "ReportsCodedPerDayPane":
                    RadDocumentPane ReportsCodedPerDayPane = new ReportsCodedPerDayPane();
                    WorkspacePanes.Items.Add(ReportsCodedPerDayPane);
                    break;
                case "SaveScreenLayout":
                    RadWindow.Alert(new DialogParameters { Content = "The screen layout was saved." });
                    break;
                case "ScheduledPatientsPane":
                    RadDocumentPane ScheduledPatientsPane = new ScheduledPatientsPane();
                    WorkspacePanes.Items.Add(ScheduledPatientsPane);
                    break;
                case "StudiesReadPerDayPane":
                    RadDocumentPane StudiesReadPerDayPane = new StudiesReadPerDayPane();
                    WorkspacePanes.Items.Add(StudiesReadPerDayPane);
                    break;
                case "ToggleLogVisibility":
                    if (LogPane.IsHidden == true)
                    {
                        LogPane.IsHidden = false;
                    }
                    else
                    {
                        LogPane.IsHidden = true;
                    }
                    break;
                case "TurnaroundTimePane":
                    RadDocumentPane TurnaroundTimePane = new TurnaroundTimePane();
                    WorkspacePanes.Items.Add(TurnaroundTimePane);
                    break;
                case "UserActivityPane":
                    RadDocumentPane UserActivityPane = new UserActivityPane();
                    UserActivityPane.Header = "User Activity";
                    WorkspacePanes.Items.Add(UserActivityPane);
                    break;
                default:
                    RadDocumentPane pane = new RadDocumentPane();
                    pane.Content = "Unknown pane goes here for ... " + pName;
                    pane.Header = pTitle;
                    WorkspacePanes.Items.Add(pane);
                    break;
            }
            // Log status
            appendLog(this.GetType().ToString(), "Opened " + pName);
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            loginRequested();
        }
        private void loginRequested()
        {
            // Request login validation
            eRISServiceClient client = new eRISServiceClient();
            client.LoginCompleted += new EventHandler<LoginCompletedEventArgs>(client_LoginCompleted);
            client.LoginAsync(userName.Text, password.Password, App.Current.Resources["localIP"].ToString());
            // Log status
            appendLog(this.GetType().ToString(), "Req Login");
        }
        // Append the log entry
        public void appendLog(string logPage, string logEntry)
        {
            // Scroll to bottom if visible
            if (LogPane.Visibility == System.Windows.Visibility.Visible) 
            {
                //Format(txtLog, "<b>" + DateTime.Now.ToString("HH:mm:ss.ff") + " " + logPage.Substring(5) + "</b><br />" + logEntry);
                txtLog.Text += DateTime.Now.ToString("HH:mm:ss.ff") + " " + logPage.Substring(5) + "\n" + logEntry + "\n";
                txtSV.UpdateLayout();
                txtSV.ScrollToVerticalOffset(double.MaxValue);
            }
        }
        //public static void Format(TextBlock source, string value)
        //{
        //    List<Run> afterBreaks = new List<Run>();
        //    foreach (string sp in Regex.Split(value, "<br />"))
        //    {
        //        Run beforeBreak = new Run();
        //        beforeBreak.Text = sp + '\n';
        //        afterBreaks.Add(beforeBreak);
        //    }
        //    List<Run> afterBold = new List<Run>();
        //    foreach (Run r in afterBreaks)
        //    {
        //        foreach (string sp in Regex.Split(r.Text, "</b>"))
        //        {
        //            string[] tempBold = Regex.Split(sp, "<b>");
        //            Run myRun = new Run();
        //            myRun.Text = tempBold[0];
        //            afterBold.Add(myRun);
        //            if (tempBold.Length > 1)
        //            {
        //                Run myBoldRun = new Run();
        //                myBoldRun.Text = tempBold[1];
        //                myBoldRun.FontWeight = FontWeights.ExtraBold;
        //                afterBold.Add(myBoldRun);
        //            }
        //        }
        //    }
        //    foreach (Run r in afterBold) source.Inlines.Add(r);
        //}
        private void password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                loginRequested();
            }
        }
        // Get the current date
        public DateTime getToday()
        {
            DateTime todayDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
            return todayDate;
        }
        //
        private void RadDocking_Close(object sender, Telerik.Windows.Controls.Docking.StateChangeEventArgs e)
        {
            // Loop through panes to clean up
            foreach (RadPane pane in e.Panes)
            {
                switch (pane.Name.ToString())
                {
                    case "_CurrentWorkflowPane":
                        CurrentWorkflowPane cwp = pane as CurrentWorkflowPane;
                        cwp.closeMe();
                        break;
                    case "_MessagePane":
                        MessagePane mp = pane as MessagePane;
                        mp.closeMe();
                        break;
                    case "_PhyCoderWorklistPane":
                        PhyCoderWorklistPane pcwp = pane as PhyCoderWorklistPane;
                        pcwp.closeMe();
                        break;
                    case "_StudiesReadPerDayPane":
                        StudiesReadPerDayPane srpd = pane as StudiesReadPerDayPane;
                        srpd.closeMe();
                        break;
                    default:
                        // Remove pane
                        pane.RemoveFromParent();
                        break;
                }
                // Log status
                appendLog(this.GetType().ToString(), "Removed " + pane.GetType().ToString());
            }
        }
        private void Menu_ItemClick(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            // Extract the panel info
            RadMenuItem item = e.OriginalSource as RadMenuItem;

            if (item.Name != "")
            {
                openPanel(item.Header.ToString(), item.Name);
            }
        }
    }
}

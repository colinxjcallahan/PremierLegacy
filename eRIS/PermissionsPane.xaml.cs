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
using System.Windows.Navigation;
using System.ComponentModel;
using System.ServiceModel;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using eRIS;
using eRIS.eRISServiceReference;

namespace eRIS
{
    public partial class PermissionsPane : RadDocumentPane
    {
        private MainPage pPage = Application.Current.RootVisual as MainPage;
        public sGetPermissionsResult currentDetail = new sGetPermissionsResult();
        private int SelRadGroup = 0;
        public PermissionsPane()
        {
            InitializeComponent();
            // Disable btnUpdate
            btnUpdate.IsEnabled = false;
            // Retrieve details
            eRISServiceClient client = new eRISServiceClient();
            client.GetPersonnelCompleted += new EventHandler<GetPersonnelCompletedEventArgs>(client_GetPersonnelCompleted);
            client.GetPersonnelAsync();
            client.GetRadGroupCompleted += new EventHandler<GetRadGroupCompletedEventArgs>(client_GetRadGroupCompleted);
            client.GetRadGroupAsync();
        }
        // Event fired when results arrive for RadGroup
        void client_GetRadGroupCompleted(object sender, GetRadGroupCompletedEventArgs e)
        {
            // Load the combo box
            RadGroup.ItemsSource = e.Result;
        }
        // Event fired when result arrives
        void client_GetPersonnelCompleted(object sender, GetPersonnelCompletedEventArgs e)
        {
            // Load grid view with result
            gvDetails.ItemsSource = e.Result;
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Rcvd Users");
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected item from the grid
            sGetPersonnelResult item = gvDetails.SelectedItem as sGetPersonnelResult;
            // Request login validation
            eRISServiceClient client = new eRISServiceClient();
            client.SetPermissionsAsync(item.ID, isAdmin.IsChecked.Value, isRISAdmin.IsChecked.Value, isManager.IsChecked.Value, isRadiologist.IsChecked.Value, isRadiologistADI.IsChecked.Value, isTech.IsChecked.Value, isClerk.IsChecked.Value, isTelerad.IsChecked.Value, isPhysician.IsChecked.Value, isPatient.IsChecked.Value, isCoder.IsChecked.Value, shortName.Text, isCoderAssignable.IsChecked.Value, SelRadGroup, isCoderLimited.IsChecked.Value, isFollowUpEnabled.IsChecked.Value);
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Updated " + item.UserID);
        }
        private void gvDetails_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            // Get the selected item from the grid
            sGetPersonnelResult item = gvDetails.SelectedItem as sGetPersonnelResult;
            // Set the title for the details
            detailTitle.Text = item.Name + " (" + item.UserID + ")";
            // Request the selected item's details
            eRISServiceClient client = new eRISServiceClient();
            client.GetPermissionsCompleted += new EventHandler<GetPermissionsCompletedEventArgs>(client_GetPermissionsCompleted);
            client.GetPermissionsAsync(item.ID);
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Req User " + item.UserID);
        }
        // Event fired when result arrives
        void client_GetPermissionsCompleted(object sender, GetPermissionsCompletedEventArgs e)
        {
            // Load details with result
            currentDetail = e.Result.First();
            isAdmin.IsChecked = currentDetail.isAdmin;
            isRISAdmin.IsChecked = currentDetail.isRISAdmin;
            isManager.IsChecked = currentDetail.isManager;
            isRadiologist.IsChecked = currentDetail.isRadiologist;
            isRadiologistADI.IsChecked = currentDetail.isRadiologistADI;
            isTech.IsChecked = currentDetail.isTech;
            isClerk.IsChecked = currentDetail.isClerk;
            isTelerad.IsChecked = currentDetail.isTelerad;
            isPhysician.IsChecked = currentDetail.isPhysician;
            isPatient.IsChecked = currentDetail.isPatient;
            isCoder.IsChecked = currentDetail.isCoder;
            isCoderAssignable.IsChecked = currentDetail.isCoderAssignable;
            isCoderLimited.IsChecked = currentDetail.isCoderLimited;
            isFollowUpEnabled.IsChecked = currentDetail.isFollowUpEnabled;
            RadGroup.SelectedValue = currentDetail.RadGroup;
            SelRadGroup = currentDetail.RadGroup;
            shortName.Text = currentDetail.ShortName;
            // Disable btnUpdate
            btnUpdate.IsEnabled = false;
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Rcvd User" + currentDetail.ID.ToString());
        }
        // Enable the update button when any field is changed
        private void enableUpdate(object sender, RoutedEventArgs e)
        {
            if (btnUpdate.IsEnabled == false)
            {
                btnUpdate.IsEnabled = true;
            }
        }
        private void RadGroup_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangedEventArgs e)
        {
            SelRadGroup = System.Convert.ToInt32(RadGroup.SelectedValue);
            if (btnUpdate.IsEnabled == false)
            {
                btnUpdate.IsEnabled = true;
            }
        }
    }
}
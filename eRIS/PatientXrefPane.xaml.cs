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
    public partial class PatientXrefPane : RadDocumentPane 
    {
        private MainPage pPage = Application.Current.RootVisual as MainPage;
        private char valueType = 'P';
        public PatientXrefPane()
        {
            InitializeComponent();
            updateFacilityList();
        }
        // Event fired when results arrive
        void client_ListInterfaceXrefCompleted(object sender, ListInterfaceXrefCompletedEventArgs e)
        {
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Rcvd Xref List");
            // Load combo with result
            gvDetails.ItemsSource = e.Result;
        }
        // Event fired when results arrive
        void client_ListInterfaceCompleted(object sender, ListInterfaceCompletedEventArgs e)
        {
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Rcvd Facility List");
            // Load grid view with result
            cbFacility.ItemsSource = e.Result;
        }
        private void updateList()
        {
            sListInterfaceResult sLI = cbFacility.SelectedItem as sListInterfaceResult;
            eRISServiceClient client = new eRISServiceClient();
            client.ListInterfaceXrefCompleted += new EventHandler<ListInterfaceXrefCompletedEventArgs>(client_ListInterfaceXrefCompleted);
            client.ListInterfaceXrefAsync(valueType, sLI.Facility);
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Req Xref List");
        }
        private void updateFacilityList()
        {
            eRISServiceClient client = new eRISServiceClient();
            client.ListInterfaceCompleted += new EventHandler<ListInterfaceCompletedEventArgs>(client_ListInterfaceCompleted);
            client.ListInterfaceAsync();
            // Log status
            pPage.appendLog(this.GetType().ToString(), "Req Facility List");
        }
        private void cbFacility_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangedEventArgs e)
        {
            updateList();
        }
        private void gvDetails_RowEditEnded(object sender, GridViewRowEditEndedEventArgs e)
        {
            // Process cancel if requested
            if (e.EditAction == GridViewEditAction.Cancel)
            {
                // Log status
                pPage.appendLog(this.GetType().ToString(), "Cancel Requested");
                //
                return;
            }
            // Get the current details
            sListInterfaceXrefResult sLIX = e.NewData as sListInterfaceXrefResult;
            sListInterfaceResult sLI = cbFacility.SelectedItem as sListInterfaceResult;
            // Process edit
            if (e.EditOperationType == GridViewEditOperationType.Edit)
            {
                // Log status
                pPage.appendLog(this.GetType().ToString(), "Edited Requested");
                //
                eRISServiceClient client = new eRISServiceClient();
                client.ListInterfaceCompleted += new EventHandler<ListInterfaceCompletedEventArgs>(client_ListInterfaceCompleted);
                client.SetInterfaceXrefAsync(sLIX.id, valueType, sLIX.ExFacility, sLIX.ExValue, sLIX.InValue);
                // Log status
                pPage.appendLog(this.GetType().ToString(), "Edited ["+sLIX.id.ToString()+"]");
                // Update the list
                if (sLIX.InValue == "")
                {
                    updateList();
                }
             }
            // Process insert
            if (e.EditOperationType == GridViewEditOperationType.Insert)
            {
                // Log status
                pPage.appendLog(this.GetType().ToString(), "Insert Requested");
                //
                eRISServiceClient client = new eRISServiceClient();
                client.ListInterfaceCompleted += new EventHandler<ListInterfaceCompletedEventArgs>(client_ListInterfaceCompleted);
                client.SetInterfaceXrefAsync(0, valueType, sLI.Facility, sLIX.ExValue, sLIX.InValue);
                // Log status
                pPage.appendLog(this.GetType().ToString(), "Inserted [" + sLIX.ExValue + "]");
                // Update the list
                updateList();
            }
        }
    }
}

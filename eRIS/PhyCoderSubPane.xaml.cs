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
using System.Windows.Navigation;
using System.ComponentModel;
using System.ServiceModel;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using eRIS;
using eRIS.eRISServiceReference;

namespace eRIS
{
    public partial class PhyCoderSubPane : UserControl
    {
        private MainPage pPage = Application.Current.RootVisual as MainPage;
        public PhyCoderSubPane(sGetReportsResult currentDetail)
        {
            InitializeComponent();
            // Set up combobox
            iAssignTo.ItemsSource = pPage.allUsers;
            //
            switch (currentDetail.Status.ToString())
            {
                case "N":
                    dStatus.Text = "New";
                    break;
                case "P":
                    dStatus.Text = "Pending";
                    break;
                case "B":
                    dStatus.Text = "Billed";
                    break;
                case "C":
                    dStatus.Text = "Coded";
                    break;
                case "D":
                    dStatus.Text = "Deleted";
                    break;
                default:
                    dStatus.Text = "?";
                    break;
            }
            //
            dExam.Text = currentDetail.ExamNumber.ToString();
            dOrder.Text = currentDetail.OrderedDate.ToString();
            dPerform.Text = currentDetail.PerformedDate.ToString();
            dReport.Text = currentDetail.SignedDate.ToString();
            dAssign.Text = currentDetail.AssignedDate.ToString();
            //
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
            iNotes.Text = currentDetail.Notes;
            // Defualt to current user if none selected
            if ((System.Convert.IsDBNull(currentDetail.AssignedTo) == true) || (currentDetail.AssignedTo == 0))
            {
                iAssignTo.SelectedValue = pPage.currentLogin.ID;
            }
            else
            {
                iAssignTo.SelectedValue = System.Convert.ToInt32(currentDetail.AssignedTo);
            }
        }
    }
}

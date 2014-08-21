using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace eRIS.Web
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class eRISService
    {
        [OperationContract]
        public string GetUserName()
        {
            return ServiceSecurityContext.Current.WindowsIdentity.Name;
        }
        [OperationContract]
        public List<fCurrentWorkflowResult> GetCurrentWorkflow()
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            return context.fCurrentWorkflow().ToList();
        }
        [OperationContract]
        public List<fCurrentWorkflowDetailsResult> GetCurrentWorkflowDetails(string chart)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            return context.fCurrentWorkflowDetails(chart).ToList();
        }
        [OperationContract]
        public List<sGetPermissionsResult> GetPermissions(int id)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            return context.sGetPermissions(id).ToList();
        }
        [OperationContract]
        public List<sGetPersonnelResult> GetPersonnel()
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            return context.sGetPersonnel().ToList();
        }
        [OperationContract]
        public List<sGetUsersResult> GetUsers()
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            return context.sGetUsers().ToList();
        }
        [OperationContract]
        public List<sLoginResult> Login(string username, string password, string ipAddress)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            return context.sLogin(username, password, ipAddress).ToList();
        }
        [OperationContract]
        public void SetPermissions(int id, bool isAdmin, bool isRISAdmin, bool isManager, bool isRadiologist, bool isRadiologistADI, bool isTech, bool isClerk, bool isTelerad, bool isPhysician, bool isPatient, bool isCoder, string ShortName, bool isCoderAssignable, int RadGroup, bool isCoderLimited)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            context.sSetPermissions(id, isAdmin, isRISAdmin, isManager, isRadiologist, isRadiologistADI, isTech, isClerk, isTelerad, isPhysician, isPatient, isCoder, ShortName, isCoderAssignable, RadGroup, isCoderLimited);
        }
        [OperationContract]
        public List<sStudiesReadPerDayResult> GetStudiesReadPerDay(DateTime begDate, DateTime endDate, int group)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            return context.sStudiesReadPerDay(begDate, endDate, group).ToList();
        }
        [OperationContract]
        public List<sStudiesReadPerHourResult> GetStudiesReadPerHour(DateTime begDate, DateTime endDate, int rad)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            return context.sStudiesReadPerHour(begDate, endDate, rad).ToList();
        }
        [OperationContract]
        public List<sGetRadGroupResult> GetRadGroup()
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            return context.sGetRadGroup().ToList();
        }
        [OperationContract]
        public void InsertUsersLog(int sourceID, int createdByID, int typeID, string description)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            // Create a temporary row
            UsersLog u = new UsersLog()
            {
                SourceID = sourceID,
                CreatedByID = createdByID,
                TypeID = typeID,
                Description = description
            };
            // Add the row to the collection.
            context.UsersLogs.InsertOnSubmit(u);
            // Submit the chages
            context.SubmitChanges();
        }
        [OperationContract]
        public List<sUsersLogPerDayResult> GetUsersLogPerDay(DateTime begDate, DateTime endDate)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            return context.sUsersLogPerDay(begDate, endDate).ToList();
        }
        [OperationContract]
        public List<sUsersLogBySourceResult> GetUsersLogPerSource(int SourceID)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            return context.sUsersLogBySource(SourceID).ToList();
        }
        [OperationContract]
        public List<sUsersLogByUserResult> GetUsersLogByUser(int userID, DateTime begDate, DateTime endDate)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            return context.sUsersLogByUser(userID, begDate, endDate).ToList();
        }
        [OperationContract]
        public List<sMessagesLogPerDayResult> GetMessagesLogPerDay(DateTime begDate, DateTime endDate)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            return context.sMessagesLogPerDay(begDate, endDate).ToList();
        }
        [OperationContract]
        public void SetLayout(int id, string layout)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            context.sSetLayout(id, layout);
        }
        [OperationContract]
        public List<fTurnaroundTimeResult> GetTurnaroundTime(DateTime begDate, DateTime endDate)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            return context.fTurnaroundTime(begDate, endDate).ToList();
        }
        [OperationContract]
        public List<fInterpretationTimeResult> GetInterpretationTime(DateTime begDate, DateTime endDate)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            return context.fInterpretationTime(begDate, endDate).ToList();
        }
        [OperationContract]
        public List<fPatientLongWaitResult> GetPatientLongWait(DateTime begDate, DateTime endDate)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            return context.fPatientLongWait(begDate, endDate).ToList();
        }
        [OperationContract]
        public List<fPatientShortWaitResult> GetPatientShortWait(DateTime begDate, DateTime endDate)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            return context.fPatientShortWait(begDate, endDate).ToList();
        }
        [OperationContract]
        public List<fPatientWaitResult> GetPatientWait(DateTime begDate, DateTime endDate)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            return context.fPatientWait(begDate, endDate).ToList();
        }
        [OperationContract]
        public List<fCompletedPerTechResult> GetCompletedPerTech(DateTime begDate, DateTime endDate)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            return context.fCompletedPerTech(begDate, endDate).ToList();
        }
        [OperationContract]
        public List<fRegisteredPatientsResult> GetRegisteredPatients(DateTime begDate, DateTime endDate)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            return context.fRegisteredPatients(begDate, endDate).ToList();
        }
        [OperationContract]
        public List<fRegistrationTimeResult> GetRegistrationTime(DateTime begDate, DateTime endDate)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            return context.fRegistrationTime(begDate, endDate).ToList();
        }
        [OperationContract]
        public List<fFirstLastAppointmentResult> GetFirstLastAppointment(DateTime begDate, DateTime endDate)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            return context.fFirstLastAppointment(begDate, endDate).ToList();
        }
        [OperationContract]
        public List<fScheduledPatientsResult> GetScheduledPatients(DateTime begDate, DateTime endDate)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            return context.fScheduledPatients(begDate, endDate).ToList();
        }
        [OperationContract]
        public List<sGetOrdersPendingResult> GetOrdersPending(int id)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            return context.sGetOrdersPending(id).ToList();
        }
        [OperationContract]
        public List<sListOrdersPendingResult> ListOrdersPending()
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            return context.sListOrdersPending().ToList();
        }
        [OperationContract]
        public void SetOrdersPending(int id, char status)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            context.sSetOrdersPending(id, status);
        }
        [OperationContract]
        public List<sGetInterfaceXrefResult> GetInterfaceXref(int id)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            return context.sGetInterfaceXref(id).ToList();
        }
        [OperationContract]
        public List<sListInterfaceXrefResult> ListInterfaceXref(char xType, string xFacility)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            return context.sListInterfaceXref(xType, xFacility).ToList();
        }
        [OperationContract]
        public void SetInterfaceXref(int id, char xType, string xFacility, string xSendingValue, string xReceivingValue)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            context.sSetInterfaceXref(id, xType, xFacility, xSendingValue, xReceivingValue);
        }
        [OperationContract]
        public List<sListInterfaceResult> ListInterface()
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            return context.sListInterface().ToList();
        }
        [OperationContract]
        public List<sListPatientsResult> ListPatients()
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            return context.sListPatients().ToList();
        }
        [OperationContract]
        public List<sListProceduresResult> ListProcedures()
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            return context.sListProcedures().ToList();
        }
        [OperationContract]
        public List<sListProvidersResult> ListProviders()
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            return context.sListProviders().ToList();
        }
        [OperationContract]
        public List<sListReferrersResult> ListReferrers()
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            return context.sListReferrers().ToList();
        }
        [OperationContract]
        public List<sMessagesSummaryResult> MessagesSummary(DateTime begDate, DateTime endDate)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            return context.sMessagesSummary(begDate, endDate).ToList();
        }
        [OperationContract]
        public List<sPhyCoderTotalsResult> PhyCoderTotals(int userID)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            return context.sPhyCoderTotals(userID).ToList();
        }
        [OperationContract]
        public List<Report> PhyCoderReports(string SQL)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();

            string nwConn = context.Connection.ConnectionString;
            var ReportList = new List<Report>();
            using (SqlConnection conn = new SqlConnection(nwConn))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                {
                    SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr != null)
                        while (dr.Read())
                        {
                            var R = new Report();
                            R.ID = int.Parse(dr["ID"].ToString());
                            R.LocationCode = Convert.ToString(dr["LocationCode"]);
                            R.ExamNumber = Convert.ToString(dr["ExamNumber"].ToString());
                            R.PatientMRN = Convert.ToString(dr["PatientMRN"]);
                            R.PatientName = Convert.ToString(dr["PatientName"]);
                            R.RadName = Convert.ToString(dr["RadName"]);
                            R.SignedDate = Convert.ToDateTime(dr["SignedDate"]);
                            if (!Convert.IsDBNull(dr["OrderedDate"]))
                                R.OrderedDate = Convert.ToDateTime(dr["OrderedDate"]);
                            if (!Convert.IsDBNull(dr["PerformedDate"]))
                                R.PerformedDate = Convert.ToDateTime(dr["PerformedDate"]);
                            R.Status = Convert.ToChar(dr["Status"]);
                            R.Modality = Convert.ToString(dr["Modality"]);
                            if (!Convert.IsDBNull(dr["ICD1"]))
                                R.ICD1 = Convert.ToString(dr["ICD1"]);
                            if (!Convert.IsDBNull(dr["CodedDate"]))
                                R.CodedDate = Convert.ToDateTime(dr["CodedDate"]);
                            if (!Convert.IsDBNull(dr["ICD2"]))
                                R.ICD2 = Convert.ToString(dr["ICD2"]);
                            if (!Convert.IsDBNull(dr["AssignedDate"]))
                                R.AssignedDate = Convert.ToDateTime(dr["AssignedDate"]);
                            ReportList.Add(R);
                        }
                    return ReportList;
                }
            }
        }
        [OperationContract]
        public List<Order> PhyCoderOrders(string SQL)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();

            string nwConn = context.Connection.ConnectionString;
            var OrderList = new List<Order>();
            using (SqlConnection conn = new SqlConnection(nwConn))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                {
                    SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr != null)
                    {
                        while (dr.Read())
                        {
                            var O = new Order();
                            O.ID = int.Parse(dr["ID"].ToString());
                            O.LocationCode = Convert.ToString(dr["LocationCode"]);
                            O.ExamNumber = Convert.ToString(dr["ExamNumber"]);
                            O.PatientMRN = Convert.ToString(dr["PatientMRN"]);
                            O.PatientName = Convert.ToString(dr["PatientName"]);
                            if (!Convert.IsDBNull(dr["OrderedDate"]))
                                O.OrderedDate = Convert.ToDateTime(dr["OrderedDate"]);
                            if (!Convert.IsDBNull(dr["PerformedDate"]))
                                O.PerformedDate = Convert.ToDateTime(dr["PerformedDate"]);
                            O.Status = Convert.ToChar(dr["Status"]);
                            OrderList.Add(O);
                        }
                    }
                }
            }
            return OrderList;
        }
        [OperationContract]
        public List<sGetDemographicsResult> GetDemographics(string location, string mrn, string visit)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            return context.sGetDemographics(location, mrn, visit).ToList();
        }
        [OperationContract]
        public List<sGetOrdersResult> GetOrders(string location, string patientmrn, string visit)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            return context.sGetOrders(location, patientmrn, visit).ToList();
        }
        [OperationContract]
        public List<sGetReportResult> GetReport(int ID)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            return context.sGetReport(ID).ToList();
        }
        [OperationContract]
        public List<sGetOrderResult> GetOrder(int ID)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            return context.sGetOrder(ID).ToList();
        }
        [OperationContract]
        public List<sGetReportsResult> GetReports(string location, string patientmrn, string examnumber, string visit)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            return context.sGetReports(location, patientmrn, examnumber, visit).ToList();
        }
        [OperationContract]
        public List<sDuplicateReportsResult> GetDuplicateReports(int id)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            return context.sDuplicateReports(id).ToList();
        }
        [OperationContract]
        public void SetReport(int id, char status, string charge1, string charge2, string charge3, string charge4, string charge5, string charge6, string charge7, string charge8, string charge9, string charge10, string charge11, string charge12, string charge13, string charge14, string charge15, string modifier1, string modifier2, string modifier3, string modifier4, string modifier5, string modifier6, string modifier7, string modifier8, string modifier9, string modifier10, string modifier11, string modifier12, string modifier13, string modifier14, string modifier15, int codedby, string notes, string icd1, string icd2, string icd3, string icd4, string icd5, string icd6, string icd7, string icd8, string icd9, string icd10, string icd11, string icd12, string icd13, string icd14, string icd15, int unit1, int unit2, int unit3, int unit4, int unit5, int unit6, int unit7, int unit8, int unit9, int unit10, int unit11, int unit12, int unit13, int unit14, int unit15, int assignedto, decimal price1, decimal price2, decimal price3, decimal price4, decimal price5, decimal price6, decimal price7, decimal price8, decimal price9, decimal price10, decimal price11, decimal price12, decimal price13, decimal price14, decimal price15, string dosage1, string dosage2, string dosage3, string dosage4, string dosage5, string dosage6, string dosage7, string dosage8, string dosage9, string dosage10, string dosage11, string dosage12, string dosage13, string dosage14, string dosage15)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            context.sSetReport(id, status, charge1, charge2, charge3, charge4, charge5, charge6, charge7, charge8, charge9, charge10, charge11, charge12, charge13, charge14, charge15, modifier1, modifier2, modifier3, modifier4, modifier5, modifier6, modifier7, modifier8, modifier9, modifier10, modifier11, modifier12, modifier13, modifier14, modifier15, codedby, notes, icd1, icd2, icd3, icd4, icd5, icd6, icd7, icd8, icd9, icd10, icd11, icd12, icd13, icd14, icd15, unit1, unit2, unit3, unit4, unit5, unit6, unit7, unit8, unit9, unit10, unit11, unit12, unit13, unit14, unit15, assignedto, price1, price2, price3, price4, price5, price6, price7, price8, price9, price10, price11, price12, price13, price14, price15, dosage1, dosage2, dosage3, dosage4, dosage5, dosage6, dosage7, dosage8, dosage9, dosage10, dosage11, dosage12, dosage13, dosage14, dosage15);
        }
        [OperationContract]
        public List<sUpdateReportResult> UpdateReport(int id, char status, string charge1, string charge2, string charge3, string charge4, string charge5, string charge6, string charge7, string charge8, string charge9, string charge10, string charge11, string charge12, string charge13, string charge14, string charge15, string modifier1, string modifier2, string modifier3, string modifier4, string modifier5, string modifier6, string modifier7, string modifier8, string modifier9, string modifier10, string modifier11, string modifier12, string modifier13, string modifier14, string modifier15, int codedby, string notes, string icd1, string icd2, string icd3, string icd4, string icd5, string icd6, string icd7, string icd8, string icd9, string icd10, string icd11, string icd12, string icd13, string icd14, string icd15, int unit1, int unit2, int unit3, int unit4, int unit5, int unit6, int unit7, int unit8, int unit9, int unit10, int unit11, int unit12, int unit13, int unit14, int unit15, int assignedto, decimal price1, decimal price2, decimal price3, decimal price4, decimal price5, decimal price6, decimal price7, decimal price8, decimal price9, decimal price10, decimal price11, decimal price12, decimal price13, decimal price14, decimal price15, string dosage1, string dosage2, string dosage3, string dosage4, string dosage5, string dosage6, string dosage7, string dosage8, string dosage9, string dosage10, string dosage11, string dosage12, string dosage13, string dosage14, string dosage15, string codingLocation)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            return context.sUpdateReport(id, status, charge1, charge2, charge3, charge4, charge5, charge6, charge7, charge8, charge9, charge10, charge11, charge12, charge13, charge14, charge15, modifier1, modifier2, modifier3, modifier4, modifier5, modifier6, modifier7, modifier8, modifier9, modifier10, modifier11, modifier12, modifier13, modifier14, modifier15, codedby, notes, icd1, icd2, icd3, icd4, icd5, icd6, icd7, icd8, icd9, icd10, icd11, icd12, icd13, icd14, icd15, unit1, unit2, unit3, unit4, unit5, unit6, unit7, unit8, unit9, unit10, unit11, unit12, unit13, unit14, unit15, assignedto, price1, price2, price3, price4, price5, price6, price7, price8, price9, price10, price11, price12, price13, price14, price15, dosage1, dosage2, dosage3, dosage4, dosage5, dosage6, dosage7, dosage8, dosage9, dosage10, dosage11, dosage12, dosage13, dosage14, dosage15, codingLocation).ToList();
        }
        [OperationContract]
        public List<fSplitHL7GPMSResult> fSplitHL7GPMS(string location, string patientmrn)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            return context.fSplitHL7GPMS(location, patientmrn).ToList();
        }
        [OperationContract]
        public List<sReportsCodedPerDayResult> GetReportsCodedPerDay(DateTime begDate, DateTime endDate)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            return context.sReportsCodedPerDay(begDate, endDate).ToList();
        }
        [OperationContract]
        public void SetPending(int id, int codedby)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            context.sSetPending(id, codedby);
        }
        [OperationContract]
        public void ClearPending(int id)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            context.sClearPending(id);
        }
        [OperationContract]
        public List<sListICD9Result> ListICD9(string code, string description)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            return context.sListICD9(code, description).ToList();
        }
        [OperationContract]
        public void SetMessagesLog(int sourceID, int typeID, string description)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            context.sSetMessagesLog(sourceID, typeID, description);
        }
        [OperationContract]
        public void MarkDeleted(string loc, string exam, int codedby)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            context.sMarkDeleted(loc, exam, codedby);
        }
        [OperationContract]
        public List<sToCodeResult> GetToCode()
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            return context.sToCode().ToList();
        }
        [OperationContract]
        public void SetExamNumber(int id, string exam)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            context.sSetExamNumber(id, exam);
        }
        [OperationContract]
        public int SetRad(int id, int rad)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();

            SqlConnection cn = new SqlConnection(context.Connection.ConnectionString);
            cn.Open();

            SqlCommand cmd = new SqlCommand("SELECT ID FROM Users WHERE isRadiologistADI = 1 AND ID = " + rad.ToString(), cn);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr == null || dr.HasRows != true)
            {
                dr.Close();
                cmd.Dispose();
                cn.Close();
                return 0;
            }

            dr.Close();
            cmd.Dispose();
            cn.Close();

            context.sSetRad(id, rad);
            return 1;
        }
        [OperationContract]
        public List<bAppointment> BioImagingReports(string SQL)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();

            string nwConn = context.Connection.ConnectionString;
            var ReportList = new List<bAppointment>();
            using (SqlConnection conn = new SqlConnection(nwConn))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                {
                    SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr != null)
                        while (dr.Read())
                        {
                            var R = new bAppointment();
                            R.id = int.Parse(dr["id"].ToString());
                            R.SiteCode = Convert.ToString(dr["SiteCode"]);
                            if (!Convert.IsDBNull(dr["PatientID"]))
                                R.PatientID = Convert.ToString(dr["PatientID"]).Trim();
                            if (!Convert.IsDBNull(dr["last"]))
                                R.last = Convert.ToString(dr["last"]).Trim();
                            if (!Convert.IsDBNull(dr["first"]))
                                R.last += ", " + Convert.ToString(dr["first"]).Trim();
                            if (!Convert.IsDBNull(dr["middle"]))
                                R.last += " " + Convert.ToString(dr["middle"]).Trim();
                            if (!Convert.IsDBNull(dr["address1"]))
                                R.address1 = Convert.ToString(dr["address1"]).Trim();
                            if (!Convert.IsDBNull(dr["address2"]))
                                R.address2 = Convert.ToString(dr["address2"]).Trim();
                            if (!Convert.IsDBNull(dr["phone1"]))
                                R.phone1 = Convert.ToString(dr["phone1"]).Trim();
                            if (!Convert.IsDBNull(dr["phone1type"]))
                                R.phone1type = Convert.ToString(dr["phone1type"]).Trim();
                            if (!Convert.IsDBNull(dr["phone2"]))
                                R.phone2 = Convert.ToString(dr["phone2"]).Trim();
                            if (!Convert.IsDBNull(dr["phone2type"]))
                                R.phone2type = Convert.ToString(dr["phone2type"]).Trim();
                            if (!Convert.IsDBNull(dr["city"]))
                                R.city = Convert.ToString(dr["city"]).Trim();
                            if (!Convert.IsDBNull(dr["state"]))
                                R.city += ", " + Convert.ToString(dr["state"]).Trim();
                            if (!Convert.IsDBNull(dr["zip"]))
                                R.city += " " + Convert.ToString(dr["zip"]).Trim();
                            if (!Convert.IsDBNull(dr["birthdate"]))
                                R.birthdate = Convert.ToString(dr["birthdate"]).Trim();
                            if (!Convert.IsDBNull(dr["ssn"]))
                                R.ssn = Convert.ToString(dr["ssn"]).Trim();
                            if (!Convert.IsDBNull(dr["medicalrecordnumber"]))
                                R.medicalrecordnumber = Convert.ToString(dr["medicalrecordnumber"]).Trim();
                            if (!Convert.IsDBNull(dr["apptstart"]))
                                R.apptstart = Convert.ToString(dr["apptstart"]).Trim();
                            if (!Convert.IsDBNull(dr["apptstop"]))
                                R.apptstop = Convert.ToString(dr["apptstop"]).Trim();
                            if (!Convert.IsDBNull(dr["cptcode"]))
                                R.cptcode = Convert.ToString(dr["cptcode"]).Trim();
                            if (!Convert.IsDBNull(dr["description"]))
                                R.description = Convert.ToString(dr["description"]).Trim();
                            if (!Convert.IsDBNull(dr["description2"]))
                                R.description2 = Convert.ToString(dr["description2"]).Trim();
                            if (!Convert.IsDBNull(dr["reportid"]))
                                R.reportid = Convert.ToString(dr["reportid"]).Trim();
                            if (!Convert.IsDBNull(dr["listname"]))
                                R.listname = Convert.ToString(dr["listname"]).Trim();
                            if (!Convert.IsDBNull(dr["jobnumber"]))
                                R.jobnumber = Convert.ToString(dr["jobnumber"]).Trim();
                            if (!Convert.IsDBNull(dr["PerformedDate"]))
                                R.PerformedDate = Convert.ToDateTime(dr["PerformedDate"]);
                            ReportList.Add(R);
                        }
                    return ReportList;
                }
            }
        }
        [OperationContract]
        public List<sCoderTotalsResult> CoderTotals(int userID)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            return context.sCoderTotals(userID).ToList();
        }
        [OperationContract]
        public List<sGetExamCommentsResult> GetExamComments(int Exam)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            return context.sGetExamComments(Exam).ToList();
        }
        [OperationContract]
        public Dictionary<string, string> GetExamDocuments(string PatientMRN, int Exam)
        {
            eRISDataClassesDataContext context = new eRISDataClassesDataContext();
            Dictionary<string, string> fileNames = new Dictionary<string, string>();

            System.IO.DirectoryInfo dI = new System.IO.DirectoryInfo("\\\\NOLWEB-FTP\\FtpRoot\\Scans");
            System.IO.FileInfo[] filesInfo = dI.GetFiles("$$" + PatientMRN.TrimEnd() + "_" + Exam.ToString() + "*.*");

            SqlConnection cn = new SqlConnection(context.Connection.ConnectionString);
            cn.Open();

            try
            {
                foreach (System.IO.FileInfo fi in filesInfo)
                {
                    string[] parts = fi.Name.Split('.');
                    string[] pieces = parts[0].Split('_');
                    SqlCommand cmd = new SqlCommand("SELECT Description FROM VRIS_DOTNET.dbo.ScanChoices WHERE Code = " + pieces[2].ToString(), cn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr != null)
                        while (dr.Read())
                        {
                            fileNames.Add(fi.Name, dr["Description"].ToString());
                        }
                    dr.Close();
                    cmd.Dispose();
                }
            }
            catch (Exception e)
            {
                fileNames.Add("Error", e.Message);
            }

            return fileNames;
        }
        //    SetMessagesLog(1, 9, "Orders retrieved...");
    }
}



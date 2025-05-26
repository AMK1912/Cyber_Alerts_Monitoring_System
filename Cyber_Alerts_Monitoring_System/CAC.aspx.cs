using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Configuration;
// using Oracle.ManagedDataAccess.Client; // This using statement is not needed if you are strictly using OleDbConnection

namespace CyberAlert
{
    public partial class CyberAlertsCentral : Page
    {
        private OleDbConnection conn;

        protected void Page_Load(object sender, EventArgs e)
        {
            conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["constring"].ToString());
            if (!IsPostBack)
            {
                // Check for authentication and get user data from Session
                if (Session["EmpCode"] != null && Session["Center"] != null)
                {
                    string center = Session["Center"].ToString();

                    // Debugging line for Page_Load
                    System.Diagnostics.Debug.WriteLine("CAC.aspx.cs Page_Load - Session[Center]: [" + center + "]");

                    if (center == "Plant")
                    {
                        // Disable Central entry fields for Plant users
                        txtReceivedDateCentral.Enabled = false;
                        txtCentreUnit.Enabled = false;
                        txtSenderDetailsCentral.Enabled = false;
                        txtIncidentDateCentral.Enabled = false;
                        txtEntryDateCentral.Enabled = false;
                        txtEmailDateCentral.Enabled = false;
                        txtPertainingToUnitCentral.Enabled = false;
                        txtAffectedSailIPCentral.Enabled = false;
                        txtAffectedPortCentral.Enabled = false;
                        txtMaliciousIPCentral.Enabled = false;
                        txtAlertDetailsCentral.Enabled = false;
                        txtActionDateCentral.Enabled = false;
                        txtActionDetails.Enabled = false;
                        txtRemarksCentral.Enabled = false;
                        txtRepliedSenderCentral.Enabled = false;
                        txtClosingDateCentral.Enabled = false;
                    }
                    //  CO users will have all fields enabled by default.
                }
                else
                {
                    // Redirect to login if not authenticated or session data is missing
                    Response.Redirect("Login.aspx");
                }
            }
        }

        protected void btnSubmitAlert_Click(object sender, EventArgs e)
        {
            // Debugging line for btnSubmitAlert_Click
            string sessionCenterOnSubmit = (Session["Center"] != null ? Session["Center"].ToString() : "NULL");
            System.Diagnostics.Debug.WriteLine("CAC.aspx.cs btnSubmitAlert_Click - Session[Center]: [" + sessionCenterOnSubmit + "]");

            // First, check the session
            if (Session["Center"] != null && Session["Center"].ToString() == "CO")
            {
                // Retrieve values from the fields
                string alertDateTime = txtAlertDateTime.Text;
                string receivedDate = txtReceivedDateCentral.Text;
                string centreUnit = txtCentreUnit.Text;
                string senderDetails = txtSenderDetailsCentral.Text;
                string incidentDate = txtIncidentDateCentral.Text;
                string entryDate = txtEntryDateCentral.Text;
                string emailDate = txtEmailDateCentral.Text;
                string pertainingToUnit = txtPertainingToUnitCentral.Text;
                string affectedSailIP = txtAffectedSailIPCentral.Text; // Now treated as string
                string affectedPort = txtAffectedPortCentral.Text;     // Now treated as string
                string maliciousIP = txtMaliciousIPCentral.Text;
                string alertDetails = txtAlertDetailsCentral.Text;
                string actionDate = txtActionDateCentral.Text;
                string actionDetails = txtActionDetails.Text;
                string remarks = txtRemarksCentral.Text;
                string repliedSender = txtRepliedSenderCentral.Text;
                string closingDate = txtClosingDateCentral.Text;


                // Validation (Removed numeric check for IP/Port)
                if (string.IsNullOrEmpty(alertDateTime) || string.IsNullOrEmpty(receivedDate)||
                    string.IsNullOrEmpty(senderDetails) || string.IsNullOrEmpty(incidentDate) ||
                    string.IsNullOrEmpty(entryDate) ||string.IsNullOrEmpty(emailDate) ||
                    string.IsNullOrEmpty(pertainingToUnit) || string.IsNullOrEmpty(affectedSailIP) || // No longer numeric check
                    string.IsNullOrEmpty(affectedPort) || string.IsNullOrEmpty(maliciousIP) ||     // No longer numeric check
                    string.IsNullOrEmpty(alertDetails) || string.IsNullOrEmpty(actionDate) ||
                    string.IsNullOrEmpty(actionDetails)|| string.IsNullOrEmpty(remarks) ||
                    string.IsNullOrEmpty(repliedSender)|| string.IsNullOrEmpty(closingDate))
                {
                    lblSubmissionMessage.Text = "Please fill in all required fields.";
                    lblSubmissionMessage.CssClass = "text-danger mt-2";
                    return;
                }

                // Removed int.TryParse for sailIP and port

                DateTime alertDt, receivedDt, incidentDt, entryDt, emailDt;
                DateTime actionDt, repliedSenderDt, closingDt;
                if (!DateTime.TryParse(alertDateTime, out alertDt) || !DateTime.TryParse(receivedDate, out receivedDt) ||
                    !DateTime.TryParse(incidentDate, out incidentDt) || !DateTime.TryParse(entryDt, out entryDt) ||
                    !DateTime.TryParse(emailDate, out emailDt) || !DateTime.TryParse(actionDate, out actionDt) ||
                    !DateTime.TryParse(repliedSender, out repliedSenderDt) || !DateTime.TryParse(closingDate, out closingDt))
                {
                    lblSubmissionMessage.Text = "Please enter valid date and time values.";
                    lblSubmissionMessage.CssClass = "text-danger mt-2";
                    return;
                }

                try
                {
                    using (OleDbConnection currentConn = new OleDbConnection(ConfigurationManager.ConnectionStrings["constring"].ToString()))
                    {
                        currentConn.Open();
                        string procedureName = "CYBER_ALERT_SUBMISSION";
                        OleDbCommand cmd = new OleDbCommand(procedureName, currentConn);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("vRECEIVE_DATE", OleDbType.Date).Value = receivedDt;
                        cmd.Parameters.Add("vCENTER_UNIT", OleDbType.VarChar).Value = centreUnit;
                        cmd.Parameters.Add("vRECEIVED_FROM_SENDER", OleDbType.VarChar).Value = senderDetails;
                        cmd.Parameters.Add("vINCIDENT_DATE", OleDbType.Date).Value = incidentDt;
                        cmd.Parameters.Add("vENTRY_DATE", OleDbType.Date).Value = entryDt;
                        cmd.Parameters.Add("vEMAIL_TO_PLANT_DATE", OleDbType.Date).Value = emailDt;
                        cmd.Parameters.Add("vPERTAINING_TO_UNIT", OleDbType.VarChar).Value = pertainingToUnit;
                        cmd.Parameters.Add("vAFFECTED_SAILIP", OleDbType.VarChar).Value = affectedSailIP; // Changed to VarChar
                        cmd.Parameters.Add("vAFFECTED_PORT", OleDbType.VarChar).Value = affectedPort;     // Changed to VarChar
                        cmd.Parameters.Add("vMALICIOUS_IP", OleDbType.VarChar).Value = maliciousIP;
                        cmd.Parameters.Add("vALERT_DETAILS", OleDbType.VarChar).Value = alertDetails;
                        cmd.Parameters.Add("vFIRST_ACTION_DATE", OleDbType.Date).Value = actionDt;
                        cmd.Parameters.Add("vDETAILS_OF_ACTION", OleDbType.VarChar).Value = actionDetails;
                        cmd.Parameters.Add("vREMARKS", OleDbType.VarChar).Value = remarks;
                        cmd.Parameters.Add("vREPLIED_SENDER_DATE", OleDbType.Date).Value = repliedSenderDt;
                        cmd.Parameters.Add("vCLOSING_DATE", OleDbType.Date).Value = closingDt;
                        cmd.ExecuteNonQuery();

                        lblSubmissionMessage.Text = "Cyber alert submitted successfully!";
                        lblSubmissionMessage.CssClass = "text-success mt-2";
                    }
                }
                catch (OleDbException ex)
                {
                    lblSubmissionMessage.Text = "Error submitting alert: " + ex.Message;
                    lblSubmissionMessage.CssClass = "text-danger mt-2";
                    System.Diagnostics.Trace.WriteLine("DB Error: " + ex.ToString());
                }
            }
            else
            {
                lblSubmissionMessage.Text = "You do not have permission to submit Central alerts.";
                lblSubmissionMessage.CssClass = "text-danger mt-2";
            }
        }
    }
}


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
                // Retrieve values from the fields (all as strings initially)
                string alertDateTimeStr = txtAlertDateTime.Text;
                string receivedDateStr = txtReceivedDateCentral.Text;
                string centreUnit = txtCentreUnit.Text;
                string senderDetails = txtSenderDetailsCentral.Text;
                string incidentDateStr = txtIncidentDateCentral.Text;
                string entryDateStr = txtEntryDateCentral.Text;
                string emailDateStr = txtEmailDateCentral.Text;
                string pertainingToUnit = txtPertainingToUnitCentral.Text;
                string affectedSailIP = txtAffectedSailIPCentral.Text;
                string affectedPort = txtAffectedPortCentral.Text;
                string maliciousIP = txtMaliciousIPCentral.Text;
                string alertDetails = txtAlertDetailsCentral.Text;
                string actionDateStr = txtActionDateCentral.Text; // String from textbox
                string actionDetails = txtActionDetails.Text;
                string remarks = txtRemarksCentral.Text;
                string repliedSenderStr = txtRepliedSenderCentral.Text; // String from textbox
                string closingDateStr = txtClosingDateCentral.Text; // String from textbox


                // Validation for empty fields
                if (string.IsNullOrEmpty(alertDateTimeStr) || string.IsNullOrEmpty(receivedDateStr)||
                    string.IsNullOrEmpty(senderDetails) || string.IsNullOrEmpty(incidentDateStr) ||
                    string.IsNullOrEmpty(entryDateStr) ||string.IsNullOrEmpty(emailDateStr) ||
                    string.IsNullOrEmpty(pertainingToUnit) || string.IsNullOrEmpty(affectedSailIP) ||
                    string.IsNullOrEmpty(affectedPort) || string.IsNullOrEmpty(maliciousIP) ||
                    string.IsNullOrEmpty(alertDetails) || string.IsNullOrEmpty(actionDateStr) ||
                    string.IsNullOrEmpty(actionDetails)|| string.IsNullOrEmpty(remarks) ||
                    string.IsNullOrEmpty(repliedSenderStr)|| string.IsNullOrEmpty(closingDateStr))
                {
                    lblSubmissionMessage.Text = "Please fill in all required fields.";
                    lblSubmissionMessage.CssClass = "text-danger mt-2";
                    return;
                }

                // No longer parsing IP/Port as int, as per your request.
                // int sailIP, port;
                // if (!int.TryParse(affectedSailIP, out sailIP) || !int.TryParse(affectedPort, out port))
                // {
                //     lblSubmissionMessage.Text = "Affected SAIL IP and Affected Port must be numeric values.";
                //     lblSubmissionMessage.CssClass = "text-danger mt-2";
                //     return;
                // }

                // Declare DateTime variables and initialize them to avoid CS0165 if parsing fails
                DateTime alertDt = DateTime.MinValue;
                DateTime receivedDt = DateTime.MinValue;
                DateTime incidentDt = DateTime.MinValue;
                DateTime entryDt = DateTime.MinValue;
                DateTime emailDt = DateTime.MinValue;
                DateTime actionDt = DateTime.MinValue;
                DateTime repliedSenderDt = DateTime.MinValue;
                DateTime closingDt = DateTime.MinValue;

                // Parse all date/time strings to DateTime objects
                if (!DateTime.TryParse(alertDateTimeStr, out alertDt) ||
                    !DateTime.TryParse(receivedDateStr, out receivedDt) ||
                    !DateTime.TryParse(incidentDateStr, out incidentDt) ||
                    !DateTime.TryParse(entryDateStr, out entryDt) ||
                    !DateTime.TryParse(emailDateStr, out emailDt) ||
                    !DateTime.TryParse(actionDateStr, out actionDt) ||
                    !DateTime.TryParse(repliedSenderStr, out repliedSenderDt) ||
                    !DateTime.TryParse(closingDateStr, out closingDt))
                {
                    lblSubmissionMessage.Text = "Please enter valid date and time values for all date fields.";
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

                        // Parameters are now passed as their parsed DateTime objects or original strings
                        cmd.Parameters.Add("vRECEIVE_DATE", OleDbType.Date).Value = receivedDt;
                        cmd.Parameters.Add("vCENTER_UNIT", OleDbType.VarChar).Value = centreUnit;
                        cmd.Parameters.Add("vRECEIVED_FROM_SENDER", OleDbType.VarChar).Value = senderDetails;
                        cmd.Parameters.Add("vINCIDENT_DATE", OleDbType.Date).Value = incidentDt;
                        cmd.Parameters.Add("vENTRY_DATE", OleDbType.Date).Value = entryDt;
                        cmd.Parameters.Add("vEMAIL_TO_PLANT_DATE", OleDbType.Date).Value = emailDt;
                        cmd.Parameters.Add("vPERTAINING_TO_UNIT", OleDbType.VarChar).Value = pertainingToUnit;
                        cmd.Parameters.Add("vAFFECTED_SAILIP", OleDbType.VarChar).Value = affectedSailIP; // Already changed to VarChar
                        cmd.Parameters.Add("vAFFECTED_PORT", OleDbType.VarChar).Value = affectedPort;     // Already changed to VarChar
                        cmd.Parameters.Add("vMALICIOUS_IP", OleDbType.VarChar).Value = maliciousIP;
                        cmd.Parameters.Add("vALERT_DETAILS", OleDbType.VarChar).Value = alertDetails;
                        cmd.Parameters.Add("vFIRST_ACTION_DATE", OleDbType.Date).Value = actionDt; // Use parsed DateTime
                        cmd.Parameters.Add("vDETAILS_OF_ACTION", OleDbType.VarChar).Value = actionDetails;
                        cmd.Parameters.Add("vREMARKS", OleDbType.VarChar).Value = remarks;
                        cmd.Parameters.Add("vREPLIED_SENDER_DATE", OleDbType.Date).Value = repliedSenderDt; // Use parsed DateTime
                        cmd.Parameters.Add("vCLOSING_DATE", OleDbType.Date).Value = closingDt; // Use parsed DateTime
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


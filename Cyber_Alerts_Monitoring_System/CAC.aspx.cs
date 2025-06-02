using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Configuration;

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
            string sessionCenterOnSubmit = (Session["Center"] != null ? Session["Center"].ToString() : "NULL");
            System.Diagnostics.Debug.WriteLine("CAC.aspx.cs btnSubmitAlert_Click - Session[Center]: [" + sessionCenterOnSubmit + "]");

            // Ensure the page is valid based on all validators before proceeding
            if (!Page.IsValid)
            {
                lblSubmissionMessage.Text = "Please correct the highlighted errors.";
                lblSubmissionMessage.CssClass = "text-danger mt-2";
                return;
            }

            if (Session["Center"] != null && Session["Center"].ToString() == "CO")
            {
                // Retrieve values from the fields (all as strings initially)
                string alertDateTimeStr = txtAlertDateTime.Text;
                string receivedDateStr = txtReceivedDateCentral.Text;
                string centreUnitStr = txtCentreUnit.Text; // Keep as string initially
                string senderDetails = txtSenderDetailsCentral.Text;
                string incidentDateStr = txtIncidentDateCentral.Text;
                string entryDateStr = txtEntryDateCentral.Text;
                string emailDateStr = txtEmailDateCentral.Text;
                string pertainingToUnitStr = txtPertainingToUnitCentral.Text; // Keep as string initially
                string affectedSailIP = txtAffectedSailIPCentral.Text; // As string
                string affectedPortStr = txtAffectedPortCentral.Text;     // As string
                string maliciousIP = txtMaliciousIPCentral.Text;
                string alertDetails = txtAlertDetailsCentral.Text;
                string actionDateStr = txtActionDateCentral.Text;
                string actionDetails = txtActionDetails.Text;
                string remarks = txtRemarksCentral.Text;
                string repliedSenderStr = txtRepliedSenderCentral.Text;
                string closingDateStr = txtClosingDateCentral.Text;

                // All date/time strings are now assumed valid due to client-side validators.
                DateTime alertDt = DateTime.Parse(alertDateTimeStr);
                DateTime receivedDt = DateTime.Parse(receivedDateStr);
                DateTime incidentDt = DateTime.Parse(incidentDateStr);
                DateTime entryDt = DateTime.Parse(entryDateStr);
                DateTime emailDt = DateTime.Parse(emailDateStr);
                DateTime actionDt = DateTime.Parse(actionDateStr);
                DateTime repliedSenderDt = DateTime.Parse(repliedSenderStr);
                DateTime closingDt = DateTime.Parse(closingDateStr);

                // --- NEW: Convert string values to int for Integer parameters ---
                int centreUnitInt;
                int pertainingToUnitInt;
                int affectedPortInt; // Renamed to avoid conflict with string variable

                // Use TryParse for robustness, even if client-side validation is present.
                // This handles cases where client-side validation might be bypassed.
                if (!int.TryParse(centreUnitStr, out centreUnitInt))
                {
                    lblSubmissionMessage.Text = "Centre Unit must be a valid number.";
                    lblSubmissionMessage.CssClass = "text-danger mt-2";
                    return;
                }
                if (!int.TryParse(pertainingToUnitStr, out pertainingToUnitInt))
                {
                    lblSubmissionMessage.Text = "Pertaining To Unit must be a valid number.";
                    lblSubmissionMessage.CssClass = "text-danger mt-2";
                    return;
                }
                if (!int.TryParse(affectedPortStr, out affectedPortInt))
                {
                    lblSubmissionMessage.Text = "Affected Port must be a valid number.";
                    lblSubmissionMessage.CssClass = "text-danger mt-2";
                    return;
                }
                // --- END NEW CONVERSIONS ---


                try
                {
                    using (OleDbConnection currentConn = new OleDbConnection(ConfigurationManager.ConnectionStrings["constring"].ToString()))
                    {
                        currentConn.Open();
                        string procedureName = "CYBER_ALERT_SUBMISSION";
                        OleDbCommand cmd = new OleDbCommand(procedureName, currentConn);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("vRECEIVE_DATE", OleDbType.Date).Value = receivedDt;
                        cmd.Parameters.Add("vCENTER_UNIT", OleDbType.Integer).Value = centreUnitInt; // Use the parsed int
                        cmd.Parameters.Add("vRECEIVED_FROM_SENDER", OleDbType.VarChar).Value = senderDetails;
                        cmd.Parameters.Add("vINCIDENT_DATE", OleDbType.Date).Value = incidentDt;
                        cmd.Parameters.Add("vENTRY_DATE", OleDbType.Date).Value = entryDt;
                        cmd.Parameters.Add("vEMAIL_TO_PLANT_DATE", OleDbType.Date).Value = emailDt;
                        cmd.Parameters.Add("vPERTAINING_TO_UNIT", OleDbType.Integer).Value = pertainingToUnitInt; // Use the parsed int
                        cmd.Parameters.Add("vAFFECTED_SAILIP", OleDbType.VarChar).Value = affectedSailIP;
                        cmd.Parameters.Add("vAFFECTED_PORT", OleDbType.Integer).Value = affectedPortInt; // Use the parsed int
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


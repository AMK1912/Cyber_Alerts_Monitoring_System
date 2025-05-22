using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;  //  Use Oracle's Managed Data Access

namespace CyberAlert
{
    public partial class CyberAlertsCentral : Page
    {
        private OleDbConnection conn; // Use OracleConnection

        protected void Page_Load(object sender, EventArgs e)
        {
            conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["constring"].ToString()); //  Use the correct connection string name
            if (!IsPostBack)
            {
                // Check for authentication and get user data from Session
                if (Session["EmpCode"] != null && Session["Center"] != null)
                {
                    string center = Session["Center"].ToString();

                    if (center == "Plant")
                    {
                        // Disable Central entry fields for Plant users
                        //txtAlertTitle.Enabled = false;
                        //txtDescription.Enabled = false;
                        //ddlSeverity.Enabled = false;
                        //txtAffectedSystems.Enabled = false;
                        //txtReceivedDateCentral.Enabled = false;
                        //txtSenderDetailsCentral.Enabled = false;
                        //txtIncidentDateCentral.Enabled = false;
                        //txtEntryDateCentral.Enabled = false;
                        //txtEmailDateCentral.Enabled = false;
                        //txtPertainingToUnitCentral.Enabled = false;
                        //txtAffectedSailIPCentral.Enabled = false;
                        //txtAffectedPortCentral.Enabled = false;
                        //txtMaliciousIPCentral.Enabled = false;
                        //txtAlertDetailsCentral.Enabled = false;
                        //txtAlertDateTime.Enabled = false;
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
                string affectedSailIP = txtAffectedSailIPCentral.Text;
                string affectedPort = txtAffectedPortCentral.Text;
                string maliciousIP = txtMaliciousIPCentral.Text;
                string alertDetails = txtAlertDetailsCentral.Text;
                string actionDate = txtActionDateCentral.Text;
                string actionDetails = txtActionDetails.Text;
                string remarks = txtRemarksCentral.Text;
                string repliedSender = txtRepliedSenderCentral.Text;
                string closingDate = txtClosingDateCentral.Text;


                // Validation
                if (string.IsNullOrEmpty(alertDateTime) || string.IsNullOrEmpty(receivedDate)||
                    string.IsNullOrEmpty(senderDetails) || string.IsNullOrEmpty(incidentDate) ||
                    string.IsNullOrEmpty(entryDate) ||string.IsNullOrEmpty(emailDate) || 
                    string.IsNullOrEmpty(pertainingToUnit) || string.IsNullOrEmpty(affectedSailIP) ||
                    string.IsNullOrEmpty(affectedPort) || string.IsNullOrEmpty(maliciousIP) ||
                    string.IsNullOrEmpty(alertDetails) || string.IsNullOrEmpty(actionDate) ||
                    string.IsNullOrEmpty(actionDetails)|| string.IsNullOrEmpty(remarks) ||
                    string.IsNullOrEmpty(repliedSender)|| string.IsNullOrEmpty(closingDate)|| 
                    string.IsNullOrEmpty(alertDetails))
                {
                    lblSubmissionMessage.Text = "Please fill in all required fields.";
                    lblSubmissionMessage.CssClass = "text-danger mt-2";
                    return;
                }

                int sailIP, port;
                if (!int.TryParse(affectedSailIP, out sailIP) || !int.TryParse(affectedPort, out port))
                {
                    lblSubmissionMessage.Text = "Affected SAIL IP and Affected Port must be numeric values.";
                    lblSubmissionMessage.CssClass = "text-danger mt-2";
                    return;
                }

                DateTime alertDt, receivedDt, incidentDt, entryDt, emailDt;
                if (!DateTime.TryParse(alertDateTime, out alertDt) || !DateTime.TryParse(receivedDate, out receivedDt) ||
                    !DateTime.TryParse(incidentDate, out incidentDt) || !DateTime.TryParse(entryDate, out entryDt) ||
                    !DateTime.TryParse(emailDate, out emailDt))
                {
                    lblSubmissionMessage.Text = "Please enter valid date and time values.";
                    lblSubmissionMessage.CssClass = "text-danger mt-2";
                    return;
                }

                try
                {
                    conn.Open();
                    //  Call the stored procedure.  Make sure the procedure name is correct.
                    string procedureName = "CYBER_ALERT_SUBMISSION";  //  Replace with your actual procedure name
                    OleDbCommand cmd = new OleDbCommand(procedureName, conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //  Add the parameters.  The order and data types must match your procedure definition.
                    cmd.Parameters.Add("vRECEIVE_DATE", OleDbType.Date).Value = receivedDt;
                    cmd.Parameters.Add("vCENTER_UNIT", OleDbType.VarChar).Value = centreUnit;
                    cmd.Parameters.Add("vRECEIVED_FROM_SENDER", OleDbType.VarChar).Value = senderDetails;
                    cmd.Parameters.Add("vINCIDENT_DATE", OleDbType.Date).Value = incidentDt;
                    cmd.Parameters.Add("vENTRY_DATE", OleDbType.Date).Value = entryDt;
                    cmd.Parameters.Add("vEMAIL_TO_PLANT_DATE", OleDbType.Date).Value = emailDt;
                    cmd.Parameters.Add("vPERTAINING_TO_UNIT", OleDbType.VarChar).Value = pertainingToUnit;
                    cmd.Parameters.Add("vAFFECTED_SAILIP", OleDbType.Integer).Value = sailIP;
                    cmd.Parameters.Add("vAFFECTED_PORT", OleDbType.Integer).Value = port;
                    cmd.Parameters.Add("vMALICIOUS_IP", OleDbType.VarChar).Value = maliciousIP;
                    cmd.Parameters.Add("vALERT_DETAILS", OleDbType.VarChar).Value = alertDetails;
                    cmd.Parameters.Add("vFIRST_ACTION_DATE", OleDbType.Date).Value = actionDate;
                    cmd.Parameters.Add("vDETAILS_OF_ACTION", OleDbType.VarChar).Value = actionDetails;
                    cmd.Parameters.Add("vREMARKS", OleDbType.VarChar).Value = remarks;
                    cmd.Parameters.Add("vREPLIED_SENDER_DATE", OleDbType.Date).Value = repliedSender;
                    cmd.Parameters.Add("vCLOSING_DATE", OleDbType.Date).Value = closingDate;
                    cmd.ExecuteNonQuery();

                    lblSubmissionMessage.Text = "Cyber alert submitted successfully!";
                    lblSubmissionMessage.CssClass = "text-success mt-2";
                }
                catch (OleDbException ex)
                {
                    lblSubmissionMessage.Text = "Error submitting alert: " + ex.Message;
                    lblSubmissionMessage.CssClass = "text-danger mt-2";
                    //  Log the error:  System.Diagnostics.Trace.WriteLine("DB Error: " + ex.ToString());
                }
                finally
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        conn.Close();
                    }
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

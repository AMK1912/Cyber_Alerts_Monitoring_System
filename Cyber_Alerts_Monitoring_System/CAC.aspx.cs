using System;
using System.Web.UI;
using System.Web.UI.WebControls; // Make sure this is included for DropDownList
using System.Data.OleDb;
using System.Configuration; // For ConfigurationManager
using System.Data; // For DataTable

namespace CyberAlert
{
    public partial class CyberAlertsCentral : Page
    {
        // No need for a global OleDbConnection 'conn' if you're using 'using' blocks
        // within methods, which is the recommended practice for connection management.

        protected void Page_Load(object sender, EventArgs e)
        {
            // The connection string is retrieved within the methods where it's used,
            // ensuring it's always fresh and correctly disposed.

            if (!IsPostBack)
            {
                // Check for authentication and get user data from Session
                if (Session["EmpCode"] != null && Session["Center"] != null)
                {
                    string center = Session["Center"].ToString();
                    System.Diagnostics.Debug.WriteLine("CAC.aspx.cs Page_Load - Session[Center]: [" + center + "]");

                    // Populate the dropdown list for all users first
                    PopulatePertainingToUnitDropdown(); // Call this before potentially disabling it

                    if (center == "Plant")
                    {
                        // Disable Central entry fields for Plant users
                        txtReceivedDateCentral.Enabled = false;
                        txtCentreUnit.Enabled = false;
                        txtSenderDetailsCentral.Enabled = false;
                        txtIncidentDateCentral.Enabled = false;
                        txtEntryDateCentral.Enabled = false;
                        txtEmailDateCentral.Enabled = false;
                        // txtPertainingToUnitCentral.Enabled = false; // REMOVE THIS LINE (it's a DropDownList now)
                        ddlPertainingToUnit.Enabled = false; // Disable the new DropDownList
                        txtAffectedSailIPCentral.Enabled = false;
                        txtAffectedPortCentral.Enabled = false;
                        txtMaliciousIPCentral.Enabled = false;
                        txtAlertDetailsCentral.Enabled = false;
                        txtActionDateCentral.Enabled = false;
                        txtActionDetails.Enabled = false;
                        txtRemarksCentral.Enabled = false;
                        txtRepliedSenderCentral.Enabled = false;
                        txtClosingDateCentral.Enabled = false;
                        btnSubmitAlert.Enabled = false; // Plant users probably can't submit central alerts
                    }
                }
                else
                {
                    // Redirect to login if not authenticated or session data is missing
                    Response.Redirect("Login.aspx");
                }
            }
        }

        // --- New Method to Populate the DropDownList ---
        private void PopulatePertainingToUnitDropdown()
        {
            ddlPertainingToUnit.Items.Clear(); // Always clear existing items
            // Add a default "select" item. The empty value makes the RequiredFieldValidator work.
            ddlPertainingToUnit.Items.Add(new ListItem("-- Select Unit --", ""));

            DataTable dtUnits = new DataTable();
            // IMPORTANT: Adjust the table and column name if different.
            // Use double quotes for column names with spaces or mixed case in Oracle.
            string query = "SELECT DISTINCT \"Pertaining to Unit\" FROM CYBER_ALERTS_MASTER ORDER BY \"Pertaining to Unit\" ASC";

            try
            {
                using (OleDbConnection currentConn = new OleDbConnection(ConfigurationManager.ConnectionStrings["constring"].ToString()))
                {
                    currentConn.Open();
                    using (OleDbCommand command = new OleDbCommand(query, currentConn))
                    {
                        using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                        {
                            adapter.Fill(dtUnits);
                        }
                    }

                    ddlPertainingToUnit.DataSource = dtUnits;
                    ddlPertainingToUnit.DataTextField = "Pertaining to Unit"; // Column to display in the dropdown
                    ddlPertainingToUnit.DataValueField = "Pertaining to Unit"; // Column to use as the value
                    ddlPertainingToUnit.DataBind();
                }
            }
            catch (OleDbException ex)
            {
                lblSubmissionMessage.Text = "Error loading Pertaining To Unit list: " + ex.Message;
                lblSubmissionMessage.CssClass = "text-danger mt-2";
                System.Diagnostics.Trace.WriteLine("DB Error loading units: " + ex.ToString());
            }
            catch (Exception ex)
            {
                lblSubmissionMessage.Text = "An unexpected error occurred while loading units: " + ex.Message;
                lblSubmissionMessage.CssClass = "text-danger mt-2";
                System.Diagnostics.Trace.WriteLine("General Error loading units: " + ex.ToString());
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

            // Only allow "CO" (Corporate Office) users to submit central alerts
            if (Session["Center"] != null && Session["Center"].ToString() == "CO")
            {
                // Retrieve values from the fields
                string alertDateTimeStr = txtAlertDateTime.Text;
                string receivedDateStr = txtReceivedDateCentral.Text;
                string centreUnitStr = txtCentreUnit.Text;
                string senderDetails = txtSenderDetailsCentral.Text;
                string incidentDateStr = txtIncidentDateCentral.Text;
                string entryDateStr = txtEntryDateCentral.Text;
                string emailDateStr = txtEmailDateCentral.Text;
                
                // --- MODIFIED: Get value from DropDownList ---
                string pertainingToUnitSelectedValue = ddlPertainingToUnit.SelectedValue; // Get selected value from dropdown
                
                string affectedSailIP = txtAffectedSailIPCentral.Text;
                string affectedPortStr = txtAffectedPortCentral.Text;
                string maliciousIP = txtMaliciousIPCentral.Text;
                string alertDetails = txtAlertDetailsCentral.Text;
                string actionDateStr = txtActionDateCentral.Text;
                string actionDetails = txtActionDetails.Text;
                string remarks = txtRemarksCentral.Text;
                string repliedSenderStr = txtRepliedSenderCentral.Text;
                string closingDateStr = txtClosingDateCentral.Text;

                // Parse Date/Time fields (assuming valid due to client-side validators, but TryParse is safer)
                DateTime alertDt, receivedDt, incidentDt, entryDt, emailDt, actionDt, repliedSenderDt, closingDt;

                if (!DateTime.TryParse(alertDateTimeStr, out alertDt) ||
                    !DateTime.TryParse(receivedDateStr, out receivedDt) ||
                    !DateTime.TryParse(incidentDateStr, out incidentDt) ||
                    !DateTime.TryParse(entryDateStr, out entryDt) ||
                    !DateTime.TryParse(emailDateStr, out emailDt) ||
                    !DateTime.TryParse(actionDateStr, out actionDt) ||
                    !DateTime.TryParse(repliedSenderStr, out repliedSenderDt) ||
                    !DateTime.TryParse(closingDateStr, out closingDt))
                {
                    lblSubmissionMessage.Text = "One or more date/time fields have an invalid format.";
                    lblSubmissionMessage.CssClass = "text-danger mt-2";
                    return;
                }

                // --- MODIFIED: Convert string values to int for Integer parameters.
                // Assuming "Pertaining to Unit" is stored as an INT in your DB,
                // and its values in the dropdown are also numbers.
                // If it's a VARCHAR in DB, you don't need this TryParse for pertainingToUnitInt.
                int centreUnitInt, pertainingToUnitInt, affectedPortInt;

                if (!int.TryParse(centreUnitStr, out centreUnitInt))
                {
                    lblSubmissionMessage.Text = "Centre Unit must be a valid number.";
                    lblSubmissionMessage.CssClass = "text-danger mt-2";
                    return;
                }
                // --- MODIFIED: Use the selected value from dropdown ---
                if (!int.TryParse(pertainingToUnitSelectedValue, out pertainingToUnitInt))
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
                // --- END MODIFIED CONVERSIONS ---

                try
                {
                    using (OleDbConnection currentConn = new OleDbConnection(ConfigurationManager.ConnectionStrings["constring"].ToString()))
                    {
                        currentConn.Open();
                        string procedureName = "CYBER_ALERT_SUBMISSION"; // Your Oracle Stored Procedure name
                        OleDbCommand cmd = new OleDbCommand(procedureName, currentConn);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        // Add parameters to the stored procedure command
                        cmd.Parameters.Add("vRECEIVE_DATE", OleDbType.Date).Value = receivedDt;
                        cmd.Parameters.Add("vCENTER_UNIT", OleDbType.Integer).Value = centreUnitInt;
                        cmd.Parameters.Add("vRECEIVED_FROM_SENDER", OleDbType.VarChar, 50).Value = senderDetails; // Specify size for VarChar
                        cmd.Parameters.Add("vINCIDENT_DATE", OleDbType.Date).Value = incidentDt;
                        cmd.Parameters.Add("vENTRY_DATE", OleDbType.Date).Value = entryDt;
                        cmd.Parameters.Add("vEMAIL_TO_PLANT_DATE", OleDbType.Date).Value = emailDt;
                        // --- MODIFIED: Use the parsed int from the dropdown ---
                        cmd.Parameters.Add("vPERTAINING_TO_UNIT", OleDbType.Integer).Value = pertainingToUnitInt;
                        
                        cmd.Parameters.Add("vAFFECTED_SAILIP", OleDbType.VarChar, 50).Value = affectedSailIP; // Specify size
                        cmd.Parameters.Add("vAFFECTED_PORT", OleDbType.Integer).Value = affectedPortInt;
                        cmd.Parameters.Add("vMALICIOUS_IP", OleDbType.VarChar, 50).Value = maliciousIP; // Specify size
                        cmd.Parameters.Add("vALERT_DETAILS", OleDbType.VarChar, 250).Value = alertDetails; // Specify size
                        cmd.Parameters.Add("vFIRST_ACTION_DATE", OleDbType.Date).Value = actionDt;
                        cmd.Parameters.Add("vDETAILS_OF_ACTION", OleDbType.VarChar, 250).Value = actionDetails; // Specify size
                        cmd.Parameters.Add("vREMARKS", OleDbType.VarChar, 250).Value = remarks; // Specify size
                        cmd.Parameters.Add("vREPLIED_SENDER_DATE", OleDbType.Date).Value = repliedSenderDt;
                        cmd.Parameters.Add("vCLOSING_DATE", OleDbType.Date).Value = closingDt;

                        cmd.ExecuteNonQuery();

                        lblSubmissionMessage.Text = "Cyber alert submitted successfully!";
                        lblSubmissionMessage.CssClass = "text-success mt-2";
                        
                        // Optional: Clear form fields after successful submission
                        ClearFormFields();
                    }
                }
                catch (OleDbException ex)
                {
                    lblSubmissionMessage.Text = "Database error submitting alert: " + ex.Message;
                    lblSubmissionMessage.CssClass = "text-danger mt-2";
                    System.Diagnostics.Trace.WriteLine("DB Error in btnSubmitAlert_Click: " + ex.ToString());
                }
                catch (Exception ex)
                {
                    lblSubmissionMessage.Text = "An unexpected error occurred: " + ex.Message;
                    lblSubmissionMessage.CssClass = "text-danger mt-2";
                    System.Diagnostics.Trace.WriteLine("General Error in btnSubmitAlert_Click: " + ex.ToString());
                }
            }
            else
            {
                lblSubmissionMessage.Text = "You do not have permission to submit Central alerts.";
                lblSubmissionMessage.CssClass = "text-danger mt-2";
            }
        }

        // Optional: Method to clear form fields
        private void ClearFormFields()
        {
            txtAlertDateTime.Text = string.Empty;
            txtReceivedDateCentral.Text = string.Empty;
            txtCentreUnit.Text = string.Empty;
            txtSenderDetailsCentral.Text = string.Empty;
            txtIncidentDateCentral.Text = string.Empty;
            txtEntryDateCentral.Text = string.Empty;
            txtEmailDateCentral.Text = string.Empty;
            ddlPertainingToUnit.SelectedValue = ""; // Reset dropdown to the default "-- Select Unit --"
            txtAffectedSailIPCentral.Text = string.Empty;
            txtAffectedPortCentral.Text = string.Empty;
            txtMaliciousIPCentral.Text = string.Empty;
            txtAlertDetailsCentral.Text = string.Empty;
            txtActionDateCentral.Text = string.Empty;
            txtActionDetails.Text = string.Empty;
            txtRemarksCentral.Text = string.Empty;
            txtRepliedSenderCentral.Text = string.Empty;
            txtClosingDateCentral.Text = string.Empty;
        }
    }
}

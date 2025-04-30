using System;
using System.Web.UI;
using System.Web.UI.WebControls; // Make sure this is included for Enable/Disable

namespace CyberAlert
{
    public partial class CyberAlertsCentral : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) // Apply this logic only on the initial page load, not postbacks
            {
                if (Session["EmpCode"] != null)
                {
                    txtReporter.Text = Session["EmpCode"].ToString();

                    string center = Session["Center"] as string;

                    if (center == "Plant")
                    {
                        // If the user is from a Plant/Unit, disable the Central entry fields
                        // These are the fields from received_date to alert_details
                        txtAlertTitle.Enabled = false;
                        txtDescription.Enabled = false;
                        ddlSeverity.Enabled = false;
                        txtAffectedSystems.Enabled = false; // This field was in the old list, keeping for now
                        txtReceivedDateCentral.Enabled = false;
                        txtSenderDetailsCentral.Enabled = false;
                        txtIncidentDateCentral.Enabled = false;
                        txtEntryDateCentral.Enabled = false;
                        txtEmailDateCentral.Enabled = false;
                        txtPertainingToUnitCentral.Enabled = false;
                        txtAffectedSailIPCentral.Enabled = false;
                        txtAffectedPortCentral.Enabled = false;
                        txtMaliciousIPCentral.Enabled = false;
                        txtAlertDetailsCentral.Enabled = false;
                        txtAlertDateTime.Enabled = false; // Assuming this is the entry timestamp

                        // The fields from first_action_taken_date to closing_date are not on this form,
                        // so we don't need to disable them here.

                        // Optionally, you could provide a message to the Plant user on this page
                        // lblSubmissionMessage.Text = "This page is for Corporate Office alert entries. You can view but not edit these details.";
                    }
                    // If the center is "CO" (or null), the fields are enabled by default.
                    // Ensure the "Action Taken Details" fields (which are not on this form) are handled on the Local page.
                }
                else
                {
                    // Redirect to login if not authenticated
                    Response.Redirect("Login.aspx");
                }
            }
        }

        protected void btnSubmitAlert_Click(object sender, EventArgs e)
        {
            // Add check to ensure only CO users can submit this form
            string center = Session["Center"] as string;
            if (center == "CO" || center == null) // Assuming null also means CO for our placeholder
            {
                // Retrieve values from the fields
                string alertTitle = txtAlertTitle.Text;
                string description = txtDescription.Text;
                string severity = ddlSeverity.SelectedValue;
                string affectedSystems = txtAffectedSystems.Text;
                string reporter = txtReporter.Text;
                DateTime alertDateTime = DateTime.Parse(txtAlertDateTime.Text);

                DateTime receivedDate = DateTime.Parse(txtReceivedDateCentral.Text);
                string senderDetails = txtSenderDetailsCentral.Text;
                DateTime incidentDate = DateTime.Parse(txtIncidentDateCentral.Text);
                DateTime entryDate = DateTime.Parse(txtEntryDateCentral.Text);
                DateTime emailDate = DateTime.Parse(txtEmailDateCentral.Text);
                string pertainingToUnit = txtPertainingToUnitCentral.Text;
                // Add validation for numeric fields
                int affectedSailIP;
                int affectedPort;
                bool isSailIPValid = int.TryParse(txtAffectedSailIPCentral.Text, out affectedSailIP);
                bool isPortValid = int.TryParse(txtAffectedPortCentral.Text, out affectedPort);

                string maliciousIP = txtMaliciousIPCentral.Text;
                string alertDetails = txtAlertDetailsCentral.Text;

                // Perform validation
                if (string.IsNullOrEmpty(alertTitle) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(senderDetails) ||
                    string.IsNullOrEmpty(pertainingToUnit) || string.IsNullOrEmpty(maliciousIP) || string.IsNullOrEmpty(alertDetails) ||
                    !isSailIPValid || !isPortValid)
                {
                    lblSubmissionMessage.Text = "Please fill all required fields and ensure IP/Port are numbers.";
                    lblSubmissionMessage.CssClass = "text-danger mt-2"; // Change color for error
                    return; // Stop processing if validation fails
                }

                // For now, display a success message
                lblSubmissionMessage.Text = $"Cyber alert submitted successfully by {reporter} from {Session["Center"]}! Title: {alertTitle}, Received Date: {receivedDate}";
                lblSubmissionMessage.CssClass = "text-success mt-2"; // Change color for success

                // In a real application, you would save all this data to a database here.

                // Optionally, clear the form after successful submission
                // txtAlertTitle.Text = ""; // Uncomment and repeat for all fields you want to clear
                // ...
            }
            else
            {
                // Prevent submission if the user is not CO
                lblSubmissionMessage.Text = "You do not have permission to submit Central alerts.";
                lblSubmissionMessage.CssClass = "text-danger mt-2";
            }
        }
    }
}

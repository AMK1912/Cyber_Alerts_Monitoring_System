using System;
using System.Web.UI;
using System.Web.UI.WebControls; // Make sure this is included

namespace CyberAlert
{
    public partial class CyberAlertsLocal : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             if (!IsPostBack) // Apply this logic only on the initial page load
            {
                if (Session["EmpCode"] != null)
                {
                    txtReporterLocal.Text = Session["EmpCode"].ToString();

                    string center = Session["Center"] as string;

                    if (center == "CO")
                    {
                        // If the user is from Corporate Office, disable the Action Taken Details fields
                        // These are the fields from first_action_taken_date to closing_date
                        txtFirstActionTakenDateLocal.Enabled = false;
                        txtDetailsOfActionLocal.Enabled = false;
                        txtRemarksLocal.Enabled = false;
                        txtRepliedToDateLocal.Enabled = false;
                        txtClosingDateLocal.Enabled = false;

                        // Optionally, provide a message
                        // lblSubmissionMessageLocal.Text = "This page is for Plant/Unit action updates. You can view but not edit action details.";
                    }
                    // If the center is "Plant", all fields are enabled by default.
                }
                else
                {
                    // Redirect to login if not authenticated
                    Response.Redirect("Login.aspx");
                }
            }
        }

        protected void btnSubmitAlertLocal_Click(object sender, EventArgs e)
        {
            // Add check to ensure only Plant users can submit the action details
            string center = Session["Center"] as string;
            if (center == "Plant")
            {
                // Retrieve values from the fields
                string alertTitle = txtAlertTitleLocal.Text;
                string description = txtDescriptionLocal.Text;
                string severity = ddlSeverityLocal.SelectedValue;
                // Note: Affected Systems was in the old list but not new local. Keep or remove as needed.
                // string affectedSystems = txtAffectedSystemsLocal.Text;

                string receivedFromSender = txtReceivedFromSenderLocal.Text;
                string pertainingToUnit = txtPertainingToUnitLocal.Text;
                string affectedSailIP = txtAffectedSailIPLocal.Text; // Based on your list being varchar
                // Add validation for numeric port
                int affectedPort;
                bool isPortValid = int.TryParse(txtAffectedPortLocal.Text, out affectedPort);

                string maliciousIP = txtMaliciousIPLocal.Text;
                string alertDetails = txtAlertDetailsLocal.Text;
                string reporter = txtReporterLocal.Text; // Pre-filled

                DateTime receivedDate = DateTime.Parse(txtReceivedDateLocal.Text);
                DateTime incidentDate = DateTime.Parse(txtIncidentDateLocal.Text);
                DateTime entryDate = DateTime.Parse(txtEntryDateLocal.Text);

                // Retrieve Action Taken Details - these should only be filled by Plants
                DateTime firstActionTakenDate = DateTime.Parse(txtFirstActionTakenDateLocal.Text); // Add TryParse and validation
                string detailsOfAction = txtDetailsOfActionLocal.Text;
                string remarks = txtRemarksLocal.Text;
                DateTime repliedToDate = DateTime.Parse(txtRepliedToDateLocal.Text); // Add TryParse and validation
                DateTime closingDate = DateTime.Parse(txtClosingDateLocal.Text);   // Add TryParse and validation

                 // Perform validation for required fields (adjust based on which fields are mandatory)
                if (string.IsNullOrEmpty(alertTitle) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(receivedFromSender) ||
                    string.IsNullOrEmpty(pertainingToUnit) || string.IsNullOrEmpty(affectedSailIP) || string.IsNullOrEmpty(maliciousIP) || string.IsNullOrEmpty(alertDetails) ||
                    !isPortValid || // Validate port
                     // Also validate Action Taken fields if they are mandatory for Plants
                     string.IsNullOrEmpty(detailsOfAction) || string.IsNullOrEmpty(remarks)
                     // Add validation for Action Taken Dates if mandatory
                    )
                {
                     lblSubmissionMessageLocal.Text = "Please fill all required fields.";
                     lblSubmissionMessageLocal.CssClass = "text-danger mt-2";
                     return;
                }


                // For now, display a success message
                lblSubmissionMessageLocal.Text = $"Local Cyber alert submitted successfully by {reporter} from {Session["Center"]}!";
                lblSubmissionMessageLocal.CssClass = "text-success mt-2";

                // In a real application, you would save all this data to a database here.

                // Optionally, clear the form
                // ...
            }
            else
            {
                // Prevent submission if the user is not Plant
                lblSubmissionMessageLocal.Text = "You do not have permission to submit Local alerts.";
                lblSubmissionMessageLocal.CssClass = "text-danger mt-2";
            }
        }
    }
}

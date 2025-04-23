using System;
using System.Web.UI;

namespace CyberAlertsWebApp
{
    public partial class CyberAlertsCentral : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["EmpCode"] != null)
            {
                txtReporter.Text = Session["EmpCode"].ToString();

                // Check the user's center
                string center = Session["Center"] as string;
                if (center == "Plant")
                {
                    // If the user is from a Plant/Unit, disable the central entry fields
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

                    // Optionally, you could provide a message or hide these fields entirely
                    // lblMessage.Text = "This form is for Corporate Office users only.";
                }
                // If the center is "CO" (or null, implying CO in our placeholder logic), the fields will be enabled by default.
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnSubmitAlert_Click(object sender, EventArgs e)
        {
            // Retrieve values from the existing fields
            string alertTitle = txtAlertTitle.Text;
            string description = txtDescription.Text;
            string severity = ddlSeverity.SelectedValue;
            string affectedSystems = txtAffectedSystems.Text;
            string reporter = txtReporter.Text;
            DateTime alertDateTime = DateTime.Parse(txtAlertDateTime.Text);

            // Retrieve values from the new central entry fields
            DateTime receivedDate = DateTime.Parse(txtReceivedDateCentral.Text);
            string senderDetails = txtSenderDetailsCentral.Text;
            DateTime incidentDate = DateTime.Parse(txtIncidentDateCentral.Text);
            DateTime entryDate = DateTime.Parse(txtEntryDateCentral.Text);
            DateTime emailDate = DateTime.Parse(txtEmailDateCentral.Text);
            string pertainingToUnit = txtPertainingToUnitCentral.Text;
            int affectedSailIP = int.Parse(txtAffectedSailIPCentral.Text); // Consider TryParse and validation
            int affectedPort = int.Parse(txtAffectedPortCentral.Text);     // Consider TryParse and validation
            string maliciousIP = txtMaliciousIPCentral.Text;
            string alertDetails = txtAlertDetailsCentral.Text;

            // For now, display a success message with some of the data
            lblSubmissionMessage.Text = $"Cyber alert submitted successfully by {reporter} from {Session["Center"]}! Title: {alertTitle}, Received Date: {receivedDate}";

            // In a real application, you would save all this data.

            // Optionally, clear the form
            // ... (Clear all the text boxes and reset dropdowns as needed)
        }
    }
}

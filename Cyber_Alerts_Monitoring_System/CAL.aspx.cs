using System;
using System.Web.UI;

namespace CyberAlertsWebApp
{
    public partial class CyberAlertsLocal : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the user is logged in and has EmpCode in the session
            if (Session["EmpCode"] != null)
            {
                // Pre-fill the Reporter field with the logged-in user's EmpCode
                txtReporterLocal.Text = Session["EmpCode"].ToString();
            }
            else
            {
                // If EmpCode is not in session, redirect to login (optional)
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnSubmitAlertLocal_Click(object sender, EventArgs e)
        {
            // Retrieve values from the form controls
            string alertTitle = txtAlertTitleLocal.Text;
            string description = txtDescriptionLocal.Text;
            string severity = ddlSeverityLocal.SelectedValue;
            string affectedSystems = txtAffectedSystemsLocal.Text;
            string reporter = txtReporterLocal.Text;
            DateTime alertDateTime = DateTime.Parse(txtAlertDateTimeLocal.Text); // Consider error handling

            // Display a success message
            lblSubmissionMessageLocal.Text = "Cyber alert submitted successfully!";

            // In a real application, you would save this data.

            // Optionally, clear the form
            txtAlertTitleLocal.Text = "";
            txtDescriptionLocal.Text = "";
            ddlSeverityLocal.SelectedIndex = 0;
            txtAffectedSystemsLocal.Text = "";
            txtAlertDateTimeLocal.Text = "";
        }
    }
}

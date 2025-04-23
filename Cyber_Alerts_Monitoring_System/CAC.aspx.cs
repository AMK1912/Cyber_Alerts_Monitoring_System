using System;
using System.Web.UI;

namespace CyberAlertsWebApp
{
    public partial class CyberAlertsCentral : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the user is logged in and has EmpCode in the session
            if (Session["EmpCode"] != null)
            {
                // Pre-fill the Reporter field with the logged-in user's EmpCode
                txtReporter.Text = Session["EmpCode"].ToString();
            }
            else
            {
                // If EmpCode is not in session, redirect to login (optional, depending on your flow)
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnSubmitAlert_Click(object sender, EventArgs e)
        {
            // Here you would typically retrieve the values from the form controls
            string alertTitle = txtAlertTitle.Text;
            string description = txtDescription.Text;
            string severity = ddlSeverity.SelectedValue;
            string affectedSystems = txtAffectedSystems.Text;
            string reporter = txtReporter.Text;
            DateTime alertDateTime = DateTime.Parse(txtAlertDateTime.Text); // You might want to add error handling here

            // For now, let's just display a success message
            lblSubmissionMessage.Text = "Cyber alert submitted successfully!";

            // In a real application, you would likely save this data to a database or perform other actions.

            // Optionally, you can clear the form after submission
            txtAlertTitle.Text = "";
            txtDescription.Text = "";
            ddlSeverity.SelectedIndex = 0; // Reset to the first item
            txtAffectedSystems.Text = "";
            txtAlertDateTime.Text = "";
        }
    }
}

using System;
using System.Web.UI;

namespace CyberAlertsWebApp
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // You can add any initial logic here
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmpCode.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                lblErrorMessage.Text = "Please Enter both Employee Code and Password";
                return;
            }

            // Placeholder for authentication - replace with your actual logic
            if (txtEmpCode.Text == "COUser" && txtPassword.Text == "COPassword")
            {
                // Simulate successful login for Corporate Office
                Session["Center"] = "CO";
                Session["EmpCode"] = txtEmpCode.Text;
                Response.Redirect("Default.aspx"); // Redirect to the home page
            }
            else if (txtEmpCode.Text == "PlantUser" && txtPassword.Text == "PlantPassword")
            {
                // Simulate successful login for a Plant/Unit
                Session["Center"] = "Plant";
                Session["EmpCode"] = txtEmpCode.Text;
                Response.Redirect("Default.aspx"); // Redirect to the home page
            }
            else
            {
                lblErrorMessage.Text = "Invalid Emp Code or Password";
            }
        }
    }
}

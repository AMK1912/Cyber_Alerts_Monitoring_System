// Login.aspx.cs
using System;
using System.Web.UI;
using System.Web.Security; // If you're using FormsAuthentication

namespace CyberAlert
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Cast the Master property to your specific Master Page type
            // This allows you to access the public properties/methods of your Master Page.
            if (this.Master is SiteMaster masterPage)
            {
                masterPage.ShowNavBar = false; // Set to false to hide the navbar for Login.aspx
            }

            // Your existing Page_Load logic for the Login page
            // e.g., for setting focus or checking for return URLs
            if (!IsPostBack)
            {
                // Optionally clear session or ensure user is logged out upon reaching login page
                // Session.Clear();
                // Session.Abandon();
                // FormsAuthentication.SignOut();
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // ... Your existing authentication logic here ...

            // Example of successful login (replace with your actual authentication)
            bool isAuthenticated = true; // Placeholder: Replace with actual authentication check
            if (txtUsername.Text == "test" && txtPassword.Text == "password") // Example credentials
            {
                 // Store user details in session
                Session["EmpCode"] = "E123";
                Session["Center"] = "CO"; // Or "Plant" based on user's role/location

                // Redirect to a secure page after successful login
                Response.Redirect("~/Default.aspx"); // Or wherever you want to send them after login
            }
            else
            {
                // Handle failed login
                lblLoginMessage.Text = "Invalid username or password.";
                lblLoginMessage.CssClass = "text-danger";
                isAuthenticated = false; // Set to false for failed login
            }

            if (isAuthenticated)
            {
                // The master page property will automatically revert to default (true)
                // when redirecting to Default.aspx or any other page.
                // No need to set masterPage.ShowNavBar = true here.
            }
        }
        // ... other methods ...
    }
}

// Site.Master.cs
using System;
using System.Web.UI;
using System.Web.UI.WebControls; // Required for WebControl type

namespace CyberAlert
{
    public partial class SiteMaster : MasterPage
    {
        // Public property to control Navbar visibility
        public bool ShowNavBar
        {
            get
            {
                // Default to true if not set (navbar is usually visible)
                if (ViewState["ShowNavBar"] == null)
                    return true;
                return (bool)ViewState["ShowNavBar"];
            }
            set
            {
                ViewState["ShowNavBar"] = value; // Store the state

                // Set visibility of the div containing navigation links
                // 'navbarLinksDiv' is the ID we added in Site.Master
                if (navbarLinksDiv != null) // Check if the control exists
                {
                    navbarLinksDiv.Visible = value;
                }

                // Also control the visibility of the logout link if it's separate
                // 'LogoutLink' is the ID of your logout LinkButton
                if (LogoutLink != null) // Check if the control exists
                {
                    LogoutLink.Visible = value;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // No specific logic needed here for navbar visibility,
            // as the 'ShowNavBar' property's setter handles it when called from content pages.
            // If you had other dynamic elements, they'd go here.
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            // Your logout logic here
            Session.Clear();
            Session.Abandon();
            // Clear authentication cookie if you are using Forms Authentication
            // FormsAuthentication.SignOut();
            Response.Redirect("~/Login.aspx");
        }
    }
}

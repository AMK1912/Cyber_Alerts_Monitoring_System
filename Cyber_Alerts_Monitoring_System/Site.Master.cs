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
                if (ViewState["ShowNavBar"] == null)
                    return true;
                return (bool)ViewState["ShowNavBar"];
            }
            set
            {
                ViewState["ShowNavBar"] = value;

                // IMPORTANT: 'navbarNav' must have runat="server" in Site.Master HTML
                if (navbarNav != null)
                {
                    navbarNav.Visible = value;
                }

                // If you want to hide the "Logout" link specifically when navbar is hidden,
                // you would need to make it an ASP.NET server control (e.g., LinkButton)
                // and give it an ID, then add similar logic here.
                // Since it's currently a simple <a> tag, it will hide/show with its parent div.
                // If you re-add a LogoutLink LinkButton:
                // if (LogoutLink != null)
                // {
                //     LogoutLink.Visible = value;
                // }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Nothing to change here for navbar visibility based on your latest Site.Master HTML.
        }

        // If you had a Logout LinkButton and an OnClick event, keep this.
        // If it's just an <a> tag now that redirects to Login.aspx, you might remove this event.
        // Assuming your previous Logout_Click was for a LinkButton, you'll need it if you revert it.
        // If it's a simple <a> tag directly linking to Login.aspx, this method won't be called for logout.
        // If you want server-side logout:
        protected void Logout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            // FormsAuthentication.SignOut(); // If you use FormsAuthentication
            Response.Redirect("~/Login.aspx");
        }
    }
}

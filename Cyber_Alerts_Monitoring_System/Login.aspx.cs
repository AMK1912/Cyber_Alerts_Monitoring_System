using System;
using System.Configuration;
using System.Collections.Specialized;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.OleDb;

using System.Text;
using System.Web.SessionState;
using System.Web.Services.Description;

public partial class Login : System.Web.UI.Page
{
    private OleDbConnection conn;

    protected void Page_Load(object sender, EventArgs e)
    {
        conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["constring"].ToString());
        if (!IsPostBack)
        {
            txtusername.Focus();
        }
    }

    protected void btnlogin_Click(object sender, EventArgs e)
    {
        message.Text = ""; // Clear any previous error message

        if (string.IsNullOrEmpty(txtusername.Text) || string.IsNullOrEmpty(txtpassword.Text))
        {
            message.Text = "Please enter both employyee code and password.";
            return;
        }

        try
        {
            conn.Open();
            string sql = "SELECT empcode, password FROM emp_cyber_alert WHERE empcode = ?";
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            cmd.Parameters.AddWithValue("?", txtusername.Text);

            OleDbDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string retrievedUsername = dr["empcode"].ToString();
                string retrievedPassword = dr["password"].ToString(); // This is the *plain-text* password

                // INSECURE: Directly compare plain-text passwords (DO NOT DO THIS IN PRODUCTION)
                if (txtpassword.Text == retrievedPassword)
                {
                    // Authentication successful
                    //FormsAuthentication.RedirectFromLoginPage(retrievedUsername, false);
                    // Or, if not using Forms Authentication:
                    System.Diagnostics.Debug.WriteLine("Authentication Successful. About to redirect to:" + Request.Url.GetLeftPart(UriPartial.Authority) + "/Default.aspx");
                    Response.Redirect(Request.Url.GetLeftPart(UriPartial.Authority) + "/Default.aspx");
                    System.Diagnostics.Debug.WriteLine("Response.Redirect() call executed.");
                    return;
                }
                else
                {
                    message.Text = "Invalid username or password.";
                }
            }
            else
            {
                message.Text = "Invalid username or password."; // No user found
            }
            dr.Close();
        }
        catch (OleDbException ex)
        {
            message.Text = "Database error: " + ex.Message;
            System.Diagnostics.Trace.WriteLine("DB Error: " + ex.ToString());
        }
        finally
        {
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }
}

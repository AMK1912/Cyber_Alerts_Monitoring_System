using System;
using System.Configuration;
using System.Data.OleDb;
using System.Web.Security; //  For FormsAuthentication
using System.Web.UI;

public partial class Login : Page
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
            message.Text = "Please enter both employee code and password.";
            return;
        }

        try
        {
            conn.Open();
            //  Query the temporary table.  Adjust the table name if needed.
            string sql = "SELECT empcode, password, center FROM emp_cyber_alert WHERE empcode = ?";
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            cmd.Parameters.AddWithValue("?", txtusername.Text);

            OleDbDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string retrievedUsername = dr["empcode"].ToString();
                string retrievedPassword = dr["password"].ToString(); // Get the plain-text password
                string retrievedCenter = dr["center"].ToString();

                //  INSECURE: Directly compare plain-text passwords.  DO NOT DO THIS IN PRODUCTION.
                if (txtpassword.Text == retrievedPassword)
                {
                    // Authentication successful
                    //  1.  Set Session variables.  This is CRUCIAL.
                    Session["EmpCode"] = retrievedUsername;
                    Session["Center"] = retrievedCenter;

                    //  2.  Use FormsAuthentication.RedirectFromLoginPage().  This is the correct way to redirect.
                    FormsAuthentication.RedirectFromLoginPage(retrievedUsername, false); //  Use false for non-persistent cookie
                    return; //  Important:  Exit the method after successful login
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


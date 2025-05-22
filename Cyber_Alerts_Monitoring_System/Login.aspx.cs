using System;
using System.Configuration;
using System.Data.OleDb;
using System.Web.Security;
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
            string sql = "SELECT empcode, password, center FROM temp_emp_cyber_alert WHERE empcode = ?"; // Added center to the query
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            cmd.Parameters.AddWithValue("?", txtusername.Text);

            OleDbDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
                {
                    string retrievedUsername = dr["empcode"].ToString();
                    string retrievedPassword = dr["password"].ToString();
                    try
                    {
                       string retrievedCenter = dr["center"].ToString();
                       System.Diagnostics.Debug.WriteLine("Center from DB: " + retrievedCenter);
                       Session["Center"] = retrievedCenter;
                    }
                    catch(Exception ex)
                    {
                       System.Diagnostics.Debug.WriteLine("Exception reading Center: " + ex.Message);
                       string retrievedCenter = "";
                       Session["Center"] = retrievedCenter;
                    }
                    System.Diagnostics.Debug.WriteLine("Login Successful - EmpCode: " + retrievedUsername + ", Center: " + retrievedCenter);
                    FormsAuthentication.RedirectFromLoginPage(retrievedUsername, false);
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

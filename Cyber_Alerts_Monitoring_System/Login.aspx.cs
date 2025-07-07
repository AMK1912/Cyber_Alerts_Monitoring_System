using System;
using System.Configuration;
using System.Data.OleDb;
using System.Web.Security; // Required for FormsAuthentication
using System.Web.UI;
using CyberAlert; // IMPORTANT: Add this using directive to access your SiteMaster class

public partial class Login : Page
{
    private OleDbConnection conn;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize connection here, as it's used in btnlogin_Click
        conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["constring"].ToString());

        if (!IsPostBack)
        {
            txtusername.Focus();

            // --- IMPORTANT: Hide the navbar on the login page ---
            // Cast the Master property to your specific Master Page type (CyberAlert.SiteMaster)
            if (this.Master is SiteMaster masterPage)
            {
                masterPage.ShowNavBar = false; // Set to false to hide the navbar
            }
            // --- End Navbar Hiding Logic ---

            // Optional: Ensure user is logged out upon reaching login page
            // This prevents issues if a user navigates directly to Login.aspx while still authenticated
            if (Context.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
                Session.Clear();
                Session.Abandon();
                Response.Redirect(Request.RawUrl); // Redirect back to refresh the state
            }
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
            string sql = "SELECT empcode, password, CENTER FROM emp_cyber_alert WHERE empcode = ?";
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            cmd.Parameters.AddWithValue("?", txtusername.Text);

            OleDbDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string retrievedUsername = dr["empcode"].ToString();
                string retrievedPassword = dr["password"].ToString();

                string retrievedCenter = string.Empty; // Initialize to empty string

                // --- DEEP DEBUGGING: List all columns in the DataReader ---
                System.Diagnostics.Debug.WriteLine("--- DataReader Columns ---");
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    string columnName = dr.GetName(i);
                    object columnValue = dr.GetValue(i);
                    System.Diagnostics.Debug.WriteLine($"Column Index: {i}, Name: [{columnName}], Value: [{columnValue ?? "NULL"}]");
                }
                System.Diagnostics.Debug.WriteLine("--------------------------");


                try
                {
                    // Attempt to get the 'CENTER' value safely and robustly.
                    // Check for DBNull.Value.
                    // Use as string to avoid issues if ToString() is problematic for certain data types.
                    // Then Trim and ToUpper.
                    if (dr["CENTER"] != DBNull.Value)
                    {
                        retrievedCenter = dr["CENTER"] as string; // Try direct cast to string
                        if (retrievedCenter == null) // If not a string, try ToString()
                        {
                            retrievedCenter = dr["CENTER"].ToString();
                        }
                        retrievedCenter = retrievedCenter.Trim(); // Trim any leading/trailing whitespace
                        retrievedCenter = retrievedCenter.ToUpper(); // Convert to uppercase for consistent comparison
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("ERROR: Exception when trying to retrieve 'CENTER' by name (robust method): " + ex.Message);
                    retrievedCenter = string.Empty; // Ensure it's empty if an error occurs
                }

                // --- CRITICAL DEBUGGING POINT 1 ---
                System.Diagnostics.Debug.WriteLine("Login.aspx.cs - Value retrieved for 'CENTER' from DB (processed): [" + retrievedCenter + "]");


                // INSECURE: Directly compare plain-text passwords. DO NOT DO THIS IN PRODUCTION.
                // You should use password hashing (e.g., bcrypt, PBKDF2) for secure production applications.
                if (txtpassword.Text == retrievedPassword)
                {
                    // Authentication successful
                    Session["EmpCode"] = retrievedUsername;
                    Session["Center"] = retrievedCenter; // Set the session variable

                    // --- CRITICAL DEBUGGING POINT 2 ---
                    System.Diagnostics.Debug.WriteLine("Login.aspx.cs - Session['Center'] SET TO: [" + Session["Center"] + "]");

                    // FormsAuthentication handles the redirect and cookie creation.
                    // It will redirect to the return URL if specified, otherwise to Default.aspx.
                    FormsAuthentication.RedirectFromLoginPage(retrievedUsername, false);
                    return; // Important: Exit the method after redirecting
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
        catch (Exception ex) // Catch any other unexpected errors
        {
            message.Text = "An unexpected error occurred during login: " + ex.Message;
            System.Diagnostics.Trace.WriteLine("General Error in login: " + ex.ToString());
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

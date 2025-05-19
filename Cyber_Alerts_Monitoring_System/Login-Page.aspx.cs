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

public partial class login : System.Web.UI.Page
{
    //public string chklogin;
    //public string inpuser;
    //public string inppasswd;
    public OleDbConnection conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["constring"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack == true)
        {

            SetFocus("txtusername");
            try
            {


                SessionIDManager manager = new SessionIDManager();
                string newSessionId = manager.CreateSessionID(HttpContext.Current);


            }
            catch
            {
                Session.Clear();
                Session.Abandon();
                Session.RemoveAll();
                Response.Redirect("login.aspx");
            }
        }
    }

    protected void btnlogin_Click(object sender, EventArgs e)
    {
        Session["vname"] = "";

        try
        {
            string sql = "select emp_code from emp_cyber_alerts where emp_code=" + Convert.ToString(txtusername.Text);
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            OleDbDataReader dr;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows == true)
            {
                chklogin = Convert.ToString(dr.GetValue(0)); // Assign value to the class-level variable
            }
            dr.Close();
            dr.Dispose();
            conn.Close();
        }
        catch (OleDbException ex)
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            Response.Redirect("login.aspx");
        }

    }

    protected void txtusername_TextChanged(object sender, EventArgs e)
    {
        SetFocus("txtpassword");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("login.aspx");
    }

}

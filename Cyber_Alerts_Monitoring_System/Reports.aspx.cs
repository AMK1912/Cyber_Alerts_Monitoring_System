using System;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Web.UI.WebControls; // For ListItem
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Globalization; // For DateTimeFormatInfo

namespace CyberAlert
{
    public partial class Reports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateMonthDropdown();
                PopulateYearDropdown();
            }
        }

        private void PopulateMonthDropdown()
        {
            ddlMonth.Items.Clear();
            ddlMonth.Items.Add(new ListItem("Select Month", "0")); // Default option

            for (int i = 1; i <= 12; i++)
            {
                string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(i);
                ddlMonth.Items.Add(new ListItem(monthName, i.ToString()));
            }

            ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
        }

        private void PopulateYearDropdown()
        {
            ddlYear.Items.Clear();
            ddlYear.Items.Add(new ListItem("Select Year", "0")); // Default option

            int currentYear = DateTime.Now.Year;
            for (int i = currentYear - 5; i <= currentYear + 1; i++)
            {
                ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

            ddlYear.SelectedValue = currentYear.ToString();
        }

        // Centralized method to get filtered data
        private DataSet GetFilteredReportData(string tableName, string dateColumnName)
        {
            int selectedMonth = Convert.ToInt32(ddlMonth.SelectedValue);
            int selectedYear = Convert.ToInt32(ddlYear.SelectedValue);

            if (selectedMonth == 0 || selectedYear == 0)
            {
                // You might want to display a message on the UI
                // Example: lblMessage.Text = "Please select both a month and a year.";
                return null;
            }

            DateTime startDate = new DateTime(selectedYear, selectedMonth, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            OleDbConnection currentConn = null;
            DataSet s = new DataSet();

            try
            {
                currentConn = new OleDbConnection(ConfigurationManager.ConnectionStrings["constring"].ToString());
                string query = "SELECT * FROM " + tableName + " WHERE " + dateColumnName + " >= ? AND " + dateColumnName + " <= ?";
                OleDbCommand cmd = new OleDbCommand(query, currentConn);

                cmd.Parameters.Add(new OleDbParameter("StartDateParam", OleDbType.DBDate)).Value = startDate;
                cmd.Parameters.Add(new OleDbParameter("EndDateParam", OleDbType.DBDate)).Value = endDate;

                currentConn.Open();
                OleDbDataAdapter oda = new OleDbDataAdapter(cmd);
                oda.Fill(s);
            }
            catch (OleDbException ex)
            {
                System.Diagnostics.Trace.WriteLine("OLE DB Error: " + ex.Message);
                // Handle error: e.g., lblMessage.Text = "Database error: " + ex.Message;
                return null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("General Error: " + ex.Message);
                // Handle error: e.g., lblMessage.Text = "An unexpected error occurred: " + ex.Message;
                return null;
            }
            finally
            {
                if (currentConn != null && currentConn.State == ConnectionState.Open)
                {
                    currentConn.Close();
                }
            }

            return s;
        }

        // Modified method for displaying report in the viewer
        private void DisplayReportInViewer(string reportFileName)
        {
            DataSet s = GetFilteredReportData("cyber_alerts_received", "alert_timestamp"); // Assuming 'alert_timestamp' is your date column

            if (s != null && s.Tables.Count > 0)
            {
                ReportDocument crp = new ReportDocument();
                crp.Load(Server.MapPath(reportFileName));
                crp.SetDataSource(s.Tables[0]); // Assumes your Crystal Report uses the first table in the DataSet

                CrystalReportViewer1.ReportSource = crp;
                CrystalReportViewer1.DataBind();
                // Clear any previous error messages if you're using a Label for them
                // lblMessage.Text = "";
            }
            else
            {
                // No data or filter not selected, clear the viewer
                CrystalReportViewer1.ReportSource = null;
                CrystalReportViewer1.DataBind();
                // Optionally, display a message to the user:
                // lblMessage.Text = "No data found for the selected month and year, or a month/year was not selected.";
            }
        }

        // --- Button Click Handlers ---

        protected void Button1_Click(object sender, EventArgs e)
        {
            DisplayReportInViewer("Affected_IP.rpt");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            DisplayReportInViewer("SEBI_Report.rpt");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            DisplayReportInViewer("Malicious IP.rpt");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            DisplayReportInViewer("Affected Ports.rpt");
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            DisplayReportInViewer("Action Pending.rpt");
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            DisplayReportInViewer("Closed Incidents.rpt");
        }
    }
}

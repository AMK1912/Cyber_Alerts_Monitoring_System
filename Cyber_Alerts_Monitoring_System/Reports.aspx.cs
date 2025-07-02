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

            // Optionally, select the current month
            ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
        }

        private void PopulateYearDropdown()
        {
            ddlYear.Items.Clear();
            ddlYear.Items.Add(new ListItem("Select Year", "0")); // Default option

            int currentYear = DateTime.Now.Year;
            // Provide a range of years, e.g., 5 years back to 1 year forward
            for (int i = currentYear - 5; i <= currentYear + 1; i++)
            {
                ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

            // Optionally, select the current year
            ddlYear.SelectedValue = currentYear.ToString();
        }

        // Centralized method to get filtered data
        private DataSet GetFilteredReportData(string tableName, string dateColumnName)
        {
            int selectedMonth = Convert.ToInt32(ddlMonth.SelectedValue);
            int selectedYear = Convert.ToInt32(ddlYear.SelectedValue);

            // Basic validation
            if (selectedMonth == 0 || selectedYear == 0)
            {
                // You might want to display a message on the UI instead of just returning null
                // Example: lblMessage.Text = "Please select both a month and a year.";
                return null;
            }

            // Calculate start and end dates for the selected month
            DateTime startDate = new DateTime(selectedYear, selectedMonth, 1);
            // Get the last day of the month by adding one month and subtracting one day
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            OleDbConnection currentConn = null; // Declare outside try for finally block access
            DataSet s = new DataSet();

            try
            {
                currentConn = new OleDbConnection(ConfigurationManager.ConnectionStrings["constring"].ToString());
                // Construct the SQL query with date filtering
                // IMPORTANT: Adjust 'your_date_column_name' to the actual date column in your table
                // For OLE DB, parameter syntax can vary. Using '?' for positional parameters is common.
                // Date literal format might also depend on your specific OLE DB provider/database.
                // '{d 'YYYY-MM-DD'}' is a common ODBC canonical format for dates.
                // If filtering by a datetime column, you might need to convert dates to strings or use CAST/TRUNC in SQL.

                // Example for Oracle via OLE DB (might need TRUNC for date-only comparison)
                // "SELECT * FROM " + tableName + " WHERE TRUNC(" + dateColumnName + ") >= ? AND TRUNC(" + dateColumnName + ") <= ?"

                // Generic OLE DB date comparison (might vary based on backend DB)
                string query = "SELECT * FROM " + tableName + " WHERE " + dateColumnName + " >= ? AND " + dateColumnName + " <= ?";

                OleDbCommand cmd = new OleDbCommand(query, currentConn);

                // Add parameters
                // For OleDb, parameters are often positional, so order matters.
                cmd.Parameters.Add(new OleDbParameter("StartDateParam", OleDbType.DBDate)).Value = startDate;
                cmd.Parameters.Add(new OleDbParameter("EndDateParam", OleDbType.DBDate)).Value = endDate;


                currentConn.Open();
                OleDbDataAdapter oda = new OleDbDataAdapter(cmd);
                oda.Fill(s); // Fills s.Tables[0]
            }
            catch (OleDbException ex)
            {
                // Log the OLE DB error
                System.Diagnostics.Trace.WriteLine("OLE DB Error: " + ex.Message);
                // Optionally, display a user-friendly error on the page
                // lblMessage.Text = "Database error: " + ex.Message;
                return null;
            }
            catch (Exception ex)
            {
                // Log general errors
                System.Diagnostics.Trace.WriteLine("General Error: " + ex.Message);
                // lblMessage.Text = "An unexpected error occurred: " + ex.Message;
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

        private void ExportReport(string reportFileName, string reportName)
        {
            DataSet s = GetFilteredReportData("cyber_alerts_received", "alert_timestamp"); // Assuming 'alert_timestamp' is your date column

            if (s != null && s.Tables.Count > 0)
            {
                ReportDocument crp = new ReportDocument();
                crp.Load(Server.MapPath(reportFileName));
                
                // SetDataSource will typically use s.Tables[0] if you pass just the DataSet
                // If your Crystal Report is expecting a specific DataTable name, you might need:
                // crp.SetDataSource(s.Tables[0]); // Or s.Tables["yourCrystalReportTableName"] if you named it

                // For simplicity, let's assume your report is configured to take the first table from the DataSet
                crp.SetDataSource(s.Tables[0]);

                CrystalReportViewer1.ReportSource = crp;
                CrystalReportViewer1.DataBind(); // Ensure the viewer displays the report

                // Clear any previous HTTP response content to avoid corruption
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + reportName + ".pdf");
                
                // Export to PDF directly
                crp.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, reportName);

                // Important: End the response after exporting to prevent "Server cannot set status after HTTP headers have been sent" error
                Response.End();
            }
            else
            {
                // Handle case where no data is returned or filter not selected
                CrystalReportViewer1.ReportSource = null;
                CrystalReportViewer1.DataBind();
                // Optionally, display a message to the user:
                // lblMessage.Text = "No data found for the selected filter or filter not applied.";
            }
        }


        // --- Button Click Handlers ---

        protected void Button1_Click(object sender, EventArgs e)
        {
            ExportReport("Affected_IP.rpt", "Affected_IP Report");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            ExportReport("SEBI_Report.rpt", "SEBI Report");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            ExportReport("Malicious IP.rpt", "Malicious IP Report");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            ExportReport("Affected Ports.rpt", "Affected Ports Report");
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            ExportReport("Action Pending.rpt", "Action Pending Report");
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            ExportReport("Closed Incidents.rpt", "Closed Incidents Report");
        }
    }
}

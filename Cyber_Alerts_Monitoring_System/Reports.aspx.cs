using System;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Web.UI.WebControls; // Needed for ListItem and Label (if you add lblMessage)
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Globalization; // Needed for DateTimeFormatInfo to get month names

namespace CyberAlert
{
    public partial class Reports : System.Web.UI.Page
    {
        // No need for a private OleDbConnection conn; field, as you're creating it inside functions.

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Populate dropdowns only on the initial page load
                PopulateMonthDropdown();
                PopulateYearDropdown();
                // Optionally, clear any previous messages on initial load
                // if (lblMessage != null) lblMessage.Text = "";
            }
        }

        // Method to populate the month dropdown
        private void PopulateMonthDropdown()
        {
            ddlMonth.Items.Clear();
            ddlMonth.Items.Add(new ListItem("Select Month", "0")); // Default option
            for (int i = 1; i <= 12; i++)
            {
                string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(i);
                ddlMonth.Items.Add(new ListItem(monthName, i.ToString()));
            }
            ddlMonth.SelectedValue = DateTime.Now.Month.ToString(); // Select current month by default
        }

        // Method to populate the year dropdown
        private void PopulateYearDropdown()
        {
            ddlYear.Items.Clear();
            ddlYear.Items.Add(new ListItem("Select Year", "0")); // Default option
            int currentYear = DateTime.Now.Year;
            // Populate years from 5 years ago to 1 year in the future (adjust range as needed)
            for (int i = currentYear - 5; i <= currentYear + 1; i++)
            {
                ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            ddlYear.SelectedValue = currentYear.ToString(); // Select current year by default
        }

        // --- Original Button Click Handlers with Date Filtering Added ---

        protected void Button1_Click(object sender, EventArgs e)
        {
            int selectedMonth = Convert.ToInt32(ddlMonth.SelectedValue);
            int selectedYear = Convert.ToInt32(ddlYear.SelectedValue);

            // Validate that a month and year have been selected
            if (selectedMonth == 0 || selectedYear == 0)
            {
                if (lblMessage != null) lblMessage.Text = "Please select both a month and a year for the Affected IP Report.";
                return; // Stop execution if filters are not selected
            }

            // Calculate the start and end dates for the selected month
            DateTime startDate = new DateTime(selectedYear, selectedMonth, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1); // Last day of the selected month

            OleDbConnection currentConn = null; // Declare outside try for finally block access
            DataSet s = new DataSet();

            try
            {
                currentConn = new OleDbConnection(ConfigurationManager.ConnectionStrings["constring"].ToString());
                // Construct the SQL query to filter by date
                // IMPORTANT: Replace 'alert_timestamp' with your actual date column name
                string query = "Select * from cyber_alerts_received WHERE alert_timestamp >= ? AND alert_timestamp <= ?";
                OleDbCommand cmd = new OleDbCommand(query, currentConn);

                // Add parameters for the start and end dates
                cmd.Parameters.Add(new OleDbParameter("StartDateParam", OleDbType.DBDate)).Value = startDate;
                cmd.Parameters.Add(new OleDbParameter("EndDateParam", OleDbType.DBDate)).Value = endDate;

                OleDbDataAdapter oda = new OleDbDataAdapter(cmd);
                currentConn.Open();
                oda.Fill(s); // Fill the DataSet with filtered data

                if (s.Tables.Count > 0 && s.Tables[0].Rows.Count > 0)
                {
                    ReportDocument crp = new ReportDocument();
                    crp.Load(Server.MapPath("Affected_IP.rpt"));
                    crp.SetDataSource(s.Tables[0]); // Use s.Tables[0] which contains the data from the query

                    // Your original code to export to PDF
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", "attachment;filename=Affected_IP_Report.pdf"); // Use .pdf extension
                    crp.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Affected_IP Report");
                    Response.End(); // Crucial for direct export to terminate response
                }
                else
                {
                    if (lblMessage != null) lblMessage.Text = "No data found for Affected IP Report for the selected month and year.";
                }
            }
            catch (OleDbException ex)
            {
                System.Diagnostics.Trace.WriteLine("OLE DB Error in Button1_Click: " + ex.Message);
                if (lblMessage != null) lblMessage.Text = "Database error generating Affected IP Report: " + ex.Message;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("General Error in Button1_Click: " + ex.Message);
                if (lblMessage != null) lblMessage.Text = "An unexpected error occurred while generating Affected IP Report: " + ex.Message;
            }
            finally
            {
                if (currentConn != null && currentConn.State == ConnectionState.Open)
                {
                    currentConn.Close();
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            int selectedMonth = Convert.ToInt32(ddlMonth.SelectedValue);
            int selectedYear = Convert.ToInt32(ddlYear.SelectedValue);

            if (selectedMonth == 0 || selectedYear == 0)
            {
                if (lblMessage != null) lblMessage.Text = "Please select both a month and a year for the SEBI Report.";
                return;
            }

            DateTime startDate = new DateTime(selectedYear, selectedMonth, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            OleDbConnection currentConn = null;
            DataSet s = new DataSet();

            try
            {
                currentConn = new OleDbConnection(ConfigurationManager.ConnectionStrings["constring"].ToString());
                string query = "Select * from cyber_alerts_received WHERE alert_timestamp >= ? AND alert_timestamp <= ?";
                OleDbCommand cmd = new OleDbCommand(query, currentConn);

                cmd.Parameters.Add(new OleDbParameter("StartDateParam", OleDbType.DBDate)).Value = startDate;
                cmd.Parameters.Add(new OleDbParameter("EndDateParam", OleDbType.DBDate)).Value = endDate;

                OleDbDataAdapter oda = new OleDbDataAdapter(cmd);
                currentConn.Open();
                oda.Fill(s);

                if (s.Tables.Count > 0 && s.Tables[0].Rows.Count > 0)
                {
                    ReportDocument crp = new ReportDocument();
                    crp.Load(Server.MapPath("SEBI_Report.rpt"));
                    crp.SetDataSource(s.Tables[0]);

                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", "attachment;filename=SEBI_Report.pdf");
                    crp.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "SEBI Report");
                    Response.End();
                }
                else
                {
                    if (lblMessage != null) lblMessage.Text = "No data found for SEBI Report for the selected month and year.";
                }
            }
            catch (OleDbException ex)
            {
                System.Diagnostics.Trace.WriteLine("OLE DB Error in Button2_Click: " + ex.Message);
                if (lblMessage != null) lblMessage.Text = "Database error generating SEBI Report: " + ex.Message;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("General Error in Button2_Click: " + ex.Message);
                if (lblMessage != null) lblMessage.Text = "An unexpected error occurred while generating SEBI Report: " + ex.Message;
            }
            finally
            {
                if (currentConn != null && currentConn.State == ConnectionState.Open)
                {
                    currentConn.Close();
                }
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            int selectedMonth = Convert.ToInt32(ddlMonth.SelectedValue);
            int selectedYear = Convert.ToInt32(ddlYear.SelectedValue);

            if (selectedMonth == 0 || selectedYear == 0)
            {
                if (lblMessage != null) lblMessage.Text = "Please select both a month and a year for the Malicious IP Report.";
                return;
            }

            DateTime startDate = new DateTime(selectedYear, selectedMonth, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            OleDbConnection currentConn = null;
            DataSet s = new DataSet();

            try
            {
                currentConn = new OleDbConnection(ConfigurationManager.ConnectionStrings["constring"].ToString());
                string query = "Select * from cyber_alerts_received WHERE alert_timestamp >= ? AND alert_timestamp <= ?";
                OleDbCommand cmd = new OleDbCommand(query, currentConn);

                cmd.Parameters.Add(new OleDbParameter("StartDateParam", OleDbType.DBDate)).Value = startDate;
                cmd.Parameters.Add(new OleDbParameter("EndDateParam", OleDbType.DBDate)).Value = endDate;

                OleDbDataAdapter oda = new OleDbDataAdapter(cmd);
                currentConn.Open();
                oda.Fill(s);

                if (s.Tables.Count > 0 && s.Tables[0].Rows.Count > 0)
                {
                    ReportDocument crp = new ReportDocument();
                    crp.Load(Server.MapPath("Malicious IP.rpt")); // Ensure this is the correct file name
                    crp.SetDataSource(s.Tables[0]);

                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", "attachment;filename=Malicious_IP_Report.pdf");
                    crp.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Malicious IP Report");
                    Response.End();
                }
                else
                {
                    if (lblMessage != null) lblMessage.Text = "No data found for Malicious IP Report for the selected month and year.";
                }
            }
            catch (OleDbException ex)
            {
                System.Diagnostics.Trace.WriteLine("OLE DB Error in Button3_Click: " + ex.Message);
                if (lblMessage != null) lblMessage.Text = "Database error generating Malicious IP Report: " + ex.Message;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("General Error in Button3_Click: " + ex.Message);
                if (lblMessage != null) lblMessage.Text = "An unexpected error occurred while generating Malicious IP Report: " + ex.Message;
            }
            finally
            {
                if (currentConn != null && currentConn.State == ConnectionState.Open)
                {
                    currentConn.Close();
                }
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            int selectedMonth = Convert.ToInt32(ddlMonth.SelectedValue);
            int selectedYear = Convert.ToInt32(ddlYear.SelectedValue);

            if (selectedMonth == 0 || selectedYear == 0)
            {
                if (lblMessage != null) lblMessage.Text = "Please select both a month and a year for the Affected Ports Report.";
                return;
            }

            DateTime startDate = new DateTime(selectedYear, selectedMonth, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            OleDbConnection currentConn = null;
            DataSet s = new DataSet();

            try
            {
                currentConn = new OleDbConnection(ConfigurationManager.ConnectionStrings["constring"].ToString());
                string query = "Select * from cyber_alerts_received WHERE alert_timestamp >= ? AND alert_timestamp <= ?";
                OleDbCommand cmd = new OleDbCommand(query, currentConn);

                cmd.Parameters.Add(new OleDbParameter("StartDateParam", OleDbType.DBDate)).Value = startDate;
                cmd.Parameters.Add(new OleDbParameter("EndDateParam", OleDbType.DBDate)).Value = endDate;

                OleDbDataAdapter oda = new OleDbDataAdapter(cmd);
                currentConn.Open();
                oda.Fill(s);

                if (s.Tables.Count > 0 && s.Tables[0].Rows.Count > 0)
                {
                    ReportDocument crp = new ReportDocument();
                    crp.Load(Server.MapPath("Affected Ports.rpt")); // Ensure this is the correct file name
                    crp.SetDataSource(s.Tables[0]);

                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", "attachment;filename=Affected_Ports_Report.pdf");
                    crp.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Affected Ports Report");
                    Response.End();
                }
                else
                {
                    if (lblMessage != null) lblMessage.Text = "No data found for Affected Ports Report for the selected month and year.";
                }
            }
            catch (OleDbException ex)
            {
                System.Diagnostics.Trace.WriteLine("OLE DB Error in Button4_Click: " + ex.Message);
                if (lblMessage != null) lblMessage.Text = "Database error generating Affected Ports Report: " + ex.Message;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("General Error in Button4_Click: " + ex.Message);
                if (lblMessage != null) lblMessage.Text = "An unexpected error occurred while generating Affected Ports Report: " + ex.Message;
            }
            finally
            {
                if (currentConn != null && currentConn.State == ConnectionState.Open)
                {
                    currentConn.Close();
                }
            }
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            int selectedMonth = Convert.ToInt32(ddlMonth.SelectedValue);
            int selectedYear = Convert.ToInt32(ddlYear.SelectedValue);

            if (selectedMonth == 0 || selectedYear == 0)
            {
                if (lblMessage != null) lblMessage.Text = "Please select both a month and a year for the Action Pending Report.";
                return;
            }

            DateTime startDate = new DateTime(selectedYear, selectedMonth, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            OleDbConnection currentConn = null;
            DataSet s = new DataSet();

            try
            {
                currentConn = new OleDbConnection(ConfigurationManager.ConnectionStrings["constring"].ToString());
                string query = "Select * from cyber_alerts_received WHERE alert_timestamp >= ? AND alert_timestamp <= ?";
                OleDbCommand cmd = new OleDbCommand(query, currentConn);

                cmd.Parameters.Add(new OleDbParameter("StartDateParam", OleDbType.DBDate)).Value = startDate;
                cmd.Parameters.Add(new OleDbParameter("EndDateParam", OleDbType.DBDate)).Value = endDate;

                OleDbDataAdapter oda = new OleDbDataAdapter(cmd);
                currentConn.Open();
                oda.Fill(s);

                if (s.Tables.Count > 0 && s.Tables[0].Rows.Count > 0)
                {
                    ReportDocument crp = new ReportDocument();
                    crp.Load(Server.MapPath("Action Pending.rpt")); // Ensure this is the correct file name
                    crp.SetDataSource(s.Tables[0]);

                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", "attachment;filename=Action_Pending_Report.pdf");
                    crp.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Action Pending Report");
                    Response.End();
                }
                else
                {
                    if (lblMessage != null) lblMessage.Text = "No data found for Action Pending Report for the selected month and year.";
                }
            }
            catch (OleDbException ex)
            {
                System.Diagnostics.Trace.WriteLine("OLE DB Error in Button5_Click: " + ex.Message);
                if (lblMessage != null) lblMessage.Text = "Database error generating Action Pending Report: " + ex.Message;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("General Error in Button5_Click: " + ex.Message);
                if (lblMessage != null) lblMessage.Text = "An unexpected error occurred while generating Action Pending Report: " + ex.Message;
            }
            finally
            {
                if (currentConn != null && currentConn.State == ConnectionState.Open)
                {
                    currentConn.Close();
                }
            }
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            int selectedMonth = Convert.ToInt32(ddlMonth.SelectedValue);
            int selectedYear = Convert.ToInt32(ddlYear.SelectedValue);

            if (selectedMonth == 0 || selectedYear == 0)
            {
                if (lblMessage != null) lblMessage.Text = "Please select both a month and a year for the Closed Incidents Report.";
                return;
            }

            DateTime startDate = new DateTime(selectedYear, selectedMonth, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            OleDbConnection currentConn = null;
            DataSet s = new DataSet();

            try
            {
                currentConn = new OleDbConnection(ConfigurationManager.ConnectionStrings["constring"].ToString());
                string query = "Select * from cyber_alerts_received WHERE alert_timestamp >= ? AND alert_timestamp <= ?";
                OleDbCommand cmd = new OleDbCommand(query, currentConn);

                cmd.Parameters.Add(new OleDbParameter("StartDateParam", OleDbType.DBDate)).Value = startDate;
                cmd.Parameters.Add(new OleDbParameter("EndDateParam", OleDbType.DBDate)).Value = endDate;

                OleDbDataAdapter oda = new OleDbDataAdapter(cmd);
                currentConn.Open();
                oda.Fill(s);

                if (s.Tables.Count > 0 && s.Tables[0].Rows.Count > 0)
                {
                    ReportDocument crp = new ReportDocument();
                    crp.Load(Server.MapPath("Closed Incidents.rpt")); // Ensure this is the correct file name
                    crp.SetDataSource(s.Tables[0]);

                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", "attachment;filename=Closed_Incidents_Report.pdf");
                    crp.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Closed Incidents Report");
                    Response.End();
                }
                else
                {
                    if (lblMessage != null) lblMessage.Text = "No data found for Closed Incidents Report for the selected month and year.";
                }
            }
            catch (OleDbException ex)
            {
                System.Diagnostics.Trace.WriteLine("OLE DB Error in Button6_Click: " + ex.Message);
                if (lblMessage != null) lblMessage.Text = "Database error generating Closed Incidents Report: " + ex.Message;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("General Error in Button6_Click: " + ex.Message);
                if (lblMessage != null) lblMessage.Text = "An unexpected error occurred while generating Closed Incidents Report: " + ex.Message;
            }
            finally
            {
                if (currentConn != null && currentConn.State == ConnectionState.Open)
                {
                    currentConn.Close();
                }
            }
        }
    }
}

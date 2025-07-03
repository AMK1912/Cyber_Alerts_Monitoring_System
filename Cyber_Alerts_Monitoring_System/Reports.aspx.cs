using System;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Globalization; // Added for date handling, though less critical with Calendar.Culture

namespace CyberAlert
{
    public partial class Reports : System.Web.UI.Page
    {
        // Removed 'private OleDbConnection conn;' as it's not used in this pattern
        // and connections are handled within 'using' blocks or localized.

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Calendar controls automatically initialize with a default date (e.g., today's date).
                // No need to populate dropdowns or set initial dates here unless desired.
                // You might set default selected dates if you want reports to run with a default range.
                // Example:
                // Calendar1.SelectedDate = DateTime.Today.AddDays(-7); // Default to last 7 days
                // Calendar2.SelectedDate = DateTime.Today;

                if (lblMessage != null) lblMessage.Text = ""; // Clear any previous messages on initial load
            }
        }

        // Event handler for when a date is selected on either calendar.
        // Its presence fixes the CS1001 compiler error from the ASPX page.
        // No specific logic needed here for filtering, as that happens on button click.
        protected void Calendar_SelectionChanged(object sender, EventArgs e)
        {
            // Optional: Provide immediate feedback to the user about selected dates
            // if (lblMessage != null)
            // {
            //     lblMessage.Text = $"Selected Start Date: {Calendar1.SelectedDate.ToShortDateString()}, Selected End Date: {Calendar2.SelectedDate.ToShortDateString()}";
            // }
        }

        // --- Original Button Click Handlers, now updated for Calendar Filtering and Excel Export ---

        protected void Button1_Click(object sender, EventArgs e)
        {
            DateTime startDate = Calendar1.SelectedDate;
            DateTime endDate = Calendar2.SelectedDate;

            // --- Validation for Selected Dates ---
            if (startDate == DateTime.MinValue || endDate == DateTime.MinValue) // DateTime.MinValue usually indicates no date selected
            {
                if (lblMessage != null) lblMessage.Text = "Please select both a Start Date and an End Date for the Affected IP Report.";
                return; // Stop processing if dates are not selected
            }
            if (startDate > endDate)
            {
                if (lblMessage != null) lblMessage.Text = "Start Date cannot be after End Date for the Affected IP Report.";
                return; // Stop processing if date range is invalid
            }
            // --- End Date Validation ---

            OleDbConnection currentConn = null;
            DataSet s = new DataSet();

            try
            {
                currentConn = new OleDbConnection(ConfigurationManager.ConnectionStrings["constring"].ToString());
                // Modified SQL query to include date filtering
                // IMPORTANT: Replace 'alert_timestamp' with the actual name of your date/datetime column in 'cyber_alerts_received'
                string query = "Select * from cyber_alerts_received WHERE alert_timestamp >= ? AND alert_timestamp <= ?";
                OleDbCommand cmd = new OleDbCommand(query, currentConn);

                // Add parameters for the start and end dates
                // OleDbType.DBDate is suitable for date/datetime fields
                cmd.Parameters.Add(new OleDbParameter("StartDateParam", OleDbType.DBDate)).Value = startDate;
                cmd.Parameters.Add(new OleDbParameter("EndDateParam", OleDbType.DBDate)).Value = endDate;

                OleDbDataAdapter oda = new OleDbDataAdapter(cmd);
                currentConn.Open();
                oda.Fill(s); // Fill the DataSet with filtered data

                if (s.Tables.Count > 0 && s.Tables[0].Rows.Count > 0)
                {
                    ReportDocument crp = new ReportDocument();
                    crp.Load(Server.MapPath("Affected_IP.rpt"));
                    crp.SetDataSource(s.Tables[0]); // Use s.Tables[0] as it's the default table filled by DataAdapter

                    // Clear any previous response headers and set up for Excel export
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.ms-excel"; // MIME type for Excel
                    Response.AddHeader("Content-Disposition", "attachment;filename=Affected_IP_Report.xls"); // Excel file extension
                    
                    // Export to Excel format
                    crp.ExportToHttpResponse(ExportFormatType.Excel, Response, false, "Affected_IP Report");
                    Response.End(); // Important: Terminates response, so page content (like CrystalReportViewer) won't render
                }
                else
                {
                    // No data found for the selected date range
                    if (lblMessage != null) lblMessage.Text = $"No data found for Affected IP Report from {startDate.ToShortDateString()} to {endDate.ToShortDateString()}.";
                    // Clear the viewer if it was previously showing a report
                    CrystalReportViewer1.ReportSource = null;
                    CrystalReportViewer1.DataBind();
                }
            }
            catch (OleDbException ex)
            {
                System.Diagnostics.Trace.WriteLine($"OLE DB Error in Button1_Click: {ex.Message}");
                if (lblMessage != null) lblMessage.Text = $"Database error for Affected IP Report: {ex.Message}";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine($"General Error in Button1_Click: {ex.Message}");
                if (lblMessage != null) lblMessage.Text = $"An unexpected error occurred for Affected IP Report: {ex.Message}";
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
            DateTime startDate = Calendar1.SelectedDate;
            DateTime endDate = Calendar2.SelectedDate;

            if (startDate == DateTime.MinValue || endDate == DateTime.MinValue)
            {
                if (lblMessage != null) lblMessage.Text = "Please select both a Start Date and an End Date for the SEBI Report.";
                return;
            }
            if (startDate > endDate)
            {
                if (lblMessage != null) lblMessage.Text = "Start Date cannot be after End Date for the SEBI Report.";
                return;
            }

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
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition", "attachment;filename=SEBI_Report.xls");
                    crp.ExportToHttpResponse(ExportFormatType.Excel, Response, false, "SEBI Report");
                    Response.End();
                }
                else
                {
                    if (lblMessage != null) lblMessage.Text = $"No data found for SEBI Report from {startDate.ToShortDateString()} to {endDate.ToShortDateString()}.";
                }
            }
            catch (OleDbException ex)
            {
                System.Diagnostics.Trace.WriteLine($"OLE DB Error in Button2_Click: {ex.Message}");
                if (lblMessage != null) lblMessage.Text = $"Database error for SEBI Report: {ex.Message}";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine($"General Error in Button2_Click: {ex.Message}");
                if (lblMessage != null) lblMessage.Text = $"An unexpected error occurred for SEBI Report: {ex.Message}";
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
            DateTime startDate = Calendar1.SelectedDate;
            DateTime endDate = Calendar2.SelectedDate;

            if (startDate == DateTime.MinValue || endDate == DateTime.MinValue)
            {
                if (lblMessage != null) lblMessage.Text = "Please select both a Start Date and an End Date for the Malicious IP Report.";
                return;
            }
            if (startDate > endDate)
            {
                if (lblMessage != null) lblMessage.Text = "Start Date cannot be after End Date for the Malicious IP Report.";
                return;
            }

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
                    crp.Load(Server.MapPath("Malicious IP.rpt"));
                    crp.SetDataSource(s.Tables[0]);

                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition", "attachment;filename=Malicious_IP_Report.xls");
                    crp.ExportToHttpResponse(ExportFormatType.Excel, Response, false, "Malicious IP Report");
                    Response.End();
                }
                else
                {
                    if (lblMessage != null) lblMessage.Text = $"No data found for Malicious IP Report from {startDate.ToShortDateString()} to {endDate.ToShortDateString()}.";
                }
            }
            catch (OleDbException ex)
            {
                System.Diagnostics.Trace.WriteLine($"OLE DB Error in Button3_Click: {ex.Message}");
                if (lblMessage != null) lblMessage.Text = $"Database error for Malicious IP Report: {ex.Message}";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine($"General Error in Button3_Click: {ex.Message}");
                if (lblMessage != null) lblMessage.Text = $"An unexpected error occurred for Malicious IP Report: {ex.Message}";
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
            DateTime startDate = Calendar1.SelectedDate;
            DateTime endDate = Calendar2.SelectedDate;

            if (startDate == DateTime.MinValue || endDate == DateTime.MinValue)
            {
                if (lblMessage != null) lblMessage.Text = "Please select both a Start Date and an End Date for the Affected Ports Report.";
                return;
            }
            if (startDate > endDate)
            {
                if (lblMessage != null) lblMessage.Text = "Start Date cannot be after End Date for the Affected Ports Report.";
                return;
            }

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
                    crp.Load(Server.MapPath("Affected Ports.rpt"));
                    crp.SetDataSource(s.Tables[0]);

                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition", "attachment;filename=Affected_Ports_Report.xls");
                    crp.ExportToHttpResponse(ExportFormatType.Excel, Response, false, "Affected Ports Report");
                    Response.End();
                }
                else
                {
                    if (lblMessage != null) lblMessage.Text = $"No data found for Affected Ports Report from {startDate.ToShortDateString()} to {endDate.ToShortDateString()}.";
                }
            }
            catch (OleDbException ex)
            {
                System.Diagnostics.Trace.WriteLine($"OLE DB Error in Button4_Click: {ex.Message}");
                if (lblMessage != null) lblMessage.Text = $"Database error for Affected Ports Report: {ex.Message}";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine($"General Error in Button4_Click: {ex.Message}");
                if (lblMessage != null) lblMessage.Text = $"An unexpected error occurred for Affected Ports Report: {ex.Message}";
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
            DateTime startDate = Calendar1.SelectedDate;
            DateTime endDate = Calendar2.SelectedDate;

            if (startDate == DateTime.MinValue || endDate == DateTime.MinValue)
            {
                if (lblMessage != null) lblMessage.Text = "Please select both a Start Date and an End Date for the Action Pending Report.";
                return;
            }
            if (startDate > endDate)
            {
                if (lblMessage != null) lblMessage.Text = "Start Date cannot be after End Date for the Action Pending Report.";
                return;
            }

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
                    crp.Load(Server.MapPath("Action Pending.rpt"));
                    crp.SetDataSource(s.Tables[0]);

                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition", "attachment;filename=Action_Pending_Report.xls");
                    crp.ExportToHttpResponse(ExportFormatType.Excel, Response, false, "Action Pending Report");
                    Response.End();
                }
                else
                {
                    if (lblMessage != null) lblMessage.Text = $"No data found for Action Pending Report from {startDate.ToShortDateString()} to {endDate.ToShortDateString()}.";
                }
            }
            catch (OleDbException ex)
            {
                System.Diagnostics.Trace.WriteLine($"OLE DB Error in Button5_Click: {ex.Message}");
                if (lblMessage != null) lblMessage.Text = $"Database error for Action Pending Report: {ex.Message}";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine($"General Error in Button5_Click: {ex.Message}");
                if (lblMessage != null) lblMessage.Text = $"An unexpected error occurred for Action Pending Report: {ex.Message}";
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
            DateTime startDate = Calendar1.SelectedDate;
            DateTime endDate = Calendar2.SelectedDate;

            if (startDate == DateTime.MinValue || endDate == DateTime.MinValue)
            {
                if (lblMessage != null) lblMessage.Text = "Please select both a Start Date and an End Date for the Closed Incidents Report.";
                return;
            }
            if (startDate > endDate)
            {
                if (lblMessage != null) lblMessage.Text = "Start Date cannot be after End Date for the Closed Incidents Report.";
                return;
            }

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
                    crp.Load(Server.MapPath("Closed Incidents.rpt"));
                    crp.SetDataSource(s.Tables[0]);

                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition", "attachment;filename=Closed_Incidents_Report.xls");
                    crp.ExportToHttpResponse(ExportFormatType.Excel, Response, false, "Closed Incidents Report");
                    Response.End();
                }
                else
                {
                    if (lblMessage != null) lblMessage.Text = $"No data found for Closed Incidents Report from {startDate.ToShortDateString()} to {endDate.ToShortDateString()}.";
                }
            }
            catch (OleDbException ex)
            {
                System.Diagnostics.Trace.WriteLine($"OLE DB Error in Button6_Click: {ex.Message}");
                if (lblMessage != null) lblMessage.Text = $"Database error for Closed Incidents Report: {ex.Message}";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine($"General Error in Button6_Click: {ex.Message}");
                if (lblMessage != null) lblMessage.Text = $"An unexpected error occurred for Closed Incidents Report: {ex.Message}";
            }
            finally
            {
                if (currentConn != null && currentConn.State == ConnectionState.Open)
                {
                    currentConn.Close();
                }
            }
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            DateTime startDate = Calendar1.SelectedDate;
            DateTime endDate = Calendar2.SelectedDate;

            if (startDate == DateTime.MinValue || endDate == DateTime.MinValue)
            {
                if (lblMessage != null) lblMessage.Text = "Please select both a Start Date and an End Date for the Consolidated Report.";
                return;
            }
            if (startDate > endDate)
            {
                if (lblMessage != null) lblMessage.Text = "Start Date cannot be after End Date for the Consolidated Report.";
                return;
            }

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
                    crp.Load(Server.MapPath("Consolidated.rpt")); // Assuming this is the correct report file name
                    crp.SetDataSource(s.Tables[0]);

                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition", "attachment;filename=Consolidated_Report.xls");
                    crp.ExportToHttpResponse(ExportFormatType.Excel, Response, false, "Consolidated Report");
                    Response.End();
                }
                else
                {
                    if (lblMessage != null) lblMessage.Text = $"No data found for Consolidated Report from {startDate.ToShortDateString()} to {endDate.ToShortDateString()}.";
                }
            }
            catch (OleDbException ex)
            {
                System.Diagnostics.Trace.WriteLine($"OLE DB Error in Button7_Click: {ex.Message}");
                if (lblMessage != null) lblMessage.Text = $"Database error for Consolidated Report: {ex.Message}";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine($"General Error in Button7_Click: {ex.Message}");
                if (lblMessage != null) lblMessage.Text = $"An unexpected error occurred for Consolidated Report: {ex.Message}";
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

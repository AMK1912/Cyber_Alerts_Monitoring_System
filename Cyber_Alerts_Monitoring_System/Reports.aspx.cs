using System;
using System.Data;
using System.Data.OleDb;
using System.Web.UI;
using System.Web.UI.WebControls; // Make sure this is included for GridView and other controls

namespace CyberAlert// IMPORTANT: Replace with your actual namespace
{
    public partial class Reports : System.Web.UI.Page
    {
        // Define your Oracle connection string here
        // Replace with your actual connection details
        private string oracleConnectionString = "Provider=OraOLEDB.Oracle;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=your_oracle_host)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=your_service_name)));User ID=your_username;Password=your_password;";

        // Placeholder for the user's plant location.
        // In a real application, this would come from session, user profile, or a database lookup.
        private string userPlantLocation = "Hyderabad"; // Example: Set this dynamically based on logged-in user

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblPlantLocation.Text = $"Reports for Plant Location: {userPlantLocation}";
                reportTitle.InnerText = "Select a Report Type"; // Initial title
            }
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            lblMessage.Text = ""; // Clear previous messages
            LinkButton btn = (LinkButton)sender;
            string reportType = btn.CommandArgument;

            reportTitle.InnerText = $"{reportType.Replace("IP", " IP ")} Reports"; // Update title dynamically

            LoadReportData(reportType);
        }

        private void LoadReportData(string reportType)
        {
            DataTable dt = new DataTable();
            string query = "";

            // Determine SQL query based on report type
            switch (reportType)
            {
                case "Consolidated":
                    query = @"SELECT PLANT_NAME, ALERT_COUNT, ACTION_TAKEN, AFFECTED_PORT, AFFECTED_IP, MALICIOUS_IP, ACTION_PENDING, CLOSED_INCIDENTS
                              FROM YOUR_CONSOLIDATED_REPORTS_TABLE
                              WHERE PLANT_LOCATION = :PlantLocation"; // Use parameter for location
                    break;
                case "AffectedIP":
                    query = @"SELECT AFFECTED_IP, ALERT_COUNT, DETECTION_TIME, STATUS
                              FROM YOUR_AFFECTED_IP_REPORTS_TABLE
                              WHERE PLANT_LOCATION = :PlantLocation"; // Example fields
                    break;
                case "Malicious":
                    query = @"SELECT MALICIOUS_IP, THREAT_LEVEL, DETECTION_DATE, SOURCE_COUNTRY
                              FROM YOUR_MALICIOUS_REPORTS_TABLE
                              WHERE PLANT_LOCATION = :PlantLocation"; // Example fields
                    break;
                // Add more cases for other report types
                default:
                    lblMessage.Text = "Invalid report type selected.";
                    gvReports.DataSource = null;
                    gvReports.DataBind();
                    return;
            }

            using (OleDbConnection connection = new OleDbConnection(oracleConnectionString))
            {
                try
                {
                    connection.Open();
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        // Add parameter for Plant Location
                        command.Parameters.AddWithValue("PlantLocation", userPlantLocation);

                        using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = $"Error fetching report data: {ex.Message}";
                    // Log the exception for debugging purposes
                    // System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
            }

            gvReports.DataSource = dt;
            gvReports.DataBind();

            if (dt.Rows.Count == 0)
            {
                lblMessage.Text += "<br/>No data found for this report type at your plant location.";
            }
        }

        protected void gvReports_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvReports.PageIndex = e.NewPageIndex;
            // You need to re-bind the data.
            // This assumes you store the last loaded report type in ViewState or a session variable.
            // For simplicity, let's re-trigger the load based on the current title or a stored value.
            // A better way is to store reportType in ViewState: ViewState["CurrentReportType"] = reportType;
            // Then retrieve it here: string currentReportType = ViewState["CurrentReportType"] as string;
            // For now, we'll try to infer or re-load.
            // If you only have one GridView, you might need to re-run the query that populated it.
            // For a robust solution, store the last report type selected in a ViewState variable.
            string currentReportType = GetReportTypeFromTitle(reportTitle.InnerText); // A helper to get the type back

            if (!string.IsNullOrEmpty(currentReportType))
            {
                LoadReportData(currentReportType);
            }
            else
            {
                lblMessage.Text = "Could not determine report type for paging.";
            }
        }

        // Helper to extract report type from the title for paging
        private string GetReportTypeFromTitle(string title)
        {
            if (title.Contains("Consolidated")) return "Consolidated";
            if (title.Contains("Affected IP")) return "AffectedIP";
            if (title.Contains("Malicious")) return "Malicious";
            return null; // Or handle other types
        }
    }
}

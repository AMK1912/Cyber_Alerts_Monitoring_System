using System;
using System.Data;
using System.Data.OleDb;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web; // Required for ToolPanelViewType
using System.IO; // For Path.Combine

namespace CyberAlert // IMPORTANT: Ensure this namespace matches your project!
{
    public partial class Reports : System.Web.UI.Page
    {
        // --- Configuration Section ---

        // Define your Oracle connection string here.
        // IMPORTANT: Replace with your actual Oracle database connection details.
        // Example: "Provider=OraOLEDB.Oracle;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=your_oracle_host)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=your_service_name)));User ID=your_username;Password=your_password;";
        private string oracleConnectionString = "Provider=OraOLEDB.Oracle;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.1.100)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ORCL)));User ID=your_username;Password=your_password;";

        // Placeholder for the user's plant location.
        // In a real application, this would be fetched dynamically from:
        // - Session (e.g., Session["PlantLocation"]) after user login.
        // - A user profile stored in your database.
        // - A query parameter, etc.
        // For this example, it's hardcoded to "Hyderabad" to match sample data.
        private string userPlantLocation = "Hyderabad";

        // --- Crystal Reports Object Management ---

        // Maintain a reference to the ReportDocument object.
        // It's crucial to dispose of this object properly to avoid memory leaks and errors.
        private ReportDocument crystalReport = new ReportDocument();

        // --- Page Life Cycle Events ---

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Initial load: Set static text
                lblPlantLocation.Text = $"Reports for Plant Location: {userPlantLocation}";
                reportTitle.InnerText = "Please Select a Report";
            }
            else
            {
                // On postbacks (e.g., Crystal Report Viewer's internal paging/export),
                // we need to re-load the report to maintain its state.
                if (ViewState["CurrentReportPath"] != null && ViewState["CurrentReportType"] != null)
                {
                    string reportPath = ViewState["CurrentReportPath"].ToString();
                    string reportType = ViewState["CurrentReportType"].ToString(); // Not strictly needed for single report, but good for future expansion
                    LoadCrystalReport(reportType, reportPath);
                }
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            // IMPORTANT: This method is CRITICAL for Crystal Reports memory management.
            // Always close and dispose the ReportDocument object to release resources.
            if (crystalReport != null)
            {
                crystalReport.Close();
                crystalReport.Dispose();
                crystalReport = null; // Set to null after disposing to prevent re-use
            }
        }

        // --- Event Handlers ---

        // Unified event handler for all "Load Report" buttons
        protected void btnLoadReport_Click(object sender, EventArgs e)
        {
            lblMessage.Text = ""; // Clear any previous messages

            // Get the button that was clicked
            Button btn = (Button)sender;
            // The CommandArgument tells us which report type was selected (e.g., "Consolidated")
            string reportType = btn.CommandArgument;

            // Update the report title dynamically
            reportTitle.InnerText = $"{reportType} Reports";

            // Construct the file path to the Crystal Report (.rpt) file
            // Assumes your .rpt files are in a folder named 'Reports' directly under your application root
            // And that filenames follow the pattern: {reportType}Report.rpt (e.g., "ConsolidatedReport.rpt")
            string reportFileName = $"{reportType}Report.rpt";
            string reportPath = Path.Combine(Server.MapPath("~/Reports"), reportFileName);

            // Store the current report's path and type in ViewState.
            // This allows the report to be re-loaded correctly on subsequent postbacks (like paging in the viewer).
            ViewState["CurrentReportPath"] = reportPath;
            ViewState["CurrentReportType"] = reportType;

            // Call the core method to load and display the Crystal Report
            LoadCrystalReport(reportType, reportPath);
        }

        // --- Core Report Loading Logic ---

        private void LoadCrystalReport(string reportType, string reportPath)
        {
            try
            {
                // Dispose of any previous report document before loading a new one
                if (crystalReport != null)
                {
                    crystalReport.Close();
                    crystalReport.Dispose();
                    crystalReport = new ReportDocument(); // Re-initialize for the new report
                }

                // Load the Crystal Report definition file (.rpt)
                crystalReport.Load(reportPath);

                // Fetch the necessary data from the Oracle database into a DataTable
                // This method selects only the columns relevant for the specific report type.
                DataTable dt = GetReportData(reportType);

                // Check if any data was returned
                if (dt.Rows.Count == 0)
                {
                    lblMessage.Text = $"No data found for {reportType} report at {userPlantLocation}.";
                    CrystalReportViewer1.ReportSource = null; // Clear the viewer if no data
                    return; // Exit the method
                }

                // Set the fetched DataTable as the data source for the Crystal Report
                crystalReport.SetDataSource(dt);

                // Pass parameters to the Crystal Report.
                // The parameter name ("PlantLocationParam") MUST EXACTLY match
                // the parameter you defined in your .rpt file.
                crystalReport.SetParameterValue("PlantLocationParam", userPlantLocation);

                // Assign the prepared ReportDocument to the CrystalReportViewer control
                CrystalReportViewer1.ReportSource = crystalReport;

                // Configure CrystalReportViewer properties programmatically
                // This resolves the "cannot be set declaratively" error for DisplayGroupTree
                CrystalReportViewer1.ToolPanelView = ToolPanelViewType.None; // Hides the entire left panel (group tree & parameters)
                // You can also set other properties here if needed, e.g.:
                // CrystalReportViewer1.HasCrystalLogo = false;
                // CrystalReportViewer1.HasPageNavigationButtons = true;
                // CrystalReportViewer1.BestFitPage = true;

                CrystalReportViewer1.DataBind(); // Ensure the viewer updates and displays the report

            }
            catch (Exception ex)
            {
                // Display user-friendly error message
                lblMessage.Text = $"Error generating report: {ex.Message}";
                CrystalReportViewer1.ReportSource = null; // Clear the viewer on error

                // IMPORTANT: Log the full exception details for debugging purposes (e.g., to a file, database, or console)
                System.Diagnostics.Debug.WriteLine($"Error in LoadCrystalReport: {ex.ToString()}");
            }
        }

        // --- Data Fetching Logic ---

        // Fetches data from the master table with only the columns needed for the specific report type.
        private DataTable GetReportData(string reportType)
        {
            DataTable dt = new DataTable();
            string query = "";

            // Determine which columns to select based on the report type
            if (reportType == "Consolidated")
            {
                // Select only the columns required for the Consolidated Report
                // from your master cyber alerts table.
                // IMPORTANT: Replace 'CYBER_ALERTS_MASTER' with your actual Oracle table name.
                query = @"SELECT
                            PLANT_NAME,
                            ALERT_COUNT,
                            ACTION_TAKEN,
                            AFFECTED_PORT,
                            AFFECTED_IP,
                            MALICIOUS_IP,
                            ACTION_PENDING,
                            CLOSED_INCIDENTS,
                            PLANT_LOCATION
                          FROM
                            CYBER_ALERTS_MASTER";
            }
            else
            {
                // Add 'else if' or 'case' statements here for other report types
                // (e.g., "AffectedIP", "Malicious", "SEBI")
                // Each report type would have its own specific SELECT query here.
                throw new ArgumentException($"Report type '{reportType}' data fetching not yet implemented.");
            }

            using (OleDbConnection connection = new OleDbConnection(oracleConnectionString))
            {
                try
                {
                    connection.Open();
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                        {
                            adapter.Fill(dt); // Fill the DataTable with data
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Re-throw with more context to be caught by LoadCrystalReport
                    throw new Exception($"Database error fetching data for '{reportType}': {ex.Message}", ex);
                }
            }
            return dt;
        }
    }
}

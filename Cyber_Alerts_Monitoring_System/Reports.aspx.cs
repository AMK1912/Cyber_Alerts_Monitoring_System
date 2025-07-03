using System;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

namespace CyberAlert
{
    public partial class Reports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (lblMessage != null) lblMessage.Text = "";
            }
        }

        protected void Calendar_SelectionChanged(object sender, EventArgs e) { }

        private void ExportReport(string reportFile, string outputFilename, string reportLabel)
        {
            DateTime startDate, endDate;

            if (!DateTime.TryParse(txtStartDate.Text, out startDate) || !DateTime.TryParse(txtEndDate.Text, out endDate))
            {
                lblMessage.Text = $"Please select valid Start Date and End Date for the {reportLabel}.";
                return;
            }

            if (startDate > endDate)
            {
                lblMessage.Text = $"Start Date cannot be after End Date for the {reportLabel}.";
                return;
            }

            OleDbConnection currentConn = null;
            DataSet s = new DataSet();

            try
            {
                currentConn = new OleDbConnection(ConfigurationManager.ConnectionStrings["constring"].ToString());
                string query = "SELECT * FROM cyber_alerts_received WHERE alert_timestamp >= ? AND alert_timestamp <= ?";
                OleDbCommand cmd = new OleDbCommand(query, currentConn);
                cmd.Parameters.Add(new OleDbParameter("StartDateParam", OleDbType.DBDate)).Value = startDate;
                cmd.Parameters.Add(new OleDbParameter("EndDateParam", OleDbType.DBDate)).Value = endDate;

                OleDbDataAdapter oda = new OleDbDataAdapter(cmd);
                currentConn.Open();
                oda.Fill(s);

                if (s.Tables.Count > 0 && s.Tables[0].Rows.Count > 0)
                {
                    ReportDocument crp = new ReportDocument();
                    crp.Load(Server.MapPath(reportFile));
                    crp.SetDataSource(s.Tables[0]);

                    using (System.IO.Stream exportStream = crp.ExportToStream(ExportFormatType.Excel))
                    {
                        byte[] byteArray = new byte[exportStream.Length];
                        exportStream.Read(byteArray, 0, byteArray.Length);

                        Response.Clear();
                        Response.Buffer = true;
                        Response.ContentType = "application/vnd.ms-excel";
                        Response.AddHeader("Content-Disposition", $"attachment;filename={outputFilename}");
                        Response.BinaryWrite(byteArray);
                        Response.Flush();

                        HttpContext.Current.ApplicationInstance.CompleteRequest();
                    }
                }
                else
                {
                    lblMessage.Text = $"No data found for {reportLabel} from {startDate.ToShortDateString()} to {endDate.ToShortDateString()}.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error while generating {reportLabel}: {ex.Message}";
            }
            finally
            {
                if (currentConn != null && currentConn.State == ConnectionState.Open)
                    currentConn.Close();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ExportReport("Affected_IP.rpt", "Affected_IP_Report.xls", "Affected IP Report");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            ExportReport("SEBI_Report.rpt", "SEBI_Report.xls", "SEBI Report");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            ExportReport("Malicious IP.rpt", "Malicious_IP_Report.xls", "Malicious IP Report");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            ExportReport("Affected Ports.rpt", "Affected_Ports_Report.xls", "Affected Ports Report");
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            ExportReport("Action Pending.rpt", "Action_Pending_Report.xls", "Action Pending Report");
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            ExportReport("Closed Incidents.rpt", "Closed_Incidents_Report.xls", "Closed Incidents Report");
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            ExportReport("Consolidated.rpt", "Consolidated_Report.xls", "Consolidated Report");
        }
    }
}

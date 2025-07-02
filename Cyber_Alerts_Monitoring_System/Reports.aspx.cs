using System;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

namespace CyberAlert
{
    public partial class Reports : System.Web.UI.Page
    {
        private OleDbConnection conn;

        protected void Page_Load(object sender, EventArgs e)
        {
        //    conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["constring"].ToString());
        }
        
        protected void Button1_Click(object sender, EventArgs e)
        {
            OleDbConnection currentConn = new OleDbConnection(ConfigurationManager.ConnectionStrings["constring"].ToString());
            OleDbCommand cmd = new OleDbCommand("Select * from cyber_alerts_received", currentConn);
            DataSet s = new DataSet();
            OleDbDataAdapter oda = new OleDbDataAdapter(cmd);
            oda.Fill(s);

            ReportDocument crp = new ReportDocument();
            crp.Load(Server.MapPath("Affected_IP.rpt"));
            crp.SetDataSource(s.Tables["table"]);

            CrystalReportViewer1.ReportSource = crp;

            crp.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Affected_IP Report");

        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            OleDbConnection currentConn = new OleDbConnection(ConfigurationManager.ConnectionStrings["constring"].ToString());
            OleDbCommand cmd = new OleDbCommand("Select * from cyber_alerts_received", currentConn);
            DataSet s = new DataSet();
            OleDbDataAdapter oda = new OleDbDataAdapter(cmd);
            oda.Fill(s);

            ReportDocument crp = new ReportDocument();
            crp.Load(Server.MapPath("SEBI_Report.rpt"));
            crp.SetDataSource(s.Tables["table"]);

            CrystalReportViewer1.ReportSource = crp;

            crp.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "SEBI Report");

        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            OleDbConnection currentConn = new OleDbConnection(ConfigurationManager.ConnectionStrings["constring"].ToString());
            OleDbCommand cmd = new OleDbCommand("Select * from cyber_alerts_received", currentConn);
            DataSet s = new DataSet();
            OleDbDataAdapter oda = new OleDbDataAdapter(cmd);
            oda.Fill(s);

            ReportDocument crp = new ReportDocument();
            crp.Load(Server.MapPath("Malicious IP.rpt"));
            crp.SetDataSource(s.Tables["table"]);

            CrystalReportViewer1.ReportSource = crp;

            crp.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Malicious IP Report");

        }
        protected void Button4_Click(object sender, EventArgs e)
        {
            OleDbConnection currentConn = new OleDbConnection(ConfigurationManager.ConnectionStrings["constring"].ToString());
            OleDbCommand cmd = new OleDbCommand("Select * from cyber_alerts_received", currentConn);
            DataSet s = new DataSet();
            OleDbDataAdapter oda = new OleDbDataAdapter(cmd);
            oda.Fill(s);

            ReportDocument crp = new ReportDocument();
            crp.Load(Server.MapPath("Affected Ports.rpt"));
            crp.SetDataSource(s.Tables["table"]);

            CrystalReportViewer1.ReportSource = crp;

            crp.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Affected Ports Report");

        }
        protected void Button5_Click(object sender, EventArgs e)
        {
            OleDbConnection currentConn = new OleDbConnection(ConfigurationManager.ConnectionStrings["constring"].ToString());
            OleDbCommand cmd = new OleDbCommand("Select * from cyber_alerts_received", currentConn);
            DataSet s = new DataSet();
            OleDbDataAdapter oda = new OleDbDataAdapter(cmd);
            oda.Fill(s);

            ReportDocument crp = new ReportDocument();
            crp.Load(Server.MapPath("Action Pending.rpt"));
            crp.SetDataSource(s.Tables["table"]);

            CrystalReportViewer1.ReportSource = crp;

            crp.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Action Pending Report");

        }
        protected void Button6_Click(object sender, EventArgs e)
        {
            OleDbConnection currentConn = new OleDbConnection(ConfigurationManager.ConnectionStrings["constring"].ToString());
            OleDbCommand cmd = new OleDbCommand("Select * from cyber_alerts_received", currentConn);
            DataSet s = new DataSet();
            OleDbDataAdapter oda = new OleDbDataAdapter(cmd);
            oda.Fill(s);

            ReportDocument crp = new ReportDocument();
            crp.Load(Server.MapPath("Closed Incidents.rpt"));
            crp.SetDataSource(s.Tables["table"]);

            CrystalReportViewer1.ReportSource = crp;

            crp.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Closed Incidents Report");

        }
    }
}

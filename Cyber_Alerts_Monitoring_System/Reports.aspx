<%@ Page Title="Reports" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Reports.aspx.cs" Inherits="CyberAlert.Reports" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2>Report Center</h2>
        <p>Select a report type below. All reports will be filtered by your plant location and selected date range.</p>

        <hr />

        <div class="row mb-4">
            <div class="col-md-6">
                <h4>Select Start Date:</h4>
                <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" Placeholder="YYYY-MM-DD" TextMode="Date" />
            </div>
            <div class="col-md-6">
                <h4>Select End Date:</h4>
                <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" Placeholder="YYYY-MM-DD" TextMode="Date" />
            </div>
        </div>

        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" EnableViewState="false" /><br />

        <ol class="list-unstyled report-buttons">
            <li><asp:Button ID="Button1" runat="server" CssClass="report-btn" OnClick="Button1_Click" Text="Affected IP Report" /></li>
            <li><asp:Button ID="Button2" runat="server" CssClass="report-btn" OnClick="Button2_Click" Text="SEBI Report" /></li>
            <li><asp:Button ID="Button3" runat="server" CssClass="report-btn" OnClick="Button3_Click" Text="Malicious IP Report" /></li>
            <li><asp:Button ID="Button4" runat="server" CssClass="report-btn" OnClick="Button4_Click" Text="Affected Ports Report" /></li>
            <li><asp:Button ID="Button5" runat="server" CssClass="report-btn" OnClick="Button5_Click" Text="Action Pending Report" /></li>
            <li><asp:Button ID="Button6" runat="server" CssClass="report-btn" OnClick="Button6_Click" Text="Closed Incidents Report" /></li>
            <li><asp:Button ID="Button7" runat="server" CssClass="report-btn" OnClick="Button7_Click" Text="Consolidated Report" /></li>
        </ol>

        <br />

        <div class="mt-4">
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content2" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        .container {
            max-width: 900px;
            margin: auto;
        }

        .row {
            display: flex;
            flex-wrap: wrap;
            margin-bottom: 20px;
        }

        .col-md-6 {
            flex: 0 0 50%;
            padding: 0 10px;
        }

        .form-control {
            width: 100%;
            padding: 0.5rem;
            font-size: 1rem;
        }

        .report-buttons li {
            margin-bottom: 10px;
        }

        .report-btn {
            font-weight: bold;
            background-color: #FFFFFF;
            width: 100%;
            max-width: 300px;
            height: 35px;
            border: 1px solid #ccc;
            border-radius: 4px;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }

        .report-btn:hover {
            background-color: #f0f0f0;
        }
    </style>
</asp:Content>

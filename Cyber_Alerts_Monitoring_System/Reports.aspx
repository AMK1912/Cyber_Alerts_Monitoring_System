<%@ Page Title="Reports" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Reports.aspx.cs" Inherits="CyberAlert.Reports" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2>Report Center</h2>
        <p>Select a report type from the given below Report types to view data. Reports will be filtered by your plant location.</p>

        <hr />

        <div class="row mb-4">
            <div class="col-md-6">
                <h4>Select Start Date:</h4>
                <asp:Calendar ID="Calendar1" runat="server" Culture="en-US" UICulture="en-US" OnSelectionChanged="Calendar_SelectionChanged"></asp:Calendar>
            </div>
            <div class="col-md-6">
                <h4>Select End Date:</h4>
                <asp:Calendar ID="Calendar2" runat="server" Culture="en-US" UICulture="en-US" OnSelectionChanged="Calendar_SelectionChanged"></asp:Calendar>
            </div>
        </div>

        <hr />
        <br />

        <%-- Label for displaying messages (e.g., date validation errors) --%>
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>
        <br />

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
        
        <div class="mt-4"> <%-- Added margin top for spacing --%>
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content2" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        /* General styling for containers and rows */
        .container {
            width: 100%;
            padding-right: 15px;
            padding-left: 15px;
            margin-right: auto;
            margin-left: auto;
        }
        @media (min-width: 576px) { .container { max-width: 540px; } }
        @media (min-width: 768px) { .container { max-width: 720px; } }
        @media (min-width: 992px) { .container { max-width: 960px; } }
        @media (min-width: 1200px) { .container { max-width: 1140px; } }

        .row {
            display: flex;
            flex-wrap: wrap;
            margin-right: -15px;
            margin-left: -15px;
        }
        .col-md-6 {
            flex: 0 0 50%;
            max-width: 50%;
            padding-right: 15px;
            padding-left: 15px;
        }

        /* Spacing utilities */
        .mt-4 { margin-top: 1.5rem !important; }
        .mt-5 { margin-top: 3rem !important; }
        .mb-4 { margin-bottom: 1.5rem !important; }

        /* Calendar specific alignment - centered within its column */
        /* ASP.NET Calendar renders as a table. This centers the table. */
        .col-md-6 table {
            margin-left: auto;
            margin-right: auto;
        }
        /* Optional: Add some padding to calendar cells for better clickability */
        .asp-calendar-wrapper td {
            padding: 5px;
        }


        /* Report Buttons Styling */
        .list-unstyled {
            padding-left: 0;
            list-style: none;
        }
        .report-buttons li {
            margin-bottom: 10px; /* Space between buttons */
        }
        .report-btn {
            font-weight: bold;
            background-color: #FFFFFF;
            width: 200px; /* Consistent width as per your original request */
            height: 26px;
            border: 1px solid #ccc; /* Add a subtle border */
            border-radius: 4px; /* Slightly rounded corners */
            cursor: pointer;
            transition: background-color 0.3s ease; /* Smooth hover effect */
        }
        .report-btn:hover {
            background-color: #f0f0f0; /* Light grey on hover */
        }
    </style>
</asp:Content>

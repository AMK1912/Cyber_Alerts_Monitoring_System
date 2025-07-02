<%@ Page Title="Reports" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Reports.aspx.cs" Inherits="CyberAlert.Reports" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2>Report Center</h2>
        <p>Select a report type from the given below Report types to view data. Reports will be filtered by your plant location.</p>

        <div class="row mb-3">
            <div class="col-md-3">
                <label for="ddlMonth">Select Month:</label>
                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="col-md-3">
                <label for="ddlYear">Select Year:</label>
                <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <%-- Optional: A button to explicitly apply filters if you don't want it tied to report buttons --%>
            <%-- <div class="col-md-3 d-flex align-items-end">
                <asp:Button ID="btnApplyFilter" runat="server" Text="Apply Filter" OnClick="btnApplyFilter_Click" CssClass="btn btn-primary" />
            </div> --%>
        </div>

        <ol>
            <li><asp:Button ID="Button1" runat="server" CssClass="auto-style1" OnClick="Button1_Click" Text="Affected IP Report" Width="175px" Height="26px" /></li>
            <li><asp:Button ID="Button2" runat="server" CssClass="auto-style3" OnClick="Button2_Click" Text="SEBI Report" Width="175px" Height="26px" /></li>
            <li><asp:Button ID="Button3" runat="server" CssClass="auto-style3" OnClick="Button3_Click" Text="Malicious IP Report" Width="175px" Height="26px" /></li>
            <li><asp:Button ID="Button4" runat="server" CssClass="auto-style3" OnClick="Button4_Click" Text="Affected Ports Report" Width="175px" Height="26px" /></li>
            <li><asp:Button ID="Button5" runat="server" CssClass="auto-style3" OnClick="Button5_Click" Text="Action Pending Report" Width="175px" Height="26px" /></li>
            <li><asp:Button ID="Button6" runat="server" CssClass="auto-style3" OnClick="Button6_Click" Text="Closed Incidents Report" Width="175px" Height="26px" /></li>
        </ol>
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />

        <div class="auto-style2">
            <br />
            <br />
        </div>

        <div>
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content2" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        /* Styles for your existing buttons */
        .auto-style1 {
            font-weight: bold;
            background-color: #FFFFFF;
            margin-left: 39px; /* Corrected to include px */
        }
        .auto-style2 {
            margin-left: 160px;
            height: 69px;
        }
        .auto-style3 {
            font-weight: bold;
            background-color: #FFFFFF;
            margin-left: 0px; /* Explicitly set to 0px */
        }

        /* Basic styling for dropdowns to mimic Bootstrap's look if not using full Bootstrap */
        .form-control {
            display: block;
            width: 100%;
            padding: .375rem .75rem;
            font-size: 1rem;
            line-height: 1.5;
            color: #495057;
            background-color: #fff;
            background-clip: padding-box;
            border: 1px solid #ced4da;
            border-radius: .25rem;
            transition: border-color .15s ease-in-out,box-shadow .15s ease-in-out;
        }
        .row {
            display: flex;
            flex-wrap: wrap;
            margin-right: -15px; /* Compensate for column padding */
            margin-left: -15px;  /* Compensate for column padding */
        }
        .col-md-3 {
            flex: 0 0 25%; /* 25% width */
            max-width: 25%;
            padding-right: 15px;
            padding-left: 15px;
        }
        .mb-3 {
            margin-bottom: 1rem !important; /* Standard Bootstrap margin-bottom */
        }
        .mt-5 {
            margin-top: 3rem !important; /* Standard Bootstrap margin-top */
        }
        /* Flexbox utilities */
        .d-flex { display: flex !important; }
        .align-items-end { align-items: flex-end !important; }
    </style>
</asp:Content>
